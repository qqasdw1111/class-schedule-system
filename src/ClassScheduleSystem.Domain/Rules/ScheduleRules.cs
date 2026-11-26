using ClassScheduleSystem.Domain.Entities;
using ClassScheduleSystem.Domain.Exceptions;

namespace ClassScheduleSystem.Domain.Rules;

public static class ScheduleRules
{
    public static void ValidateLessonTime(Lesson lesson)
    {
        if (lesson.EndTime <= lesson.StartTime)
            throw new DomainException(
                "Lesson end time must be later than start time.");
    }

    public static void ValidateClassroomCapacity(Classroom classroom)
    {
        if (classroom.Capacity <= 0)
            throw new DomainException(
                "Classroom capacity must be greater than zero.");
    }

    public static void CheckConflicts(
        Lesson newLesson,
        IEnumerable<Lesson> existingLessons)
    {
        foreach (var lesson in existingLessons)
        {
            if (lesson.LessonId == newLesson.LessonId ||
                lesson.Date != newLesson.Date)
                continue;

            bool overlaps =
                newLesson.StartTime < lesson.EndTime &&
                newLesson.EndTime > lesson.StartTime;

            if (!overlaps)
                continue;

            if (newLesson.TeacherId == lesson.TeacherId)
                throw new DomainException(
                    "Teacher already has a lesson at this time.");

            if (newLesson.GroupId == lesson.GroupId)
                throw new DomainException(
                    "Study group already has a lesson at this time.");

            if (newLesson.ClassroomId == lesson.ClassroomId)
                throw new DomainException(
                    "Classroom is already occupied at this time.");
        }
    }
}
