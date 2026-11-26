using ClassScheduleSystem.Application.DTOs;
using ClassScheduleSystem.Application.Services;
using ClassScheduleSystem.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace ClassScheduleSystem.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ScheduleController : ControllerBase
{
    private readonly ScheduleService _scheduleService;

    public ScheduleController(ScheduleService scheduleService)
    {
        _scheduleService = scheduleService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateLesson(
        [FromBody] CreateLessonRequest request)
    {
        try
        {
            await _scheduleService.CreateLessonAsync(request);

            return Ok(new
            {
                message = "Lesson created successfully."
            });
        }
        catch (DomainException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpGet("group/{groupId:int}")]
    public async Task<IActionResult> GetGroupSchedule(int groupId)
    {
        try
        {
            var lessons =
                await _scheduleService.GetGroupScheduleAsync(groupId);

            return Ok(lessons);
        }
        catch (DomainException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpGet("teacher/{teacherId:int}")]
    public async Task<IActionResult> GetTeacherSchedule(int teacherId)
    {
        if (teacherId <= 0)
            return BadRequest(new { error = "Invalid teacher ID." });

        var lessons =
            await _scheduleService.GetTeacherScheduleAsync(teacherId);

        return Ok(lessons);
    }
}
