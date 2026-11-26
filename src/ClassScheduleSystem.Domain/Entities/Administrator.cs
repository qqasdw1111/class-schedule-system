namespace ClassScheduleSystem.Domain.Entities;

public class Administrator
{
    public int AdminId { get; set; }
    public required string FullName { get; set; }
    public required string Email { get; set; }
}
