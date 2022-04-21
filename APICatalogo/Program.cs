using APICatalogo.Context;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container +
//Resolvendo o problema e erro 500 da referencia aciclica(chave estrangeira) com JSON Serializer
builder.Services.AddControllers()
    .AddJsonOptions(options=>options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//coneção com o banco mysql com a string de conexão
string mySqlConnection = builder.Configuration.GetConnectionString("DefaultConnection");
//apontar o contexto local e ao provedor mysql
builder.Services.AddDbContext<AppDbContext>(options => options.UseMySql(mySqlConnection,
    ServerVersion.AutoDetect(mySqlConnection)));




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
