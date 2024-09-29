# RestApiApp

## Description
It is a .NET C# console application that interacts with .NET REST
API services on the server. Users enter their username and
password through the console. After successful authentication, the user receives their profile details or a corresponding error message.

## Table of Contents
- [Installation](#installation)
- [Usage](#usage)
- [Solution structure](#Solution structure)

## Installation
1. Clone the repo from `https://github.com/TaniaY/RestApiApp
2. Install dependencies

## Usage
Use the following command to apply migrations to your local database:
`dotnet ef database update

In project `ConsoleApplication` class `Program` change variable `ApiUrl`
`http://localhost` Refers to the local machine, meaning this URL is pointing to a service that runs locally on your computer.
`5027` This is the port number on which the API is running. APIs can run on various ports, and here should be YOUR port.

Run `TestAPI` in background mode (you can use Terminal - 1) `cd TestAPI` ; 2) `dotnet run`).
Run `ConsoleApplication`.

## Solution structure
.NET Web API project `TestAPI` 
structure:
	Folder `DBFiles` contains files related to database:
		Folder `Entities` contains database table models;
		Folder `Migrations` contains database migrations;
		Class `AppDBContext`.
	Folder `DTOS` has data transfer objects for sending data over a network.
	Class `Program` with authentication, authorization, and some custom configurations for handling database connections, middleware, and identity management.
	Folder `Controllers`:
		`AccountController` - ASP.NET Core Web API controller which handles user account operations like registration and login using ASP.NET Core Identity.
	`appsettings.json` updated by a connection string that specifies how the application should connect to a database.

Project `ConsoleApplication`- app for users.
structure:
	`UserService`class is responsible for handling user-related API requests, such as logging in and registering users.
	Folder `Contracts` has models for requests and responses.
	Class `UserInterface` is responsible for all interactions with the user, including prompting for input and displaying messages.
	Class `Program` main class of this application where execution begins. It combines all methods into a common scenario of interaction with the user. 

Library `RestApiLibrary` contains things that can be reused in other APIs of your project.
structure:
		`ServiceConfiguration` contain setting up services and middleware for dependency injection, database context, and API documentation.
		
		



Technologies Used
	Programming language - C#;
	ASP.NET Core: modern web framework for building web applications and APIs. It supports dependency injection, middleware, routing, and more.
	Entity Framework Core: Object-Relational Mapping (ORM) framework that simplifies database interactions and allow to work with database objects as C# objects.
	HTTP Client: used in the console application to make asynchronous HTTP requests to the REST API for login and registration.
Design principles	
	Separation of Concerns: code organized into different classes and methods, each handling specific tasks.
	SOLID: 
		Single Responsibility Principle (SRP): each class and method has a single responsibility.
		Dependency Inversion Principle: Higher-level modules are not dependent on lower-level modules.
	Error Handling
	OOP:
		Encapsulation: internal workings of classes are hidden from other classes, exposing only what is necessary through public methods and properties.