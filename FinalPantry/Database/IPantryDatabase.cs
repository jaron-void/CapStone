using Pantry.Core.Recipe;


namespace Pantry.Core.Database;
public interface IPantryDatabase
{
    // Ingredient Database
    public void CreateIngredientTable();
    void InsertIngredient(IIngredient ingredient);
    void UpdateIngredient(IIngredient ingredient);
    void DeleteIngredient(IIngredient ingredient);
    IEnumerable<IIngredient> GetAllIngredients();
    void ExportIngredientsToExcel(string excelFilePath);

    // Measurement Database
    public void CreateMeasurementTable();
    void InsertMeasurement(IMeasurement measurement);
    
    
    // Recipe Database
    public void CreateRecipeTable();
    void InsertRecipe(IRecipe recipe);
    void UpdateRecipe(IRecipe recipe);
    void DeleteRecipe(IRecipe recipe);
    public IEnumerable<IRecipe?> GetAllRecipes();
    void CreateTable();
    public IRecipe? GetRecipe(string? name);
    public IEnumerable<IRecipe> GetRecipesOrderedByPricePerServing();
    public IEnumerable<IRecipe?> GetRecipesContainingIngredient(string ingredientName);


    protected IEnumerable<IMeasurement> GetMeasurementsForRecipe(string? recipeName);



}