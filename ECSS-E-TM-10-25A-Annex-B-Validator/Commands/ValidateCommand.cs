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
    using Reporting;

    /// <summary>
    /// The purpose of the <see cref="ValidateCommand"/> is t.o initiate the execution of the
    /// validation of an E-TM-10-25 population
    /// </summary>
    public class ValidateCommand : IValidateCommand
    {
        /// <summary>
        /// The <see cref="IReportingService"/> used to create reports of validation results
        /// </summary>
        private readonly IReportingService reportingService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidateCommand"/> class.
        /// </summary>
        /// <param name="reportingService">
        /// The (injected) <see cref="IReportingService"/> used to create reports of validation results
        /// </param>
        public ValidateCommand(IReportingService reportingService)
        {
            this.reportingService = reportingService;
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
        /// Executes the <see cref="ValidateCommand"/>
        /// </summary>
        public void Execute()
        {
            throw new System.NotImplementedException();
        }
    }
}