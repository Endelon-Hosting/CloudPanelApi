using CloudPanelApi.App.Services;
using Logging.Net;

Logger.Info("Starting cloud panel api server");

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<CliService>();

builder.Services.AddScoped<DatabaseService>();
builder.Services.AddScoped<SiteService>();
builder.Services.AddScoped<VHostTemplatesService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<LetsEncryptService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();