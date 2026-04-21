# CleanArchitecture.Web

A .NET 10 Blazor Web App with ServiceStack.Blazor and Tailwind CSS.

## Features

- **Blazor Web App** (.NET 10)
- **ServiceStack.Blazor** integration for enhanced components
- **Tailwind CSS** for styling

## Development

### Prerequisites

- .NET 10 SDK
- Node.js and npm

### Building Tailwind CSS

The project uses Tailwind CSS for styling. To build the CSS:

```bash
# Build CSS once
npm run build:css

# Watch for changes during development
npm run watch:css
```

### Running the Application

```bash
dotnet run
```

Or use `dotnet watch` for hot reload:

```bash
dotnet watch
```

## Project Structure

- `/Components` - Blazor components
- `/wwwroot` - Static files
  - `/css` - Generated CSS files (from Tailwind)
  - `app.input.css` - Tailwind CSS input file
- `tailwind.config.js` - Tailwind configuration
- `package.json` - NPM dependencies for Tailwind CSS

## Notes

- The Tailwind CSS is generated from `wwwroot/app.input.css` to `wwwroot/css/app.css`
- Remember to run `npm run build:css` before publishing
- ServiceStack.Blazor package provides additional Blazor components and utilities
