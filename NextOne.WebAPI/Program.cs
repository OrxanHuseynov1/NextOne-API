using DAL.SqlServer;
using BusinessLayer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.RegisterDAL(builder.Configuration);
builder.Services.AddBlServices(builder.Configuration);
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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
