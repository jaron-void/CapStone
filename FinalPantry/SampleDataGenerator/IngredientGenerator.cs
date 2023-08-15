using Pantry.Core.Recipe;

namespace FinalPantry.SampleDataGenerator;

public static class IngredientGenerator
{
    public static List<Ingredient> GenerateIngredients()
    {
        List<Ingredient> ingredients = new List<Ingredient>();

        for (int i = 0; i < SampleData.IngredientNames.Count; i++)
        {
            string name = SampleData.IngredientNames[i];
            MeasuredIn measuredIn = (MeasuredIn)SampleData.RandomNumbers[i];
            decimal pricePerMeasurement = SampleData.RandomDecimals[i];

            Ingredient ingredient = new Ingredient(name, measuredIn, pricePerMeasurement);
            ingredients.Add(ingredient);
        }

        return ingredients;
    }
}