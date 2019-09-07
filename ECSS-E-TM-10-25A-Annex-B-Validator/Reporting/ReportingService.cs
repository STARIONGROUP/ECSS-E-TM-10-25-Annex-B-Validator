// -------------------------------------------------------------------------------------------------
// <copyright file="ReportingService.cs" company="RHEA System S.A.">
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

namespace com.rheagroup.validator.Reporting
{
    using System;
    using System.Collections.Generic;
    using CDP4Rules.Common;

    /// <summary>
    /// The purpose of the <see cref="ReportingService"/> is to create reports regarding the results
    /// of the validation of an E-TM-10-25 Annex B population.
    /// </summary>
    public class ReportingService : IReportingService
    {
        /// <summary>
        /// The (injected) <see cref="ICsvReportingService"/> used to generate a CSV report
        /// </summary>
        private readonly ICsvReportingService csvReportingService;

        /// <summary>
        /// The (injected) <see cref="ICsvReportingService"/> used to generate a HTML report
        /// </summary>
        private readonly IHtmlReportingService htmlReportingService;

        /// <summary>
        /// Initializes the <see cref="ReportingService"/> class.
        /// </summary>
        /// <param name="csvReportingService"></param>
        /// <param name="htmlReportingService"></param>
        public ReportingService(ICsvReportingService csvReportingService, IHtmlReportingService htmlReportingService)
        {
            this.csvReportingService = csvReportingService ?? throw new ArgumentNullException(nameof(csvReportingService));
            this.htmlReportingService = htmlReportingService ?? throw new ArgumentNullException(nameof(htmlReportingService));
        }

        /// <summary>
        /// Generates a report with the results of a validation run.
        /// </summary>
        /// <param name="target">
        /// the target path
        /// </param>
        /// <param name="reportKind">
        /// The kind of report that is to be generated.
        /// </param>
        /// <param name="results">
        /// The <see cref="RuleCheckResult"/> from which a report is to be generated
        /// </param>
        public void Generate(string target, ReportKind reportKind, IEnumerable<RuleCheckResult> results)
        {
            switch (reportKind)
            {
                case ReportKind.csv:
                    this.csvReportingService.Generate(target, results);
                    break;
                case ReportKind.html:
                    this.htmlReportingService.Generate(target, results);
                    break;
            }
        }
    }
}