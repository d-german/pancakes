# Buttermilk pancake calculator

Scale the original recipe from 1 to 10,000 pancakes in a browser.
The app always shows grams and pounds alongside US gallons, cups, tablespoons,
teaspoons, and mixed fractions for every ingredient except eggs. Oil also shows
US fluid ounces for shopping.
For example, 9 pancakes need **1 1/2 eggs**; fractional eggs are preserved.
Check off ingredients as you add them while cooking or buy them while shopping.
Checks stay when the pancake count changes. **Clear checks** starts a new list;
refreshing the page also clears the checks.

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
every volume, gram, pound and oil fluid-ounce amount across all supported counts.

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
this app implements the recipe's US cooking measures and ingredient-specific gram weights.

## Gram conversions

Weights scale directly from the original recipe's cups or teaspoons, then round
the final batch total to the nearest 0.1 g (midpoints round up). Eggs remain counts.
No intermediate per-pancake weight is rounded. These are approximate baking
weights, not laboratory densities: brands, temperature, and flour packing vary.
Flour assumes all-purpose flour spooned and leveled; sugar is white granulated;
buttermilk is liquid cultured buttermilk; oil is soybean vegetable oil; vanilla is extract.

| Ingredient | Reference weight | Source |
| --- | --- | --- |
| All-purpose flour | 120 g / US cup | [King Arthur Baking](https://www.kingarthurbaking.com/learn/ingredient-weight-chart) |
| Cultured buttermilk | 245 g / US cup | [USDA SR Legacy 170874](https://api.nal.usda.gov/fdc/v1/food/170874?api_key=DEMO_KEY) |
| Granulated sugar | 200 g / US cup | [USDA SR Legacy 169655](https://api.nal.usda.gov/fdc/v1/food/169655?api_key=DEMO_KEY) |
| Soybean vegetable oil | 218 g / US cup | [USDA SR Legacy 171411](https://api.nal.usda.gov/fdc/v1/food/171411?api_key=DEMO_KEY) |
| Double-acting phosphate baking powder | 4.6 g / teaspoon | [USDA SR Legacy 172804](https://api.nal.usda.gov/fdc/v1/food/172804?api_key=DEMO_KEY) |
| Baking soda | 4.6 g / teaspoon | [USDA SR Legacy 175040](https://api.nal.usda.gov/fdc/v1/food/175040?api_key=DEMO_KEY) |
| Vanilla extract | 4.2 g / teaspoon | [USDA SR Legacy 173471](https://api.nal.usda.gov/fdc/v1/food/173471?api_key=DEMO_KEY) |

USDA source links open the record JSON; values were verified against each record's `foodPortions` in the
[FoodData Central API](https://fdc.nal.usda.gov/api-guide/).
Use the source portion matching the recipe unit: USDA's cup, tablespoon, and
teaspoon weights are independently rounded and can differ when multiplied.

## Shopping units

Pounds use **1 lb = 453.59237 g**. Oil uses **8 US fluid ounces per US cup**,
calculated directly from the recipe volume rather than treating fluid ounces as
weight ounces. See [NIST's SI conversion factors](https://www.nist.gov/pml/special-publication-811/nist-guide-si-appendix-b-conversion-factors/nist-guide-si-appendix-b8)
and [NIST's household equivalents](https://tsapps.nist.gov/publication/get_pdf.cfm?pub_id=936072).
Pounds and oil fluid ounces round to 0.001; grams retain their existing 0.1 g
rounding. Each unit rounds only the final batch total, independently, so the
gram display's rounding does not affect the pound conversion. Eggs remain counts.

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
