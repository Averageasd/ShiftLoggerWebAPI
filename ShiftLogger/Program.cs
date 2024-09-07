using ShiftLogger.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddScoped<ShiftService, ShiftService>();
var app = builder.Build();
app.MapControllers();

app.Run();
