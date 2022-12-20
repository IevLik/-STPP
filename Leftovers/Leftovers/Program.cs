using Leftovers.Auth;
using Leftovers.Auth.Model;
using Leftovers.Data;
using Leftovers.Data.Dtos.Auth;
using Leftovers.Data.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Leftovers.Data;
using Leftovers.Data.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

builder.Services.AddIdentity<LeftoversUser, IdentityRole>()
    .AddEntityFrameworkStores<LeftoversContext>()
    .AddDefaultTokenProviders();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters.ValidAudience = builder.Configuration["JWT:ValidAudience"];
        options.TokenValidationParameters.ValidIssuer = builder.Configuration["JWT:ValidIssuer"];
        options.TokenValidationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]));
    });
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(PolicyNames.SameUser, policy => policy.Requirements.Add(new SameUserRequirment()));
});
builder.Services.AddSingleton<IAuthorizationHandler, SameUserAuthorizationHandler>();
// Add services to the container.
builder.Services.AddDbContext<LeftoversContext>();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddTransient<IChainsRepository, ChainsRepository>();
builder.Services.AddTransient<IRestaurantsRepository, RestaurantsRepository>();
builder.Services.AddTransient<IMealsRepository, MealsRepository>();
builder.Services.AddTransient<ITokenManager, TokenManager>();
//builder.Services.AddTransient<DatabaseSeeder, DatabaseSeeder>();

builder.Services.AddScoped<DatabaseSeeder>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalHost", policy =>
    {
        policy.AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials()
            .WithOrigins("http://localhost:3000");
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowLocalHost");
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

//using var scope = app.Services.CreateScope();

//var dbContext = scope.ServiceProvider.GetRequiredService<LeftoversContext>();
//dbContext.Database.Migrate();

var dbSeeder = app.Services.CreateScope().ServiceProvider.GetRequiredService<DatabaseSeeder>();
await dbSeeder.SeedAsync();

app.Run();