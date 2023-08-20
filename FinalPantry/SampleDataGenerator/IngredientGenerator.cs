using Pantry.Core.Recipe;

namespace FinalPantry.SampleDataGenerator;

public static class IngredientGenerator
{
    public static List<Ingredient?> GenerateIngredients()
    {
        var ingredients = new List<Ingredient?>();

        for (var i = 0; i < SampleData.IngredientNames.Count; i++)
        {
            var name = SampleData.IngredientNames[i];
            var measuredIn = (MeasuredIn)SampleData.RandomNumbers[i];
            var pricePerMeasurement = SampleData.RandomDecimals[i];

            var ingredient = new Ingredient(name, measuredIn, pricePerMeasurement);
            ingredients.Add(ingredient);
        }

        return ingredients;
    }
}