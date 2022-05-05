using RadicarPolizaAPI.Data;
using RadicarPolizaAPI.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager Configuration = builder.Configuration;
// Add services to the container.
builder.Services.AddDbContext<PolizaContext>(options => options.UseSqlServer(Configuration.GetConnectionString("bdconn")));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
//builder.Services.AddMvc();
builder.Services.AddScoped<IPoliza,PolizaProvider>();

//Authentication and authorization
builder.Services.AddAuthentication(x =>
	{
		x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
		x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
	}).AddJwtBearer(o =>
	{
		var Key = Encoding.UTF8.GetBytes(Configuration["JWT:Key"]);
		o.SaveToken = true;
		o.TokenValidationParameters = new TokenValidationParameters
		{
			ValidateIssuer = false,
			ValidateAudience = false,
			ValidateLifetime = true,
			ValidateIssuerSigningKey = true,
			ValidIssuer = Configuration["JWT:Issuer"],
			ValidAudience = Configuration["JWT:Audience"],
			IssuerSigningKey = new SymmetricSecurityKey(Key)
		};
	});

builder.Services.AddSingleton<IJWTManagerRepository, JWTManagerRepository>();


builder.Services.AddControllers();
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

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

