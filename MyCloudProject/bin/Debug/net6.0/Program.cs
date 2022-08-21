using MyCloudProject.Common;
using System;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System.Threading;
using MyExperiment;
using System.Threading.Tasks;

namespace MyCloudProject
{
    class Program
    {
        /// <summary>
        /// Your project ID from the last semester.
        /// </summary>
        private static string projectName = "ML21/22-1.2.Analyse Image Classification(Hand Drawn Shapes)";

        string test;

        static async Task Main(string[] args)
        {
            /* It gives me some control to step out of the Project
            When it happens it help us to exit a docker Container which somewhere is running */
            CancellationTokenSource tokeSrc = new CancellationTokenSource();

            Console.CancelKeyPress += (sender, e) =>
            {
                e.Cancel = true;
                tokeSrc.Cancel();
            };

            Console.WriteLine($"Started experiment: {projectName}");

            //init configuration
            var cfgRoot = Common.InitHelpers.InitConfiguration(args);

            var cfgSec = cfgRoot.GetSection("MyConfig");

            // InitLogging
            var logFactory = InitHelpers.InitLogging(cfgRoot);
            var logger = logFactory.CreateLogger("Train.Console");

            logger?.LogInformation($"{DateTime.Now} -  Started experiment: {projectName}");

            IStorageProvider storageProvider = new AzureStorageProvider(cfgSec);

            Experiment experiment = new Experiment(cfgSec, storageProvider, logger/* put some additional config here */);
            
            await experiment.RunQueueListener(tokeSrc.Token);

            logger?.LogInformation($"{DateTime.Now} -  Experiment exit: {projectName}");
        }


    }
}
