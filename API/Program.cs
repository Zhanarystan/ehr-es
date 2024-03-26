using API.Application;
using API.Interfaces;
using API.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<DataContext>(opt =>
{
    opt.UseSqlServer("Server=localhost;Database=master;Trusted_Connection=True;TrustServerCertificate=True");
});
builder.Services.AddScoped<EventStore>();
builder.Services.AddScoped<IElectronicHealthRecordCommandHandler, ElectronicHealthRecordCommandHandler>();
builder.Services.AddScoped<IElectronicHealthRecordQueryHandler, ElectronicHealthRecordQueryHandler>();
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

app.UseAuthorization();

app.MapControllers();

app.Run();
