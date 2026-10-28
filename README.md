# Ohm's law web app

An interactive Ohm's law calculator built with **Blazor WebAssembly**. The app runs entirely in the browser and can be hosted as static files on GitHub Pages.

## Features

- Dynamic calculator for voltage, current, resistance, and power
- Interactive Ohm's law formula wheel
- English and Swedish localization

## Run locally

Install the .NET 10 SDK, then run:

```bash
dotnet run
```

Open the local URL printed by the command.

## How I deploy to GitHub Pages

The workflow in `.github/workflows/deploy.yml` publishes the app whenever changes are pushed to `main`. The site is available at `https://johannesl2.github.io/blazor-ohms-law//`.