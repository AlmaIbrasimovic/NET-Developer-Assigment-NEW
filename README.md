# NET-Developer-Assigment

## Preparing SQL Server database
Application was created using ASP.NET Core 2.1
After cloning the repository, couple things need to be done in order to run the application:
* In the file `appsettings.json`, under `ConnectionStrings`, change `BlogContext` Server name to your SQL Server name
* In the `Package Manager Console` enter the following commands: `remove-migration` and then `EntityFrameworkCore\Add-Migration Initial` (or any other name then Initial, it's up   to you)
* Since `code-first approach` is used, after successful migaration, under the folder `Migrations`, couple `.cs` classes will be created
* Finally, run the application

When creating migration, make sure that `Microsoft SQL Server Management Studio 18` is running and make sure that `Windows Authentication` is selected under `Authentication`. If everything is finished without errors, you should see database named `BlogDatabase` under your databases folder.

After you run application, browser will open new address `https://localhost:44326/api/posts`. You can access `Swagger` by entering URL `https://localhost:44326/swagger/index.html`

# API specifications


## POST request
`https://localhost:44326/api/posts`

Request body:
```
{
    "blogPost": {
        "slug": "react-native-application",
        "title": "React Native Application",
        "description": "Mobile application for AR!",
        "body": "With this app you will feel like you are there!",
        "createdAt": "2020-10-13T10:56:32.6894954",
        "updatedAt": "2020-10-13T11:15:32.6894962",
        "tagList": ["AR", "iOS"]
       
    }
}
```

## PUT request
`https://localhost:44326/api/posts/react-native-application`

Request body:
```
{
    "blogPost": {
        "slug": "react-native-application",
        "title": "React Native Application New Title",
        "description": "Mobile application for AR New Description!",
        "body": "With this app you will feel like you are there!",
        "createdAt": "2020-10-13T10:56:32.6894954",
        "updatedAt": "2020-10-13T11:15:32.6894962"
    }
}
```

## DELETE request
`https://localhost:44326/api/posts/react-native-application`

## GET request (single Post object)
`https://localhost:44326/api/posts/react-native-application`

## GET request (all Post objects)
`https://localhost:44326/api/posts`

Optional parameter is `tag`\
`https://localhost:44326/api/posts?tag=AR`

## GET request (all Tag objects)
`https://localhost:44326/api/tags`

# NuGet packages
Following packages are required in order to run application:
```
Microsoft.AspNetCore.App (version 2.1.1)
Microsoft.AspNetCore.Razor.Design (version 2.1.2)
Microsoft.EntityFrameworkCore.Design (version 2.1.14)
Microsoft.EntityFrameworkCore.SqlServer (version 2.1.14)
Microsoft.EntityFrameworkCore.Tools (version 2.1.14)
Microsoft.NETCore.App (version 2.1.0)
Swashbuckle.AspNetCore (version 5.6.3)
Swashbuckle.AspNetCore.Swagger (version 5.6.3)
Swashbuckle.AspNetCore.SwaggerUI (version 5.6.3)
```

Before running the application, please check if all mentioned packages are installed:\
Right click on the **Blog** (under Solution Explorer) and select **Manage NuGet Packages** and check under **Installed** tab
