using System;
using MongoDB.Bson.Serialization.Attributes;

namespace DatabaseAccess.Models;

public class EventModel
{
	[BsonId]
	[BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
	public string? Id { get; set; }
	public string? EventName { get; set; }
	public string? Description { get; set; }
	public string? Location { get; set; }
	public UserModel? AssignedTo { get; set; }
	public DateTime DateTime { get; set; }
}


