using ClassScheduleSystem.Application.DTOs;
using ClassScheduleSystem.Application.Services;
using ClassScheduleSystem.Tests.Fakes;
using Xunit;


namespace ClassScheduleSystem.Tests;

public class ScheduleServiceTests
{
    [Fact]
    public async Task CreateLessonAsync_ValidRequest_AddsLessonToRepository()
    {
        // Arrange
        var repository = new FakeLessonRepository();
        var service = new ScheduleService(repository);

        var request = new CreateLessonRequest
        {
            Date = new DateOnly(2026, 11, 27),
            StartTime = new TimeOnly(9, 0),
            EndTime = new TimeOnly(10, 0),
            LessonType = "Lecture",
            GroupId = 1,
            TeacherId = 2,
            SubjectId = 3,
            ClassroomId = 4
        };

        // Act
        await service.CreateLessonAsync(request);

        // Assert
        var lessons = await repository.GetByGroupIdAsync(1);

        Assert.Single(lessons);
        Assert.Equal("Lecture", lessons[0].LessonType);
        Assert.Equal(2, lessons[0].TeacherId);
    }
}
