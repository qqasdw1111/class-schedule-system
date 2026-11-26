namespace ClassScheduleSystem.Domain.Entities;

public class Student
{
    public int StudentId { get; set; }
    public required string FullName { get; set; }
    public required string Email { get; set; }
}
