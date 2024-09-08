# Library App

## Note:
This code does throw a few exceptions which seem to be unexpected based on [the lesson conclusion notes (scroll to bottom)](https://learn.microsoft.com/en-us/training/modules/guided-project-accelerate-app-development-using-github-copilot-tools/6-exercise-refactor-improve-code-github-copilot).
```
Exception thrown: 'System.NullReferenceException' in Library.Console.dll
Exception thrown: 'System.AggregateException' in System.Private.CoreLib.dll
Exception thrown: 'System.NullReferenceException' in System.Private.CoreLib.dll
```

## Description
Library App is a console application designed to manage library operations such as patron searches and loan management. It is built using .NET 8.0 and follows a modular architecture with separate projects for application core, console interface, and infrastructure.

## Project Structure
- `AccelerateDevGitHubCopilot.generated.sln` - Solution file for the project
- `README.md` - Project documentation
- `readme_old.md` - Previous version of the project documentation
- `src/`
  - `Library.ApplicationCore/`
	- `Entities/` - Contains entity classes
	- `Enums/` - Contains enumeration types
	- `Interfaces/` - Contains interface definitions
	- `Library.ApplicationCore.csproj` - Project file for the application core
	- `Services/` - Contains service classes
  - `Library.Console/`
	- `appSettings.json` - Configuration file
	- `CommonActions.cs` - Contains common actions for the console app
	- `ConsoleApp.cs` - Main console application class
	- `ConsoleState.cs` - Contains state definitions for the console app
	- `Json/` - Contains JSON data files
	- `Library.Console.csproj` - Project file for the console application
	- `Program.cs` - Entry point for the console application
  - `Library.Infrastructure/`
	- `Data/` - Contains data access classes
	- `Library.Infrastructure.csproj` - Project file for the infrastructure
- `tests/`
  - `UnitTests/` - Contains unit tests for the application

## Key Classes and Interfaces
- `src/Library.ApplicationCore/Entities/Book.cs` - Represents a book entity
- `src/Library.ApplicationCore/Enums/EnumHelper.cs` - Contains helper methods for enums
- `src/Library.Console/ConsoleApp.cs` - Main class for the console application
- `src/Library.Console/Program.cs` - Entry point for the console application

## Usage
To run the Library App, follow these steps:
1. Clone the repository.
2. Open the solution file `AccelerateDevGitHubCopilot.generated.sln` in Visual Studio.
3. Build the solution.
4. Run the `Library.Console` project.

## License
This project is licensed under the MIT License.
