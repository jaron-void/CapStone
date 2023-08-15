namespace Pantry.Core.Recipe;

public interface IRecipe
{
    string Name { get; set; }
    List<IMeasurement> Measurements { get; set; }
    List<string> Instructions { get; set; }
    int ServingsPerRecipe { get; set; }
    
    // Property for total price (remove derived property)
    float TotalPriceForRecipe { get; }
    
    // Additional methods
    string InstructionsToJson();
}