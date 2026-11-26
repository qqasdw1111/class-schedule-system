namespace ClassScheduleSystem.Application.DTOs;

public class CreateLessonRequest
{
    public DateOnly Date { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public string LessonType { get; set; } = string.Empty;
    public int GroupId { get; set; }
    public int TeacherId { get; set; }
    public int SubjectId { get; set; }
    public int ClassroomId { get; set; }
}
