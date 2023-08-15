using FinalPantry.SampleDataGenerator;
using Pantry.Core.Recipe;

namespace FinalPantry;

public class Program
{
    private static void Main()
    {
        List<Ingredient> ingredients = IngredientGenerator.GenerateIngredients();
        var recipes = RecipeGenerator.GenerateRecipes(ingredients, SampleData.RecipeNames);
        
        ingredients.ForEach(Console.WriteLine);
        recipes.ForEach(x => Console.WriteLine(x.Name));
    }
}