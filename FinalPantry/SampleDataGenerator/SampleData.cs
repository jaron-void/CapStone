namespace FinalPantry;



public class SampleData
{
    public static List<string> RecipeNames = new List<string>()
    {
        "Spaghetti", "Meatloaf", "Chicken Alfredo", "Lasagna", "Grilled Cheese", "Pancakes",
        "Burger", "Taco", "Pizza", "Salad", "Stir-Fry", "Omelette", "Chili", "Sushi",
        "Burrito", "Ramen", "Fried Chicken", "Sausage Sandwich", "Casserole", "Soup"
    };
    
    public static List<string> IngredientNames = new List<string>()
    {
        "pepper", "salt", "sugar", "flour", "butter", "eggs", "milk", "cheese", "chicken", "rice",
        "onion", "tomato", "garlic", "potato", "carrot", "lettuce", "bacon", "shrimp", "pasta", "bread"
    };

    public static List<int> RandomNumbers = GenerateRandomNumbers(20, 0, 2);

    public static List<decimal> RandomDecimals = GenerateRandomDecimals(20, 0.50m, 9.99m);

    private static List<int> GenerateRandomNumbers(int count, int minValue, int maxValue)
    {
        List<int> numbers = new List<int>();
        Random random = new Random();

        for (int i = 0; i < count; i++)
        {
            int randomNumber = random.Next(minValue, maxValue + 1); // Include maxValue in the range
            numbers.Add(randomNumber);
        }

        return numbers;
    }

    private static List<decimal> GenerateRandomDecimals(int count, decimal minValue, decimal maxValue)
    {
        List<decimal> decimals = new List<decimal>();
        Random random = new Random();

        for (int i = 0; i < count; i++)
        {
            decimal randomNumber = (decimal)(random.NextDouble() * (double)(maxValue - minValue) + (double)minValue);
            decimals.Add(randomNumber);
        }

        return decimals;
    }
}