using Pantry.Core.Recipe;

namespace FinalPantry.SampleDataGenerator;

public static class MeasurementGenerator
{


    public static List<IMeasurement>? GenerateMeasurements(List<Ingredient?> ingredients, List<string?> recipeNames)
    {
        var measurements = new List<IMeasurement>();

        var random = new Random();
        
        for (var i = 0; i < recipeNames.Count; i++)
        {
            var measuredIn = (MeasuredIn)SampleData.RandomNumbers[random.Next(SampleData.RandomNumbers.Count)];
            var amount = SampleData.RandomDecimals[random.Next(SampleData.RandomDecimals.Count)];

            var measurement = new Measurement(recipeNames[i], ingredients[i], measuredIn, amount);
            measurements.Add(measurement);
        }
        

        return measurements;
    }
}