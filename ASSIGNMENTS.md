NNUG - Global Azure Bootcamp 2015
=================================

This is a collection of workshop assignments that was given during NNUG's Global Azure Bootcamp 2015 @ Bouvet on 25. April 2015. 

Overview of assignments
-----------------------

* **Create** and **deploy** an **Azure WebJob**
* **Store** and **retrieve** orders on an **Azure Service Bus**
* **Store** orders in **Azure Table Storage**

### Before you begin

If you are going to follow all of the steps in this assignment, then make sure you have all of the prerequisites:

* Have a PC with Visual Studio 2013 Update 4
* Have installed the latest version of [Azure SDK](http://go.microsoft.com/fwlink/p/?linkid=323510&clcid=0x409)
* Have an account for the [Azure-portal](http://www.windowsazure.com/) with an active subscription
* Have [Azure Storage Explorer](https://azurestorageexplorer.codeplex.com/) (or a similar storage explorer application) for viewing Table Storage data.

### Resources

[http://azure.microsoft.com/en-us/documentation/articles/websites-dotnet-webjobs-sdk-service-bus/](http://azure.microsoft.com/en-us/documentation/articles/websites-dotnet-webjobs-sdk-service-bus/)

[http://www.troyhunt.com/2015/01/azure-webjobs-are-awesome-and-you.html](http://www.troyhunt.com/2015/01/azure-webjobs-are-awesome-and-you.html)

[http://channel9.msdn.com/Tags/azurefridaywebjobs](http://channel9.msdn.com/Tags/azurefridaywebjobs)

[https://github.com/Azure/azure-webjobs-sdk-samples/tree/master/BasicSamples](https://github.com/Azure/azure-webjobs-sdk-samples/tree/master/BasicSamples)

[https://resources.azure.com/](https://resources.azure.com/)

[http://www.ni.com/white-paper/3023/en/](http://www.ni.com/white-paper/3023/en/)

[https://aws.amazon.com/blogs/aws/sqs_super_queue/](https://aws.amazon.com/blogs/aws/sqs_super_queue/)

[https://msdn.microsoft.com/en-us/library/azure/hh528527.aspx](https://msdn.microsoft.com/en-us/library/azure/hh528527.aspx)

[http://www.kcode.me/post/azure-service-bus-brokeredmessage-serialization](http://www.kcode.me/post/azure-service-bus-brokeredmessage-serialization)

### Disclaimer

The code in this assignment have been created to be consise and light by purpose, and is intended to be educational rather than production-ready. There are minimal efforts to be resilient and error handling is minimal. 

### Estimated assignment duration

2-4 hours

### Questions?

Contact us:

* **henrik.moe@bouvet.no** or on [GitHub](https://github.com/HenrikWM)
* **solve.heggem@bouvet.no**

Background
----------

You work as an independent consultant and have been contracted by Contoso Inc. to help them implement their new e-commerce website. 

Their old website didn't handle high-volume traffic and they lost business over popular holidays when orders coming in were dropped because their website couldn't handle the load. Therefore it is a critical requirement that the new solution will take care of every single order that comes in, especially during peak traffic.

They have staff to handle orders coming in manually should the website go down, but the orders must not be lost once they have been entered!

To start things off they would like you to create a proof-of-concept solution before going into a full blown project, so the tasks are small in scope and requirements for UX and GUI are minimal. At this stage they are mainly concerned with creating a skeleton-solution that delivers on their primary requirements. You main goals are therefore to maximize throughput of order placements and ensure consistent data throughout the order process, from order inception until it's stored.


Assignment #1 - Store to Azure Table Storage
---------------------------------------------

You have already created two applications that can produce orders. These act as a substitute for a website with users creating orders. 
One takes in order details from manual input (`GAB.Console.ManualInput`), the other creates a high volume of orders randomly in order to simulate high volume traffic (`GAB.Console.AutomaticInput`). They have been designed to run locally on a developer computer.

Alter the `GAB.Console.ManualInput` and `GAB.Console.AutomaticInput`-applications so that they store orders to a table in Azure Table Storage. You will need to create a table called **orders**.

1) Replace the class `InMemoryOrderStorage` with a new class called `AzureTableStorageOrderStorage`.

2) Implement the methods `Store` and `GetTotalNumberOfOrders` in `AzureTableStorageOrderStorage`

3) Ensure you have set an *appSetting* named **StorageConnectionString** in the `App.config` of the console-applications:

	<appSettings>
    	<add key="StorageConnectionString" value="DefaultEndpointsProtocol=https;AccountName=<your storage account name>;AccountKey=<your storage account key>"/>
  	</appSettings>

4) Run the console applications and watch as orders are stored. Also, use the console application `GAB.Console.ConsumerOutput` to monitor the throughput. 

Make sure your `Store`-method creates the table "orders" if it doesn't already exist. Use the `OrderEntity`-class for storing an order and map from `Order` to `OrderEntity` before you store the order.

**Definition of Done**: You can run these console applications and watch as they generate orders and store them into your **orders** table.


Assignment #2 - Send orders to an Azure Service Bus Topic
----------------------------------------------------------

Alter the `GAB.Console.ManualInput` and `GAB.Console.AutomaticInput`-applications so that they send orders to a Topic on an Azure Service Bus.

1) Create an Azure Service Bus. Create a Topic called **order-dispatch**.

1.1) Go to [https://manage.windowsazure.com](https://manage.windowsazure.com)

1.2) Click on "+ New" and select **App Services->Service Bus->Topic->Custom Create**.

![](https://f8h9tq.dm2302.livefilestore.com/y2pyNutCzfX1OxjAvpIAhqvkJD6FSWJI9vVbPOagi4FKP7WmdzkR3HdtsbscSNMCcTMhO86otdHGCTVXhotZ_zDuVE_yd7DIgfPnmaMGATtFQPsibQeqmn0JnT1jh9z6s-_KprWPXKOJshjeBz5GpAnmQ/ServiceBus%201.PNG?psid=1)

1.3) Enter the following details:

**Topic Name**: **order-dispatch**

**Region**: Select your closest region

**Namespace**: Create a new namespace

**Namespace Name**: < your initials >-gab-oslo-2015-ns.servicebus.windows.net

![](https://fsh9tq.dm2302.livefilestore.com/y2pp6f1KGC8DXatH4WIHNDy1usnE1d4DQYf0RdebtOPJZmA1xho7ryN6faKHzVElHfyfhs2jd8gnE_cvKc84N6GE093qFCY7lVnUBOWI-PJn75S2ZMIIjmDYmGoShocBJnmdna-5x1IXey1AwlGj6UuYA/ServiceBus%202.PNG?psid=1)

1.4) Click **Next** and set "Default Message Time To Live" to **5 Minutes**. Click **Complete**. 
Note: We use a very low value to make it easier to get to a clean state (empty queue/topic).
[More info about TimeToLive](https://msdn.microsoft.com/en-us/library/microsoft.servicebus.messaging.brokeredmessage.timetolive.aspx)

1.5) Once the Service Bus has been created, click on it and go to the tab **Topics** and click on **order-dispatch**. 

1.6) Click on the tab **Configure** and go down to **Shared access policies** and create a policy called **SendListen** that have the permissions **Send** and **Listen**. Click **Save**. Note the generated **Primary Key**, use it in step 1.7.

1.7) Configure appSettings for connecting to the Topic. In both `GAB.Console.AutomaticInput` and `GAB.Console.ManualInput`, add these keys to `App.config`:

	<appSettings>
	    <add key="ServiceBusAddress" value="sb://<service bus namespace>.servicebus.windows.net/"/>
	    <add key="ServiceBusAccessKey" value="<policy primary key>"/>
	    <add key="ServiceBusAccessKey" value="SendListen"/>
  	</appSettings>

3) Create the class `AzureServiceBusTopicOrderSender` that implements `IOrderSender`.

4) Implement the methods `SendOrders` and `SendOrder` in `AzureServiceBusTopicOrderSender`
	

**Definition of Done**: You can now run these console applications and watch them generate orders and store them onto the **order-dispatch** topic.


Assignment #3 - Use an Azure WebJob that consumes orders via a Service Bus Topic
---------------------------------------------------------------------------------

Use `GAB.OrderConsumerWebJob` to listen for order messages that are put into the topic, and send them into your Azure Table Storage order table.

1) Add the appropriate method parameters in the `ProcessTopicMessage`-method in `Functions.cs`

* Use the following classes as parameters: *ServiceBusTrigger*, *Table* and *TextWriter*.

2) Create the WebApp-container that will host your WebJob.

2.1) Open [https://portal.azure.com](https://portal.azure.com/#) and click on "+ New" in the bottom left corner:

![](https://fch9tq.dm2302.livefilestore.com/y2pT-SNCWS0ufJbF2a4p2Wy9P2mC_wi7eht4U4z7xpk087zlQopBoR85h-sqxIzCS1xLpcZBhZmEldXCUM3-IqpQtvUVdQTybAc9tjlKS8zNAd0bz3OGpDe31ttgpLY03HF-JSKP9hJZCbC5tak3zupqw/WebApp%201.PNG?psid=1) 

2.2) Choose **Web + Mobile->Web App**:

![](https://fmh9tq.dm2302.livefilestore.com/y2pwLFG_imUleTBIx4H6zd20wkfW628IYnIhqfahJ2cgmIZFNr7FnaM67PPcDTeQdX09FIqsvKVeeT5Emf6FfkYP5dVCo13q2Qa1YCE03bCyu1zENExDfoMvagKmL4wXqx4aTCjsU1-l1WuuVLMyo1a5g/WebApp%202.PNG?psid=1)

2.3) Enter the following details:

**URL**: < your initials >-gab-oslo-2015

**App Service Plan**: Select the **Basic** plan in order to test scaling later on. This can be downgraded to **Free** after you're done with these assignments.

**Resource Group**: Select a resource group or create one close to your Service Bus region.

![](https://fcgykg.dm2302.livefilestore.com/y2pP-G3NnVrizCAjYPVRxaRX05pCjGpOhEagh4MtZ8gL02IJs32rZ4pIBbt05psPj1kpE9KvjllUvBXLVB-dq3ucqyndEvpJEvGFWc7MtwfFdbCe3163SETJEQiEgEy_TLuD1H_DNccqUNUCre99w4BhA/WebApp%203.PNG?psid=1)

2.4) Click **Create**.

3) Publish the WebJob to Azure. You will create a publish profile during publishing if this is your first WebApp-publish. Right-click on the `GAB.OrderConsumer.WebJob`-project and select **Publish as Azure WebJob**.

3.1) Click on **Microsoft Azure Web Apps** to create a new publish profile.

3.2) Under **Existing Web Apps**, select the one you just created and click **Ok**.

3.3) Click on **Publish**.

4) Run either `GAB.Console.ManualInput` or `GAB.Console.AutomaticInput`, and monitor on the WebJob *dashboard* (https://< webapp name >.scm.azurewebsites.net/azurejobs/#/jobs) as it picks messages off of the Topic **order-dispatch**. Also, use the console application `GAB.Console.ConsumerOutput` to monitor the throughput as the WebJob saves the order messages to Table Storage. 

**Definition of Done**: You can run either input console application and the orders will be sent to the **order-dispatch** Topic. Once it arrives the WebJob should trigger your static WebJob-method and the order should then be stored into your **orders** table.

* Note that after running a while, the throughput increases slowly. This is the Azure-infrastructure adapting to the volume of traffic. Let it input-application run for about 5 minutes and see how high the throughput goes.

Assignment #4 - Increase performance and throughput
----------------------------------------------------

Identify ways to increase throughput and try to implement them.

* Think of ways to make orders travel faster through the service bus topic.

* Measure throughputs when you make changes in order to assess the different effects each change has to the order throughput

* You could consider discarding the WebJob and instead implement another component that uses the more traditional topic subscription alternative, and use subscription filtering to conditionally fetch messages. Ideally this alternative should also be scalable.

Areas for improvement might include:

* improve order serialization
	* Read [this article](http://www.kcode.me/post/azure-service-bus-brokeredmessage-serialization) for an overview of performance differences of different serialization implementations.
* optimizing the scaling of the WebJob
	* Warning: you will probably need to increase your payment plan in order to scale out the WebJob. 
* switch between storage implementations (Table Storage, DocumentDB etc.)
* synchronous vs asynchronous

