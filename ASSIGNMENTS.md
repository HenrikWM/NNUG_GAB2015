NNUG - Global Azure Bootcamp 2015
=================================

This is a collection of workshop assignments that was given during NNUG's Global Azure Bootcamp 2015 @ Bouvet on 25. April 2015. 


About

These assignments were given during a Global Azure Bootcamp 2015 event hosted by NNUG and held at Bouvet in Oslo, Norway on 25th of April 2015. The assignments use Microsoft Azure and is intended for developers with intermediate to advanced skills. 

These assignments are meant to give the developer an introduction to creating an application that can handle high-volume traffic by using the power of the Cloud and a Service Bus. The assignments will navigate the developer through creating producers of work items, placing them onto a queue, and consuming the work items with consumers that stores them.

For more information about using queues, producers and consumers, see http://www.ni.com/white-paper/3023/en/ and https://aws.amazon.com/blogs/aws/sqs_super_queue/ for an explanation of the Producer-Consumer design pattern.

Keywords

Azure, Cloud, Scalability, Consistency, WebJobs, Azure SQL, Azure Table Storage, Azure DocumentDB, Azure Service Bus with topics and subscriptions, Producer/Consumer

Documentation

http://azure.microsoft.com/en-us/documentation/articles/websites-dotnet-webjobs-sdk-service-bus/

http://www.troyhunt.com/2015/01/azure-webjobs-are-awesome-and-you.html

http://channel9.msdn.com/Tags/azurefridaywebjobs

https://github.com/Azure/azure-webjobs-sdk-samples/tree/master/BasicSamples

https://resources.azure.com/

Prerequisites

Latest Azure SDK (installed and ready)
Visual Studio 2013 Update 4
An active Azure subscription
Access to the Azure portal

Background:

You work as an independent consultant and have been contracted by Contoso Inc. to help them implement their new ecommerce website. Contoso are in the pet food industry and wants to tap into social media to gain business by selling more pet food on their website.

Their old website didn't handle high-volume traffic and they lost business over popular holidays when orders coming in were dropped because their back-end couldn't handle the load. Therefore it is a critical requirement that the new solution will take care of every single order that comes in. 

They have staff to keep track of orders so manually entering is an option should the back-end systems go down, but the orders must not be lost and the customers purchasing must get feedback of the order placement!

To start things off they would like you to create a proof of concept solution before going into a full blown project, so the tasks are small in scope and requirements for UX and GUI are minimal. At this stage they are mainly concerned with creating a skeleton-solution that delivers on their primary requirements. You main goals are therefore to maximize throughput of order placements and ensure consistent data throughout the order process.

Assignment #xx - Consume orders and store in Azure

You have already created two components that can produce orders. One handles manual input from the console (GAB.Console.ManualInput), the other creates a high volume of orders in order to later simulate high volume traffic (GAB.Console.AutomaticInput).

You have also started to implement the "Order Consumer"-component (GAB.Services.OrderConsumer) and "Order Producer"-component (GAB.Services.OrderProducer).

Next you will alter the "GAB.Console.AutomaticInput"-application so that is uses Azure to store orders.

1) Replace the class "InMemoryOrderStorage" with a new class called "AzureOrderStorage".

2) Implement the method "Store" in "AzureOrderStorage"

Examples of storage location (use one): Azure Sql, Azure Table Storage, Azure DocumentDB

Definition of Done: You can now run the "GAB.Console.AutomaticInput"-application and watch it generate orders and store them into an Azure storage location.
Note that the throughput should decrease. You can use async in your storage-code in order to mitigate the decreased throughput.

Assignment #xx - Create two Azure WebJobs that produces and consumes orders via the Service Bus

* The Producer and Consumer should use the Service Bus topic "order dispatch".
* The Producer puts orders onto the topic and the consumer subscribes to the topic for incoming orders 

Produce orders inside a WebJob by moving the produce-code from the "GAB.Console.AutomaticInput"-project to the "GAB.OrderProducerWebJob"-project.

1) Move the producer-code: 'randomOrdersProducer.Produce(OrdersPerSecond);' to the Producer WebJob's Program.cs.

2) Wrap the moved code in a loop that calls the Produce-method once per second.

3) Implement code that sends the produced orders onto a Service Bus topic called "order dispatch".

4) Next, consume orders inside a WebJob by moving the consume-code from the "GAB.Console.AutomaticInput"-project to the "GAB.OrderConsumerWebJob"-project.

Include the code that calculates throughput as well.

Assignment #xx - Increase performance and throughput

* Think of ways to make orders travel faster through the service bus.

* Measure throughputs when you make changes in order to assess the effect the changes have

Areas for improvement might be: serialization, optimizing the scaling of the Producer and Consumer workers, switch between storage infrastructure, synchronous vs asynchronous

