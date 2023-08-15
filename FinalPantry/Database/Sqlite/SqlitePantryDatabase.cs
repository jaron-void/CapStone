using Pantry.Core.Recipe;
using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace Pantry.Core.Database.Sqlite
{
    internal class SqlitePantryDatabase : IPantryDatabase
    {
        
        // Ingredients
        public void CreateIngredientTable(string connectionString = DB.ConnectionString)
        {
            const string createTableQuery = @"CREATE TABLE IF NOT EXISTS Ingredient (
                Name TEXT PRIMARY KEY,
                MeasuredIn INTEGER,
                PricePerMeasurement DECIMAL
            );";

            DB.CreateTable(createTableQuery, connectionString);
        }

        public void DeleteIngredient(IIngredient ingredient)
        {
            const string deleteCommandText = "DELETE FROM Ingredient WHERE Name = @name;";

            SQLiteParameter[] parameters =
            {
                new SQLiteParameter("@name", ingredient.Name)
            };

            DB.ExecuteNonQuery(deleteCommandText, parameters);
        }

        public IEnumerable<IIngredient> GetAllIngredients()
        {
            const string selectCommandText = "SELECT * FROM Ingredient;";

            List<IIngredient> ingredients = new List<IIngredient>();

            using SQLiteDataReader reader = DB.ExecuteReader(selectCommandText, Array.Empty<SQLiteParameter>());
            while (reader.Read())
            {
                string name = reader.GetString(reader.GetOrdinal("Name"));
                MeasuredIn measuredIn = (MeasuredIn)reader.GetInt32(reader.GetOrdinal("MeasuredIn"));
                decimal pricePerMeasurement = reader.GetDecimal(reader.GetOrdinal("PricePerMeasurement"));

                Ingredient ingredient = new Ingredient(name, measuredIn, pricePerMeasurement);
                ingredients.Add(ingredient);
            }

            return ingredients;
        }

        public void InsertIngredient(IIngredient ingredient)
        {

            const string insertCommandText = "INSERT INTO Ingredient (Name, MeasuredIn, PricePerMeasurement) " +
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
                                             "WHERE Name = @name;";

            SQLiteParameter[] parameters =
            {
                new SQLiteParameter("@measuredIn", (int)ingredient.MeasuredIn),
                new SQLiteParameter("@pricePerMeasurement", ingredient.PricePerMeasurement),
                new SQLiteParameter("@name", ingredient.Name)
            };

            DB.ExecuteNonQuery(updateCommandText, parameters);
        }

        // Measurements
        public void CreateMeasurementTable(string connectionString = DB.ConnectionString)
        {
            const string createTableQuery = @"CREATE TABLE IF NOT EXISTS Measurement (
                RecipeName TEXT,
                IngredientName TEXT,
                MeasuredIn INTEGER,
                Amount DECIMAL,
                PRIMARY KEY (RecipeName, IngredientName),
                FOREIGN KEY (RecipeName) REFERENCES Recipe(Name) ON DELETE CASCADE ON UPDATE CASCADE,
                FOREIGN KEY (IngredientName) REFERENCES Ingredient(Name) ON DELETE CASCADE ON UPDATE CASCADE
            );";

            DB.CreateTable(createTableQuery, connectionString);
        }

        public void InsertMeasurement(IMeasurement measurement)
        {
            const string insertCommandText = "INSERT INTO Measurement (RecipeName, IngredientName, MeasuredIn, Amount) " +
                                             "VALUES (@recipeName, @ingredientName, @measuredIn, @amount);";

            SQLiteParameter[] parameters =
            {
                new SQLiteParameter("@recipeName", measurement.RecipeName),
                new SQLiteParameter("@ingredientName", measurement.Ingredient.Name),
                new SQLiteParameter("@measuredIn", (int)measurement.MeasuredIn),
                new SQLiteParameter("@amount", measurement.Amount)
            };

            DB.ExecuteNonQuery(insertCommandText, parameters);
        }

        /*private void UpdateMeasurement(IMeasurement measurement)
        {
            const string updateMeasurementCommandText = "UPDATE Measurement SET MeasuredIn = @measuredIn, Amount = @amount " +
                                                        "WHERE RecipeName = @recipeName AND IngredientName = @ingredientName;";

            SQLiteParameter[] measurementParameters =
            {
                new SQLiteParameter("@measuredIn", (int)measurement.MeasuredIn),
                new SQLiteParameter("@amount", measurement.Amount),
                new SQLiteParameter("@recipeName", measurement.RecipeName),
                new SQLiteParameter("@ingredientName", measurement.Ingredient.Name)
            };

            DB.ExecuteNonQuery(updateMeasurementCommandText, measurementParameters);
        }

        private void DeleteMeasurement(IMeasurement measurement)
        {
            const string deleteMeasurementCommandText = "DELETE FROM Measurement WHERE RecipeName = @recipeName AND IngredientName = @ingredientName;";
            SQLiteParameter[] measurementParameters =
            {
                new SQLiteParameter("@recipeName", measurement.RecipeName),
                new SQLiteParameter("@ingredientName", measurement.Ingredient.Name)
            };

            DB.ExecuteNonQuery(deleteMeasurementCommandText, measurementParameters);
        }*/
        // Recipes
        public void CreateRecipeTable(string connectionString = DB.ConnectionString)
        {
            const string createTableQuery = @"CREATE TABLE IF NOT EXISTS Recipe (
                Name TEXT PRIMARY KEY,
                ServingsPerRecipe INTEGER
            );";

            DB.CreateTable(createTableQuery, connectionString);
        }

        public void InsertRecipe(IRecipe recipe)
        {
            const string insertRecipeCommandText = "INSERT INTO Recipe (Name, ServingsPerRecipe) " +
                                                   "VALUES (@name, @servingsPerRecipe);";

            SQLiteParameter[] recipeParameters =
            {
                new SQLiteParameter("@name", recipe.Name),
                new SQLiteParameter("@servingsPerRecipe", recipe.ServingsPerRecipe)
            };

            DB.ExecuteNonQuery(insertRecipeCommandText, recipeParameters);

            foreach (IMeasurement measurement in recipe.Measurements)
            {
                InsertMeasurement(measurement);
            }
        }

        public void UpdateRecipe(IRecipe recipe)
        {
            const string updateRecipeCommandText = "UPDATE Recipe SET ServingsPerRecipe = @servingsPerRecipe " +
                                                   "WHERE Name = @name;";

            SQLiteParameter[] recipeParameters =
            {
                new SQLiteParameter("@servingsPerRecipe", recipe.ServingsPerRecipe),
                new SQLiteParameter("@name", recipe.Name)
            };

            DB.ExecuteNonQuery(updateRecipeCommandText, recipeParameters);

            /*foreach (IMeasurement measurement in recipe.Measurements)
            {
                UpdateMeasurement(measurement);
            }*/
        }

        public void DeleteRecipe(IRecipe recipe)
        {
            const string deleteRecipeCommandText = "DELETE FROM Recipe WHERE Name = @name;";
            SQLiteParameter[] recipeParameters = { new SQLiteParameter("@name", recipe.Name) };

            DB.ExecuteNonQuery(deleteRecipeCommandText, recipeParameters);

            /*foreach (IMeasurement measurement in recipe.Measurements)
            {
                DeleteMeasurement(measurement);
            }*/
        }

        public IEnumerable<IRecipe> GetAllRecipes()
        {
            throw new NotImplementedException();
        }

        // Additional methods for working with measurements


        // Private method to create the necessary tables
        private void CreateTable()
        {
            CreateIngredientTable();
            CreateMeasurementTable();
            CreateRecipeTable();
        }
    }
}
