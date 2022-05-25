To implement generic repository pattern, create two projects - one is an ASP.NET Core Web Application and another is a class library project, which are Web and Data respectively in the solution. The class library is named as Data project, which has data access logic with a generic repository, entities, and context, so we install Entity Framework Core in this project.
 
There is an unsupported issue of EF Core 1.0.0-preview2-final with "NETStandard.Library": "1.6.0". Thus, we have changed the target framework to netstandard1.6 > netcoreapp1.0. We modify the project.json file of GR.Data project to implement Entity Framework Core in this class library project. Thus, the code snippet, mentioned below for the project.json file after modification.

Purpose

Easy Repository For EF Core provides implementation generic repository pattern on Entity Framework Core

What is Generic Repository Pattern

The generic repository pattern implements in a separate class library project. ... The repository pattern is intended to create an Abstraction layer between the Data Access layer and Business Logic layer of an Application. It is a data access pattern that prompts a more loosely coupled approach to data access.
