using System;
using System.Collections.Generic;
using System.Text;

namespace MyCloudProject.Common
{
    /// <summary>
    /// Defines the contract for the message request that will run your experiment.
    /// </summary>
    public interface IExerimentRequestMessage
    {

        string ExperimentId { get; set; }




        string ProjectName { get; set; }


        string GroupName { get; set; }



        string Student { get; set; }



        string StudentId { get; set; }





        string htmConfigFile { get; set; }





        string Description { get; set; }



        string InputFile { get; set; }
        /*Toan add this
        public double potentialRadius { get; set; }
        */
    }
}



