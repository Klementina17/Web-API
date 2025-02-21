# Web-API
# Web-API Documentation

-This Web API provides CRUD API operations for managing Companies, Contacts,and Countries.

It is built using ASP.NET Core (.NET 8) and Entity Framework Core.

It follows a layered architecture with repository and unit of work patterns

-Technologies Used

.NET 8 (LTS)

ASP.NET Core 

Entity Framework Core

Swagger for API documentation

Logging and error handling

Docker (Windows Containers)


-Prerequisites

Before running this project, ensure you have:

•	.NET 8 SDK installed

•	SQL Server installed and running

-Installation & Setup

1.Clone the Repository

 git clone https://github.com/your-repo/WebApi.git

 cd WebApi

2.Install Dependencies

  dotnet restore

3.Configure database

Update appsettings.json with your SQL Server connection string:

 "ConnectionStrings": {
   "DefaultConnection": "Server=localhost\\MSSQLSERVER2022;Database=YourDatabase;Trusted_Connection=True;TrustServerCertificate=True"
 }

4.Apply Migrations & Seed Database

  dotnet ef migrations add InitialCreate

  dotnet ef database update

5.Run the API

  dotnet run --project WebApiProject

6.Open SwaggerUI 

  Navigate to https://localhost:5001/swagger in your browser

-API ENDPOINTS

  * CONTACT ENDPOINTS
  * 
    Method       Endpoint                                Description
  * 
    GET         /api/contact                           Retrieves a list of all contacts.


    GET         /api/contact/{id}                      Retrieves a specific contact by ID.


    POST        /api/contact                           Create a new contact


    PUT         /api/contact/{id}                      Updates an existing contact.


    DELETE      /api/contact/{id}                      Deletes a contact by ID.


    GET         /api/contatc/withcompanyandcountry     Retrieves contacts along with their associated company and country details.
   
    GET         /api/contact/FilterContacts            Retrieves contacts based on applied filters.


    * COMPANY ENDPOINTS
    
    * 
    Method       Endpoint                                Description


    GET         /api/company                           Retrieves a list of all companies.


    GET         /api/company/{id}                      Retrieves a specific company by ID.


    POST        /api/company                           Create a new company


    PUT         /api/company/{id}                      Updates an existing company.

    DELETE      /api/company/{id}                      Deletes a company by ID.


* COUNTRY ENDPOINTS

* 
    Method       Endpoint                                Description


    GET         /api/country                           Retrieves a list of all countries.


    GET         /api/country/{id}                      Retrieves a specific country by ID.


    POST        /api/country                           Create a new country.


    PUT         /api/country/{id}                      Updates an existing country.

    DELETE      /api/country/{id}                      Deletes a country by ID.
  

-Testing the API

 Use the Swagger UI to test API endpoints.

 Run the API.

   dotnet run

 Open your browser and go to:

   https://localhost:5001/swagger

-Logging with Serilog

 This project uses Serilog for logging.

 Logs are stored in logs/api_log.txt

 To enable loggind you should write this in your Program.cs:

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File("logs/api_log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();
    
-Error Handling

This API includes a global error-handling middleware to handle exceptions.All endpoints return appropriate HTTP status codes.
   
    Validation errors return 400 Bad Request.
    
    Not found errors return 404 Not Found.
    
    Internal errors return 500 Internal Server Error.

Notes

The API follows the Repository and Unit of Work patterns for data access.

Docker support is enabled for Windows Containers.