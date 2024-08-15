using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WeladSanad.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IGroupRepository, GroupRepository>();
builder.Services.AddScoped<IAttendTypeRepository, AttendTypeRepository>();
builder.Services.AddScoped<IAttendenceRepository, AttendenceRepository>();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(
    options => options.Password.RequireDigit = true)
    .AddEntityFrameworkStores<MyContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<DataProtectionTokenProviderOptions>(option =>
    option.TokenLifespan = TimeSpan.FromHours(2));

builder.Services.AddCors(corsOptions =>
{
    corsOptions.AddPolicy("MyPolicy", corsPolicyBuilder =>
    {
        corsPolicyBuilder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

builder.Services.AddDbContext<MyContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("RemoteCS"));
});




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("MyPolicy");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
