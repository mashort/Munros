# Munros API
This is the repository for a REST API that can be consumed to search and sort munro data.

## Functional Goals
High-level features.

**Searching**
- By hill category
  - Uncategorised hills are excluded from results
- For hills of a minimum specified height in meters
- For hills of a maximum specified height in meters
- Can limit the total number of results returned
- Results returned in JSON format as a collection of items. Each (hill) item comprises of:
  - Name
  - Height (in meters)
  - Category
  - Grid reference

**Sorting**
- By hill height in meters in ascending or descending order
- By hill name alphabetically in ascending or descending order

**General**
- Queries may include any combination of the above search/sort criteria and none are mandatory
- Suitable error responses for invalid queries

## Technology
The solution is built with the following technologies:
- Microsoft .NET Core 3.1 & ASP.NET Core
- Microsoft Entity Framework Core 3.1
- xUnit.NET testing framework

## Architecture
The solution architecture is based on Steve "ardalis" Smith's [Clean Architecture](https://github.com/ardalis/CleanArchitecture) template. This architecture is the latest in a series of names for loosely-coupled, dependency-injected architectures - see [Robert C. Martin's Clean Architecture](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html).

The solution comprises of the following projects:
- Munros.Core - contains the data model and any abstractions
- Munros.Infrastructure - contains external dependencies (e.g. data access implementations)
- Munros.API - the entry point of the application
- Munros.Tests - contains the unit/functional tests

## Getting Started
Guide for users to get the solution up and running on their own system, including any any prerequisite software / dependencies...

### Prerequisites
TODO

### Running the API
TODO

### Running the Tests
TODO