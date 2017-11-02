#ASP.NET Web API - Keeping It Simple

Showing an attempt to split out the controller layer to reduce its complexity and improve quality and maintainability of resulting code

## Introduction

Problem with MVC: controllers entrusted with too much:
1. serving as the entry point into the application.
2. managing the interaction of the other two layers
3. containing the business logic for the domain
4. handling persistence
5. handling security
6. etc.

Web API allows for a more granular division of controller activities into reusable and independent business logic and repository layers, easily shared across applications.

### Design Goals

Domain/business logic is one of the major components within an application. 

Having this component in a separate layer makes it more easily shared between applications - if it has other code with complementary functions like security, audit, and persistence of data, it would be less likely to be shared.

First goal is to design a an independent business (Services) layer, independent of the rest of the controller code.

The repository/persistence layer deals with permanent storage and retrieval of data. 
  - problems:
    - results in repository code duplication
    - makes a change in the persistence layer a more resource-intensive and error-prone operation

## Setup Model, Repository, and Service Layers

![layers](https://www.codeproject.com/KB/aspnet/1157685/NorthwindWebAPI2_11_LayeredArchitecture.PNG)

Each layer references the layers immediately below it. 

The Models layer is a common dependency for all of the layers.

**Repository Layer** - responsible for all database operations (no other layer should have any database code)

**Services Layer** - encapsulates the business logic
  - Controllers (in MVC project) react to evens initiated by the UI of the application
  - Controllers ensure validity of request and validate parameters
  - Controllers pass the request to the appropriate Service for processing
   
  - Example: 
    - OrderController receives Get request
    - OrderController makes a call to the relevant method of the OrderService
    - OrderService assembles the Order by making calls to OrderRepository and OrderDetailRepository
    - OrderService may also contact the CustomerRepository to retrieve the Customer for the Order and/or EmployeeRepository for the Employee handling the order.
    - It returns the Order object back to the OrderController - which uses it to populate a view or respond to a web service request.

### Add references:

WebAPI - models, services
Services - models, repository
Repository - models

### Create components for the Model layer

**use empty constructor in each class to aid serialization**

Customer.cs
Employee.cs
Order.cs
Order_Detail.cs
Product.cs


