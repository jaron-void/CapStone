using System.Data.SQLite;

namespace Pantry.Core.Database;

public class DB
{
    internal const string ConnectionString = "Data Source=:memory:";
    
        // Returns an open SQLite Connection, don't forget to close it
    private static SQLiteConnection CreateConnection(string connectionString = ConnectionString) //an optional argument incase I need to change it in a different function
    {
        var connection = new SQLiteConnection(connectionString);
        connection.Open();
        return connection;
    }

    // Create a table with the "createTableQuery"
    internal static void CreateTable(string createTableQuery, string connectionString = ConnectionString)
    {
        using var connection = CreateConnection(connectionString);
        using var createTableCommand = new SQLiteCommand(createTableQuery, connection);
        createTableCommand.ExecuteNonQuery();
    }

    // Execute a non-query SQL command with parameters
    internal static void ExecuteNonQuery(string commandText, SQLiteParameter[] parameters, string connectionString = ConnectionString)
    {
        using var connection = CreateConnection(connectionString);
        using var command = new SQLiteCommand(connection);
        command.CommandText = commandText;
        command.Parameters.AddRange(parameters);

        command.ExecuteNonQuery();
    }

    // Execute a scalar SQL command with parameters
    internal static object ExecuteScalar(string commandText, SQLiteParameter[] parameters, string connectionString = ConnectionString)
    {
        using var connection = CreateConnection(connectionString);
        using var command = new SQLiteCommand(connection);
        command.CommandText = commandText;
        command.Parameters.AddRange(parameters);

        return command.ExecuteScalar();
    }

    // Execute a reader SQL command with parameters
    internal static SQLiteDataReader ExecuteReader(string commandText, SQLiteParameter[] parameters, string connectionString = ConnectionString)
    {
        var connection = CreateConnection(connectionString);
        var command = new SQLiteCommand(connection);
        command.CommandText = commandText;
        command.Parameters.AddRange(parameters);

        return command.ExecuteReader();
    }
}