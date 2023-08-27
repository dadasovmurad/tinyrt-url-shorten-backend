# Tinyrt ShortLink API README

This repository contains a .NET Core Web API project that handles URL shortening and encryption. It utilizes Entity Framework (EF) Core, Clean Architecture, and Aspect-Oriented Programming with Autofac.

## Project Overview

The ShortLink API is designed to receive requests from the frontend and perform URL shortening and encryption tasks. When a request is made to the API with a long URL, it generates a shorter version of the link and can also encrypt the links for added security. In addition, you can find the interface of this API in my repository.

### Features

- URL Shortening: Converts long URLs into shorter, more manageable links.
- Link Encryption: Provides the option to encrypt the generated short links for enhanced security.

## Tools and Technologies

The following tools and technologies are utilized in this project:

- .NET Core: The foundation for building cross-platform applications.
- Entity Framework (EF) Core: An object-relational mapper for database interactions.
- Clean Architecture: A software architectural pattern that separates concerns into layers.
- Aspect-Oriented Programming: Achieved using Autofac, it allows modular and cross-cutting concerns.
- Autofac: A popular dependency injection framework for .NET.

## Getting Started

Follow these steps to set up the project locally:

1. **Clone the repository:**

   ```bash
   git clone https://github.com/dadasovmurad/tinyrt-url-shorten-backend.git
   cd tinyrt-url-shorten-backend
