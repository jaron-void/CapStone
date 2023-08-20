using Pantry.Core.Recipe;
using System.Data.SQLite;
using System.Text.Json;
using OfficeOpenXml;

namespace Pantry.Core.Database.Sqlite
{
    public class SqlitePantryDatabase : IPantryDatabase
    {
        
        // Ingredients
        public void CreateIngredientTable()
        {
            const string createTableQuery = @"CREATE TABLE IF NOT EXISTS Ingredient (
                IngredientName TEXT PRIMARY KEY,
                MeasuredIn INTEGER,
                PricePerMeasurement DECIMAL
            );";

            DB.CreateTable(createTableQuery);
        }

        public void DeleteIngredient(IIngredient ingredient)
        {
            const string deleteCommandText = "DELETE FROM Ingredient WHERE IngredientName = @name;";

            SQLiteParameter[] parameters =
            {
                new SQLiteParameter("@name", ingredient.Name)
            };

            DB.ExecuteNonQuery(deleteCommandText, parameters);
        }

        private IIngredient? GetIngredientByName(string? ingredientName)
        {
            const string selectCommandText = "SELECT * FROM Ingredient WHERE IngredientName = @name;";

            SQLiteParameter[] ingredientParameters = { new SQLiteParameter("@name", ingredientName) };

            using var reader = DB.ExecuteReader(selectCommandText, ingredientParameters);

            if (!reader.Read()) return null; // Ingredient not found
            var measuredIn = (MeasuredIn)reader.GetInt32(reader.GetOrdinal("MeasuredIn"));
            var pricePerMeasurement = reader.GetDecimal(reader.GetOrdinal("PricePerMeasurement"));

            IIngredient? ingredient = new Ingredient(ingredientName, measuredIn, pricePerMeasurement);
            return ingredient;

        }
        public IEnumerable<IIngredient> GetAllIngredients()
        {
            const string selectCommandText = "SELECT * FROM Ingredient;";

            var ingredients = new List<IIngredient>();

            using var reader = DB.ExecuteReader(selectCommandText, Array.Empty<SQLiteParameter>());
            while (reader.Read())
            {
                var name = reader.GetString(reader.GetOrdinal("IngredientName"));
                var measuredIn = (MeasuredIn)reader.GetInt32(reader.GetOrdinal("MeasuredIn"));
                var pricePerMeasurement = reader.GetDecimal(reader.GetOrdinal("PricePerMeasurement"));

                var ingredient = new Ingredient(name, measuredIn, pricePerMeasurement);
                ingredients.Add(ingredient);
            }

            return ingredients;
        }

        public void InsertIngredient(IIngredient ingredient)
        {

            const string insertCommandText = "INSERT INTO Ingredient (IngredientName, MeasuredIn, PricePerMeasurement) " +
                                             "VALUES (@name, @measuredIn, @pricePerMeasurement);";

            SQLiteParameter[] parameters =
            {
                new SQLiteParameter("@name", ingredient.Name),
                new SQLiteParameter("@measuredIn", (int)ingredient.MeasuredIn),
                new SQLiteParameter("@pricePerMeasurement", ingredient.PricePerMeasurement)
            };

            DB.ExecuteNonQuery(insertCommandText, parameters);
        }

        public void UpdateIngredient(IIngredient ingredient)
        {
            const string updateCommandText = "UPDATE Ingredient SET MeasuredIn = @measuredIn, PricePerMeasurement = @pricePerMeasurement " +
                                             "WHERE IngredientName = @name;";

            SQLiteParameter[] parameters =
            {
                new SQLiteParameter("@measuredIn", (int)ingredient.MeasuredIn),
                new SQLiteParameter("@pricePerMeasurement", ingredient.PricePerMeasurement),
                new SQLiteParameter("@name", ingredient.Name)
            };

            DB.ExecuteNonQuery(updateCommandText, parameters);
        }
        
        public void ExportIngredientsToExcel(string excelFilePath)
        {
            using var package = new ExcelPackage();
            ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Ingredients");

            // Set column headers
            worksheet.Cells["A1"].Value = "Name";
            worksheet.Cells["B1"].Value = "Measured In";
            worksheet.Cells["C1"].Value = "Price Per Measurement";

            var row = 2;

            using (var reader = DB.ExecuteReader("SELECT * FROM Ingredient;", Array.Empty<SQLiteParameter>()))
            {
                while (reader.Read())
                {
                    var name = reader.GetString(0);
                    var measuredInValue = reader.GetInt32(1); // Store the integer value for now
                    var pricePerMeasurement = reader.GetDecimal(2);

                    var measuredIn = Enum.GetName(typeof(MeasuredIn), measuredInValue); // Get the enum name

                    var ingredient = new Ingredient(name, (MeasuredIn)measuredInValue, pricePerMeasurement);

                    // Populate the Excel rows with ingredient data
                    worksheet.Cells[row, 1].Value = ingredient.Name;
                    worksheet.Cells[row, 2].Value = measuredIn; // Use the enum name
                    worksheet.Cells[row, 3].Value = ingredient.PricePerMeasurement;

                    row++;
                }
            }

            package.SaveAs(new FileInfo(excelFilePath));
        }

        // Measurements
        public void CreateMeasurementTable()
        {
            const string createTableQuery = @"CREATE TABLE IF NOT EXISTS Measurement (
                RecipeName TEXT,
                IngredientName TEXT,
                MeasuredIn INTEGER,
                Amount DECIMAL,
                FOREIGN KEY (RecipeName) REFERENCES Recipe(RecipeName) ON DELETE CASCADE ON UPDATE CASCADE,
                FOREIGN KEY (IngredientName) REFERENCES Ingredient(IngredientName) ON DELETE CASCADE ON UPDATE CASCADE
            );";

            DB.CreateTable(createTableQuery);
        }

        public void InsertMeasurement(IMeasurement measurement)
        {
            const string insertCommandText = "INSERT INTO Measurement (RecipeName, IngredientName, MeasuredIn, Amount) " +
                                             "VALUES (@recipeName, @ingredientName, @measuredIn, @amount);";

            SQLiteParameter[] parameters =
            {
                new SQLiteParameter("@recipeName", measurement.RecipeName),
                new SQLiteParameter("@ingredientName", measurement.Ingredient?.Name),
                new SQLiteParameter("@measuredIn", (int)measurement.MeasuredIn),
                new SQLiteParameter("@amount", measurement.Amount)
            };

            DB.ExecuteNonQuery(insertCommandText, parameters);
        }
        
        // Recipes
        public void CreateRecipeTable()
        {
            const string createTableQuery = @"CREATE TABLE IF NOT EXISTS Recipe (
                RecipeName TEXT PRIMARY KEY,
                ServingsPerRecipe INTEGER,
                Instructions TEXT,
                TotalPriceForRecipe INT,
                PricePerServing INT
            );";

            DB.CreateTable(createTableQuery);
        }

        public void InsertRecipe(IRecipe recipe)
        {
            const string insertRecipeCommandText = "INSERT INTO Recipe (RecipeName, ServingsPerRecipe, Instructions, TotalPriceForRecipe, PricePerServing) " +
                                                   "VALUES (@name, @servingsPerRecipe, @instructions, @totalPriceForRecipe, @pricePerServing);";

            var instructions = recipe.InstructionsToJson();
            
            SQLiteParameter[] recipeParameters =
            {
                new SQLiteParameter("@name", recipe.Name),
                new SQLiteParameter("@servingsPerRecipe", recipe.ServingsPerRecipe),
                new SQLiteParameter("@instructions", instructions),
                new SQLiteParameter("@totalPriceForRecipe", recipe.TotalPriceForRecipe),
                new SQLiteParameter("@pricePerServing", recipe.PricePerServing)

            };

            DB.ExecuteNonQuery(insertRecipeCommandText, recipeParameters);

            foreach (var measurement in recipe.Measurements)
            {
                InsertMeasurement(measurement);
            }
        }

        public void UpdateRecipe(IRecipe recipe)
        {
            const string updateRecipeCommandText = "UPDATE Recipe SET ServingsPerRecipe = @servingsPerRecipe, " +
                                                   "Instructions = @instructions, TotalPriceForRecipe = @totalPricePerRecipe, " +
                                                   "PricePerServing = @pricePerServing " +
                                                   "WHERE RecipeName = @name";

            
            var instructions = recipe.InstructionsToJson();

            SQLiteParameter[] recipeParameters =
            {
                new SQLiteParameter("@servingsPerRecipe", recipe.ServingsPerRecipe),
                new SQLiteParameter("@instructions", instructions),
                new SQLiteParameter("@totalPriceForRecipe", recipe.TotalPriceForRecipe),
                new SQLiteParameter("@pricePerServing", recipe.PricePerServing),
                new SQLiteParameter("@name", recipe.Name)
            };

            DB.ExecuteNonQuery(updateRecipeCommandText, recipeParameters);
        }

        public void DeleteRecipe(IRecipe recipe)
        {
            const string deleteRecipeCommandText = "DELETE FROM Recipe WHERE RecipeName = @name;";
            SQLiteParameter[] recipeParameters = { new("@name", recipe.Name) };

            DB.ExecuteNonQuery(deleteRecipeCommandText, recipeParameters);
        }
        public IRecipe? GetRecipe(string? name)
        {
            const string selectCommandText = "SELECT * FROM Recipe WHERE RecipeName = @name;";

            SQLiteParameter[] recipeParameters = { new("@name", name) };

            using var reader = DB.ExecuteReader(selectCommandText, recipeParameters);

            if (!reader.Read()) return null; // Recipe not found
            var recipeName = reader.GetString(reader.GetOrdinal("RecipeName"));
            var servingsPerRecipe = reader.GetInt32(reader.GetOrdinal("ServingsPerRecipe"));
            var instructionsJson = reader.GetString(reader.GetOrdinal("Instructions"));
            var instructions = JsonSerializer.Deserialize<List<string>>(instructionsJson);
            var measurements = GetMeasurementsForRecipe(name).ToList();

            if (instructions == null) return null; // Recipe not found
            var recipe = new Recipe.Recipe(recipeName, measurements, instructions, servingsPerRecipe);

            return recipe;

        }

        public IEnumerable<IMeasurement> GetMeasurementsForRecipe(string? recipeName)
        {
            const string selectCommandText = "SELECT * FROM Measurement WHERE RecipeName = @recipeName;";

            SQLiteParameter[] measurementParameters = { new SQLiteParameter("@recipeName", recipeName) };

            using var reader = DB.ExecuteReader(selectCommandText, measurementParameters);

            while (reader.Read())
            {
                var ingredientName = reader.GetString(reader.GetOrdinal("IngredientName"));
                var measuredIn = (MeasuredIn)reader.GetInt32(reader.GetOrdinal("MeasuredIn"));
                var amount = reader.GetDecimal(reader.GetOrdinal("Amount"));

                var ingredient = GetIngredientByName(ingredientName);

                var measurement = new Measurement(recipeName, ingredient, measuredIn, amount);
                yield return measurement;
            }
        }

        public IEnumerable<IRecipe?> GetAllRecipes()
        {
            const string selectCommandText = "SELECT RecipeName FROM Recipe;";

            using var reader = DB.ExecuteReader(selectCommandText, Array.Empty<SQLiteParameter>());
            while (reader.Read())
            {
                var recipeName = reader.GetString(reader.GetOrdinal("RecipeName"));
                var recipe = GetRecipe(recipeName);
        
                if (recipe != null)
                {
                    yield return recipe;
                }
            }
        }
        
        public IEnumerable<IRecipe?> GetRecipesContainingIngredient(string ingredientName)
        {
            const string selectCommandText = "SELECT DISTINCT RecipeName FROM Measurement WHERE IngredientName = @ingredientName;";

            SQLiteParameter[] ingredientParameters = { new("@ingredientName", ingredientName) };

            var recipeNames = new List<string?>();

            using (var reader = DB.ExecuteReader(selectCommandText, ingredientParameters))
            {
                while (reader.Read())
                {
                    recipeNames.Add(reader.GetString(reader.GetOrdinal("RecipeName")));
                }
            }

            foreach (var recipe in recipeNames.Select(GetRecipe).Where(recipe => recipe != null))
            {
                yield return recipe;
            }
        }

        public IEnumerable<IRecipe> GetRecipesOrderedByPricePerServing()
        {
            var allRecipes = GetAllRecipes();
    
            IEnumerable<IRecipe> sortedRecipes = allRecipes.OrderBy(recipe => recipe.PricePerServing);
    
            return sortedRecipes;
        }
        // Private method to create the necessary tables
        public void CreateTable()
        {
            CreateIngredientTable();
            CreateMeasurementTable();
            CreateRecipeTable();
        }
    }
}
