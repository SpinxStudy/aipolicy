using AIPolicy.Application.Mapper;
using AIPolicy.Application.Service;
using AIPolicy.Core.Interface.Repository;
using AIPolicy.Infrastructure.Persistency.Repository;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "AIPolicy API", Version = "v1" });
});

// Dependencies
builder.Services.AddScoped<ITriggerRepository, TriggerRepository>();
builder.Services.AddScoped<TriggerService>();
builder.Services.AddScoped<IPolicyRepository, PolicyRepository>();
builder.Services.AddScoped<IPolicyService, PolicyService>();

// AutoMapper
builder.Services.AddAutoMapper(typeof(PolicyMapperProfile));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSwagger", policy =>
    {
        policy.WithOrigins("https://localhost:7073") // Origem do Swagger
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowSwagger");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
