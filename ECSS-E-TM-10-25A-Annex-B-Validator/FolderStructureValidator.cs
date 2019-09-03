// -------------------------------------------------------------------------------------------------
// <copyright file="FolderStructureValidator.cs" company="RHEA System S.A.">
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
//   along with Foobar.  If not, see<http://www.gnu.org/licenses/>.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace com.rheagroup.validator
{
    using System.IO;
    using System.Linq;
    using com.rheagroup.validator.Exceptions;

    /// <summary>
    /// The purpose of the <see cref="FolderStructureValidator"/> is to validate that the structure of
    /// a folder conforms to ECSS-E-TM-10-25 Annex C.3
    /// </summary>
    public static class FolderStructureValidator
    {
        /// <summary>
        /// Validates that a folder conforms to ECSS-E-TM-10-25 Annex C.3
        /// </summary>
        /// <param name="path"></param>
        public static DirectoryInfo Validate(string path)
        {
            if (!System.IO.Directory.Exists(path))
            {
                throw new DirectoryNotFoundException($"The root of the archive folder structure could not be found: {path}");
            }

            var directoryInfo = new DirectoryInfo(path);
            var files = directoryInfo.GetFiles().ToList();

            if (files.All(f => f.Name != "Header.json"))
            {
                throw new InvalidArchiveStructureException("The Header file is missing");
            }

            if (files.All(f => f.Name != "SiteDirectory.json"))
            {
                throw new InvalidArchiveStructureException("The SiteDirectory file is missing");
            }

            var directories = directoryInfo.GetDirectories().ToList();

            if (directories.All(d => d.Name != "SiteReferenceDataLibraries"))
            {
                throw new InvalidArchiveStructureException("The SiteReferenceDataLibraries folder is missing");
            }

            if (directories.All(d => d.Name != "EngineeringModels"))
            {
                throw new InvalidArchiveStructureException("The EngineeringModels folder is missing");
            }

            if (directories.All(d => d.Name != "ModelReferenceDataLibraries"))
            {
                throw new InvalidArchiveStructureException("The EngineeringModels folder is missing");
            }

            return directoryInfo;
        }
    }
}