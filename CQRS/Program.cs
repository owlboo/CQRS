using CQRS;
using CQRS.Application.Middleware;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddCustomizedMasstransit(builder.Configuration)   
    .AddCustomizedDbContext(builder.Configuration)
    .AddCustomizedOption(builder.Configuration)
    .AddCustomizedAutoMapper()
    .AddMediatR(cfg=>cfg.RegisterServicesFromAssembly(typeof(Program).Assembly))
    .AddValidatorsFromAssembly(typeof(Program).Assembly)
    .AddServices()
    ;



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


//app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
