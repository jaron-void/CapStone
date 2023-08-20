using Pantry.Core.Database;
using Pantry.Core.Recipe;

namespace PantryUI;

public static class TerminalOptions
{
    public static void Welcome()
    {
        Console.WriteLine("Welcome to The Pantry!");
        Console.WriteLine("An ingredient and recipe Database Application");
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
        Console.Clear();
    }

    public static void PrintOptions(IPantryDatabase db)
    {
        Console.Clear();
            
        Console.WriteLine("Press I for (I)ngredient, R for (R)ecipe, Q for (Q)uit");
        var choice = Console.ReadKey().KeyChar;

        switch (char.ToUpper(choice))
        {
            case 'I':
                IngredientOptions(db);
                break;
            case 'R':
                RecipeOptions(db);
                break;
            case 'Q':
                Environment.Exit(0);
                break;
            default:
                Console.WriteLine("Invalid choice. Please try again.");
                break;
        }
    }


    private static void IngredientOptions(IPantryDatabase db)
    {
        while (true)
        {
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("Ingredient Options:");
            Console.WriteLine("1. Insert new Ingredient");
            Console.WriteLine("2. Update Ingredient");
            Console.WriteLine("3. Delete Ingredient");
            Console.WriteLine("4. View All Ingredients");
            Console.WriteLine("5. Export Ingredients to Excel");
            Console.WriteLine("6. Back to Main Menu");

            Console.Write("Select an option: ");
            var choice = int.Parse(Console.ReadLine() ?? string.Empty);

            switch (choice)
            {
                case 1:
                    var ingredient = ClassesFromUserInput.AddIngredient();
                    db.InsertIngredient(ingredient);
                    Console.WriteLine("Ingredient inserted successfully.");
                    Console.Clear();
                    break;
                case 2:
                    ingredient = ClassesFromUserInput.AddIngredient();
                    db.UpdateIngredient(ingredient);
                    Console.WriteLine("Ingredient updated successfully.");
                    Console.Clear();
                    break;
                case 3:
                    var ingredients = db.GetAllIngredients();
                    foreach (var ing in ingredients)
                    {
                        Console.WriteLine(ing);
                    }
                    Console.Write("Enter ingredient name to delete: ");
                    var ingredientName = Console.ReadLine();
                    Console.Clear();
                    db.DeleteIngredient(new Ingredient(ingredientName, MeasuredIn.Volume, 0)); // You can adjust MeasuredIn as needed
                    Console.WriteLine("Ingredient deleted successfully.");
                    break;
                case 4:
                    ingredients = db.GetAllIngredients();
                    foreach (var ing in ingredients)
                    {
                        Console.WriteLine(ing);
                    }
                    Console.Clear();
                    break;
                case 5:
                    db.ExportIngredientsToExcel(DB.ExcelFilePath);
                    break;
                case 6:
                    PrintOptions(db); // Return to main menu
                    Console.Clear();
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    private static void RecipeOptions(IPantryDatabase db)
    {
        while (true)
        {
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("Recipe Options:");
            Console.WriteLine("1. Insert new Recipe");
            Console.WriteLine("2. Update Recipe");
            Console.WriteLine("3. Delete Recipe");
            Console.WriteLine("4. View All Recipes");
            Console.WriteLine("5. Sort Recipes by Price per Serving");
            Console.WriteLine("6. View Recipes Containing an Ingredient");
            Console.WriteLine("7. Back to Main Menu");

            Console.Write("Select an option: ");
            var choice = int.Parse(Console.ReadLine() ?? string.Empty);
            Console.Clear();
            switch (choice)
            {
                case 1:
                    var recipe = ClassesFromUserInput.AddRecipe(db);
                    db.InsertRecipe(recipe);
                    Console.WriteLine("Recipe inserted successfully.");
                    break;
                case 2:
                    Console.Write("Enter the name of the recipe to update: ");
                    var recipeNameToUpdate = Console.ReadLine() ?? string.Empty;
                    var updatedRecipe = ClassesFromUserInput.AddRecipe(db);
                    updatedRecipe.Name = recipeNameToUpdate;
                    db.UpdateRecipe(updatedRecipe);
                    Console.WriteLine("Recipe updated successfully.");
                    break;
                case 3:
                    Console.Write("Enter the name of the recipe to delete: ");
                    var recipeNameToDelete = Console.ReadLine();
                    db.DeleteRecipe(new Recipe(recipeNameToDelete, null, null, 0)); // Replace null and 0 with appropriate values
                    Console.WriteLine("Recipe deleted successfully.");
                    break;
                case 4:
                    var recipes = db.GetAllRecipes();
                    foreach (var r in recipes)
                    {
                        Console.WriteLine(r);
                    }
                    break;
                case 5:
                    var sortedRecipes = db.GetRecipesOrderedByPricePerServing();
                    foreach (var r in sortedRecipes)
                    {
                        Console.WriteLine(r);
                    }
                    break;
                case 6:
                    Console.Write("Enter the ingredient name: ");
                    var ingredientName = Console.ReadLine() ?? string.Empty;
                    var recipesWithIngredient = db.GetRecipesContainingIngredient(ingredientName);
                    foreach (var r in recipesWithIngredient)
                    {
                        Console.WriteLine(r);
                    }
                    break;
                case 7:
                    PrintOptions(db); // Return to main menu
                    break;

                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }
}