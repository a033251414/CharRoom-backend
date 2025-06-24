using Server.Models;
using Server.Services;
using MongoDB.Driver;
//JWT
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
//雙向
using Server.Hubs;

var builder = WebApplication.CreateBuilder(args);

//JWT
builder.Services.AddSingleton<JwtService>();
//雙向
builder.Services.AddSignalR();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    var jwtSettings = builder.Configuration.GetSection("Jwt");
     var key = jwtSettings["Key"] ?? throw new InvalidOperationException("JWT Key 未設定");
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))

    };
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<MongoDBSettings>(
builder.Configuration.GetSection("MongoDBSettings"));
builder.Services.AddSingleton<GroupService>();
builder.Services.AddSingleton<UserService>();
builder.Services.AddSingleton<MessageService>();


builder.Services.AddSingleton<IMongoClient, MongoClient>(sp =>
{
    var connectionString = builder.Configuration.GetConnectionString("MongoDb");
    return new MongoClient(connectionString);
});


builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy =>
        {
            policy.WithOrigins(
                "https://a033251414.github.io",
                "http://localhost:5173"
                    
                )
                .AllowAnyHeader()
                .AllowAnyMethod()
            //雙向
               .AllowCredentials();
               
        });
});

var app = builder.Build();

// Swagger 開發環境啟用
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}






app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("AllowReactApp");
//JWT
app.UseAuthentication();
app.UseAuthorization();

//雙向
app.MapHub<ChatHub>("/chathub");



app.MapControllers();

app.Run();


