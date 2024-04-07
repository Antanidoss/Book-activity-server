# **Books activity**

#### This project has been designed to teach and apply skills.
#### The project provides a kind of social network for book lovers.
# The main functionality of the project:
- The sequel contains a large number of books. You can make them active and then indicate how much you read and what progress you have.
- You can leave reviews about the book.
- You can subscribe to a friend of a user and see what he reads, what reviews he leaves about the book.
- Can take notes on and quotes.
- You can give your assessment of the review or quote from.

# Technologies implemented:
- .NET 6
- ASP.NET 6
- Entity Framework Core 6
- Identity
- AutoMapper
- MediatR
- .NET DevPack
- .NET DevPack.Identity
- FluentValidator
- Swagger
- SignalR
- MongoDB

# Architecture:
- Full architecture with responsibility separation concerns, SOLID and Clean Code
- Domain Driven Design (Layers and Domain Model Pattern)
- Domain Events
- Domain Notification
- Domain Validations
- CQRS (Imediate Consistency)
- Event Sourcing
- Unit of Work
- Repository

# How to start a project locally

#### 1. Clone the repository
---
#### 2. Unit git submodule
### `git submodule intit`
---
#### 3. At the root directory, restore required packages by running:
### `dotnet restore`
---
#### 4. Change connection strings in appsettings.Development.json
---
#### 5. Next, build the solution by running:
### `dotnet build`
---
#### 6. Once the front end has started, within the \src\BookActivity.Api directory, launch the back end by running:
### `dotnet run`
---
#### 7. Copy the address and go to the site


# How to start a project with docker

#### 1. Clone the repository
---
#### 2. Run docker-compose file
### `docker-compose up`
---
#### 3. If everything went well, then go to the website at http://localhost:5001/books

