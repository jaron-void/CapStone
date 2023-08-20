using Pantry.Core.Recipe;

namespace FinalPantry.SampleDataGenerator;

public static class RecipeGenerator
{

    public static List<IRecipe> GenerateRecipes(List<Ingredient?> ingredients, List<string?> recipeNames)
    {
        var random = new Random();

        var measurements =
            MeasurementGenerator.GenerateMeasurements(ingredients, SampleData.RecipeNames);

        var sampleInstructions = new List<string>
        {
            "Mix all ingredients together and bake in the oven.",
            "Saute the ingredients in a pan and serve.",
            "Boil the ingredients and add seasoning.",
            "Layer the ingredients and bake until golden brown.",
        };

        return (from recipeName in recipeNames let instructions = sampleInstructions[random.Next(sampleInstructions.Count)] 
                let servingsPerRecipe = random.Next(5, 21) 
                select new Recipe(recipeName, measurements, sampleInstructions, servingsPerRecipe))
            .Cast<IRecipe>()
            .ToList();
    }
}