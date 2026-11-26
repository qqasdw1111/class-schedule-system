using ClassScheduleSystem.Domain.Entities;
using ClassScheduleSystem.Domain.Exceptions;
using ClassScheduleSystem.Domain.Rules;
using Xunit;

namespace ClassScheduleSystem.Tests;

public class ScheduleRulesTests
{
    [Fact]
    public void ValidateLessonTime_ValidTime_DoesNotThrow()
    {
        var lesson = new Lesson
        {
            LessonId = 1,
            Date = new DateOnly(2026, 11, 26),
            StartTime = new TimeOnly(9, 0),
            EndTime = new TimeOnly(10, 0),
            LessonType = "Lecture"
        };

        ScheduleRules.ValidateLessonTime(lesson);
    }

    [Fact]
    public void ValidateLessonTime_InvalidTime_ThrowsException()
    {
        var lesson = new Lesson
        {
            LessonId = 1,
            Date = new DateOnly(2026, 11, 26),
            StartTime = new TimeOnly(10, 0),
            EndTime = new TimeOnly(9, 0),
            LessonType = "Lecture"
        };

        Assert.Throws<DomainException>(
            () => ScheduleRules.ValidateLessonTime(lesson));
    }

    [Fact]
    public void ValidateClassroomCapacity_PositiveCapacity_DoesNotThrow()
    {
        var classroom = new Classroom
        {
            ClassroomId = 1,
            Number = "101",
            Capacity = 30
        };

        ScheduleRules.ValidateClassroomCapacity(classroom);
    }

    [Fact]
    public void CheckConflicts_NoOverlap_DoesNotThrow()
    {
        var existingLesson = CreateLesson(
            1, 1, 1, 1, 1, 9, 0, 10, 0);

        var newLesson = CreateLesson(
            2, 1, 1, 1, 1, 10, 0, 11, 0);

        ScheduleRules.CheckConflicts(
            newLesson, new[] { existingLesson });
    }

    [Fact]
    public void CheckConflicts_SameTeacher_ThrowsException()
    {
        var existingLesson = CreateLesson(
            1, 1, 1, 1, 1, 9, 0, 10, 30);

        var newLesson = CreateLesson(
            2, 1, 1, 2, 2, 10, 0, 11, 0);

        Assert.Throws<DomainException>(() =>
            ScheduleRules.CheckConflicts(
                newLesson, new[] { existingLesson }));
    }

    private static Lesson CreateLesson(
        int id,
        int groupId,
        int teacherId,
        int subjectId,
        int classroomId,
        int startHour,
        int startMinute,
        int endHour,
        int endMinute)
    {
        return new Lesson
        {
            LessonId = id,
            Date = new DateOnly(2026, 11, 26),
            StartTime = new TimeOnly(startHour, startMinute),
            EndTime = new TimeOnly(endHour, endMinute),
            LessonType = "Lecture",
            GroupId = groupId,
            TeacherId = teacherId,
            SubjectId = subjectId,
            ClassroomId = classroomId
        };
    }
}
