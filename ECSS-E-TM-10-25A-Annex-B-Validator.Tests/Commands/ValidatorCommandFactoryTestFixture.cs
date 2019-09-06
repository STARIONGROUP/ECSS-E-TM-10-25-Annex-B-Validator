// -------------------------------------------------------------------------------------------------
// <copyright file="ValidatorCommandFactory.cs" company="RHEA System S.A.">
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

namespace com.rheagroup.validator.tests.Commands
{
    using com.rheagroup.validator.Commands;
    using Microsoft.Extensions.CommandLineUtils;
    using Moq;
    using NUnit.Framework;

    /// <summary>
    /// Suite of tests for the <see cref="ValidatorCommandFactory"/> class.
    /// </summary>
    [TestFixture]
    public class ValidatorCommandFactoryTestFixture
    {
        private Mock<IValidateCommand> validateCommand;

        private ValidatorCommandFactory validatorCommandFactory;

        [SetUp]
        public void SetUp()
        {
            this.validateCommand = new Mock<IValidateCommand>();
            this.validateCommand.Setup(x => x.Source).Returns("source");
            this.validateCommand.Setup(x => x.Target).Returns("target");
            this.validateCommand.Setup(x => x.Configuration).Returns("configuration");

            this.validatorCommandFactory = new ValidatorCommandFactory(this.validateCommand.Object);
        }

        [Test]
        public void Verify_that_Register_does_not_throw_an_exception()
        {
            var commandLineApplication = new CommandLineApplication();

            Assert.DoesNotThrow(() => this.validatorCommandFactory.Register(commandLineApplication));
        }
    }
}