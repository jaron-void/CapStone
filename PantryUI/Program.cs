using FinalPantry;
using FinalPantry.SampleDataGenerator;
using Pantry.Core.Database;
using Pantry.Core.Recipe;
using PantryUI;

public class Program
{
    private static void Main()
    {
        DB.EnsureFoldersExist();

        DatabaseManager manager = new DatabaseManager();
        IPantryDatabase db = manager.GetPantryDatabase();
        db.CreateTable();
        LoadSampleData(db);
        
        TerminalOptions.Welcome();
        TerminalOptions.PrintOptions(db);
    }

    static void LoadSampleData(IPantryDatabase db)
    {
        Console.WriteLine("Load Sample Data? (Y) or (N)");
        char choice = Console.ReadKey().KeyChar;

        switch (char.ToUpper(choice))
        {
            case 'Y':
                List<Ingredient?> ingredients = IngredientGenerator.GenerateIngredients();
                var recipes = RecipeGenerator.GenerateRecipes(ingredients, SampleData.RecipeNames);
                ingredients.ForEach(db.InsertIngredient);
                ingredients.ForEach(Console.WriteLine);
                recipes.ForEach(db.InsertRecipe);
                Console.Clear();
                break;
            case 'N':
                break;
            case 'Q':
                Environment.Exit(0);
                break;
            default:
                Console.WriteLine("Invalid choice. Please try again.");
                break;
        }

    }
}