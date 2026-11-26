namespace ClassScheduleSystem.Domain.Entities;

public class StudyGroup
{
    public int GroupId { get; set; }
    public required string Name { get; set; }
    public required string Specialty { get; set; }
    public ICollection<Student> Students { get; set; } = new List<Student>();
}
