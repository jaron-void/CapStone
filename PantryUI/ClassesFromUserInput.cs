using Pantry.Core.Database;
using Pantry.Core.Recipe;
using Pantry.Core.Database.Sqlite;

namespace PantryUI;

public static class ClassesFromUserInput
{
    private static decimal FindPricePerMeasurement()
    {
        Console.WriteLine();
        Console.WriteLine("What is the total price for the case");
        var TotalPrice = Convert.ToDecimal(Console.ReadLine());
        Console.WriteLine("How many Packages came in the case? Enter 1 there was no case, just a package");
        var PackagesInCase = Convert.ToDecimal(Console.ReadLine());
        Console.WriteLine("How many units of measurement are in each Package?");
        var UnitsInPackage = Convert.ToDecimal(Console.ReadLine());
        return (TotalPrice / PackagesInCase) / UnitsInPackage;


    }
    
    
    public static Ingredient AddIngredient()
    {
        Console.WriteLine("Welcome to the Ingredient Creator!");
        Console.Write("Enter ingredient name: ");
        var name = Console.ReadLine();

        Console.Write("Enter measurement type (weight, volume, eaches): ");
        Enum.TryParse(Console.ReadLine(), out MeasuredIn measuredIn);

        Console.Write("Enter price per measurement: ");
        var pricePerMeasurement = FindPricePerMeasurement();

        var ingredient = new Ingredient(name, measuredIn, pricePerMeasurement);

        Console.WriteLine("\nIngredient Created:");
        Console.WriteLine(ingredient);
        return ingredient;
    }

    static Measurement AddMeasurement(IIngredient? ingredient)
    {
        Console.Write("Enter recipe name: ");
        var recipeName = Console.ReadLine();

        Console.Write("Enter amount: ");
        decimal.TryParse(Console.ReadLine(), out var amount);

        var recipeUnit = ingredient.MeasuredIn;

        var measurement = new Measurement(recipeName, ingredient, recipeUnit, amount);

        Console.WriteLine("\nMeasurement Created:");
        Console.WriteLine(measurement);

        return measurement;
    }

    private static Measurement AddMeasurement(IIngredient? ingredient, string? recipeName)
    {
        Console.Write("Enter amount: ");
        decimal.TryParse(Console.ReadLine(), out var amount);

        var recipeUnit = ingredient.MeasuredIn;

        var measurement = new Measurement(recipeName, ingredient, recipeUnit, amount);

        Console.WriteLine("\nMeasurement Created:");
        Console.WriteLine(measurement);

        return measurement;
    }

    public static Recipe AddRecipe(IPantryDatabase db)
    {
        Console.WriteLine("Adding a New Recipe");
    
        Console.Write("Enter recipe name: ");
        var recipeName = Console.ReadLine();

        var measurements = new List<IMeasurement>();
        while (true)
        {
            Console.WriteLine("Available Ingredients:");
            IEnumerable<IIngredient> ingredients = db.GetAllIngredients();
            foreach (var ingredient in ingredients)
            {
                Console.WriteLine(ingredient.Name);
            }

            Console.Write("Enter ingredient name (or 'done' to finish): ");
            var ingredientName = Console.ReadLine();
            Console.Clear();

            if (ingredientName?.ToLower() == "done")
            {
                break;
            }

            var selectedIngredient = ingredients.FirstOrDefault(ingredient => ingredient.Name == ingredientName);
            if (selectedIngredient == null)
            {
                Console.WriteLine("Ingredient not found. Please try again.");
                continue;
            }

            var measurement = AddMeasurement(selectedIngredient, recipeName);
            measurements.Add(measurement);
        }

        var instructions = new List<string?>();
        while (true)
        {
            Console.Write("Enter instruction (or 'done' to finish): ");
            var instruction = Console.ReadLine();

            if (instruction.ToLower() == "done")
            {
                break;
            }

            instructions.Add(instruction);
        }

        Console.Write("How many servings will this make?");
        int.TryParse(Console.ReadLine(), out var servingsPerRecipe);

        var recipe = new Recipe(recipeName, measurements, instructions, servingsPerRecipe);

        Console.WriteLine("\nRecipe Created:");
        Console.WriteLine(recipe);

        return recipe;
    }

    
    
}