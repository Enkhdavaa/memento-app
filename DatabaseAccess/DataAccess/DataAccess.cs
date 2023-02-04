using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace DatabaseAccess.DataAccess;

public class DataAccess : IDataAccess
{
    public string? DatabaseName { get; }
    public string? UserCollectionName { get; }
    public string? EventCollectionName { get; }

    private readonly IMongoClient _client;
    private readonly IConfiguration? _config;

    public DataAccess(IConfiguration config, ConnectionStringData connectionData)
    {
        _config = config;
        DatabaseName = _config[connectionData.DatabaseNameString];
        UserCollectionName = _config[connectionData.UserCollectionNameString];
        EventCollectionName = _config[connectionData.EventCollectionNameString];


        string? connectionStrings = _config.GetConnectionString(connectionData.ConnectionString);
        var settings = MongoClientSettings.FromConnectionString(connectionStrings);
        _client = new MongoClient(settings);
    }

    public IMongoCollection<T> GetCollection<T>(string? databaseName, in string? collectionName)
    {
        var db = _client.GetDatabase(databaseName);
        return db.GetCollection<T>(collectionName);
    }
}

