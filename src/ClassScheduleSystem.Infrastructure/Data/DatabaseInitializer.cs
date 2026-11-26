using Microsoft.Data.Sqlite;
using ClassScheduleSystem.Infrastructure.Exceptions;

namespace ClassScheduleSystem.Infrastructure.Data;

public class DatabaseInitializer
{
    private readonly string _connectionString;

    public DatabaseInitializer(string connectionString)
    {
        _connectionString = connectionString;
    }

   
public async Task InitializeAsync()
{
    try
    {
        await using var connection =
            new SqliteConnection(_connectionString);

        await connection.OpenAsync();

        var command = connection.CreateCommand();

        command.CommandText = """
            CREATE TABLE IF NOT EXISTS Lessons (
                LessonId INTEGER PRIMARY KEY AUTOINCREMENT,
                Date TEXT NOT NULL,
                StartTime TEXT NOT NULL,
                EndTime TEXT NOT NULL,
                LessonType TEXT NOT NULL,
                GroupId INTEGER NOT NULL,
                TeacherId INTEGER NOT NULL,
                SubjectId INTEGER NOT NULL,
                ClassroomId INTEGER NOT NULL
            );
            """;

        await command.ExecuteNonQueryAsync();
    }
    catch (SqliteException ex)
    {
        throw new DataAccessException(
            "Could not initialize the database.", ex);
    }
}
}

