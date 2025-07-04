﻿# ASP.NET Blog

A personal blog project built using ASP.NET Core MVC. This project showcases basic blogging functionality, including post creation, editing, deletion, and display.

---

## 1. Features

- Create, edit, delete blog posts  
- List all posts on the homepage
- Create, edit, delete events
- Search for events
- View individual posts
- Add, delete pictures
- Basic authentication and authorization  
- Simple layout using Bootstrap  

---

## 2. Getting Started

### Prerequisites

- [.NET SDK 8+](https://dotnet.microsoft.com/download)  
- Visual Studio 2022 or higher (with ASP.NET workload)  
- SQL Server Express or LocalDB (optional, depending on config)

### Setup Instructions

```bash
git clone https://github.com/danaroug/aspnet-blog.git
cd aspnet-blog
dotnet restore
dotnet build
dotnet run
```
## 3. Development

To run in development mode with auto-reload:

```bash
dotnet watch run
```
## 4. Folder Structure

```
aspnet-blog/
├── Controllers/
├── Models/
├── Views/
│   ├── Shared/
│   └── Blog/
├── wwwroot/
├── appsettings.json
├── Program.cs
├── Startup.cs
└── README.md
```

## 5. Contributions

Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

## 6. License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.


