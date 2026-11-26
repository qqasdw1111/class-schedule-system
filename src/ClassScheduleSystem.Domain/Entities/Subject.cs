namespace ClassScheduleSystem.Domain.Entities;

public class Subject
{
    public int SubjectId { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
}
