# JanShopper
Full-stack e-commerce web application using Angular for a responsive front-end, ASP.NET Core for a RESTful API, and SQL Server for database management, featuring user authentication, product CRUD operations, shopping cart functionality, and order tracking.

Process:
<ul>
  <li>Create ASP.NET Core API
    <ul>
      <li>Add Required NuGet Packages
        <ul>
          <li><code>Microsoft.EntityFrameworkCore</code></li>
          <li><code>Microsoft.EntityFrameworkCore.SqlServer</code></li>
          <li><code>Microsoft.EntityFrameworkCore.Tools</code></li>
        </ul>
      </li>
      <li>Create Models(User) with data annotations.</li>
      <li>Create DTOs to transfer only the required data between the client and server.
        <ul>User
          <li>UserRegistrationDTO for write operations where validation is critical.</li>
          <li>UserProfileDTO for read operations where validation is not needed.</li>
        </ul>
      </li>
      <li>Create Database Context JanShopperDbContext. The DbContext simplifies database interactions, manages entities and their relationships, and ensures data consistency.</li>
      <li></li>
    </ul>
  <li>
</ul>