using Microsoft.EntityFrameworkCore;
using ToDoAPI.DBContext;
using ToDoAPI.IHelper;
using ToDoAPI.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("conn")));
builder.Services.AddScoped<IToDo, ToDoRepository>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

    //using(var scope = app.Services.CreateAsyncScope())
    //{
    //    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    //    await dbContext.Database.EnsureCreatedAsync();
    //}

app.UseAuthorization();

app.MapControllers();

app.Run();
