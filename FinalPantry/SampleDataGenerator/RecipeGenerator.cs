using Pantry.Core.Recipe;

namespace FinalPantry.SampleDataGenerator;

public static class RecipeGenerator
{

    public static List<IRecipe> GenerateRecipes(List<Ingredient> ingredients, List<string> recipeNames)
    {
        List<IRecipe> recipes = new List<IRecipe>();

        Random random = new Random();

        List<IMeasurement> measurements =
            MeasurementGenerator.GenerateMeasurements(ingredients, SampleData.RecipeNames);

        List<string> sampleInstructions = new List<string>
        {
            "Mix all ingredients together and bake in the oven.",
            "Saute the ingredients in a pan and serve.",
            "Boil the ingredients and add seasoning.",
            "Layer the ingredients and bake until golden brown.",
        };
     
        foreach (string recipeName in recipeNames)
        {
            string instructions = sampleInstructions[random.Next(sampleInstructions.Count)];
            int servingsPerRecipe = random.Next(5, 21);

            Recipe recipe = new Recipe(recipeName, measurements, sampleInstructions, servingsPerRecipe);
            recipes.Add(recipe);
        }

        return recipes;
    }
}