# Ohms law web app

A interactive webapplication for **Ohms law**, using **Blazor** (.NET). Built to be simple and help you calculate using **Ohms law** that can be complicated sometimes.

## Functions

- **Dynamic calculator**
- **Interactive Ohms Law wheel SVG**

## Tech stack

- **Framework:** Blazor (ASP.NET Core)
- **Language:** C#, HTML, CSS
- **Graphics:** Scalable vector graphics (SVG)

## Structure

```text
OhmsLaw/
├── Components/
│   ├── Layout/          # Navigation and main layout
│   └── Pages/
│       ├── Ohmslag.razor      # UI and HTML structure
│       ├── Ohmslag.razor.cs   # C# logic and calculations (Partial Class)
│       └── Ohmslag.razor.css  # Component-scoped styling
├── wwwroot/             # Static assets and CSS
└── Program.cs           # Application entry point
```

## Try it yourself locally

```bash
1. git clone
2. cd OhmsLaw
3. dotnet run
4. Open your browser and navigate to the local address provided in the terminal (for example, http://localhost:5169).
```