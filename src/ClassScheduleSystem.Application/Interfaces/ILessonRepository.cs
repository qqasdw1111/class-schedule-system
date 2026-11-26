using ClassScheduleSystem.Domain.Entities;

namespace ClassScheduleSystem.Application.Interfaces;

public interface ILessonRepository
{
    Task<IReadOnlyList<Lesson>> GetByGroupIdAsync(int groupId);

    Task<IReadOnlyList<Lesson>> GetByTeacherIdAsync(int teacherId);

    Task AddAsync(Lesson lesson);

    Task<bool> HasConflictsAsync(Lesson lesson);
}
