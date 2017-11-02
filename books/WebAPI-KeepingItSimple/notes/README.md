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



