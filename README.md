# GameStore.Api 🎮

> **A practical project to master .NET 10 Minimal APIs.**
> Following the comprehensive guide by Julio Casal to build a modern, scalable backend service.

[![.NET 10](https://img.shields.io/badge/.NET-10-512BD4?logo=dotnet)](https://dotnet.microsoft.com/)
[![C# 13](https://img.shields.io/badge/C%23-13-239120?logo=csharp)](https://learn.microsoft.com/en-us/dotnet/csharp/)

---

## 📌 Project Overview
This repository contains my progress as I learn **ASP.NET Core** and **Minimal APIs**. The goal is to build a fully functional Game Store backend, moving from in-memory data to a persistent database while following industry best practices.

## 🛠 Tech Stack & Learning Goals
* **Framework:** .NET 10 (ASP.NET Core).
* **API Style:** **Minimal APIs** for lightweight and efficient routing.
* **Modern C#:** Practicing C# 13 features like **Primary Constructors** and **Collection Expressions**.
* **Data Handling:** Using **C# Records** as DTOs for clean and immutable data contracts.
* **Clean Code:** Refactoring logic into **Extension Methods** to keep `Program.cs` organized.

## ✨ Current Features (Milestone 1)
* [x] **In-Memory CRUD:** Implemented endpoints to Create, Read, Update, and Delete games.
* [x] **RESTful Responses:** Proper usage of HTTP Status Codes (201 Created, 204 No Content).
* [x] **Route Management:** Using `CreatedAtRoute` to return proper `Location` headers.
* [x] **Integrated Testing:** Local API testing via the `games.http` file in VS Code.

## 📁 Project Structure
```text
GameStore.Api/
├── Dtos/              # Data Transfer Objects
├── Endpoints/         # Endpoint definitions (Refactored logic)
├── Entities/          # Domain Models (Database-ready)
├── games.http         # Local API Testing Suite
└── Program.cs         # Application Entry Point

🚀 Getting Started
Prerequisites
Install the .NET 10 SDK.

How to Run
Clone the repository:

Bash
git clone [https://github.com/TruongThanhIT/GameStore.Api.git](https://github.com/TruongThanhIT/GameStore.Api.git)
Run the application:

Bash
cd GameStore.Api
dotnet run
Test the API:
Open games.http in VS Code and click "Send Request".

🗺 Roadmap
[x] Phase 1: API Foundation & In-Memory Logic (Completed)

[ ] Phase 2: Database Persistence (Next Step)

Integrating Entity Framework Core & SQLite.

Handling Data Migrations.

[ ] Phase 3: Validation & Error Handling

[ ] Phase 4: Security & Authentication

Learning by: [Thanh Truong]

Connect with me on LinkedIn: [Thanh Truong](https://www.linkedin.com/in/truong-thanh-973269135)

## 📚 Credits & Learning Resources
This project is part of my journey to master .NET 10, following the architectural patterns and best practices shared by [Julio Casal](https://www.youtube.com/@juliocasal).
