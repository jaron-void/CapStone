namespace FinalPantry;



public class SampleData
{
    public static readonly List<string?> RecipeNames = new()
    {
        "Spaghetti", "Meatloaf", "Chicken Alfredo", "Lasagna", "Grilled Cheese", "Pancakes",
        "Burger", "Taco", "Pizza", "Salad", "Stir-Fry", "Omelette", "Chili", "Sushi",
        "Burrito", "Ramen", "Fried Chicken", "Sausage Sandwich", "Casserole", "Soup"
    };
    
    public static readonly List<string?> IngredientNames = new()
    {
        "pepper", "salt", "sugar", "flour", "butter", "eggs", "milk", "cheese", "chicken", "rice",
        "onion", "tomato", "garlic", "potato", "carrot", "lettuce", "bacon", "shrimp", "pasta", "bread"
    };

    public static readonly List<int> RandomNumbers = GenerateRandomNumbers(20, 0, 2);

    public static readonly List<decimal> RandomDecimals = GenerateRandomDecimals(20, 0.50m, 9.99m);

    private static List<int> GenerateRandomNumbers(int count, int minValue, int maxValue)
    {
        var numbers = new List<int>();
        var random = new Random();

        for (var i = 0; i < count; i++)
        {
            var randomNumber = random.Next(minValue, maxValue + 1); // Include maxValue in the range
            numbers.Add(randomNumber);
        }

        return numbers;
    }

    private static List<decimal> GenerateRandomDecimals(int count, decimal minValue, decimal maxValue)
    {
        var decimals = new List<decimal>();
        var random = new Random();

        for (var i = 0; i < count; i++)
        {
            var randomNumber = (decimal)(random.NextDouble() * (double)(maxValue - minValue) + (double)minValue);
            decimals.Add(randomNumber);
        }

        return decimals;
    }
}