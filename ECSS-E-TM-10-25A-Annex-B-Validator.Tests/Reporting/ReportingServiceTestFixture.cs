// -------------------------------------------------------------------------------------------------
// <copyright file="ReportingServiceTestFixture.cs" company="RHEA System S.A.">
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
    using System.Collections.Generic;
    using CDP4Rules.Common;
    using com.rheagroup.validator.Reporting;
    using Moq;
    using NUnit.Framework;
    
    /// <summary>
    /// Suite of tests for the <see cref="ReportingService"/> class.
    /// </summary>
    [TestFixture]
    public class ReportingServiceTestFixture
    {
        private Mock<ICsvReportingService> csvReportingService;

        private Mock<IHtmlReportingService> htmlReportingService;

        private ReportingService reportingService;

        [SetUp]
        public void SetUp()
        {
            this.csvReportingService = new Mock<ICsvReportingService>();
            this.htmlReportingService = new Mock<IHtmlReportingService>();

            this.reportingService = new ReportingService(this.csvReportingService.Object, this.htmlReportingService.Object);
        }

        [Test]
        public void Verify_that_the_encapsulated_html_service_is_called()
        {
            this.reportingService.Generate("target", ReportKind.html, new List<RuleCheckResult>());

            this.htmlReportingService.Verify(x => x.Generate(It.IsAny<string>(), It.IsAny<IEnumerable<RuleCheckResult>>()), Times.Once);
        }

        [Test]
        public void Verify_that_the_encapsulated_csv_service_is_called()
        {
            this.reportingService.Generate("target", ReportKind.csv, new List<RuleCheckResult>());

            this.csvReportingService.Verify(x => x.Generate(It.IsAny<string>(), It.IsAny<IEnumerable<RuleCheckResult>>()), Times.Once);
        }
    }
}