namespace Pantry.Core.Recipe;

public class Measurement : IMeasurement
{
    public string? RecipeName { get; set; }
    public IIngredient? Ingredient { get; set; }
    public MeasuredIn MeasuredIn { get; set; }
    public decimal Amount { get; set; }
    
    // Derived Property
    public decimal Price => Ingredient.PricePerPortion(Amount);

    // Constructor
    public Measurement(string? recipeName, IIngredient? ingredient, MeasuredIn measuredIn, decimal amount)
    {
        RecipeName = recipeName;
        Ingredient = ingredient;
        MeasuredIn = measuredIn;
        Amount = amount;
    }
    
    // Override
    public override string ToString()
    {
        return $"{Amount} {MeasuredIn.ToString()} of {Ingredient.Name} for the {RecipeName} recipe.";
    }
}