// -------------------------------------------------------------------------------------------------
// <copyright file="RuleCheckResultCsvMap.cs" company="RHEA System S.A.">
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
    using CDP4Common.CommonData;
    using CDP4Rules.Common;
    using CsvHelper.Configuration;

    /// <summary>
    /// The purpose of the <see cref="RuleCheckResultCsvMap"/> is to provide
    /// a custom mapping of the <see cref="RuleCheckResult"/> to a CSV Record.
    /// </summary>
    public class RuleCheckResultCsvMap : ClassMap<RuleCheckResult>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RuleCheckResultCsvMap"/> class.
        /// </summary>
        public RuleCheckResultCsvMap()
        {
            Map(m => m.Severity);
            Map(m => m.Description);
            Map(m => m.Id).Name("Rule-ID");
            Map(m => m.Thing.Iid).Name("Thing-Iid");
            Map(m => m.Thing).TypeConverter<PocoConverter<Thing>>();
        }
    }
}