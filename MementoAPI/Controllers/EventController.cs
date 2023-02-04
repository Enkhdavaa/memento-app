using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatabaseAccess.Data;
using DatabaseAccess.Models;
using MementoAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace MementoAPI.Controllers;

[Route("api/[controller]")]
public class EventController : Controller
{
    private readonly IEventData _eventData;

    public EventController(IEventData eventData)
    {
        _eventData = eventData;
    }

    [HttpPost]
    [ValidateModel]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post(EventModel eventModel)
    {
        await _eventData.CreateEvent(eventModel);

        var id = eventModel.Id;

        return Ok(id);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(string id)
    {
        if(String.IsNullOrEmpty(id))
        {
            return BadRequest();
        }

        var _event = await _eventData.ReadEvent(id);

        if(_event == null)
        {
            return BadRequest();
        }

        return Ok(_event);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Put([FromBody] EventUpdateModel data)
    {
        await _eventData.UpdateEvent(data.Id, data.EventName, data.Description, data.Location, data.DateTime);

        return Ok();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(string id)
    {
        await _eventData.DeleteEvent(id);

        return Ok();
    }
}

