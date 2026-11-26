namespace ClassScheduleSystem.Domain.Entities;

public class DepartmentStaff
{
    public int StaffId { get; set; }
    public required string FullName { get; set; }
    public required string Email { get; set; }
}
