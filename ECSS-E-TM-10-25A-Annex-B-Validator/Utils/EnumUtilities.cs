// -------------------------------------------------------------------------------------------------
// <copyright file="EnumUtilities.cs" company="RHEA System S.A.">
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

    /// <summary>
    /// A helper class for handling Enumeration
    /// </summary>
    public static class EnumUtilities
    {
        /// <summary>
        /// Parse a source string to it's equivalent enumeration representation.
        /// </summary>
        /// <param name="source">
        /// The source string.
        /// </param>
        /// <typeparam name="T">
        /// A passed in enumeration type
        /// </typeparam>
        /// <returns>
        /// The parsed enumeration entry as per the passed in type for <see cref="T"/>.
        /// </returns>
        public static T ParseEnum<T>(string source) where T : struct, IConvertible
        {
            return (T)Enum.Parse(typeof(T), source.Replace(" ", string.Empty), true);
        }
    }
}