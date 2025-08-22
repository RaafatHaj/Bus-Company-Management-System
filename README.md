
# Bus Company Management System

A robust ASP.NET Core MVC application designed to streamline bus company operations, including trip scheduling, vehicle assignment, and booking management while preventing scheduling conflicts.

🛠️ Technologies Used:
Backend: ASP.NET Core MVC, C#, Clean Architecture

Database: SQL Server (Stored Procedures for complex logic)

Data Access: Hybrid approach – EF Core (CRUD) + ADO.NET (Stored Procedures)

Patterns: Unit of Work, Repository Pattern

Frontend: HTML, CSS, JavaScript, Bootstrap

Authentication: ASP.NET Core Identity

✨ Key Features:
✔ Smart Trip Scheduling – Ensures no overlapping trips.
✔ Vehicle Conflict Detection – Prevents double-booking of buses.
✔ Booking System – Manages passenger reservations.
✔ Modular & Scalable – Follows Clean Architecture principles.
✔ Optimized Performance – Uses stored procedures for heavy database operations.

🔜 Planned Enhancements:
User-generated routes & stations.

Real-time booking updates.

Advanced reporting dashboard.

# Database Connection Setup

1. Copy `appsettings.Development.example.json` to `appsettings.Development.json`
2. Update the connection string with your local SQL Server credentials:


🗄️ Complete Database Configuration

1. Apply Entity Framework Core Migrations : update-database
3. Run the schema script on your database: TravelCompany_Script.sql
4. Run the data seeding script on your database: Seed_Essential_Data.sql

