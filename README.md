# GameStore.Api 🎮

> **A practical project to master .NET 10 Minimal APIs and build a modern React + Vite frontend.**
> Following the comprehensive guide by Julio Casal and Mosh to build a modern, scalable backend service and a polished web UI.

[![.NET 10](https://img.shields.io/badge/.NET-10-512BD4?logo=dotnet)](https://dotnet.microsoft.com/)
[![C# 13](https://img.shields.io/badge/C%23-13-239120?logo=csharp)](https://learn.microsoft.com/en-us/dotnet/csharp/)

---

## 📌 Project Overview
This repository contains my progress learning **ASP.NET Core Minimal APIs** while also building a dedicated **React + Vite** frontend to consume the backend. The goal is to deliver a fully functional Game Store with a clean API, persistent data storage, and a modern user interface.

## 🛠 Tech Stack & Learning Goals
* **Framework:** .NET 10 (ASP.NET Core) for the backend API.
* **Frontend:** React + Vite for the web interface.
* **API Style:** **Minimal APIs** for lightweight and efficient routing.
* **Modern C#:** Practicing C# 13 features like **Primary Constructors** and **Collection Expressions**.
* **Data Handling:** Using **C# Records** as DTOs for clean and immutable data contracts.
* **Clean Code:** Refactoring logic into **Extension Methods** to keep `Program.cs` organized.
* **Frontend Integration:** Building reusable UI components and connecting them to the API for a complete experience.

## ✨ Current Features (Milestone 1)
* [x] **In-Memory CRUD:** Implemented endpoints to Create, Read, Update, and Delete games.
* [x] **RESTful Responses:** Proper usage of HTTP Status Codes (201 Created, 204 No Content).
* [x] **Route Management:** Using `CreatedAtRoute` to return proper `Location` headers.
* [x] **Integrated Testing:** Local API testing via the `games.http` file in VS Code.
* [x] **Frontend Scaffold:** Added initial `game-store-react` React + Vite project structure.

## 📁 Project Structure
```text
GameStore/
├── GameStore.Api/      # .NET 10 Minimal API backend
│   ├── Dtos/
│   ├── Endpoints/
│   ├── Entities/
│   ├── games.http
│   └── Program.cs
└── game-store-react/   # React + Vite frontend
```

## 🚀 Getting Started

### Prerequisites
* .NET 10 SDK
* Node.js 20+ / npm

### Run the backend
```bash
git clone https://github.com/TruongThanhIT/GameStore.Api.git
cd GameStore.Api
dotnet run
```

### Run the frontend
```bash
cd ../game-store-react
npm install
npm run dev
```

Test the API:
Open games.http in VS Code and click "Send Request".

🗺 Roadmap
* [x] Phase 1: API Foundation & In-Memory Logic (Completed)
* [x] Phase 2: Database Persistence (Next Step)
  * Integrating Entity Framework Core & SQLite.
  * Handling Data Migrations.
* [x] Phase 3: Validation & Error Handling
* [ ] Phase 4: Security & Authentication
* [ ] Phase 5: GameStore Web UI (React + Vite)
  * [x] Stage 1: Project Initialization & Tooling
  * [] Stage 2: Static UI Components
  * [] Stage 3: API Integration
  * [] Stage 4: Full CRUD Implementation


Learning by: [Thanh Truong]

Connect with me on LinkedIn: [Thanh Truong](https://www.linkedin.com/in/truong-thanh-973269135)

## 📚 Credits & Learning Resources
This project is part of my journey to master .NET 10, following the architectural patterns and best practices shared by [Julio Casal](https://www.youtube.com/@juliocasal).
Additional frontend learning resources:
* React basics and modern component patterns from the React crash course: https://www.youtube.com/watch?v=SqcY0GlETPk
