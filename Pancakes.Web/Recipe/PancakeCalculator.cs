using PancakeModel;

namespace Pancakes.Web.Recipe;

public sealed record IngredientAmount(string Name, string Amount, decimal? Grams = null,
    decimal? Pounds = null, decimal? FluidOunces = null);

public static class PancakeCalculator
{
    public const int MaximumPancakes = 10000;
    public const int DefaultPancakes = 12;

    public static IReadOnlyList<IngredientAmount> Scale(int pancakeCount)
    {
        if (pancakeCount is < 1 or > MaximumPancakes)
            throw new ArgumentOutOfRangeException(nameof(pancakeCount), $"Choose 1 to {MaximumPancakes} pancakes.");

        var recipe = new ButtermilkPancakeRecipe(pancakeCount);
        return
        [
            // Ingredient-specific household weights and assumptions are sourced in README.md.
            Weighed("Flour", recipe.NumCupsFlower * 48, recipe.NumCupsFlower * 120m),
            Weighed("Buttermilk", recipe.NumCupsButtermilk * 48, recipe.NumCupsButtermilk * 245m),
            new("Eggs", Quantity(Ticks(recipe.NumEggs), 6, "egg", "eggs")),
            Weighed("Sugar", recipe.NumCupsSugar * 48, recipe.NumCupsSugar * 200m),
            Weighed("Oil", recipe.NumCupsOil * 48, recipe.NumCupsOil * 218m, recipe.NumCupsOil * 8m),
            Weighed("Baking powder", recipe.NumTspBakingPowder, recipe.NumTspBakingPowder * 4.6m),
            Weighed("Baking soda", recipe.NumTspBakingSoda, recipe.NumTspBakingSoda * 4.6m),
            // The original WPF recipe uses the baking-powder ratio for vanilla.
            Weighed("Vanilla", recipe.NumTspBakingPowder, recipe.NumTspBakingPowder * 4.2m)
        ];
    }

    private static IngredientAmount Weighed(string name, decimal teaspoons, decimal grams, decimal? fluidOunces = null) =>
        new(name, Volume(teaspoons), decimal.Round(grams, 1, MidpointRounding.AwayFromZero),
            decimal.Round(grams / 453.59237m, 3, MidpointRounding.AwayFromZero),
            fluidOunces is { } ounces ? decimal.Round(ounces, 3, MidpointRounding.AwayFromZero) : null);

    private static int Ticks(decimal amount) => decimal.ToInt32(decimal.Round(amount * 6));

    private static string Volume(decimal teaspoons)
    {
        var remaining = Ticks(teaspoons);
        var parts = new List<string>();
        var gallons = remaining / 4608;
        remaining %= 4608;
        if (gallons > 0)
            parts.Add(Quantity(gallons, 1, "gallon", "gallons"));

        var cupTicks = remaining / 288 * 288;
        remaining %= 288;
        foreach (var fraction in new[] { 216, 192, 144, 96, 72 })
        {
            if (remaining < fraction)
                continue;
            cupTicks += fraction;
            remaining -= fraction;
            break;
        }

        if (cupTicks > 0)
            parts.Add(Quantity(cupTicks, 288, "cup", "cups"));

        var tablespoons = remaining / 18;
        remaining %= 18;
        if (tablespoons > 0)
            parts.Add(Quantity(tablespoons, 1, "tbsp", "tbsp"));
        if (remaining > 0)
            parts.Add(Quantity(remaining, 6, "tsp", "tsp"));

        return string.Join(" ", parts);
    }

    private static string Quantity(int numerator, int denominator, string singular, string plural)
    {
        var whole = numerator / denominator;
        var remainder = numerator % denominator;
        var amount = whole > 0 ? whole.ToString(System.Globalization.CultureInfo.InvariantCulture) : "";
        if (remainder > 0)
        {
            var divisor = Gcd(remainder, denominator);
            amount += (whole > 0 ? " " : "") + $"{remainder / divisor}/{denominator / divisor}";
        }

        return $"{amount} {(numerator > denominator ? plural : singular)}";
    }

    private static int Gcd(int left, int right)
    {
        while (right != 0)
            (left, right) = (right, left % right);
        return left;
    }
}
