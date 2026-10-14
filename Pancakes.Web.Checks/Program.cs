using System.Globalization;
using System.Text.RegularExpressions;
using Pancakes.Web.Recipe;

var names = new[] { "Flour", "Buttermilk", "Eggs", "Sugar", "Oil", "Baking powder", "Baking soda", "Vanilla" };
var cases = new Dictionary<int, string[]>
{
    [6] = ["1 cup", "1 cup", "1 egg", "1/4 cup", "1/4 cup", "1 tsp", "1 tsp", "1 tsp"],
    [12] = ["2 cups", "2 cups", "2 eggs", "1/2 cup", "1/2 cup", "2 tsp", "2 tsp", "2 tsp"],
    [9] = ["1 1/2 cups", "1 1/2 cups", "1 1/2 eggs", "1/3 cup 2 tsp", "1/3 cup 2 tsp", "1 1/2 tsp", "1 1/2 tsp", "1 1/2 tsp"],
    [25] = ["4 cups 2 tbsp 2 tsp", "4 cups 2 tbsp 2 tsp", "4 1/6 eggs", "1 cup 2 tsp", "1 cup 2 tsp", "1 tbsp 1 1/6 tsp", "1 tbsp 1 1/6 tsp", "1 tbsp 1 1/6 tsp"],
    [50] = ["8 1/3 cups", "8 1/3 cups", "8 1/3 eggs", "2 cups 1 tbsp 1 tsp", "2 cups 1 tbsp 1 tsp", "2 tbsp 2 1/3 tsp", "2 tbsp 2 1/3 tsp", "2 tbsp 2 1/3 tsp"],
    [1000] = ["10 gallons 6 2/3 cups", "10 gallons 6 2/3 cups", "166 2/3 eggs", "2 gallons 9 2/3 cups", "2 gallons 9 2/3 cups", "3 1/3 cups 2 tbsp 2/3 tsp", "3 1/3 cups 2 tbsp 2/3 tsp", "3 1/3 cups 2 tbsp 2/3 tsp"]
};

foreach (var (count, expected) in cases)
{
    var actual = PancakeCalculator.Scale(count);
    Equal(8, actual.Count, $"{count}: ingredient count");
    for (var index = 0; index < names.Length; index++)
    {
        Equal(names[index], actual[index].Name, $"{count}: ingredient {index}");
        Equal(expected[index], actual[index].Amount, $"{count}: {names[index]}");
    }
}

Equal("1/6 egg", PancakeCalculator.Scale(1)[2].Amount, "One pancake keeps its fractional egg");
Equal(27.3m, PancakeCalculator.Scale(3)[4].Grams, "Oil rounds the final batch weight away from a midpoint");
Equal(0.529m, PancakeCalculator.Scale(12)[0].Pounds, "Twelve pancakes need 0.529 lb of flour");
Equal(0.240m, PancakeCalculator.Scale(12)[4].Pounds, "Twelve pancakes need 0.240 lb of oil");
Equal(4m, PancakeCalculator.Scale(12)[4].FluidOunces, "Half a US cup of oil is 4 fluid ounces, independent of its weight");
Equal(48m, PancakeCalculator.Scale(144)[4].FluidOunces, "Shopping-sized oil quantity for 144 pancakes");
Equal(0.333m, PancakeCalculator.Scale(1)[4].FluidOunces, "Fractional fluid ounces round only the final batch total");

// Reconstruct every displayed amount to catch dropped fractions and unit carries.
var factors = new[] { 8m, 8m, 1m / 6m, 2m, 2m, 1m / 6m, 1m / 6m, 1m / 6m };
// Independent six-pancake weights: cups for flour/milk/sugar/oil, teaspoons for leaveners/vanilla.
var gramsForSix = new decimal?[] { 120m, 245m, null, 50m, 54.5m, 4.6m, 4.6m, 4.2m };
for (var count = 1; count <= PancakeCalculator.MaximumPancakes; count++)
{
    var ingredients = PancakeCalculator.Scale(count);
    for (var index = 0; index < ingredients.Count; index++)
    {
        var expected = count * factors[index];
        var actual = ParseAmount(ingredients[index].Amount);
        if (Math.Abs(actual - expected) > 0.000000001m)
            throw new Exception($"{count}: {ingredients[index].Name} lost quantity: {ingredients[index].Amount}");
        decimal? expectedGrams = gramsForSix[index] is decimal baseGrams
            ? decimal.Round(count * baseGrams / 6m, 1, MidpointRounding.AwayFromZero)
            : null;
        Equal(expectedGrams, ingredients[index].Grams, $"{count}: {ingredients[index].Name} grams (eggs remain counts)");
        decimal? expectedPounds = gramsForSix[index] is decimal unroundedBaseGrams
            ? decimal.Round(count * unroundedBaseGrams / 6m / 453.59237m, 3, MidpointRounding.AwayFromZero)
            : null;
        Equal(expectedPounds, ingredients[index].Pounds, $"{count}: {ingredients[index].Name} pounds without intermediate gram rounding");
        decimal? expectedFluidOunces = names[index] == "Oil"
            ? decimal.Round(count / 3m, 3, MidpointRounding.AwayFromZero)
            : null;
        Equal(expectedFluidOunces, ingredients[index].FluidOunces, $"{count}: US fluid ounces only for oil (eggs remain counts)");
    }
}

foreach (var invalid in new[] { 0, -1, int.MinValue, PancakeCalculator.MaximumPancakes + 1, int.MaxValue })
{
    try
    {
        PancakeCalculator.Scale(invalid);
        throw new Exception($"Accepted invalid pancake count {invalid}.");
    }
    catch (ArgumentOutOfRangeException)
    {
    }
}

Console.WriteLine("Passed recipe fixtures, volume, grams, pounds and oil US fluid ounces for 1–10,000 pancakes, egg counts, and invalid inputs.");

static void Equal<T>(T expected, T actual, string context)
{
    if (!EqualityComparer<T>.Default.Equals(expected, actual))
        throw new Exception($"{context}: expected '{expected}', got '{actual}'.");
}

static decimal ParseAmount(string amount)
{
    var matches = Regex.Matches(amount, @"(?<value>\d+(?: \d+/\d+|/\d+)?) (?<unit>gallons?|cups?|tbsp|tsp|eggs?)");
    Equal(amount, string.Join(" ", matches.Select(match => match.Value)), "Readable amount");
    decimal total = 0;
    foreach (Match match in matches)
    {
        var value = match.Groups["value"].Value;
        var components = value.Split(' ');
        decimal quantity = 0;
        foreach (var component in components)
        {
            var fraction = component.Split('/');
            quantity += decimal.Parse(fraction[0], CultureInfo.InvariantCulture) /
                        (fraction.Length == 2 ? decimal.Parse(fraction[1], CultureInfo.InvariantCulture) : 1);
        }
        total += quantity * (match.Groups["unit"].Value switch
        {
            "gallon" or "gallons" => 768m,
            "cup" or "cups" => 48m,
            "tbsp" => 3m,
            _ => 1m
        });
    }
    return total;
}
