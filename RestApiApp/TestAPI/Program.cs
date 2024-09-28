using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RestApiLibrary;
using TestAPI.DBFiles;
using TestAPI.DBFiles.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add Authentication
builder.Services.AddAuthentication()
    .AddBearerToken(IdentityConstants.BearerScheme);


// Add Authorization
builder.Services.AddAuthorization();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//// Configure DBContext
//// Add services to the container.
//builder.Services.AddDbContext<AppDBContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddCustomServices<AppDBContext>(builder.Configuration);


// Add Authentication
builder.Services.AddAuthentication()
    .AddCookie(IdentityConstants.ApplicationScheme);
// Add IdentityCore
builder.Services
    .AddIdentityCore<User>()
    .AddEntityFrameworkStores<AppDBContext>()
    .AddApiEndpoints();

var app = builder.Build();

app.UseCustomMiddleware();
// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

// Enable Identity APIs
app.MapIdentityApi<User>();

app.UseAuthorization();
app.UseHttpsRedirection();
app.UseAuthentication();




app.MapControllers();

app.Run();

/////////////////////////////////////////////////////

















