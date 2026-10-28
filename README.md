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

## Deploy to GitHub Pages

The workflow in `.github/workflows/deploy.yml` publishes the app whenever changes are pushed to `main`, and can also be started manually from the Actions tab. It configures the base path for the repository name and deploys the static WebAssembly files.

For the first deployment, open **Settings → Pages** in the repository and set **Build and deployment → Source** to **GitHub Actions**. The site will then be available at `https://<owner>.github.io/<repository>/`.
