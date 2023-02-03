using DatabaseAccess.Data;
using DatabaseAccess.DataAccess;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSingleton(new ConnectionStringData
{
    ConnectionString = "default",
    DatabaseNameString = "Database:DatabaseName",
    EventCollectionNameString = "Database:Event:CollectionName",
    UserCollectionNameString = "Database:User:CollectionName",
});

builder.Services.AddSingleton<IDataAccess, DataAccess>();
builder.Services.AddSingleton<IUserData, UserData>();
builder.Services.AddSingleton<IEventData, EventData>();

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

