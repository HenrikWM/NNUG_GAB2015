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


Assignment #1 - Create orders

Firstly, you will need to create a component that will be responsible for handling data entry of orders and producing orders. Output to the console a JSON-serialized order after it has been created.

You may also want to create a domain library for the POCO-objects that will contain domain objects.

1) Create an application (“Order Producer”) that creates orders from console input 

Each order should contain the following details: 

Customer (No, Name)
OrderItem (No, Name)
Created (DateTime)

For simplicity’s sake we’re going to limit each order with a single order item.

You will find that the domain object are already created for you.

* Components: Order Producer, Domain

Definition of Done: You now have a component that can read order input from console. It creates the order and outputs the order to the console as a serialized JSON string.

Assignment #2 - Consume incoming orders

You now have a component that can produce orders. Next you will need to add a component that takes in orders and stores them to a storage location. For this assignment you can re-use the console application but replace reading order arguments with a call to NBuilder.

1) Replace manual console input with auto-generation (NBuilder) in order to simulate larger volumes of incoming orders.

2) Create a component (“Order Consumer”) that takes in orders and stores them. 
Examples of storage location (use one): Azure Sql, Azure Table Storage, Azure DocumentDB

3) Implement the Producer and Consumer as Azure WebJobs

4) Measure the throughput you have by dividing total number of orders by the time it takes to store them. You need to come up with a "orders per minute"-ratio, e.g. "349/min". You can round up to nearest whole integer. Output this ratio to the console.

Example on using a stop-watch to measure elapsed time:

<code>

* Components: Order Producer, Domain, Order Consumer, Azure Storage

Assignment #3 - Create orders, send them to the Service Bus, and consume orders from the bus and put into storage

* The Producer and Consumer should use the Service bus topic "order dispatch".
* The Producer puts orders onto the topic and the consumer subscribes for incoming orders 

* Note the throughput

* Components: Order Producer, Domain, Order Consumer, Azure Storage, Azure Service Bus

Assignment #4 - Add volume

* For creating orders, replace auto-generation with reading instagram hash tags. Start with #cats and then support #dogs and #birds 

Username is Customer Name
UserId = Customer No.
Item No. = <hashtag>
Item Name = <hashtag>

* Any orders that fails to be stored (at any point from creation to storing) must be stored to an alternative location for manual entering. Either way, the Producer should get an order reference ID to give to a customer

* Note the throughput

* Components: Order Producer, Order Consumer, Azure Storage, Azure Service Bus

Assignment #5 - Increase performance and throughput

* Think of ways to make orders travel faster through the service bus.

Areas for improvement might be: serialization, optimizing the scaling of the Producer and Consumer workers, switch between storage infrastructure, synchronous vs asynchronous

