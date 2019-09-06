// -------------------------------------------------------------------------------------------------
// <copyright file="ValidateCommandTestFixture.cs" company="RHEA System S.A.">
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
    using System.IO;
    using System.Threading.Tasks;
    using com.rheagroup.validator.Commands;
    using com.rheagroup.validator.Reporting;
    using CDP4Common.MetaInfo;
    using CDP4Rules;
    using Moq;
    using NUnit.Framework;

    /// <summary>
    /// Suite of tests for the <see cref="ValidateCommand"/> class
    /// </summary>
    [TestFixture]
    public class ValidateCommandTestFixture
    {
        private Mock<IFolderStructureValidator> folderStructureValidator;

        private ISiteReferenceDataLibraryReader siteReferenceDataLibraryReader;

        private Mock<IRuleCheckerEngine> ruleCheckerEngine;

        private Mock<IReportingService> reportingService;

        private ValidateCommand validateCommand;

        private string source;

        [SetUp]
        public void SetUp()
        {
            this.source = System.IO.Path.Combine(TestContext.CurrentContext.TestDirectory, "Data", "annex-c3-model");
            var directoryInfo = new DirectoryInfo(this.source);

            this.folderStructureValidator = new Mock<IFolderStructureValidator>();
            this.folderStructureValidator.Setup(x => x.Validate(It.IsAny<string>())).Returns(directoryInfo);

            var metaDataProvider = StaticMetadataProvider.GetMetaDataProvider;
            this.siteReferenceDataLibraryReader = new SiteReferenceDataLibraryReader(this.folderStructureValidator.Object, metaDataProvider);
            
            this.ruleCheckerEngine = new Mock<IRuleCheckerEngine>();
            this.reportingService = new Mock<IReportingService>();
            
            this.validateCommand = new ValidateCommand(
                this.folderStructureValidator.Object, 
                this.siteReferenceDataLibraryReader,
                this.ruleCheckerEngine.Object,
                this.reportingService.Object);

            this.validateCommand.Source = this.source;
        }

        [Test]
        public async Task Verify_that_the_ValidateCommand_can_Execute()
        {
            Assert.DoesNotThrowAsync(async () => await this.validateCommand.Execute()); 
        }

        [Test]
        public void Verify_that_CommandPropertiesCanBeGetAndSet()
        {
            this.validateCommand.Source = "source";
            this.validateCommand.Target = "target";
            this.validateCommand.Configuration = "configuration";

            Assert.That(this.validateCommand.Source, Is.EqualTo("source"));
            Assert.That(this.validateCommand.Target, Is.EqualTo("target"));
            Assert.That(this.validateCommand.Configuration, Is.EqualTo("configuration"));

        }
    }
}