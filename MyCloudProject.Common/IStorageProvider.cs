﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyCloudProject.Common
{
    /// <summary>
    /// Defines the contract for all storage operations.
    /// </summary>
    public interface IStorageProvider
    {
        /// <summary>
        /// Downloads the input file for training.
        /// </summary>
        /// <param name="fileName">The name of the local file where the input is downloaded.</param>
        /// <returns>The fullpath name of the file as downloaded locally.</returns>
        Task<string> DownloadInputFile(string fileName);

        Task UploadResultFile(string fileName);

        /// <summary>
        /// Uploads results of the experiment as the entoty of the table storage.
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        Task UploadExperimentResult(IExperimentResult result);
    }
}
