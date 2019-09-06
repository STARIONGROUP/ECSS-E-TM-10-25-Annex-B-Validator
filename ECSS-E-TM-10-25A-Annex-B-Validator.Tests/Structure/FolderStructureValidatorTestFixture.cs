// -------------------------------------------------------------------------------------------------
// <copyright file="FolderStructureValidatorTestFixture.cs" company="RHEA System S.A.">
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

namespace com.rheagroup.validator.tests.Structure
{
    using System;
    using System.IO;
    using System.Text;
    using Exceptions;
    using NUnit.Framework;

    /// <summary>
    /// Suite of tests for the <see cref="FolderStructureValidator"/> class.
    /// </summary>
    public class FolderStructureValidatorTestFixture
    {
        private FolderStructureValidator folderStructureValidator;

        [SetUp]
        public void SetUp()
        {
            this.folderStructureValidator = new FolderStructureValidator();
        }

        [Test]
        public void Verify_that_when_FilePath_does_not_exist_an_error_is_thrown()
        {
            var nonexistentpath = "C:/bladibla/i/do/not/exist";
            Assert.Throws<DirectoryNotFoundException>(() => this.folderStructureValidator.Validate(nonexistentpath));
        }

        [Test]
        public void Verify_that_when_header_is_missing_InvalidArchiveStructureException_is_thrown()
        {
            var tempPath = this.RandomString(5, true);

            var path = System.IO.Path.Combine(TestContext.CurrentContext.TestDirectory, tempPath);
            var directoryInfo = Directory.CreateDirectory(path);

            // missing Header.json
            var exception = Assert.Throws<InvalidArchiveStructureException>(() => this.folderStructureValidator.Validate(path));
            Assert.That(exception.Message, Is.EqualTo("The Header file is missing"));

            File.Create(Path.Combine(directoryInfo.FullName, "Header.json"));

            // missing SiteDirectory.json
            exception = Assert.Throws<InvalidArchiveStructureException>(() => this.folderStructureValidator.Validate(path));
            Assert.That(exception.Message, Is.EqualTo("The SiteDirectory file is missing"));

            File.Create(Path.Combine(directoryInfo.FullName, "SiteDirectory.json"));

            // missing SiteReferenceDataLibraries folder
            exception = Assert.Throws<InvalidArchiveStructureException>(() => this.folderStructureValidator.Validate(path));
            Assert.That(exception.Message, Is.EqualTo("The SiteReferenceDataLibraries folder is missing"));

            directoryInfo.CreateSubdirectory("SiteReferenceDataLibraries");
            
            // missing EngineeringModels folder
            exception = Assert.Throws<InvalidArchiveStructureException>(() => this.folderStructureValidator.Validate(path));
            Assert.That(exception.Message, Is.EqualTo("The EngineeringModels folder is missing"));

            directoryInfo.CreateSubdirectory("EngineeringModels");

            exception = Assert.Throws<InvalidArchiveStructureException>(() => this.folderStructureValidator.Validate(path));
            Assert.That(exception.Message, Is.EqualTo("The ModelReferenceDataLibraries folder is missing"));
        }

        private string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }
    }
}