using AutoMapper;
using Items.DB;
using Microsoft.EntityFrameworkCore;
using User.API.Mappings;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services
    .AddControllersWithViews()
    .AddNewtonsoftJson();


builder.Services.AddDbContext<MainContext>(options =>
    options
        .UseNpgsql("Server=localhost;Port=5432;Database=zmokers; User Id=postgres;Password=postgres")
        .UseSnakeCaseNamingConvention()
    );

builder.Services.AddSingleton(provider => new MapperConfiguration(cfg =>
{
    cfg.AddProfile(provider.GetService<MappingProfile>());
}
).CreateMapper());

builder.Services.AddSingleton<MappingProfile>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
