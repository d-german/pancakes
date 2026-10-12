# Buttermilk pancake calculator

Scale the original recipe from 1 to 10,000 pancakes in a browser.
The app shows exact US gallons, cups, tablespoons, teaspoons, and mixed fractions.
For example, 9 pancakes need **1 1/2 eggs**; fractional eggs are preserved.

[Published calculator](https://d-german.github.io/pancakes/)

## Run locally

Install the .NET 10 SDK, then run:

```powershell
dotnet run --project Pancakes.Web --urls http://localhost:5182
```

Open http://localhost:5182. No database or API service is required.
Open `Pancakes.Web.slnx` in Rider for the current web app and checks.
The original Windows solution and projects remain archived in this repository.

## Verify

```powershell
dotnet run --project Pancakes.Web.Checks --configuration Release
dotnet publish Pancakes.Web --configuration Release
```

The dependency-free checks cover complete recipes for 6, 9, 12, 25, 50,
and 1,000 pancakes, fractional eggs, invalid inputs, and preservation of
every ingredient amount across all supported counts.

## Original recipe

The existing `PancakeModel` remains the source of ingredient ratios.
For six pancakes:

| Ingredient | Amount |
| --- | --- |
| Flour | 1 cup |
| Buttermilk | 1 cup |
| Eggs | 1 egg |
| Sugar | 1/4 cup |
| Oil | 1/4 cup |
| Baking powder | 1 tsp |
| Baking soda | 1 tsp |
| Vanilla | 1 tsp |

Vanilla follows the original WPF view model's baking-powder ratio.
`Measurement_Breakdown_Requirements.md` records broader conversion ideas;
this app implements the recipe's US cooking measures.

## Why this repository

| Repository | Assessment |
| --- | --- |
| [pancakes](https://github.com/d-german/pancakes) | Existing shared recipe model, working WPF behavior, tests, and measurement requirements; continued here. |
| [pancakes-blazor](https://github.com/d-german/pancakes-blazor) | Essentially empty; no existing app to continue. |
| [pancakeService](https://github.com/d-german/pancakeService) | API only; .NET Framework 4.5.2 and ASP.NET Core 1.1, with an unresolved model gitlink. |
| [PancakesMVC](https://github.com/d-german/PancakesMVC) | Stock MVC template on .NET Framework 4.5 with an external model reference. |
| [pancakesWP8_WPF](https://github.com/d-german/pancakesWP8_WPF) | Useful legacy recipe code and instructions, with old native Windows/Phone UIs. |

## Publish

GitHub Pages must use **GitHub Actions** as its build source.
`.github/workflows/pages.yml` checks and publishes the .NET 10 browser app.
Pull requests build and check; pushes to `master` deploy after those steps pass.
The workflow sets the site base path to `/pancakes/`, adds `.nojekyll`,
and copies the entry page to `404.html` for browser navigation.
A manual workflow run on `master` can also deploy.
