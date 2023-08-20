using System.Text.Json;

namespace Pantry.Core.Recipe;

public class Recipe : IRecipe
{
    // Properties
    public string? Name { get; set; }
    public List<IMeasurement>? Measurements { get; set; }
    public List<string>? Instructions { get; set; }
    public int ServingsPerRecipe { get; set; }
    
    // Derived Property
    public float TotalPriceForRecipe => Measurements.Sum(measurement => (float)measurement.Price);

    public float PricePerServing => TotalPriceForRecipe / ServingsPerRecipe;
    // Constructor
    public Recipe(string? name, List<IMeasurement>? measurements, List<string> instructions, int servingsPerRecipe)
    {
        Name = name;
        Measurements = measurements;
        Instructions = instructions;
        ServingsPerRecipe = servingsPerRecipe;
    }
    
    // Method to convert Instructions to JSON
    public string InstructionsToJson()
    {
        // Serialize Instructions list to JSON
        return JsonSerializer.Serialize(Instructions);
    }
    
    // Overrides
    public override string ToString()
    {
        return $"Recipe Name: {Name}. Costs : {TotalPriceForRecipe}" + 
               $"It makes {ServingsPerRecipe} servings costing  {PricePerServing} per serving";
    }
}