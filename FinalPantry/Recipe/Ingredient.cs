namespace Pantry.Core.Recipe;

public class Ingredient : IIngredient
{
    public string? Name { get; set; }
    public MeasuredIn MeasuredIn { get; set; } // weight, volume or "eaches"
    public decimal PricePerMeasurement { get; set; } //This will keep from storing pricePerCase and whatnot

    // Constructor
    public Ingredient(string? name, MeasuredIn measuredIn, decimal pricePerMeasurement)
    {
        Name = name;
        MeasuredIn = measuredIn;
        PricePerMeasurement = pricePerMeasurement;
    }
    // Methods
    public decimal PricePerPortion(decimal portion)
    {
        return PricePerMeasurement * portion;
    }


    // Overrides
    public override string ToString()
    {
        return $"Ingredient Name: {Name} " + $"{PricePerMeasurement} dollars per {MeasuredIn.ToString()}";
    }
}