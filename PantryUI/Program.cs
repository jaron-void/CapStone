/*// See https://aka.ms/new-console-template for more information

using Pantry.Core.Database;
using Pantry.Core.Recipe;

Console.WriteLine("Hello, World!");


var pantryDatabaseManager = new DatabaseManager();

var pantryDatabase = pantryDatabaseManager.GetPantryDatabase();

FormatIngredientsList(pantryDatabase.GetAllIngredients());


// Methods

void FormatIngredientsList(IEnumerable<IIngredient> ingredients)
{
    Console.WriteLine($"{0,-20}{1}", "Ingredient", "Description");
    foreach (var ingredient in ingredients)
    {
        Console.WriteLine($"{ingredient.Name,-20}{ingredient.Description}");
    }
}*/