# API_Wrapper-SWAPI

The SWAPI Wrapper API provides a simple interface to retrieve information about Star Wars films, starships, and characters from the [Star Wars API (SWAPI)](https://swapi.dev/).

## Table of Contents

- [Features](#features)
- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [Installation](#installation)
  - [Run the Application](#run-the-application)
- [Architecture](#architecture)
- [Design Decisions](#design-decisions)
- [Usage](#usage)
  - [Endpoints](#endpoints)
- [Swagger Documentation](#swagger-documentation)
- [Contributing](#contributing)
- [License](#license)

## Features

- Retrieve a list of Star Wars films.
- Get details about starships featured in a specific film.
- Get details about characters featured in a specific film.
- Caching mechanism to improve API response time.

## Getting Started

### Prerequisites

- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/) with the latest updates.
- [.NET 6.0 SDK](https://dotnet.microsoft.com/download/dotnet/6.0) must be installed.

### Installation

1. Clone the repository:

    ```bash
    git clone https://github.com/your-username/swapi-wrapper-api.git
    ```

2. Open the solution in Visual Studio.

3. Build the solution.

### Run the Application

4. Run the application from Visual Studio.

## Architecture

The SWAPI Wrapper API is built using ASP.NET Core Web API, leveraging the features of .NET 6.0. The architecture follows a standard MVC (Model-View-Controller) pattern. It utilizes asynchronous programming for handling API requests efficiently.

## Design Decisions

- **Caching**: To enhance performance, a caching mechanism is implemented using `MemoryCache` from the `Microsoft.Extensions.Caching.Memory` package. Cached data is stored for 5 minutes to ensure timely updates from the SWAPI.

- **HttpClient Usage**: The application uses `HttpClient` for making HTTP requests to the SWAPI. The `HttpClient` is registered as a service to take advantage of dependency injection, making it easier to manage the lifecycle of the client.

## Usage

### Endpoints

- **Get Films**: Retrieve a list of Star Wars films.

    ```http
    GET /api/swapi/films
    ```

- **Get Starships for a Film**: Retrieve details about starships featured in a specific film.

    ```http
    GET /api/swapi/starships/{filmId}
    ```

- **Get Characters for a Film**: Retrieve details about characters featured in a specific film.

    ```http
    GET /api/swapi/characters/{filmId}
    ```

## Swagger Documentation

Explore and test the API using Swagger documentation. In development mode, Swagger UI is available at:
[https://localhost:44373/swagger/index.html]

## Contributing

Contributions are welcome! Please follow the [contribution guidelines](CONTRIBUTING.md).

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

