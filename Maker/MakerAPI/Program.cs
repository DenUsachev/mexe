using MakerAPI;
using MakerAPI.Contexts;
using MakerAPI.Services;
using Serilog;

// Host configuration
var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

// Add services to the container.
builder.Services.Configure<MakerSettings>(builder.Configuration.GetSection("MakerSettings"));
builder.Services.AddSingleton<IConnectionContext, BybitConnectionContext>();
builder.Services.AddSingleton<IExchangeService, BybitService>();
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Initialize Market Connection
app.UseMaker();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();