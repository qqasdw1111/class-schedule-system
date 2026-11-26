using ClassScheduleSystem.Application.DTOs;
using ClassScheduleSystem.Application.Interfaces;
using ClassScheduleSystem.Domain.Entities;
using ClassScheduleSystem.Domain.Exceptions;
using ClassScheduleSystem.Domain.Rules;

namespace ClassScheduleSystem.Application.Services;

public class ScheduleService
{
    private readonly ILessonRepository _repository;

    public ScheduleService(ILessonRepository repository)
    {
        _repository = repository;
    }

    public async Task CreateLessonAsync(CreateLessonRequest request)
    {
        var lesson = new Lesson
        {
            Date = request.Date,
            StartTime = request.StartTime,
            EndTime = request.EndTime,
            LessonType = request.LessonType,
            GroupId = request.GroupId,
            TeacherId = request.TeacherId,
            SubjectId = request.SubjectId,
            ClassroomId = request.ClassroomId
        };

        ScheduleRules.ValidateLessonTime(lesson);

        if (await _repository.HasConflictsAsync(lesson))
        {
            throw new DomainException(
                "The lesson conflicts with an existing schedule.");
        }

        await _repository.AddAsync(lesson);
    }

    public async Task<IReadOnlyList<Lesson>> GetGroupScheduleAsync(
        int groupId)
    {
        if (groupId <= 0)
            throw new DomainException("Invalid study group ID.");

        return await _repository.GetByGroupIdAsync(groupId);
    }

    public async Task<IReadOnlyList<Lesson>> GetTeacherScheduleAsync(
        int teacherId)
    {
        if (teacherId <= 0)
            throw new DomainException("Invalid teacher ID.");

        return await _repository.GetByTeacherIdAsync(teacherId);
    }
}
