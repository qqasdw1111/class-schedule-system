
using ClassScheduleSystem.Application.Interfaces;
using ClassScheduleSystem.Domain.Entities;

namespace ClassScheduleSystem.Tests.Fakes;

public class FakeLessonRepository : ILessonRepository
{
    private readonly List<Lesson> _lessons = new();

    public Task<IReadOnlyList<Lesson>> GetByGroupIdAsync(int groupId)
    {
        IReadOnlyList<Lesson> result = _lessons
            .Where(lesson => lesson.GroupId == groupId)
            .ToList();

        return Task.FromResult(result);
    }

    public Task<IReadOnlyList<Lesson>> GetByTeacherIdAsync(int teacherId)
    {
        IReadOnlyList<Lesson> result = _lessons
            .Where(lesson => lesson.TeacherId == teacherId)
            .ToList();

        return Task.FromResult(result);
    }

    public Task AddAsync(Lesson lesson)
    {
        _lessons.Add(lesson);
        return Task.CompletedTask;
    }

    public Task<bool> HasConflictsAsync(Lesson lesson)
    {
        return Task.FromResult(false);
    }
}
