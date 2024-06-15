using MerchantProfile.Api.Models;
using MerchantProfile.Api.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using MerchantProfile.Api.Services.IServices;
using MerchantProfile.Api.Middleware;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpLogging(c =>
{
    c.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.All;
});

builder.Services.AddDbContext<MerchantDbContext>(option =>
{
    option.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddHttpClient();
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IAuthenService, AuthenService>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Adding Authentication  
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
    //.AddCookie(options =>
    //{
    //    options.Cookie.Name = "merchant_jwt";
    //})

    // Adding Jwt Bearer  
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };

        //options.Events = new JwtBearerEvents
        //{
        //    OnMessageReceived = ctx =>
        //    {
        //        ctx.Request.Cookies.TryGetValue("merchant_jwt", out var accessToken);
        //        if (!string.IsNullOrEmpty(accessToken))
        //            ctx.Token = accessToken;

        //        return Task.CompletedTask;
        //    }
        //};

    });


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder
                   .WithOrigins("http://localhost:8000")
                   .AllowAnyHeader()
                   .AllowAnyMethod()
                   .AllowCredentials(); // Cho phép cookie
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


//app.UseMiddleware<AuthenticationMiddleware>();

app.UseHttpsRedirection();

app.UseHttpLogging();

app.UseCors("AllowAll");

app.UseAuthorization();


app.MapControllers();

app.Run();
