namespace ClassScheduleSystem.Domain.Entities;

public class Classroom
{
    public int ClassroomId { get; set; }
    public required string Number { get; set; }
    public int Capacity { get; set; }
}
