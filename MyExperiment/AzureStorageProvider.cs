using Azure.Data.Tables;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Configuration;
using MyCloudProject.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using SEPImageClassification;


namespace MyExperiment
{

    public class AzureStorageProvider : IStorageProvider
    {
        private MyConfig config;

        public AzureStorageProvider(IConfigurationSection configSection)
        {
            config = new MyConfig();
            configSection.Bind(config);
        }

        /// <summary>
        /// It is responsible to download the input file from the blob storage. 
        /// it goes through the blobstorage where the input is stored, if the blob exists, 
        /// downloads it and returns the path where the input is downloaded
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns>Input Training Folder Path</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<string> DownloadInputFile(string fileName)
        {
            // Creating an object for Blob Container Client
            BlobContainerClient container = new BlobContainerClient(this.config.StorageConnectionString,this.config.TrainingContainer);
            await container.CreateIfNotExistsAsync();

            // Get a reference to a blob named "metaverse-input"
            BlobClient blob = container.GetBlobClient(fileName);

            // Check if BlobItem exist in Azure Storage
            if (blob.Exists().Value == false)
                return await Task.FromResult("Download Input from AzureBlobStorage Fail.");

            // Download Blob to local File
            var response = blob.DownloadTo(fileName);

            if (response.Status == 206 || response.Status == 200)
            {
                return await Task.FromResult($"Download Input {blob} from AzureBlobStorage Success.");
            }
            else
            {
                return await Task.FromResult($"Download Input {blob} from AzureBlobStorage Fail.");
            }
        }



        /// <summary>
        /// Uploads the final results of prediction to the table storage on Azure and determines which 
        /// coloumns the table should have.
        /// </summary>
        /// <param name="fileName"></param>
        public async Task UploadExperimentResult(IExperimentResult result)
        {
            try
            {
                var client = new TableClient(this.config.StorageConnectionString, this.config.ResultTable);

                await client.CreateIfNotExistsAsync();
                



                ExperimentResult res = new ExperimentResult("ST", "rowKey")
                {
                    //Timestamp = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc),

                    Name = result.Name,
                    RowKey = Guid.NewGuid().ToString(),
                    
                    PredictedLabel =result.PredictedLabel,

                    Description = "Image Classification",
                    testfilesname=result.testfilesname,
                    maxPredictedSimilarity=result.maxPredictedSimilarity,

                };



                //await client.AddEntityAsync(res);

                //this dude uploads res to table
                await client.UpsertEntityAsync((ExperimentResult)res);
            }
            catch (Exception)
            {
                throw;
            }
        }




        /// <summary>
        /// Iit upload the created text file which contains the experiment correlation table and all details of the experiment, 
        /// to the blob storage created named as "metaverse-output"
        /// </summary>
        /// <param name="fileName"></param>
        public async Task UploadResultFile(string filePath)
        {
            Console.WriteLine("File upload");

            string connectionString = this.config.StorageConnectionString;
            string fileName = System.IO.Path.GetFileName(filePath);

            // Get a reference to a container name and then create it
            BlobContainerClient container = new BlobContainerClient(connectionString, this.config.ResultContainer);

            await container.CreateIfNotExistsAsync();

            try
            {


                // Get a reference to a blob
                BlobClient blobClient = container.GetBlobClient(fileName);

                Trace.WriteLine("Uploading to Blob storage as blob:\n\t {0}\n", blobClient.Uri.ToString());

                // Upload data from the local file
                await blobClient.UploadAsync(filePath, true);

                Trace.WriteLine($"Uploaded Result File to {this.config.ResultContainer} container successfully");
                
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        /* Maybe u need it later
        //The use of this line is to download mutiple files from blob storage
        public async Task<List<string>> DownloadMultipleFiles(List<String> a)
        { 
            new List<string> a = new List<string>();
            return a;
        }
        */


    }
}
