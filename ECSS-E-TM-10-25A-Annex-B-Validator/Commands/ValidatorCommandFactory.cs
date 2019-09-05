// -------------------------------------------------------------------------------------------------
// <copyright file="IValidatorCommandFactory.cs" company="RHEA System S.A.">
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
    using Microsoft.Extensions.CommandLineUtils;

    /// <summary>
    /// The purpose of the <see cref="ValidatorCommandFactory"/> is to register the
    /// <see cref="IValidateCommand"/> with a <see cref="CommandLineApplication"/>
    /// </summary>
    public class ValidatorCommandFactory : IValidatorCommandFactory
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidatorCommandFactory"/>
        /// </summary>
        /// <param name="validateCommand">
        /// The <see cref="IValidateCommand"/> used to execute 
        /// </param>
        public ValidatorCommandFactory(IValidateCommand validateCommand)
        {
            this.ValidateCommand = validateCommand;
        }

        /// <summary>
        /// Gets the <see cref="IValidateCommand"/>
        /// </summary>
        public IValidateCommand ValidateCommand { get; private set; }

        /// <summary>
        /// Registers the factory with the <see cref="CommandLineApplication"/>
        /// </summary>
        /// <param name="commandLineApplication">
        /// the subject <see cref="CommandLineApplication"/>
        /// </param>
        public void Register(CommandLineApplication commandLineApplication)
        {
            commandLineApplication.Description = $"Validate an E-TM-10-25 Annex B population and report the results";
            commandLineApplication.HelpOption("-?|--help");

            var configurationOption = commandLineApplication.Option("-c|--configuration", "path to the configuration file used to determine which validation rules",
                CommandOptionType.SingleValue);

            var sourceOption = commandLineApplication.Option("-s|--source", "root directory of an E-TM-10-25 Annex C.3 folder structure",
                CommandOptionType.SingleValue);

            var targetOption = commandLineApplication.Option("-t|--target", "target directory for reporting results, if nothing is specified the current directory is used",
                CommandOptionType.SingleValue);

            commandLineApplication.OnExecute(() =>
            {
                this.ValidateCommand.Configuration = configurationOption.HasValue() ? configurationOption.Value() : string.Empty;
                this.ValidateCommand.Source= sourceOption.HasValue() ? sourceOption.Value() : string.Empty;
                this.ValidateCommand.Configuration = targetOption.HasValue() ? targetOption.Value() : string.Empty;

                this.ValidateCommand.Execute();

                return 0;
            });
        }
    }
}