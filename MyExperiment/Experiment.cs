using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MyCloudProject.Common;
using System;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using SEPImageClassification;
using SEPArgsConfig;
using System.IO;
using System.IO.Compression;

namespace MyExperiment
{
    /// <summary>
    /// This class implements the ML experiment that will run in the cloud. This is refactored code from my SE project.
    /// </summary>
    public class Experiment : IExperiment
    {   
        private IStorageProvider storageProvider;

        private ILogger logger;

        private MyConfig config;

        public Experiment(IConfigurationSection configSection, IStorageProvider storageProvider, ILogger log)
        {
            this.storageProvider = storageProvider;
            this.logger = log;

            config = new MyConfig();
            configSection.Bind(config);
        }

        public Task<IExperimentResult> Run(string inputString)
        {
            // TODO read file

            // YOU START HERE WITH YOUR SE EXPERIMENT!!!!
            // inputString = "/inputFolder_htmconfig.json"
            List<string> inputValues = new List<string>(inputString.Split("_"));

            ExperimentResult res = new ExperimentResult(this.config.GroupId, null);
            
            //u should put some results of your experiment into the variable "res"
            res.StartTimeUtc = DateTime.UtcNow;

            DateTime start = DateTime.Now;

            string[] experimentParams = SEPArgsConfig.SEPArgsConfig.GetCommandLineStringFromInput(inputValues);
            // Run your experiment code here.
            // Getting the list of args from the command line
            SEPArgsConfig.SEPArgsConfig config = new SEPArgsConfig.SEPArgsConfig(experimentParams);

            //for setting the initialized parameter of the HTM
            SEPExperiment ex1 = new SEPExperiment(config);

            
            var experimentResult =ex1.Run();

 
 

            DateTime end = DateTime.Now;
            TimeSpan Duration = (end - start);
            double DurationSec = Duration.TotalSeconds;
            res.DurationSec = experimentResult["DurationSec"];

            res.PredictedLabel = experimentResult["predictedLabel"];
            res.testfilesname = experimentResult["testfilesname"];
            res.maxPredictedSimilarity = experimentResult["maxPredictedSimilarity"];

           

            return Task.FromResult<IExperimentResult>(res); // TODO...
        }

        /// <inheritdoc/>
        public async Task RunQueueListener(CancellationToken cancelToken)
        {


            QueueClient queueClient = new QueueClient(this.config.StorageConnectionString, this.config.Queue);

            
            while (cancelToken.IsCancellationRequested == false)
            {
                QueueMessage message = await queueClient.ReceiveMessageAsync();

                if (message != null)
                {
                    try
                    {

                        string msgTxt = Encoding.UTF8.GetString(message.Body.ToArray());

                        //For printing on to the logger
                        this.logger?.LogInformation($"Received the message {msgTxt}");

                        // TODO: add directory of the files on blob into ExerimentRequestMessage
                        // add the info to your queue message
                        ExerimentRequestMessage request = JsonSerializer.Deserialize<ExerimentRequestMessage>(msgTxt);


                        string zipFileName = request.InputFile;
                        string trainingData = "TrainingData";
                        // TODO implement download multiple files from a directory in a blob
                        // NOTE if archiving is implemented, download 1 zip file

                        var inputTrainingFolder = await this.storageProvider.DownloadInputFile(request.InputFile);


                        // extracting the downloaded zip file (from blob) into the current path (as the execution happens where the zip is downloaded)
                        // if : extracts only if its not already extracted
                        string currDir = Directory.GetCurrentDirectory();
                        if (!Directory.Exists(Path.Combine(currDir,trainingData)))
                        {
                            ZipFile.ExtractToDirectory(zipFileName, trainingData);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("ZipFile Extracted");
                            Console.ResetColor();

                            //this code moves the test img for prediction to another desired directory
                            string TestImgDir = Path.Combine(currDir, trainingData, "TestImg")
  ;
                            string destDirName = Path.Combine(currDir, "TestImg")
;
                            try
                            {
                                Console.WriteLine(TestImgDir);
                                Console.WriteLine("to"+ destDirName);
                                Directory.Move(TestImgDir, destDirName);
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine("prediction folder moved successfully!");
                                Console.ResetColor();
                            }
                            catch (IOException exp)
                            {
                                Console.WriteLine(exp.Message);
                            }
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("Extracted Data Set exists");
                            Console.ResetColor();

                        }
                        Console.WriteLine(Directory.GetCurrentDirectory());
                            string inputString = $"{trainingData}_{request.htmConfigFile}";

                        IExperimentResult result = await this.Run(inputString);
                        
                        //TODO. do serialization of the result.
                        await storageProvider.UploadResultFile("Redirect.txt");

                        await storageProvider.UploadExperimentResult(result);

                        await queueClient.DeleteMessageAsync(message.MessageId, message.PopReceipt);
                    }
                    catch (Exception ex)
                    {
                        this.logger?.LogError(ex, "");
                    }
                }
                else
                {
                    await Task.Delay(500);
                    logger?.LogTrace("Queue empty...");
                }
            }

            this.logger?.LogInformation("Cancel pressed. Exiting the listener loop.");
        }


        #region Private Methods


        #endregion
    }
}
