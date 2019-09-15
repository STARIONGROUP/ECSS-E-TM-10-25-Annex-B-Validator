// -------------------------------------------------------------------------------------------------
// <copyright file="HtmlReportingServiceTestFixture.cs" company="RHEA System S.A.">
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

namespace com.rheagroup.validator.tests.Reporting
{
    using System;
    using System.Collections.Generic;
    using CDP4Common.EngineeringModelData;
    using CDP4Rules.Common;
    using com.rheagroup.validator.Reporting;
    using com.rheagroup.validator.Resources;
    using NUnit.Framework;

    /// <summary>
    /// Suite of tests for the <see cref="HtmlReportingService"/> class.
    /// </summary>
    [TestFixture]
    public class HtmlReportingServiceTestFixture
    {
        private ResourceLoader resourceLoader;

        private HtmlReportingService htmlReportingService;

        private List<RuleCheckResult> results;

        private string target;

        private EngineeringModel engineeringModel;

        private Iteration iteration;

        private ElementDefinition elementDefinition;

        [SetUp]
        public void SetUp()
        {
            this.resourceLoader = new ResourceLoader();
            
            this.htmlReportingService = new HtmlReportingService(this.resourceLoader);
            this.target = System.IO.Path.Combine(TestContext.CurrentContext.WorkDirectory, "html-report.html");
            this.results = new List<RuleCheckResult>();

            this.engineeringModel = new EngineeringModel { Iid = Guid.Parse("eb629a30-f610-4183-9796-7568bd8c6011") };
            this.iteration = new Iteration { Iid = Guid.Parse("cff3034c-3b09-422e-a52b-5663c567189b") };
            this.elementDefinition = new ElementDefinition { Iid = Guid.Parse("27e49040-6b10-4b1a-a039-eca4aca37720"), ShortName = "BAT" };

            this.engineeringModel.Iteration.Add(this.iteration);
            this.iteration.Element.Add(this.elementDefinition);

            var result = new RuleCheckResult(elementDefinition, "MA-0010", "this is a description", SeverityKind.Error);

            this.results.Add(result);
        }

        [TearDown]
        public void TearDown()
        {
            if (System.IO.File.Exists(this.target))
            {
                System.IO.File.Delete(this.target);
            }
        }

        [Test]
        public void Verify_that_when_resourceloader_is_null_ArgumentNullException_is_thrown()
        {
            this.resourceLoader = null;
            Assert.Throws<ArgumentNullException>(() => new HtmlReportingService(this.resourceLoader));
        }

        [Test]
        public void Verify_that_HtmlReportingService_generates_HTML_report()
        {
            Assert.DoesNotThrow(() => this.htmlReportingService.Generate(this.target, this.results));
        }
    }
}