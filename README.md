# **Banking Control Panel - Setup and Running Guide**

## **Postman Collection**

To simplify API testing, we've provided a Postman collection. You can download it and import it into your Postman application.

[Download Postman Collection](./BankingControlPanel.postman_collection.json)

## **Introduction**

Welcome to the **Banking Control Panel** project! This guide provides instructions to set up the project in your local environment, run the application, and understand its functionality. Please follow the steps carefully to ensure everything works correctly.

**Seed Data**
1. I added a seed admin user with these credintials
```
  Email: "admin@gmail.com",
  password: "Admin@123",
  role: "Admin",
```
2. I also added a clients for testing
## **Prerequisites**

Before you begin, ensure you have the following software installed on your machine:

- **.NET 6 SDK**: [Download .NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- **SQL Server**: Ensure you have a running instance of SQL Server (you can use SQL Server Express).
- **Visual Studio 2022** or **Visual Studio Code**: [Download Visual Studio](https://visualstudio.microsoft.com/)
- **Git**: [Download Git](https://git-scm.com/)

## **Step 1: Clone the Repository**

Start by cloning the repository to your local machine:

```bash
git clone https://github.com/yourusername/BankingControlPanel.git
```

Navigate to the project directory:
```
cd BankingControlPanel
```

## **Step 2: Configure the Database Connection**

The project uses SQL Server as its database. You need to update the connection string in the **appsettings.json** file to point to your local SQL Server instance.
1. Open appsettings.json located in the root of the project.
2. Locate the ConnectionStrings section.
3. Update the DefaultConnection with your SQL Server credentials. Example:
```
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=BankingControlPanel;Trusted_Connection=True;MultipleActiveResultSets=true"
},
```

If you're using SQL Server with a username and password, your connection string might look like this:
```
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=BankingControlPanel;User Id=yourusername;Password=yourpassword;MultipleActiveResultSets=true"
},
```

## **Step 3: Apply Migrations and Seed the Database**

To set up the database with the required schema and seed data, follow these steps:

1. Open the terminal or command prompt.
2. Navigate to the project directory.
3. Run the following commands to apply migrations and update the database:
```
dotnet ef migrations add InitialCreate
dotnet ef database update
```
This will create the database schema and insert initial seed data, including roles and a default admin user.

## **Step 4: Configure JWT Settings**

The application uses JWT (JSON Web Token) for authentication. You need to update the JWT settings in the appsettings.json file.

1. Locate the Jwt section in appsettings.json.
2. Replace the Key with a strong secret key, Issuer, and ExpireDays as needed:
```
"Jwt": {
  "Key": "YourSuperSecretKeyHere",
  "Issuer": "YourIssuerHere",
  "ExpireDays": 7
}
```

## **Step 5: Running the Application**

1. When run the project you have to login first with user have **Admin** role to be able to use the Clients API's, otherwise you can login as normal user with role **User** that dont have privilege to access clients endpoints.

2. you can register new users without having the **Admin role** or even before you login to the system 

## **Testing with Swagger**

The project comes with Swagger, a tool for API documentation and testing. To test the API endpoints:

Open your browser and navigate to https://localhost:{yourPort}/swagger.
You'll see the API documentation with available endpoints.

You can test the endpoints directly from the Swagger UI.
