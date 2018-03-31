﻿using System;
using Autofac;
using CommandLine;
using ProjectUpgrade.Configration;
using ProjectUpgrade.Upgrade;

namespace ProjectUpgrade
{
    public class Program
    {
        private static readonly IContainer Container = SetupContainer();

        public static void Main(string[] args)
        {
            Parser.Default.ParseArguments<UpgradeParameters, EnrichParameters>(args)
                  .MapResult((UpgradeParameters upgrade) => UpgradeProjects(upgrade),
                             (EnrichParameters enrich) => EnrichProjects(enrich),
                             errors => -1);
        }

        private static int EnrichProjects(EnrichParameters enrich)
        {
            throw new System.NotImplementedException();
        }

        private static int UpgradeProjects(UpgradeParameters upgrade)
        {
            try
            {
                var orchestrator = Container.Resolve<UpgradeOrchestrator>();
                orchestrator.ProcessProjects(upgrade);
                return 0;
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);
                return -1;
            }
        }

        private static IContainer SetupContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule<UpgradeModule>();

            return builder.Build();
        }
    }
}
