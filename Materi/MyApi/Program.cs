using Microsoft.EntityFrameworkCore;
using MyApi.Databases;
using MyApi.Mapper;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContex>(options =>
{
	options.UseSqlite("FileName=./MyDatabase.db");
});
builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(MapProfile));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();



app.Run();


