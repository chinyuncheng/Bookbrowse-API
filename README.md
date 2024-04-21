# Bookbrowse-API
A simple RESTful API with ASP.NET Core.

## Settings
1. Restore NuGet Packages if necessary
2. Run the program and execute one of the APIs to create `book.db`
3. Run the following scripts in `Package Manager Console`
```
dotnet tool install --global dotnet-ef
cd [THE_PROJECT_LOCATION]\Bookbrowse-API\BookbrowseAPI
dotnet ef database update
```
4. After updating the `book.db`, we're good to execute the APIs now