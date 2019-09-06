// -------------------------------------------------------------------------------------------------
// <copyright file="SiteReferenceDataLibraryReader.cs" company="RHEA System S.A.">
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

namespace com.rheagroup.validator
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Collections.Generic;
    using CDP4Common.CommonData;
    using CDP4Common.MetaInfo;
    using CDP4Common.SiteDirectoryData;
    using CDP4JsonSerializer;

    /// <summary>
    /// The purpose of the <see cref="SiteReferenceDataLibraryReader"/> is to read the <see cref="SiteReferenceDataLibrary"/> DTO's
    /// from json files of a the E-TM-10-25 Annex C.3 structured folder 
    /// </summary>
    public class SiteReferenceDataLibraryReader : ISiteReferenceDataLibraryReader
    {
        /// <summary>
        /// The (injected) IFolderStructureValidator
        /// </summary>
        private readonly IFolderStructureValidator folderStructureValidator;

        /// <summary>
        /// The (IMetaDataProvider) IFolderStructureValidator
        /// </summary>
        private readonly IMetaDataProvider metaDataProvider;

        /// <summary>
        /// The <see cref="Cdp4JsonSerializer"/> used to deserialize the JSON data
        /// </summary>
        private readonly Cdp4JsonSerializer cdp4JsonSerializer;

        /// <summary>
        /// Initializes a new instance of the <see cref="SiteReferenceDataLibraryReader"/> class.
        /// </summary>
        /// <param name="folderStructureValidator">
        /// The (injected) <see cref="IFolderStructureValidator"/> used to validate the folders containing the E-tM-10-25 Annex C.3 population
        /// </param>
        /// <param name="metaDataProvider">
        /// The (injected) <see cref="IMetaDataProvider"/> used for optimized (de)serialization of E-TM-10-25 json data
        /// </param>
        public SiteReferenceDataLibraryReader(IFolderStructureValidator folderStructureValidator, IMetaDataProvider metaDataProvider)
        {
            this.folderStructureValidator = folderStructureValidator ?? throw new ArgumentNullException(nameof(folderStructureValidator));
            this.metaDataProvider  = metaDataProvider ?? throw new ArgumentNullException(nameof(metaDataProvider));

            var dalVersion = new Version("1.0.0");
            
            this.cdp4JsonSerializer = new Cdp4JsonSerializer(metaDataProvider, dalVersion);
        }

        /// <summary>
        /// Reads reference data from an E-TM-10-25 Annex C.3 folder structure.
        /// </summary>
        /// <param name="path">
        /// path to the root of the E-TM-10-25 folder structure
        /// </param>
        public IEnumerable<CDP4Common.DTO.Thing> Read(string path)
        {
            var directoryInfo = this.folderStructureValidator.Validate(path);

            var result = new List<CDP4Common.DTO.Thing>();

            var siteDirectoryThings = this.ReadSiteDirectoryJson(directoryInfo).ToList();

            result.AddRange(siteDirectoryThings);

            var siteDirectory = (CDP4Common.DTO.SiteDirectory)siteDirectoryThings.Single(x => x.ClassKind == ClassKind.SiteDirectory);

            foreach (var siteReferenceDataLibraryIid in siteDirectory.SiteReferenceDataLibrary)
            {
                var referenceData = this.ReadSiteReferenceDataLibraryJson(directoryInfo, siteReferenceDataLibraryIid);
                result.AddRange(referenceData);
            }

            return result;
        }

        /// <summary>
        /// Reads <see cref="SiteDirectory"/> data from the folder structure
        /// </summary>
        /// <param name="directory">
        /// the root <see cref="directory"/> of the E-TM-10-25 folder structure
        /// </param>
        /// <returns>
        /// an <see cref="IEnumerable{Thing}"/> containing <see cref="SiteDirectory"/> data
        /// </returns>
        private IEnumerable<CDP4Common.DTO.Thing> ReadSiteDirectoryJson(DirectoryInfo directory)
        {
            var siteDirectoryFile = directory.GetFileSystemInfos().Single(x => x.Name == "SiteDirectory.json");

            List<CDP4Common.DTO.Thing> result;

            using (var fileStream = System.IO.File.OpenRead(siteDirectoryFile.FullName))
            {
                result = this.cdp4JsonSerializer.Deserialize(fileStream).ToList();
            }

            return result;
        }

        /// <summary>
        /// Reads <see cref="SiteReferenceDataLibrary"/> data from the folder structure
        /// </summary>
        /// <param name="directory">
        /// the root <see cref="directory"/> of the E-TM-10-25 folder structure
        /// </param>
        /// <returns>
        /// an <see cref="IEnumerable{Thing}"/> containing <see cref="SiteDirectory"/> data
        /// </returns>
        private IEnumerable<CDP4Common.DTO.Thing> ReadSiteReferenceDataLibraryJson(DirectoryInfo directory, Guid siteReferenceDataLibraryIid)
        {
            var siteReferenceDataLibrariesFolder = directory.GetDirectories().Single(x => x.Name == "SiteReferenceDataLibraries");

            var siteReferenceDataLibraryFile = siteReferenceDataLibrariesFolder.GetFileSystemInfos().Single(x => x.Name == $"{siteReferenceDataLibraryIid}.json");

            List<CDP4Common.DTO.Thing> result;

            using (var fileStream = System.IO.File.OpenRead(siteReferenceDataLibraryFile.FullName))
            {
                result = this.cdp4JsonSerializer.Deserialize(fileStream).ToList();
            }

            return result;
        }
    }
}