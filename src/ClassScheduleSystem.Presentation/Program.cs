using ClassScheduleSystem.Application.Interfaces;
using ClassScheduleSystem.Application.Services;
using ClassScheduleSystem.Infrastructure.Data;
using ClassScheduleSystem.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

var connectionString =
    builder.Configuration.GetConnectionString("DefaultConnection")
    ?? "Data Source=classschedule.db";

builder.Services.AddSingleton(
    new DatabaseInitializer(connectionString));

builder.Services.AddScoped<ILessonRepository>(
    _ => new SqliteLessonRepository(connectionString));

builder.Services.AddScoped<ScheduleService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var initializer =
        scope.ServiceProvider.GetRequiredService<DatabaseInitializer>();

    await initializer.InitializeAsync();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
