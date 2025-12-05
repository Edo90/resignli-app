using Application.Settings;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Infrastructure;
using Infrastructure.Data;
using Infrastructure.Seed;
using Microsoft.EntityFrameworkCore;
using Domain.Interfaces;
using Domain.Entities;
using Application;
using Api.Mapping;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
	c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme()
	{
		Description = "JWT Authorization header. Like: Bearer {token}",
		Name = "Authorization",
		In = Microsoft.OpenApi.Models.ParameterLocation.Header,
		Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
		Scheme = "bearer"
	});

	c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
	{
		{
			new OpenApiSecurityScheme
			{
				Reference = new OpenApiReference
				{
					Type = ReferenceType.SecurityScheme,
					Id = "Bearer"
				}
			},
			Array.Empty<string>()
		}
	});
});

var jwtSection = builder.Configuration.GetSection("JwtSettings");
var databaseSettingsSection = builder.Configuration.GetSection("DatabaseSettings");

builder.Services.Configure<JwtSettings>(jwtSection);
builder.Services.Configure<DatabaseSettings>(databaseSettingsSection);

var jwtSettings = jwtSection.Get<JwtSettings>()!;
var databaseSettings = databaseSettingsSection.Get<DatabaseSettings>()!;

builder.Services.AddAuthentication("Bearer")
	.AddJwtBearer("Bearer", options =>
	{
		options.TokenValidationParameters = new TokenValidationParameters
		{
			ValidateIssuer = true,
			ValidateAudience = true,
			ValidateIssuerSigningKey = true,
			ValidIssuer = jwtSettings.Issuer,
			ValidAudience = jwtSettings.Audience,
			IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key))
		};
	});



builder.Services.AddApplication();
builder.Services.AddInfrastructure(databaseSettings.Name);
builder.Services.AddAutoMapper(typeof(ApiMappingProfile));

var app = builder.Build();

//seeding the database
using(var scope = app.Services.CreateScope())
{
	var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
	await AppDbContextSeed.SeedAsync(db);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

////this is just for testing
//app.MapGet("test/employees", async (AppDbContext db) =>
//{
//	var users = await db.Employees.ToListAsync();
//	return Results.Ok(users);
//});

//app.MapGet("test/users", async (AppDbContext db) =>
//{
//	var employees = await db.Users.ToListAsync();
//	return Results.Ok(employees);
//});

//app.MapGet("test/repo/employees", async (IEmployeeRepository repo) =>
//{
//	var employee = new Employee { IdentityNumber = "123", Name = "testrepo", Birthdate = new DateTime(), Email = "test@test.com" };

//	var createdEmployee = await repo.AddAsync(employee);
//	return Results.Ok(createdEmployee);
//});
app.Run();
