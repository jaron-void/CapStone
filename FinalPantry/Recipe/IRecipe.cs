namespace Pantry.Core.Recipe;

public interface IRecipe
{
    string? Name { get; set; }
    List<IMeasurement>? Measurements { get; set; }
    List<string>? Instructions { get; set; }
    int ServingsPerRecipe { get; set; }
    
    // Derived Properties
    float TotalPriceForRecipe { get; }
    
    float PricePerServing { get; }
    // Additional methods
    string InstructionsToJson();
    
    // overrides
    string ToString();

}