using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Trabalho_Tecnologias.Context;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<LocadoraContext>(options =>
    options.UseInMemoryDatabase(databaseName: "LocadoraDB"));


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowWebApp",
        policy =>
        {
            policy.AllowAnyOrigin()   
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<LocadoraContext>();
    DataSeeder.Seed(context);
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowWebApp");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();