using ClothesWebAPI.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//Connect Database
var configDbString = builder.Configuration.GetConnectionString("ClothesData");
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(configDbString));

// Add services to the container.
builder.Services.AddControllersWithViews();

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

//Check Connection to DB
var databaseTest = new CheckConnectDb(app.Configuration);
databaseTest.TestConnection();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors(builder => builder
.AllowAnyHeader()
.AllowAnyMethod()
.AllowAnyOrigin());

app.MapControllers();

app.Run();

