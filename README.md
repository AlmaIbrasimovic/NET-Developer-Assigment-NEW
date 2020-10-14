# NET-Developer-Assigment

Application was created using ASP.NET Core 2.1 and Entity Framework 6.
After cloning the repository, couple things need to be done in order to run the application:
* In the file `appsettings.json`, under `ConnectionStrings`, change `BlogContext` Server name to your SQL Server name
* In the `Package Manager Console` enter the following commands: `remove-migration` and then `EntityFrameworkCore\Add-Migration Initial` (or any other name then Initial, it's up   to you)
* After that, under the folder `Migrations`, couple `.cs` classes will be created
* Finally, run the application

When creating migration, make sure that `Microsoft SQL Server Management Studio` is running. If everything is finished without errors, you should see database named `BlogDatabase` under your databases folder.
