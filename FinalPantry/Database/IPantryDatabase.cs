using Pantry.Core.Recipe;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pantry.Core.Database;
public interface IPantryDatabase
{
    // Ingredient Database
    public void CreateIngredientTable(string connectionString = DB.ConnectionString);
    void InsertIngredient(IIngredient ingredient);
    void UpdateIngredient(IIngredient ingredient);
    void DeleteIngredient(IIngredient ingredient);
    IEnumerable<IIngredient> GetAllIngredients();
    
    // Measurement Database
    public void CreateMeasurementTable(string connectionString = DB.ConnectionString);
    void InsertMeasurement(IMeasurement measurement);
    
    // Recipe Database
    public void CreateRecipeTable(string connectionString = DB.ConnectionString);
    void InsertRecipe(IRecipe recipe);
    void UpdateRecipe(IRecipe recipe);
    void DeleteRecipe(IRecipe recipe);
    IEnumerable<IRecipe> GetAllRecipes();
}