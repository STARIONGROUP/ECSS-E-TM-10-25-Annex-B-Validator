// -------------------------------------------------------------------------------------------------
// <copyright file="ResultsDrop.cs" company="RHEA System S.A.">
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

namespace com.rheagroup.validator.Reporting.HTML
{
    using CDP4JsonSerializer;
    using Newtonsoft.Json;
    using CDP4Rules.Common;
    using DotLiquid;

    public class RuleCheckResultDrop : Drop
    {
        /// <summary>
        /// encapsulated <see cref="RuleCheckResult"/>
        /// </summary>
        private readonly RuleCheckResult result;

        /// <summary>
        /// Initializes a new instance of the <see cref="RuleCheckResultDrop"/> class.
        /// </summary>
        /// <param name="result">
        /// The encapsulated <see cref="RuleCheckResult"/>
        /// </param>
        public RuleCheckResultDrop(RuleCheckResult result)
        {
            this.result = result;
        }

        /// <summary>
        /// Gets the identifier or code of the Rule that may have been broken
        /// </summary>
        public string Id
        {
            get => this.result.Id;
        }

        /// <summary>
        /// Gets the description of the Rule that may have been broken
        /// </summary>
        public string Description
        {
            get => this.result.Description;
        }
        
        /// <summary>
        /// Json representation of the encapsulated <see cref="Thing"/>
        /// </summary>
        public string Thing
        {
            get => this.result.Thing.ToDto().ToJsonObject().ToString(Formatting.None);
        }

        /// <summary>
        /// Gets the string representation of the <see cref="SeverityKind"/>
        /// </summary>
        public string Severity
        {
            get => this.result.Severity.ToString();
        }
    }
}