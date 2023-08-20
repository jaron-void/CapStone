namespace Pantry.Core.Recipe;

public interface IMeasurement
{
    string? RecipeName { get; set; }
    IIngredient? Ingredient { get; set; }
    MeasuredIn MeasuredIn { get; set; }
    decimal Amount { get; set; }

    // Derived Properties
    decimal Price { get; } //=> Ingredient.PricePerPortion(Amount);

}