using MyCloudProject.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyExperiment
{
    internal class ExerimentRequestMessage : IExerimentRequestMessage
    {

        /// <summary>
        /// ExperimentIt Identifier which is assigned uniquely in each run of the experiment
        /// </summary>
        public string ExperimentId { get; set; }



        /// <summary>
        /// The name of the project.
        /// </summary>
        public string ProjectName { get; set; }



        /// <summary>
        /// The name of the Group.
        /// </summary>
        public string GroupName { get; set; }



        /// <summary>
        /// Name of the Student.
        /// </summary>
        public string Student { get; set; }



        /// <summary>
        /// Name of the Student.
        /// </summary>
        public string StudentId { get; set; }




        /// <summary>
        /// Name of the json which is used for configuring our htm model.
        /// </summary>
        public string htmConfigFile { get; set; }




        /// <summary>
        /// Describes the experiment.
        /// </summary>
        public string Description { get; set; }


        /// <summary>
        /// The name of the input file.
        /// </summary>
        public string InputFile { get; set; }
        /*Toan add this
        public double potentialRadius { get; set; }
        */
    }
}


/*
 
{
    "ExperimentId": "1",
    "ProjectName": "ML21/22-1.2",
    "GroupName": "Metaverse", 
    "Student": "Mahdieh Pirmoradian",
    "StudentId": "1323281",
    "InputFileName": "MinCycle1000.json",
    "Description": "Experiment with Potential Radious = 10, with Minimum Cycle for stable = 1000",
    "htmConfigFile" : "htmconfig.json" ,
    "InputFile" : "smallSet.zip"
    
}
 
 */

