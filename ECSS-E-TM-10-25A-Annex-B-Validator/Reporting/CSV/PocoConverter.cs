// -------------------------------------------------------------------------------------------------
// <copyright file="PocoConverter.cs" company="RHEA System S.A.">
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

namespace com.rheagroup.validator.Reporting
{
    using System;
    using CDP4Common.CommonData;
    using CDP4JsonSerializer;
    using CsvHelper;
    using CsvHelper.Configuration;
    using CsvHelper.TypeConversion;
    using Newtonsoft.Json;

    /// <summary>
    /// The purpose of the <see cref="PocoConverter{T}"/> is to convert a <see cref="Thing"/>
    /// to a JSON representation suitable for use in a CSV record.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PocoConverter<T> : DefaultTypeConverter where T : Thing
    {
        /// <summary>
        /// Converts the value to a JSON string
        /// </summary>
        /// <param name="value">
        /// the subject <see cref="object"/> to convert
        /// </param>
        /// <param name="row">
        /// the row that the <see cref="Thing"/> is written to
        /// </param>
        /// <param name="memberMapData">
        /// an instance of <see cref="MemberMapData"/>
        /// </param>
        /// <returns></returns>
        public override string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
        {
            var thing = value as T;
            var dto = thing.ToDto();
            var json = dto.ToJsonObject().ToString(Formatting.None);
            
            return base.ConvertToString(json, row, memberMapData);
        }

        /// <summary>
        /// Not Supported
        /// </summary>
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            throw new NotSupportedException();
        }
    }
}