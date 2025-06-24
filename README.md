# PROG7311
PROG7311 - Semester 1 Year 3 - ST10378305

Name: Dean James Greeff

Student Number: ST10378305

Module: PROG7311

Assignment Part 2

GitHub Link: https://github.com/Deanjamesg/PROG7311

Prototype: Agri Energy Connect Web Application

## 1. User Types & Functionality

Farmer:
- Can log in and out.
- Can add new products they produce.
- Can view a list of their own products.
- Can view their account profile details.

Employee:
- Can log in and out.
- Can view a list of all products from all farmers.
- Can filter the products list by date range, category, and farmer name.
- Can register new "Farmer" user accounts.
- Can view their account profile details.

## 2. Employee Account Details for Testing:

Employee:
- Email: d@j.com
- Password: zxcvzxcv

## 3. Farmer Account Details for Testing:

Farmer:
- Email: simon@kurt.com
- Password: zxcvzxcv

## 4. Services Used: 

- ASP.NET Core MVC: The web framework used to build the application.
- Entity Framework Core: ORM used for database interactions (with SQLite).
- ASP.NET Core Identity: For user authentication and authorization. 
- UserManager<User>: Manages user-related operations like creating users, finding users by ID or name, managing user roles, and retrieving user information.
- SignInManager<User>: Handles user sign-in and sign-out processes, including password verification.
- RoleManager<IdentityRole> (Implied): Used to define and manage user roles ("Farmer", "Employee").
- ProductService (Custom Service): Contains the business logic for product-related operations, such as creating products, fetching products by user ID, retrieving all product categories, and filtering products based on various requirements.
- SQLite: The local database used to store application data.

## 5. How to Run Prototype:

Step 1: Open up the .ReadMe and go to the GitHub link: https://github.com/Deanjamesg/PROG7311.

Step 2: Create a fork of the repository on your local device / computer.

Step 3: Open the Visual Studio application, and then click "Open Project" and locate the file of the local fork of the repository on your computer.

Step 4: Open AECPrototype folder, then select the AECPrototype.sln and click "Open".

Step 5: Once the project has opened on your Visual Studio application, you can click "Run Application."

## 6. References:

1. Microsoft, 2025. UserManager<TUser> Class (Microsoft.AspNetCore.Identity). [online] Available at: https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.identity.usermanager-1?view=aspnetcore-9.0 [Accessed 12 May 2025].

2. Microsoft, 2025. RoleManager<TRole> Class (Microsoft.AspNetCore.Identity). [online] Available at: https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.identity.rolemanager-1?view=aspnetcore-9.0 [Accessed 12 May 2025].

3. Microsoft, 2025. SignInManager<TUser> Class (Microsoft.AspNetCore.Identity). [online] Available at: https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.identity.signinmanager-1?view=aspnetcore-9.0 [Accessed 12 May 2025].

4. Microsoft, 2025. IdentityUser Class (Microsoft.AspNetCore.Identity). [online] Available at: https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.identity.identityuser?view=aspnetcore-9.0 [Accessed 12 May 2025].
