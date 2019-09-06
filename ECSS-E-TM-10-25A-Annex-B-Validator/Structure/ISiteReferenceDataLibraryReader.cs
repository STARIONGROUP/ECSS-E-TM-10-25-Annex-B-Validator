// -------------------------------------------------------------------------------------------------
// <copyright file="ISiteReferenceDataLibraryReader.cs" company="RHEA System S.A.">
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
    using System.Collections.Generic;

    /// <summary>
    /// Definition of the <see cref="ISiteReferenceDataLibraryReader"/> that is used is to read the <see cref="SiteReferenceDataLibrary"/> DTO's
    /// from json files of a the E-TM-10-25 Annex C.3 structured folder 
    /// </summary>
    public interface ISiteReferenceDataLibraryReader
    {
        /// <summary>
        /// Reads reference data from an E-TM-10-25 Annex C.3 folder structure.
        /// </summary>
        /// <param name="path">
        /// path to the root of the E-TM-10-25 folder structure
        /// </param>
        IEnumerable<CDP4Common.DTO.Thing> Read(string path);
    }
}