Medical Record Dashboard - Assignment
This project is a Full-Stack Medical Record Management System built with Next.js and ASP.NET Core 9. It allows users to register, manage their profiles, and securely handle medical documents.

Technology Stack
Frontend: Next.js 15 (App Router), TypeScript, Tailwind CSS
Backend: ASP.NET Core 9 Web API
Database: MySQL (Entity Framework Core)
API Documentation: Swagger UI
Core Features
User Authentication: Secure signup functionality with MySQL integration.
Profile Management: Dashboard to view user info and upload profile pictures.
Medical Records: - Upload files (Images/PDFs).
Preview/View files directly in a new tab.
Delete records with real-time server and database sync.
How to Run the Project
Follow these steps to set up the project on your local machine.

1. Database Setup
Create a database named hfiles_db in your MySQL server and run the following SQL queries:

CREATE TABLE UserProfiles (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    FullName VARCHAR(255) NOT NULL,
    Email VARCHAR(255) NOT NULL UNIQUE,
    Password VARCHAR(255) NOT NULL,
    Phone VARCHAR(20),
    Gender VARCHAR(20),
    ProfilePicPath VARCHAR(255)
);

CREATE TABLE MedicalRecords (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    FileName VARCHAR(255),
    FileType VARCHAR(50),
    FilePath VARCHAR(255)
);
2. Backend Setup (.NET)
2.1 Navigate to the backend folder:cd backend
2.2 Open appsettings.json and update the DefaultConnection with your MySQL username and password.
2.3 Run the backend server: dotnet run (The API will run at: http://localhost:5139)

3. Frontend Setup (Next.js)
3.1 Open a new terminal and navigate to the frontend folder:cd frontend
3.2 Install dependencies: npm install
3.3 Start the development server: npm run dev (The Dashboard will be available at: http://localhost:3000)
