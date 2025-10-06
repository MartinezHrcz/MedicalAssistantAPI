using Api.DB;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<MedicalDataContext>(
    options =>
    {
       options.UseSqlite(builder.Configuration.GetConnectionString("SQLite")); 
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(); 
}

app.MapControllers();
app.UseHttpsRedirection();

app.Run();