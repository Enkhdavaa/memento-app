using DatabaseAccess.Data;
using DatabaseAccess.Models;
using MementoAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace MementoAPI.Controllers;

[Route("api/[controller]")]
public class UserController : Controller
{
    public IUserData _userData { get; }

    public UserController(IUserData userData)
    {
        _userData = userData;
    }


    [HttpPost]
    [ValidateModel]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post(UserModel userModel)
    {
        await _userData.CreateUser(userModel);

        var userID = userModel.Id;

        return Ok(userID);
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

        var user = await _userData.ReadUser(id);

        if (user != null)
        {
            return Ok(user);
        }
        return BadRequest();
    }
    
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Put([FromBody] UserUpdateModel data)
    {
        await _userData.UpdateUser(data.Id, data.Email);

        return Ok();
    }


    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(string id)
    {
        await _userData.DeleteUser(id);

        return Ok();
    }
}

