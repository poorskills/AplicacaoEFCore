using AplicacaoEFCore;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddFluentValidation(o => o.RegisterValidatorsFromAssemblyContaining<ClienteValidator>());
ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("pt-br");

builder.Services.AddDbContext<MeuDbContext>(options =>
{
    options.UseSqlServer("Data Source=Localhost;Initial Catalog=Test;User id=sa;Password=Master.007");
});


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
