# JanShopper
Full-stack e-commerce web application using Angular for a responsive front-end, ASP.NET Core for a RESTful API, and SQL Server for database management, featuring user authentication, product CRUD operations, shopping cart functionality, and order tracking.

Process:
<ul>
  <li>SQL Server Management Studio
    <ul>
      <li>Connect to Server
        <ul>
          <li>Server Type: Database Engine</li>
          <li>Server Name: localhost</li>
          <li>Authentication: Windows Authentication</li>
          <li>Connect</li>
        </ul>
      </li>
      <li>Create a new Database
        <ul>
          <li>In the <strong>Object Explorer</strong>, right-click on Databases and select <strong>New Database</strong></li>
          <li>Database Name: JanShopperDb</li>
        </ul>
      </li>
      <li>Configure a User
        <ul>
          <li>Expand the <strong>Security</strong> node.</li>
          <li>Right-click <strong>Logins</strong> and select <strong>New Login</strong></li>
          <li>In the <strong>Login - New</strong> dialog:
            <ul>
              <li>Login Name: Enter a username</li>
              <li>Authentication: Choose SQL Server Authentication and set a password.</li>
            </ul>
          </li>
          <li>In the left panel, go to <strong>User Mapping</strong>, check your database (JanShopperDb), and assign the db_owner role.</li>
        </ul>
      </li>
      <li>Configure the Connection String in appsettings.json
        <ul>
          <li><code>"DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=JanShopperDb;User Id=youUserId;Password=yourPassword;Trusted_Connection=True;"</code></li>
        </ul>
      </li>
    </ul>
  </li>
  <li>Create ASP.NET Core API
    <ul>
      <li>Add Required NuGet Packages
        <ul>
          <li><code>Microsoft.EntityFrameworkCore</code></li>
          <li><code>Microsoft.EntityFrameworkCore.SqlServer</code></li>
          <li><code>Microsoft.EntityFrameworkCore.Tools</code></li>
          <li>For password hashing in UserRepository: Install BCrypt.Net: ~<code>Install-Package BCrypt.Net-Next</code></li>   
        </ul>
      </li>
      <li>Create Models(User, Category, Product) with data annotations.</li>
      <li>Create DTOs to transfer only the required data between the client and server.
        <ul>User
          <li>UserRegistrationDTO for write operations where validation is critical.</li>
          <li>UserProfileDTO for read operations where validation is not needed.</li>
        </ul>
      </li>
      <li>Create Database Context JanShopperDbContext. The DbContext simplifies database interactions, manages entities and their relationships, and ensures data consistency.</li>
      <li>Create Repositories and IRepositories(Interface) for each model with async methods to abstract data access logic, promotes separation of concerns, and makes the code cleaner, more maintainable, and easier to test.
      </li>
      <li>Create Controllers for each model to handle incoming HTTP requests, process the HTTP requests, and return appropriate responses.</li>
      <li>Configure Program.cs to use connection string to SQL Server.</li>
      <li>Create migrations and apply to the database to create tables. 
        <ul>
          <li>~<code>dotnet ef migrations add InitialCreate</code></li>
          <li>~<code>dotnet ef database update</code></li>
        </ul>
      </li>
      <li>Seed database with dummy data.
        <ul>
          <li>Create SeedData.cs.</li>
          <li>Modify Program.cs to call the <code>SeedData.Initialize</code> method during application startup.</li>
          <li>Add migrations and apply to the database to create tables.
            <ul>
              <li>Add Migration: ~<code>dotnet ef migrations add SeedDataMigration</code></li>
              <li>Apply Migration: ~<code>dotnet ef database update</code></li>
            </ul>
          </li>
          <li>Clean and Recreate Database (Development Only)
            <ul>
              <li>~<code>dotnet ef database drop</code></li>
              <li>~<code>dotnet ef migrations remove</code></li>
              <li>~<code>dotnet ef migrations add InitialCreate</code></li>
              <li>~<code>dotnet ef database update</code></li>
            </ul>
          </li>
        </ul>
      </li>
      <li>Test all methods on Swagger.
      </li>
      <li></li>
    </ul>
  </li>
</ul>

Database Schema
<ul>
  <li>User: <code>Id, UserName, Email, Password</code></li>
  <li>Category: <code>Id, Name</code></li>
  <li>Product: <code>Id, Name, Description, Price, Stock, CategoryId</code></li>
  <li>Order: <code>Id, UserId, OrderDate, TotalAmount, Status</code></li>
  <li>OrderItems: <code>Id, OrderId, ProductId, Quantity, Price</code></li>
  <li>Payment: <code>Id, OrderId, PaymentDate, Amount, PaymentMethod</code></li>
</ul>