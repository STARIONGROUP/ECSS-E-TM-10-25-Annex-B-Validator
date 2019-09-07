// -------------------------------------------------------------------------------------------------
// <copyright file="ValidateCommand.cs" company="RHEA System S.A.">
//   Copyright (c) 2019 RHEA System S.A.
//
//   This file is part of ECSS-E-TM-10-25A Annex B Validator
//
//   The ECSS-E-TM-10-25A Annex B Validator is free software: you can redistribute it and/or modify
//   it under the terms of the GNU General Public License as published by
//   the Free Software Foundation, either version 3 of the License, or
//   (at your option) any later version.
//   
//   ECSS-E-TM-10-25A Annex B Validator is distributed in the hope that it will be useful,
//   but WITHOUT ANY WARRANTY; without even the implied warranty of
//   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
//   GNU General Public License for more details.
//
//   You should have received a copy of the GNU General Public License
//   along with ECSS-E-TM-10-25A Annex B Validator. If not, see<http://www.gnu.org/licenses/>.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace com.rheagroup.validator.Commands
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using CDP4Dal;
    using CDP4Rules;
    using CDP4Rules.Common;
    using Reporting;
    using Serilog;

    /// <summary>
    /// The purpose of the <see cref="ValidateCommand"/> is t.o initiate the execution of the
    /// validation of an E-TM-10-25 population
    /// </summary>
    public class ValidateCommand : IValidateCommand
    {
        /// <summary>
        /// The (injected) <see cref="IFolderStructureValidator"/> used to validate the structure of
        /// an E-TM-10-25 Annex C.3 folder structure
        /// </summary>
        private readonly IFolderStructureValidator folderStructureValidator;

        /// <summary>
        /// The (injected) <see cref="ISiteReferenceDataLibraryReader"/> used to deserialize
        /// an E-TM-10-25 Annex C.3 folder structure
        /// </summary>
        private readonly ISiteReferenceDataLibraryReader siteReferenceDataLibraryReader;

        /// <summary>
        /// The (injected) <see cref="IRuleCheckerEngine"/> used to validate the E-TM-10-25 population
        /// </summary>
        private readonly IRuleCheckerEngine ruleCheckerEngine;

        /// <summary>
        /// The (injected) <see cref="IReportingService"/> used to create reports of validation results
        /// </summary>
        private readonly IReportingService reportingService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidateCommand"/> class.
        /// </summary>
        /// <param name="folderStructureValidator">
        /// The (injected) <see cref="IFolderStructureValidator"/> used to validate the structure of
        /// an E-TM-10-25 Annex C.3 folder structure
        /// </param>
        /// <param name="siteReferenceDataLibraryReader">
        /// The (injected) <see cref="ISiteReferenceDataLibraryReader"/> used to deserialize
        /// an E-TM-10-25 Annex C.3 folder structure
        /// </param>
        /// <param name="ruleCheckerEngine">
        /// The (injected) <see cref="IRuleCheckerEngine"/> used to validate the E-TM-10-25 population
        /// </param>
        /// <param name="reportingService">
        /// The (injected) <see cref="IReportingService"/> used to create reports of validation results
        /// </param>
        public ValidateCommand(IFolderStructureValidator folderStructureValidator, ISiteReferenceDataLibraryReader siteReferenceDataLibraryReader, IRuleCheckerEngine ruleCheckerEngine, IReportingService reportingService)
        {
            this.folderStructureValidator = folderStructureValidator ?? throw new ArgumentNullException(nameof(folderStructureValidator));
            this.siteReferenceDataLibraryReader = siteReferenceDataLibraryReader ?? throw new ArgumentNullException(nameof(siteReferenceDataLibraryReader));
            this.ruleCheckerEngine = ruleCheckerEngine ?? throw new ArgumentNullException(nameof(ruleCheckerEngine));
            this.reportingService = reportingService ?? throw new ArgumentNullException(nameof(reportingService));
        }

        /// <summary>
        /// Gets or sets the path to the configuration file used to determine which validation rules
        /// to use. 
        /// </summary>
        /// <remarks>
        /// When the path is not set, all rules are executed.
        /// </remarks>
        public string Configuration { get; set; }

        /// <summary>
        /// Gets or sets the path to the root directory of an E-TM-10-25 Annex C.3 folder structure.
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// Gets or sets the path to the target directory where validation results are stored in the form
        /// of a report.
        /// </summary>
        public string Target { get; set; }

        /// <summary>
        /// Gets or sets the kind of report that is to be generated
        /// </summary>
        public ReportKind ReportKind { get; set; }

        /// <summary>
        /// Executes the <see cref="ValidateCommand"/>
        /// </summary>
        public async Task Execute()
        {
            var sw = Stopwatch.StartNew();
            Log.Logger.Information("Execute Validate Command");

            this.folderStructureValidator.Validate(this.Source);

            var dtos = this.siteReferenceDataLibraryReader.Read(this.Source).ToList();
            Log.Logger.Debug("read {dtoCount} from E-TM-10-25 Annex C.3 data structure", dtos.Count);
            
            var filUrl = new Uri(this.Source);

            var assembler = new Assembler(filUrl);
            await assembler.Synchronize(dtos);

            var pocos = assembler.Cache.Values.Select(x => x.Value).ToList();
            Log.Logger.Debug("Assembled {pocoCount} from E-TM-10-25 Annex C.3 data structure", pocos.Count);

            var results = this.ruleCheckerEngine.Run(pocos).ToList();

            var errors = results.Select(x => x.Severity == SeverityKind.Error).Count();
            var warnings = results.Select(x => x.Severity == SeverityKind.Warning).Count();
            var information = results.Select(x => x.Severity == SeverityKind.Info).Count();

            Log.Logger.Information("{errors} out of {total} RuleCheckResults ", errors, results.Count);
            Log.Logger.Information("{warnings} out of {total} RuleCheckResults ", warnings, results.Count);
            Log.Logger.Information("{information} out of {total} RuleCheckResults ", information, results.Count);
            
            this.reportingService.Generate(this.Target, this.ReportKind, results);

            Log.Logger.Information("Execute Validate Command in {executionTime} [ms]", sw.ElapsedMilliseconds);
        }
    }
}