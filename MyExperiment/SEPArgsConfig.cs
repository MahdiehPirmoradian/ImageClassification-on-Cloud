using NeoCortexApi.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace SEPArgsConfig
{
    public class SEPArgsConfig
    {
        private readonly List<string> listInputWithValue = new() { "-cf", "-if" };
        public HtmConfig htmConfig;
        public string inputFolder;
        //public string testFolder;
        public string configFile;
        public string saveFormat;
        public bool ifSaveResult;
        public string saveResultPath;


        public SEPArgsConfig(string[] args)
        {
            configFile = "";
            //Parsing the input cmd
            int index = 0;
            string currentDir = Directory.GetCurrentDirectory();
            while (index < args.Length)
            {
                if (listInputWithValue.Contains(args[index]))
                {
                    switch (args[index])
                    {
                        case "-if":
                            // current input InputFolder
                            index += 1;
                            inputFolder = Path.Combine(currentDir, args[index]);
                            break;
                        case "-cf":
                            // current config file htmconfig1.json
                            index += 1;
                            configFile = Path.Combine(currentDir, args[index]);
                            break;
                        case "--save-format":
                            index += 1;
                            saveFormat = args[index];
                            break;
                        case "--save-result":
                            ifSaveResult = true;
                            break;
                        case "--save-result-path":
                            index += 1;
                            saveResultPath = args[index];
                            break;
                        default:
                            break;
                    }
                }
                index += 1;
            }
            // Adding htmconfig1.json to a Dictionary
            htmConfig = SetupHtmConfigParameters(configFile);
        }

        
        /// <summary>
        /// Convert List of Parameter into commandline for args Config.
        /// "/InputFile_htmconfig.json" --> "-if \"/InputFile\" -cf \"htmconfig.json\""
        /// </summary>
        /// <param name="inputValues"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public static string[] GetCommandLineStringFromInput(List<string> inputValues)
        {
            string[] paramsArray = new string[] {"-if", inputValues[0], "-cf", inputValues[1] };
            return paramsArray;
        }

        public HtmConfig SetupHtmConfigParameters(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentNullException("File Name is empty ");
            }
            using (StreamReader sw = new StreamReader(fileName))
            {
                var cfgJson = sw.ReadToEnd();
                JsonSerializerSettings settings1 = new JsonSerializerSettings { Formatting = Formatting.Indented };
                HtmConfig htmConfig = JsonConvert.DeserializeObject<HtmConfig>(cfgJson, settings1);
                //htmConfig.Random = new Random(42);
                return htmConfig;
            }
        }
    }
}