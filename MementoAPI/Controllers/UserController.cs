using DatabaseAccess.Data;
using DatabaseAccess.Models;
using Microsoft.AspNetCore.Mvc;

namespace MementoAPI.Controllers;

[Route("api/[controller]")]
public class UserController : Controller
{
    public IUserData UserData { get; }

    public UserController(IUserData userData)
    {
        UserData = userData;
    }


    [HttpPost]
    [ValidateModel]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post(UserModel userModel)
    {
        await UserData.CreateUser(userModel);

        var userID = userModel.Id;

        return Ok(userID);
    }

    // GET: api/values
    [HttpGet]
    public IEnumerable<string> Get()
    {
        return new string[] { "value1", "value2" };
    }

    // GET api/values/5
    [HttpGet("{id}")]
    public string Get(int id)
    {
        return "value";
    }

    //// POST api/values
    //[HttpPost]
    //public void Post([FromBody]string value)
    //{
    //}

    // PUT api/values/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody]string value)
    {
    }

    // DELETE api/values/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
}

