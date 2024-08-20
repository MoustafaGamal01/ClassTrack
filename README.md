# ClassTrack API
ClassTrack, a powerful attendance tracking API designed to streamline attendance management for students and their groups. This project provides a modern, efficient solution for teachers to record and monitor student attendance through an easy-to-use interface, eliminating the need for manual tracking methods.

## Technologies Used
* ASP.NET Core 8
* Entity Framework Core
* RESTful APIs
* Identity for User Management
* JWT Authentication
* AutoMapper
* MS SQL Server

## Architecture
ClassTrack follows N-tier architecture, which includes:

* **Business Layer**: Implements core business logic.
* **Data Access Layer**: Utilizes the Repository and Unit of Work patterns for efficient data retrieval.
* **Presentation Layer**: Handles the API endpoints and user interactions.
* **Repository Pattern**: Organizes data access logic.
* **Dependency Injection**: Enhances code modularity and testability.

### User Management and Authentication
* User registration and login.
* User Roles (Admin, Teacher).
* Password Reset.
* Manage User Profile Info.
* Session Management.
* Role-Based Access Control.

### Groups Management
* Add, update, and delete groups.

### Students Management
* Add, update, and delete students.
* Search students within a group. 

### Attendance Management
* Add, update, and delete absence excuses.
* Add, update, and delete student attendance with specific excuses.
* Filter and/or search attendance records based on excuses and date. 

### Roles Management
* Add, update, and delete roles.
* Assign roles to users.

## Running the Project
To run ClassTrack API Project:

1. Clone the repository.
2. Add `appsettings.json` File.
3. Configure connection strings in `appsettings.json` for database interaction.
5. Run database migrations to initialize the data structure.
6. Build and run the application.

## Configuration
Ensure that your `appsettings.json` is correctly set up for your environment. Here is an example:

```json
{
    "ConnectionStrings": {
    "RemoteCS": "PutYourConnectionStringHere"
    },
    "Jwt": {
    "Key": "PutUrKeyOfLength32",
    "Issuer": "UrIssure",
    "Audience": "UrAudience",
    "LifeTime": // life time in mins ex: 90
    }
}
```

## Contact
For any inquiries or issues, please contact the repository owner @MoustafaGamal01.
