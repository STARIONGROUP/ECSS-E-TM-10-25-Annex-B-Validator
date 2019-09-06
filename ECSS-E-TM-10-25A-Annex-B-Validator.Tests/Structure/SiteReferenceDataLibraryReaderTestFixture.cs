// -------------------------------------------------------------------------------------------------
// <copyright file="SiteReferenceDataLibraryReaderTestFixture.cs" company="RHEA System S.A.">
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

namespace com.rheagroup.validator.tests
{
    using System.IO;
    using CDP4Common.MetaInfo;
    using NUnit.Framework;

    /// <summary>
    /// Suite of tests for the <see cref="SiteReferenceDataLibraryReader"/> class.
    /// </summary>
    [TestFixture]
    public class SiteReferenceDataLibraryReaderTestFixture
    {
        private IFolderStructureValidator folderStructureValidator;

        private IMetaDataProvider metaDataProvider;

        private SiteReferenceDataLibraryReader siteReferenceDataLibraryReader;

        private string validFolderStrucuturePath;

        [SetUp]
        public void SetUp()
        {
            this.folderStructureValidator = new FolderStructureValidator();
            this.metaDataProvider = new MetaDataProvider();

            this.siteReferenceDataLibraryReader = new SiteReferenceDataLibraryReader(this.folderStructureValidator, this.metaDataProvider);

            this.validFolderStrucuturePath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Data", "annex-c3-model");
        }

        [Test]
        public void Verify_that_when_valid_folder_structure_is_read_dtos_are_returned()
        {
            var dtos = this.siteReferenceDataLibraryReader.Read(this.validFolderStrucuturePath);

            Assert.That(dtos, Is.Not.Empty);
        }
    }
}