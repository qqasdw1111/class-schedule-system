using ClassScheduleSystem.Application.Interfaces;
using ClassScheduleSystem.Domain.Entities;
using Microsoft.Data.Sqlite;
using ClassScheduleSystem.Infrastructure.Exceptions;

namespace ClassScheduleSystem.Infrastructure.Repositories;

public class SqliteLessonRepository : ILessonRepository
{
    private readonly string _connectionString;

    public SqliteLessonRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

  
public async Task AddAsync(Lesson lesson)
{
    try
    {
        await using var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync();

        var command = connection.CreateCommand();
        command.CommandText = """
            INSERT INTO Lessons
            (Date, StartTime, EndTime, LessonType,
             GroupId, TeacherId, SubjectId, ClassroomId)
            VALUES
            ($date, $start, $end, $type,
             $group, $teacher, $subject, $classroom);
            """;

        command.Parameters.AddWithValue("$date", lesson.Date.ToString("yyyy-MM-dd"));
        command.Parameters.AddWithValue("$start", lesson.StartTime.ToString("HH:mm"));
        command.Parameters.AddWithValue("$end", lesson.EndTime.ToString("HH:mm"));
        command.Parameters.AddWithValue("$type", lesson.LessonType);
        command.Parameters.AddWithValue("$group", lesson.GroupId);
        command.Parameters.AddWithValue("$teacher", lesson.TeacherId);
        command.Parameters.AddWithValue("$subject", lesson.SubjectId);
        command.Parameters.AddWithValue("$classroom", lesson.ClassroomId);

        await command.ExecuteNonQueryAsync();
    }
    catch (SqliteException ex)
    {
        throw new DataAccessException(
            "Could not save the lesson to the database.", ex);
    }
}


    public Task<IReadOnlyList<Lesson>> GetByGroupIdAsync(int groupId)
    {
        return GetLessonsAsync("GroupId", groupId);
    }

    public Task<IReadOnlyList<Lesson>> GetByTeacherIdAsync(int teacherId)
    {
        return GetLessonsAsync("TeacherId", teacherId);
    }

  
private async Task<IReadOnlyList<Lesson>> GetLessonsAsync(
    string column, int id)
{
    try
    {
        var lessons = new List<Lesson>();

        await using var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync();

        var command = connection.CreateCommand();
        command.CommandText = $"""
            SELECT LessonId, Date, StartTime, EndTime, LessonType,
                   GroupId, TeacherId, SubjectId, ClassroomId
            FROM Lessons
            WHERE {column} = $id
            ORDER BY Date, StartTime;
            """;

        command.Parameters.AddWithValue("$id", id);

        await using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            lessons.Add(new Lesson
            {
                LessonId = reader.GetInt32(0),
                Date = DateOnly.Parse(reader.GetString(1)),
                StartTime = TimeOnly.Parse(reader.GetString(2)),
                EndTime = TimeOnly.Parse(reader.GetString(3)),
                LessonType = reader.GetString(4),
                GroupId = reader.GetInt32(5),
                TeacherId = reader.GetInt32(6),
                SubjectId = reader.GetInt32(7),
                ClassroomId = reader.GetInt32(8)
            });
        }

        return lessons;
    }
    catch (SqliteException ex)
    {
        throw new DataAccessException(
            "Could not retrieve lessons from the database.", ex);
    }
}



public async Task<bool> HasConflictsAsync(Lesson lesson)
{
    try
    {
        await using var connection =
            new SqliteConnection(_connectionString);

        await connection.OpenAsync();

        var command = connection.CreateCommand();

        command.CommandText = """
            SELECT COUNT(*)
            FROM Lessons
            WHERE Date = $date
            AND time(StartTime) < time($end)
            AND time(EndTime) > time($start)
              AND (
                  TeacherId = $teacher
                  OR GroupId = $group
                  OR ClassroomId = $classroom
              );
            """;

        command.Parameters.AddWithValue(
            "$date", lesson.Date.ToString("yyyy-MM-dd"));
        command.Parameters.AddWithValue(
            "$start", lesson.StartTime.ToString("HH:mm"));
        command.Parameters.AddWithValue(
            "$end", lesson.EndTime.ToString("HH:mm"));
        command.Parameters.AddWithValue(
            "$teacher", lesson.TeacherId);
        command.Parameters.AddWithValue(
            "$group", lesson.GroupId);
        command.Parameters.AddWithValue(
            "$classroom", lesson.ClassroomId);

        var count =
            (long)(await command.ExecuteScalarAsync() ?? 0L);

        return count > 0;
    }
    catch (SqliteException ex)
    {
        throw new DataAccessException(
            "Could not check schedule conflicts in the database.", ex);
    }
}

}