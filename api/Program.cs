using api.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddHttpClient();

/// sql server connection
/// 

builder.Services.AddDbContext<dbContext>(options =>
{
    options.UseSqlite("Data Source=judge.db");
});


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});
var app = builder.Build();


    app.UseSwagger();
    app.UseSwaggerUI();




app.UseHttpsRedirection();
app.MapControllers();


app.UseCors("AllowAllOrigins");
app.Run();

