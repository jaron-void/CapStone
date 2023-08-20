using System.Data.SQLite;

namespace Pantry.Core.Database;

public class DB
{
    private static readonly string DatabasePath = Path.Combine("..", "..","..", "Data", "PantryDatabase.db");
    public static readonly string ExcelFilePath = Path.Combine("..", "..", "..", "Output", "Ingredients.xlsx");

    private static readonly string DataFolderPath = Path.Combine("..", "..", "..", "Data");
    private static readonly string OutputFolderPath = Path.Combine("..", "..", "..", "Output");

    // Modify your ConnectionString to use the DatabasePath
    private static string ConnectionString => $"Data Source={DatabasePath}";

    public static void EnsureFoldersExist()
    {
        Directory.CreateDirectory(DataFolderPath);
        Directory.CreateDirectory(OutputFolderPath);
    }
    private static SQLiteConnection CreateConnection() 
    {
        var connection = new SQLiteConnection(ConnectionString);
        connection.Open();
        return connection;
    }

    // Create a table with the "createTableQuery"
    internal static void CreateTable(string createTableQuery)
    {
        using var connection = CreateConnection();
        using var createTableCommand = new SQLiteCommand(createTableQuery, connection);
        createTableCommand.ExecuteNonQuery();
    }

    // Execute a non-query SQL command with parameters
    internal static void ExecuteNonQuery(string commandText, SQLiteParameter[] parameters)
    {
        using var connection = CreateConnection();
        using var command = new SQLiteCommand(connection);
        command.CommandText = commandText;
        command.Parameters.AddRange(parameters);
        command.ExecuteNonQuery();
    }

    // Execute a scalar SQL command with parameters
    internal static object ExecuteScalar(string commandText, SQLiteParameter[] parameters)
    {
        using var connection = CreateConnection();
        using var command = new SQLiteCommand(connection);
        command.CommandText = commandText;
        command.Parameters.AddRange(parameters);
        return command.ExecuteScalar();
    }

    // Execute a reader SQL command with parameters
    internal static SQLiteDataReader ExecuteReader(string commandText, SQLiteParameter[] parameters)
    {
        var connection = CreateConnection();
        var command = new SQLiteCommand(connection);
        command.CommandText = commandText;
        command.Parameters.AddRange(parameters);
        return command.ExecuteReader();
    }
}