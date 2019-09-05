// -------------------------------------------------------------------------------------------------
// <copyright file="InvalidArchiveStructureException.cs" company="RHEA System S.A.">
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

namespace com.rheagroup.validator.Exceptions
{
    using System;

    /// <summary>
    /// A <see cref="InvalidArchiveStructureException"/> is thrown the when the folder structure does
    /// not conform to E-TM-10-25 Annex C.3
    /// </summary>
    public class InvalidArchiveStructureException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidArchiveStructureException"/> class.
        /// </summary>
        public InvalidArchiveStructureException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidArchiveStructureException"/> class.
        /// </summary>
        /// <param name="message">
        /// The exception message
        /// </param>
        public InvalidArchiveStructureException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidArchiveStructureException"/> class.
        /// </summary>
        /// <param name="message">
        /// The exception message
        /// </param>
        /// <param name="innerException">
        /// A reference to the inner <see cref="Exception"/>
        /// </param>
        public InvalidArchiveStructureException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}