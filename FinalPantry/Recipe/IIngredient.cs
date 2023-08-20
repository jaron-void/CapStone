namespace Pantry.Core.Recipe;

public interface IIngredient
{
    public string? Name { get; set; }
    public MeasuredIn MeasuredIn { get; set; } // weight, volume or "eaches"
    public decimal PricePerMeasurement { get; set; } //This will keep from storing pricePerCase and whatnot
    
    // Methods
    decimal PricePerPortion(decimal portion);

    // Overrides
    string ToString();
    
}