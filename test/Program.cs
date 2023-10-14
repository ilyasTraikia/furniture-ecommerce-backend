using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Stripe;
using System.Text;
using test.Middleware;
using test.Models;



var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

ConfigurationManager configuration = builder.Configuration;

// Add services to the container.
// For Cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("MyCorsPolicy",
                      policy =>
                      {
                          policy
                           .SetIsOriginAllowed(origin => true) // allow any origin
                           .AllowCredentials() // allow credentials
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                      });
});


// For Web API
builder.Services.AddControllers();
// For Entity Framework
builder.Services.AddDbContext<ApplicationContext>(  opt => 
   opt.UseSqlServer(builder.Configuration.GetConnectionString("Default")) 
   );

// For Identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationContext>()
                .AddDefaultTokenProviders();

// Adding Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})

// Adding Jwt Bearer
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = configuration["JWT:ValidAudience"],
        ValidIssuer = configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
    };
});



builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();





var app = builder.Build();



// This is your test secret API key.
StripeConfiguration.ApiKey = "sk_test_51JS5bvJlodOva1JDbGTvxZur3QMAmQcYdL31CUdKh2WCwr8kWju881ZckFa8LM9DACbqdw7iUqi4c3phnZPQWRRq00OnJNCtrb";

app.UseMiddleware(typeof(ExceptionHandlingMiddleware));

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseExceptionHandler("/error-development");
}



app.UseHttpsRedirection();

app.UseRouting();



app.UseCors("MyCorsPolicy");


app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();
