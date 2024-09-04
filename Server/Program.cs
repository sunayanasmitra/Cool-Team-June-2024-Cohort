using System.IdentityModel.Tokens.Jwt;
using HealthCareApp.Server.Configuration;
using HealthCareApp.Server.Data;
using HealthCareApp.Server.Models;
using HealthCareApp.Server.Tests;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString =
	builder.Configuration.GetConnectionString("DefaultConnection")
	?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
	options.UseSqlServer(connectionString)
);
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder
	.Services.AddDefaultIdentity<ApplicationUser>(options =>
		options.SignIn.RequireConfirmedAccount = true)
	.AddRoles<IdentityRole>()
	.AddRoleManager<RoleManager<IdentityRole>>()
	.AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddIdentityServer()
	.AddApiAuthorization<ApplicationUser, ApplicationDbContext>(options =>
	{
		options.IdentityResources["openid"].UserClaims.Add("role");
		options.ApiResources.Single().UserClaims.Add("role");
	});

builder.Services.AddAuthentication(options =>
{
	options.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
	options.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
	options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
}).AddIdentityServerJwt();

builder.Services.AddAuthorization();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddSwaggerGen();
builder.Services.AddMvc().AddControllersAsServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseMigrationsEndPoint();
	app.UseWebAssemblyDebugging();
	app.UseSwagger();
	app.UseSwaggerUI();
}
else
{
	app.UseExceptionHandler("/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();


app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();
//app.UseAuthentication();
app.UseIdentityServer();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");
await Roles.AddRoles(app.Services);
await Roles.CreateDefaultUsers(app.Services);
app.Run();
