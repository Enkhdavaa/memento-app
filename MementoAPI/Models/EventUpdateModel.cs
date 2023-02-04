using System;
using DatabaseAccess.Models;

namespace MementoAPI.Models;

public class EventUpdateModel
{
    public string? Id { get; set; }
    public string? EventName { get; set; }
    public string? Description { get; set; }
    public string? Location { get; set; }
    public DateTime DateTime { get; set; }
}