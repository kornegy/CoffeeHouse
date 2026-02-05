CoffeeHouse is a web-based application designed for the needs of a modern cafe. 
The goal of the system is to digitalize the process of presenting the menu, managing the assortment, 
and interacting with customers online. 
It solves the problem of static, hard-to-update physical menus while providing a platform for online ordering.

🎯 Project Objectives
Online Availability: Provide 24/7 public access to an up-to-date menu.
Digital Orders: Enable registered customers to create orders through a digital shopping cart.
Easy Administration: Provide tools for the owner to edit, add, and delete products without modifying source code.
Security: Implement role-based access control for Hosts, Users, and Administrators.

🏗 Architecture
The system is designed as a layered software architecture that strictly follows the MVC (Model-View-Controller) design pattern. 
This ensures a clean separation between the user interface, application logic, and data.

Logical Layers 
Presentation Layer (View): Responsible for user interaction. It uses Razor Views (.cshtml), Bootstrap (CSS), and JavaScript to generate HTML for the client's browser.
Application Layer (Controller): The "brain" of the system. Controllers (Home, Admin, Auth, Cart) manage the application flow, handle HTTP requests, and execute business logic like password verification and cart calculations.
Data Layer (Model): Ensures data persistence. It uses data models (CoffeeItem, UserRecord) and handles reading/writing to files using System.Text.Json.

Physical Architecture 
The project utilizes a Client-Server architecture.
Storage: Instead of a traditional SQL database, the project uses structured JSON files (menu.json, users.json) stored in the file system for simplicity and portability.

👥 User Roles (Actors)
The system distinguishes between three main groups of users:
Host (Unregistered Visitor): Can browse the interactive menu, view prices, and see contact information.
User (Registered Customer): Has a personal account and the authorization to add items to the cart and submit orders.
Administrator (Manager): Has full access to the administrative section to manage the product database (CRUD operations) and overview users.

🚀 Key Functionalities
Interactive Menu: A dynamically generated list of products including names, descriptions, prices, and photos.
Shopping Cart: Session-based functionality for adding products and calculating the total price (available to logged-in users).
Secure Authentication: A robust system for registration and login with role differentiation.

💻 Tech Stack
Framework: ASP.NET Core MVC.
Frontend: HTML5, CSS3 (Bootstrap), JavaScript, Razor Views.
Data Persistence: JSON-based file storage (System.IO, System.Text.Json).
Development Environment: JetBrains Rider / Visual Studio.
