namespace ClassScheduleSystem.Domain.Entities;

public class Teacher
{
    public int TeacherId { get; set; }
    public required string FullName { get; set; }
    public required string Email { get; set; }
}
