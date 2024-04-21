using One_Time_Password.Data;
using One_Time_Password.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<OneTimePasswordRepository, OneTimePasswordRepository>();
builder.Services.AddTransient<OneTimePasswordService, OneTimePasswordService>();

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

app.UseAuthorization();

app.MapControllers();

app.Run();
