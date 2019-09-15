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
    using System;
    using System.Collections.Generic;
    using CDP4Rules.Common;
    using DotLiquid;

    /// <summary>
    /// The purpose of the <see cref="ResultsDrop"/> class is to encapsulate the <see cref="RuleCheckResult"/>s
    /// and expose them to the DotLiquid template used to generate the HTML report
    /// </summary>
    public class ResultsDrop : Drop
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResultsDrop"/>
        /// </summary>
        /// <param name="ruleCheckResults">
        /// An <see cref="IEnumerable{RuleCheckResult}"/> used to generate the results
        /// </param>
        public ResultsDrop(IEnumerable<RuleCheckResult> ruleCheckResults)
        {
            this.RuleCheckResult = new List<RuleCheckResultDrop>();
            this.PopulateRuleCheckResult(ruleCheckResults);
            this.Date = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffffffK");
        }

        /// <summary>
        /// Gets or sets the <see cref="Date"/> at which the results have been generated.
        /// </summary>
        public string Date { get; set; }

        /// <summary>
        /// Gets the <see cref="List{RuleCheckResultDrop}"/>
        /// </summary>
        public List<RuleCheckResultDrop> RuleCheckResult { get; private set; }

        /// <summary>
        /// Populates the RuleCheckResult drop
        /// </summary>
        /// <param name="ruleCheckResults">
        /// An <see cref="IEnumerable{RuleCheckResult}"/> used to generate the results
        /// </param>
        private void PopulateRuleCheckResult(IEnumerable<RuleCheckResult> ruleCheckResults)
        {
            foreach (var ruleCheckResult in ruleCheckResults)
            {
                var drop = new RuleCheckResultDrop(ruleCheckResult);
                this.RuleCheckResult.Add(drop);
            }
        }
    }
}