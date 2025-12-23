using Microsoft.EntityFrameworkCore;
using Train.Mapper;
using Train.Models.Repository;
using Train.Models.Repository.Managers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//------------------------------Connection DB------------------------------
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<TrainDbContext>(options =>
    options.UseNpgsql(connectionString));

//------------------------------Mapper------------------------------
builder.Services.AddAutoMapper(typeof(MapperProfile));

//------------------------------Managers (DI)------------------------------
builder.Services.AddScoped<TrainManager>();
builder.Services.AddScoped<CompagnieManager>();

//------------------------------JSON Options------------------------------
builder.Services.AddControllers().AddJsonOptions(opt =>
{
    opt.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});

//------------------------------CORS------------------------------
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();