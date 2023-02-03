using System;
namespace DatabaseAccess.DataAccess;

public class ConnectionStringData
{
    public string ConnectionString { get; set; } = "default";
    public string DatabaseNameString { get; set; } = "mementodb";
    public string UserCollectionNameString { get; set; } = "Users";
    public string EventCollectionNameString { get; set; } = "Events";
}

