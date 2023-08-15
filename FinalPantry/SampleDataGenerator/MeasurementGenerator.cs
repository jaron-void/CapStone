using Pantry.Core.Recipe;

namespace FinalPantry.SampleDataGenerator;

public static class MeasurementGenerator
{


    public static List<IMeasurement> GenerateMeasurements(List<Ingredient> ingredients, List<string> recipeNames)
    {
        List<IMeasurement> measurements = new List<IMeasurement>();

        Random random = new Random();

        foreach (string recipeName in recipeNames)
        {
            IIngredient ingredient = ingredients[random.Next(ingredients.Count)];
            MeasuredIn measuredIn = (MeasuredIn)SampleData.RandomNumbers[random.Next(SampleData.RandomNumbers.Count)];
            decimal amount = SampleData.RandomDecimals[random.Next(SampleData.RandomDecimals.Count)];

            Measurement measurement = new Measurement(recipeName, ingredient, measuredIn, amount);
            measurements.Add(measurement);
        }

        return measurements;
    }
}