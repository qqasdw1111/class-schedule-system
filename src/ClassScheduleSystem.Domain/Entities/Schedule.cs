namespace ClassScheduleSystem.Domain.Entities;

public class Schedule
{
    public int ScheduleId { get; set; }
    public required string Semester { get; set; }
    public required string AcademicYear { get; set; }
    public ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
}
