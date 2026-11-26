namespace ClassScheduleSystem.Domain.Entities;

public class Lesson
{
    public int LessonId { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public required string LessonType { get; set; }

    public int GroupId { get; set; }
    public int TeacherId { get; set; }
    public int SubjectId { get; set; }
    public int ClassroomId { get; set; }
}
