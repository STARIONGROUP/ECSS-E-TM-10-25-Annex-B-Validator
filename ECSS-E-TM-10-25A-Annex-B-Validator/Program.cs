// -------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="RHEA System S.A.">
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

using CDP4Common.MetaInfo;
using CDP4Rules;

namespace com.rheagroup.validator
{
    using System;
    using Autofac;
    using AutofacSerilogIntegration;
    using com.rheagroup.validator.Commands;
    using com.rheagroup.validator.Reporting;
    using com.rheagroup.validator.Resources;
    using Microsoft.Extensions.CommandLineUtils;
    using Microsoft.Extensions.Configuration;
    using Serilog;

    /// <summary>
    /// Command-line application to validate an E-TM-10-25 Annex B population.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Inversion of Control container
        /// </summary>
        private static IContainer Container { get; set; }

        /// <summary>
        /// main entry point of the application
        /// </summary>
        /// <param name="args">
        /// The command line arguments
        /// </param>
        public static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            Container = ConfigureContainer();

            ConfigureCommandLineApplication(args);
        }

        /// <summary>
        /// Configure the IOC container
        /// </summary>
        /// <returns>
        /// an instance of <see cref="IContainer"/>
        /// </returns>
        private static IContainer ConfigureContainer()
        {
            Log.Logger.Debug("Configure IoC Container");

            var builder = new ContainerBuilder();

            builder.RegisterType<ResourceLoader>().As<IResourceLoader>();
            builder.RegisterType<SiteReferenceDataLibraryReader>().As<ISiteReferenceDataLibraryReader>();
            builder.RegisterType<FolderStructureValidator>().As<IFolderStructureValidator>();
            builder.RegisterType<ReportingService>().As<IReportingService>();
            builder.RegisterType<ValidateCommand>().As<IValidateCommand>();
            builder.RegisterType<ValidatorCommandFactory>().As<IValidatorCommandFactory>();
            builder.RegisterType<RuleCheckerEngine>().As<IRuleCheckerEngine>();
            builder.RegisterType<MetaDataProvider>().As<IMetaDataProvider>();

            builder.RegisterLogger();

            Log.Logger.Debug("IoC Container Configured");

            return builder.Build();
        }

        /// <summary>
        /// Configure the <see cref="CommandLineApplication"/> with actions
        /// </summary>
        /// <param name="args">
        /// The command line arguments
        /// </param>
        public static void ConfigureCommandLineApplication(string[] args)
        {
            Log.Logger.Debug("Configure Command Line Arguments");

            var commandLineApplication = new CommandLineApplication();
            commandLineApplication.Name = "E-TM-10-25-Annex-B-Validator";
            commandLineApplication.Description = "Console application to validate an E-TM-10-25 Annex B population";

            commandLineApplication.HelpOption("-?|--help");

            commandLineApplication.OnExecute(() =>
            {
                Console.WriteLine();
                Console.WriteLine("        use -? or --help to display help");
                return 0;
            });

            using (var scope = Container.BeginLifetimeScope())
            {
                var validatorversion = QueryValidatorVersion();

                Console.WriteLine(scope.Resolve<Resources.IResourceLoader>().
                    LoadEmbeddedResource("com.rheagroup.validator.Resources.ascii-art.txt")
                    .Replace("validatorversion", validatorversion));

                commandLineApplication.Command("validate", scope.Resolve<IValidatorCommandFactory>().Register);
            }

            commandLineApplication.Execute(args);

            Log.Logger.Debug("Command Line Arguments Configured");
        }

        /// <summary>
        /// queries the version number from the executing assembly
        /// </summary>
        /// <returns>
        /// a string representation of the version of the application
        /// </returns>
        private static string QueryValidatorVersion()
        {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            return assembly.GetName().Version.ToString();
        }
    }
}