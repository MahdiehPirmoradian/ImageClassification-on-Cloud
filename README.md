## Experiment : ML21/22-1.2.Analyse Image Classification 

## I. Software Engineering Project Description

### Software Engineering Project : [Url](https://github.com/UniversityOfAppliedSciencesFrankfurt/se-cloud-2021-2022/tree/Metaverse/MySEProject/Documentation) & [Document](https://github.com/UniversityOfAppliedSciencesFrankfurt/se-cloud-2021-2022/blob/Metaverse/MySEProject/Documentation/Analyse%20Image%20Classification(Hand%20Drawn%20Shapes)Metaverse4.pdf)


The "Analyse Image Classification project" is a new approach to image classification based on the open HTM version algorithm.


The primary goal of the project was to investigate an open-source implementation of the Hierarchical Temporal Memory(htm) in C#/.NET Core. In the previous efforts, team Metaverse was working on different aspects of this project, such as optimizing the configuration settings of the main experiment, investigating the results based on different configurations, and providing the prediction code where a test image could get classified in a category given in the training data. Besides, developing the output correlation table which showed the correlation results of the training images before training the system.



the idea of the project was to train the system with a data set, that consisted of some shape categories (like triangle, hexagon, and straightcross) and let the system reach a stable status after training itself. Then give the stable system a picture as a test_input, and let the system figure out if the test_input image belongs to one of the training images category. By manipulating the parameters shown in Table 1 we were trying to get the best possible outputs of the system.




***HTM: HTM is used for prediction, anomaly detection, and understanding complex data streams. It's distinct from other machine learning methods, emphasizing a biological basis and focusing on time-based data.



## II. Cloud Project Description




A cloud approach of the previous project was used, as a result of the necessity of executing multiple experiments and a bigger dataset of training input images.

[Azure](https://azure.microsoft.com/de-de/resources/cloud-computing-dictionary/what-is-azure/?OCID=AIDcmmzzaokddl_SEM_2f19d22faf361b1291eeb12fc3f0c36f:G:s&ef_id=2f19d22faf361b1291eeb12fc3f0c36f:G:s&msclkid=2f19d22faf361b1291eeb12fc3f0c36f) as a cloud platform was used in this experiment.


Note that we use 1k learning cycles in this experiment.

#### We have provided the following input files:
* Input Images
* HTM Config File (htmconfig.json)


## III. Input DataSet: **(Hand Drawn Shapes)**
The images DataSet for the training process are chosen from the following link: [DataSetImages](https://www.kaggle.com/abdurrahumaannazeer/handdrawnshapes)


The training set consists of 1420 images in 7 different categories. we trained our model with 3 categories 

The testing set consists of 280 images in 7 different categories.

![Input DataSet](https://user-images.githubusercontent.com/74245613/185719920-5c02237d-f067-49dc-a6fa-edb448f96b67.jpg)
<p align="center">Figure 1 : Input Dataset</p>


This picture (Figure 1) shows an example of input images which are 60x60 pixels images of Black & White Hand Drawn Shapes. 



On the upper side of Figure 1, we see the training data set, divided into 3 categories, and on the bottom side of the picture, we see the binarised data of the image, produced by the binarisation part of the code. This code helps the system to convert an image to an array of zeros and ones, in order to be able to compare the images to each other, find similarities between them, and train the system upon these data.


..........................................................................................................................................................................................................................................................................................

### Parameters that we changed during this experiment:

Table 1: Experiment Parameters

| Parameters                         	  |   Value   	|    
|---------------------------------------|------------|
| Potential Radius (PotentialRadius)   	|     20     |
| Local Area Density (localAreaDensity) |     0.02   |
| Number of Active Columns per Inhibition Area (NumOfActiveColumnsPerInhArea)	| -1       	|
| Global Inhibition (GlobalInhibition)| false        	|





## IV. Experiment Architecture


#### How to run the experiment


Creating and deploying a web application on Microsoft Azure Cloud that can be triggered by a third-party person using a queue involves several steps.
Here's a high-level overview of the process:

* Develop the Web Application:

First, you need to develop the web application that you want to deploy on Azure. (Here we will use our previous software engineering project.)


* Choose a Storage Queue Service:

Azure provides a service called Azure Queue Storage, which you can use to create and manage queues. You can choose this service to create your queue.


* Create an Azure Queue:

In the Azure Portal, create a new Azure Queue using Azure Queue Storage. This queue will act as a trigger for your web application. Messages sent to this queue will initiate the processing.


* Configure the Web Application:

Your web application needs to be configured to listen for messages from the Azure Queue. This can be done using Azure SDKs or libraries for your chosen programming language.


* Deploy the Web Application:

You can deploy your web application on Azure App Service, Azure Kubernetes Service (AKS), or other suitable Azure services based on your application's requirements.


* Integrate Queue Listener in the Web Application:

Within your web application code, implement a listener that checks the Azure Queue for new messages periodically or in real time. When a message is detected in the queue, the application should process it accordingly. This might involve running specific tasks, jobs, or actions based on the message content.


* Triggering the Web Application:

To trigger your web application, the third-party person should send a message to the Azure Queue using Azure Queue Storage SDKs or tools like Azure Storage Explorer. The message should contain the necessary information or instructions for your application to process.


* Processing the Queue Message:

When a message is added to the queue, your web application's listener will detect it and initiate the corresponding action or task. This can be anything from data processing to initiating specific workflows.


* Scaling and Monitoring:

Depending on the workload and requirements, you may need to scale your web application and queue to handle a larger number of messages efficiently. Azure provides various scaling options, and you can use Azure Monitor to keep an eye on the performance and health of your application.


* Security and Authentication:

Ensure that you implement proper security measures, including access control and authentication mechanisms, to protect your web application and the queue from unauthorized access and potential security threats.


* Logging and Error Handling:

Implement robust logging and error handling within your web application to track the processing of messages and handle any unexpected issues gracefully.


* Cost Management:

Keep an eye on the cost of using Azure services, especially if your application scales significantly. Azure provides tools for cost management and optimization to help you control expenses.


![Overall1](https://github.com/UniversityOfAppliedSciencesFrankfurt/se-cloud-2021-2022/blob/Mahdieh-Pirmoradian/Source/MyCloudProjectSample/Documentation/Project%20Progress/Overall1.jpg)
<p align="center">Figure 2 : Experiment Architecture</p>

* Step 1: For Training, the User uploads Input data files on the Blob az a zip file also the htmconfigfile (htmconfig.json).
* Step 2: The program listens for a queue msg.
* Step 3: User sends the Trigger Message.
* Step 4: The program reads the queue.
* Step 5: Input files must be downloaded and extracted for running the code.
* Step 6: Experiment runs (Training).
* Step 7: Reach Stable State.
* Step 8: Received from another Queue the prediction msg.
* Step 9: Predict the test image.
* Step 10: Output file is uploaded to Table Storage & Blob Storages.
* Step 11: Start listening again.



Another important part of the project is the prediction part of the code which, after reaching the stable status after the training, enables the system to get a test_input image and let the user know if the test_input image belongs to any of the categories of the training data images, and if yes, to which one, providing a similarity percentage of the test_input image to the chosen category. 



Here we have one service for training and another service for processing. Menas one service till the time we train our model another one which comes to us after we reach the stable state, used for prediction.


After training our model by the cloud, we can either use it Locally and use it or save it online and use it in other services from the same provider, to predict anything that we give to it The output of it can be labeled for example.


## V. Azure Storage Account [(guide)](https://docs.microsoft.com/en-us/azure/storage/common/storage-account-create?tabs=azure-portal)
We created a storage account to use its containers for storing our input data, and also the output result (which will be uploaded by the program later after execution), the queue section to take advantage of the queues as a run command, and tables to store an overview of our final results.

![AzureStorageAccount](https://user-images.githubusercontent.com/74245613/185732837-30286bbe-58d5-49b5-9469-92358d3eb076.jpg)
<p align="center">Figure 3 : Azure Storage Account</p>


## VI. Azure Storage [Connection String](https://docs.microsoft.com/en-us/azure/storage/common/storage-configure-connection-string) :

First and foremost, we must create communication between our application and the Azure Storage account. To do this, we'll make a CloudStorageAccount object and parse the connection string to the class instance.

~~~
DefaultEndpointsProtocol=https;AccountName=mpcloudproject;AccountKey=8MLP5lBeDU9Sdqen2EOaJjgzvoOjioXFUSsb2MvUHknJKKuJkoO7Xdca8rB/uac1hObVF/9OyZuZ+ASt23V2OQ==;EndpointSuffix=core.windows.net
~~~





### This Experiment has these Storages:



Table 2: [Azure Storages](https://docs.microsoft.com/en-us/azure/storage/common/storage-account-overview):
| Parameters                         	  |   Value   	|  Description  |
|-----------------------------------------|-------------|---------------|
| [Azure Blob Storage](https://mpcloudproject.blob.core.windows.net/metaverse-input?sp=r&st=2022-08-17T17:58:29Z&se=2023-08-18T01:58:29Z&sv=2021-06-08&sr=c&sig=1k0%2BfEy1OenXD4BO0VZT3k7eJdi0QIi5y2FEO1Iufwk%3D)  |     [metaverse-input](https://portal.azure.com/#view/Microsoft_Azure_Storage/ContainerMenuBlade/~/overview/storageAccountId/%2Fsubscriptions%2Fede04061-dc1f-471f-abcb-0eee7c99a504%2Fresourcegroups%2FMahdieh_Pirmoradian%2Fproviders%2FMicrosoft.Storage%2FstorageAccounts%2Fmpcloudproject/path/metaverse-input/etag/%220x8DA734F372E0DE1%22/defaultEncryptionScope/%24account-encryption-key/denyEncryptionScopeOverride~/false/defaultId//publicAccessVal/Container)    | Keeps the Input Traing images and the htmconfig.json for training |
| [Azure Blob Storage](https://mpcloudproject.blob.core.windows.net/metaverse-output?sp=r&st=2022-08-17T18:02:19Z&se=2023-08-18T02:02:19Z&sv=2021-06-08&sr=c&sig=TtQ8tLcYZ8UgTBBIzneTX3aiTxn75rzIYwVvDWY8wV0%3D)  |     [metaverse-output](https://portal.azure.com/#view/Microsoft_Azure_Storage/ContainerMenuBlade/~/overview/storageAccountId/%2Fsubscriptions%2Fede04061-dc1f-471f-abcb-0eee7c99a504%2Fresourcegroups%2FMahdieh_Pirmoradian%2Fproviders%2FMicrosoft.Storage%2FstorageAccounts%2Fmpcloudproject/path/metaverse-output/etag/%220x8DA7FC928B8B50D%22/defaultEncryptionScope/%24account-encryption-key/denyEncryptionScopeOverride~/false/defaultId//publicAccessVal/None)   | on this container we upload the output test file after training and predicting the category of test file |
| [Azure Queue Storage](https://mpcloudproject.queue.core.windows.net/metaverse-queue)	|     [metaverse-queue](https://portal.azure.com/#view/Microsoft_Azure_Storage/QueueMenuBlade/~/overview/storageAccountId/%2Fsubscriptions%2Fede04061-dc1f-471f-abcb-0eee7c99a504%2Fresourcegroups%2FMahdieh_Pirmoradian%2Fproviders%2FMicrosoft.Storage%2FstorageAccounts%2Fmpcloudproject/queueName/metaverse-queue)  | It keeps the message for triggering and executing of the code. |
| [Azure Table Storage](https://mpcloudproject.table.core.windows.net/metaverseTResult) |     [metaverseTResult](https://github.com/UniversityOfAppliedSciencesFrankfurt/se-cloud-2021-2022/blob/Mahdieh-Pirmoradian/Source/MyCloudProjectSample/Documentation/Project%20Progress/TableStorage.JPG)   | On this Storage we keep some details from the experiment which gives a general overview about the experiment. Compared to the text file on the Output Blob Storage it has fewer details.|






Inside appsettings.json which is located in MyCloudProject inside  ["StorageConnectionString"](https://github.com/UniversityOfAppliedSciencesFrankfurt/se-cloud-2021-2022/blob/fff277192db1f6e155ed3443c61379debf6fb79d/Source/MyCloudProjectSample/MyCloudProject/appsettings.json#L14) I provide access to my AzureStorageAccount & also specified the names of Azure Storages.

![StorageConnectionString](https://user-images.githubusercontent.com/74245613/185737288-0372a902-6fa8-4830-8ddb-49506157d5c5.jpg)
<p align="center">Figure 4 : appsettings.json</p>
..........................................................................................................................................................................................................................................................................................






### Message which will trigger the experiment:


The Queue Message should be in a JSON format.

~~~json
{
    "ExperimentId": "1",
    "ProjectName": "ML21/22-1.2 Analyse Image Classification",
    "GroupName": "Metaverse", 
    "Student": "Mahdieh Pirmoradian",
    "StudentId": "1323281",
    "InputFileName": "MinCycle1000.json",
    "Description": "Experiment with Potential Radious = 10, with Minimum Cycle for stable = 1000",
    "htmConfigFile": "htmconfig.json",
    "InputFolder": "smallSet.zip"
}
~~~










Table 3: Queue Message parameter details:

| Parameter                         	  |   Description  	|
|---------------------------------------|------------|
| ExperimentID   	|     ExperimentIt Identifier which is assigned uniquely in each run of the experiment     |
| ProjectName |     Name of the experiment   |
| GroupName	| Name of the Group as it is a group work       	|
| Student | Name of the student who works on this branch        	|
| StudentId   	|     Matriculation Number of the students who work on this branch     |
| InputFileName |     Name of the InputFile, which will be downloaded from Training Container ”InputData.json”   |
| Description	| Describes this experiment request       	|
| htmConfigFile | Name of the config file, we use for the configuration of our htm model        	|
|InputFolder | Name of the InputFile, which will be downloaded from Input Training Blob Container ”smallSet.zip” |





#### A Sample Queue Message on Microsoft Azure Storage Explorer
![Queue Message](https://user-images.githubusercontent.com/74245613/183306330-48835b59-d049-4a0a-aa4f-c71c7a6f13af.jpg)
<p align="left">Figure 5 : Queue Message</p>













## VII. Experiment Input

We have provided the following input files:
* Input Images
* HTM Config File (htmconfig.json)
on the Azure Blob Storage Container (metaverse-input)

![Blob](https://user-images.githubusercontent.com/74245613/185472869-4e94355a-02cc-4729-a982-88233cae35e6.jpg)
<p align="center">Figure 6 : Azure Blob Container for Input Images</p>



## VIII. Experiment Output




* ### Azure Blob Storage:

the output of the experiment means the Macro and Micro Corrolation of training Input Images Besides the results of comparing the test image with the input image  categories which were used for training the model will be uploaded to Blob storage named as metaverse-output as a text file. The name of the uploaded text file is [Redirect. txt](https://mpcloudproject.blob.core.windows.net/metaverse-output/Redirect.txt?sp=r&st=2022-08-17T19:23:43Z&se=2023-08-18T03:23:43Z&sv=2021-06-08&sr=b&sig=B%2FqA7E%2BoUFoPAYnZGuvkjCRFXSRpbbeT5jbs6Ueg1P4%3D).

![Blob Output](https://user-images.githubusercontent.com/74245613/185225592-b5d66573-f4a1-4f38-ac17-88d24a348535.jpg)
<p align="center">Figure 7 : Azure Blob Container for Output Images</p>







Here we can see the result of the experiment which is uploaded to the Redirect.txt file.

![ExperimentOutputFile](https://user-images.githubusercontent.com/74245613/185480209-8a3302d4-5b71-4413-8c66-f63f258e954b.jpg)
<p align="center">Figure 8 : Redirect.txt file</p>



Numbers in the table represent the similarity percentage that the system can recognize between two categories. under the input columns, the similarity percentage between two categories before the system is trained, and under the output columns, the similarity percentage between two categories after the system is trained, is shown. As it can be observed, generally, after the system is trained(output columns) with the data set, the similarity of each category to itself is increased, and the similarity of each category to other categories is decreased when compared to the similarity percentages before training(input columns).


* ### Table Storage:

In order to connect to Azure Storage accounts, we must build an instance of the CloudTableClient.

[Table Storage](https://github.com/UniversityOfAppliedSciencesFrankfurt/se-cloud-2021-2022/blob/Mahdieh-Pirmoradian/Source/MyCloudProjectSample/Documentation/Project%20Progress/TableStorage.JPG) on Microsoft Azure Storage Explorer:

![TableStorage](https://user-images.githubusercontent.com/74245613/185221290-30040e3c-57b0-49e7-bdd9-ca29df5c467b.jpg)
<p align="center">Figure 9 : Azure Table Storage</p>



## IX. Docker :
This part completes the whole process of automating the project and running it independently of the client's computer speed.


* 1- First we create a Docker Image from the application:
![Build Docker Image](https://user-images.githubusercontent.com/74245613/185482016-39beb022-1bb4-4c15-b870-a1eff96bec3e.jpg)
<p align="center">Figure 10 : How to build a Docker Image</p>





..........................................................................................................................................................................................................................................................................................





* 2- Creating [Azure Container Registry](https://docs.microsoft.com/en-us/azure/container-registry/container-registry-get-started-portal?tabs=azure-cli):
![AzureContainerRegistry](https://user-images.githubusercontent.com/74245613/185762757-bf745ff0-5210-46a6-8fcb-a986011e289c.jpg)
<p align="center">Figure 11 : Azure Container Registry</p>




..........................................................................................................................................................................................................................................................................................



* 3- Deploy & Store the docker image on an "[Azure Container Registry](https://docs.microsoft.com/en-us/azure/container-registry/container-registry-get-started-docker-cli?tabs=azure-cli)" :

These steps are for deploying Our Docker Application on a private Container Registry (Azure) not a public one (Docker Hub)




However, the important part here which is tricky is logging into ACR(Azure Container Registry), so as shown in Figure 13, we go to our ACR and enable the Admin user as shown in Figure so now we will have the name and password for logging into the ACR.

![loging into ACR](https://user-images.githubusercontent.com/74245613/185765136-db9db77e-2fac-47aa-88f8-c056c2aa443e.jpg)
<p align="center">Figure 12 : Enable Logging into Azure Container Registry</p>


Then as shown in Figure 13 we can log in to the Container Registry from Command Prompt with this command. by having the Login Server, username, and password.


![Docker login](https://user-images.githubusercontent.com/74245613/185765041-293091e1-bc0e-4475-af8b-819bfd3fa68e.jpg)
<p align="center">Figure 13 : Docker login</p>







After tagging the Image, it is possible to upload it to the Azure Container Registry (this is a storage space on Azure, where we can deploy and store our images).

![Tag immage List](https://user-images.githubusercontent.com/74245613/185765081-fa86e57e-0008-4453-9465-d78284b38845.jpg)
<p align="center">Figure 14 : Tag Docker Image</p>



Great now everything is ready to push the Docker Image into ACR


![Push the Docker Image into ACR](https://user-images.githubusercontent.com/74245613/185765181-ae36b8c9-66ab-4520-9a33-ad3e5c961f4a.jpg)
<p align="center">Figure 15 : Push the Docker Image into ACR</p>




As shown in Figure 16, we can verify that the Docker Image is available on ACR by using the Azure portal:

![Verify Pushing the Docker Image into ACR](https://user-images.githubusercontent.com/74245613/185765199-3425a4eb-b548-4edc-a1ec-0923e443af69.jpg)
<p align="center">Figure 16 : Verify we have Docker Image in ACR</p>



..........................................................................................................................................................................................................................................................................................

* 4- Deploy a container instance in Azure: [Guide](https://docs.microsoft.com/en-us/azure/container-instances/container-instances-quickstart-portal)


Why Container Instance? An easy method for running a container in Azure is Azure Container Instances.



By following the instructions as shown in Figure 17, we deployed a publicly accessible application in Azure Container Instances.

Note that here we choose the Image source, Azure Container Registry, and choose the available docker Image On Azure Container Registry which we deployed in section 3.



![Create Container Instance](https://user-images.githubusercontent.com/74245613/185767673-f6a7cb87-1075-457e-8702-acc5a70bb2e2.jpg)
<p align="center">Figure 17 : Deploy a container instance in Azure</p>



![Container Instance](https://user-images.githubusercontent.com/74245613/185767696-0cdc6461-055a-497c-8b69-6edb1ae3ebc3.jpg)
<p align="center">Figure 18 : Running the Image on container instance</p>



![StatusContainerInstance](https://user-images.githubusercontent.com/74245613/185767706-246269b6-6ada-4a02-84d8-e00fe92cd554.jpg)
<p align="center">Figure 19 : Status of Container Instance on Azure</p>

..........................................................................................................................................................................................................................................................................................








If you have any questions regarding the project you can reach me under my email: 
* [Email](mahdieh.ai@yahoo.com)






