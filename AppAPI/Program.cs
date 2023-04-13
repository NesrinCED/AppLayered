
using BusinessLogicLayer.DependencyInjection;
using DataAccessLayer.DataContext;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;
using AppAPI.AutoMapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();// AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//dependency injection 
ServicesRegistration.RegisterServices(builder.Services);

//ignore cycles
JsonSerializerOptions options = new()
{
    ReferenceHandler = ReferenceHandler.Preserve,
    WriteIndented = true
};

//add context
builder.Services.AddDbContext<AppLayeredDBDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("constring"));

});
builder.Services.AddCors(options => options.AddPolicy(name: "applayered1",
    policy =>
    {
        policy.WithOrigins("http://localhost:4300").AllowAnyMethod().AllowAnyHeader().AllowCredentials();
     
    }));
/*builder.Services.AddCors(option =>
{
    option.AddPolicy("MyPolicy1", builder =>
    {
        builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
});*/

//Automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//builder.Services.AddAutoMapper(typeof(Program).Assembly);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("applayered1");
///!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
app.UseAuthentication();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
