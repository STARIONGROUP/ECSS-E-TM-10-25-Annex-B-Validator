// -------------------------------------------------------------------------------------------------
// <copyright file="HtmlReportingService.cs" company="RHEA System S.A.">
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
    using System.Collections.Generic;
    using System.IO;
    using DotLiquid;
    using CDP4Rules.Common;
    using com.rheagroup.validator.Reporting.HTML;
    using com.rheagroup.validator.Resources;

    /// <summary>
    /// The purpose of the <see cref="HtmlReportingService"/> is to generate an
    /// HTML report of the <see cref="RuleCheckResult"/>s
    /// </summary>
    public class HtmlReportingService : IHtmlReportingService
    {
        /// <summary>
        /// The (injected) <see cref="IResourceLoader"/> used to load liquid templates
        /// </summary>
        private readonly IResourceLoader resourceLoader;

        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlReportingService"/> class.
        /// </summary>
        /// <param name="resourceLoader">
        /// The (injected) <see cref="IResourceLoader"/> used to load liquid templates
        /// </param>
        public HtmlReportingService(IResourceLoader resourceLoader)
        {
            this.resourceLoader = resourceLoader ?? throw new ArgumentNullException(nameof(resourceLoader));
        }

        /// <summary>
        /// Generates an HTML report with the results of a validation run.
        /// </summary>
        /// <param name="target">
        /// the target path
        /// </param>
        /// <param name="results">
        /// The <see cref="RuleCheckResult"/> from which a report is to be generated
        /// </param>
        public void Generate(string target, IEnumerable<RuleCheckResult> results)
        {
            var drop = new ResultsDrop(results);

            string reportTemplate = this.resourceLoader.LoadEmbeddedResource("com.rheagroup.validator.Reporting.HTML.report.liquid");
            var template = Template.Parse(reportTemplate);
            var generatedReport = template.Render(Hash.FromAnonymousObject(new { content = drop }));

            File.WriteAllText(target, generatedReport);
        }
    }
}