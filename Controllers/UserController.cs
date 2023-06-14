using Microsoft.AspNetCore.Mvc;
using Infoapi.Models;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Infoapi.Services;

namespace Infoapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly Helper _valueService;


        public UserController(IConfiguration config)
        {
            _config = config;
            _valueService = new Helper(config);


        }
        [HttpGet("Cred")]
        public async Task<ActionResult<IEnumerable<Cred>>> GetByCred()
        {
            var creds = await _valueService.GetCred();
            return Ok(creds);
        }

        [HttpGet("count")]
        public async Task<ActionResult<int>> GetUserCount()
        {
            int count = (int)await _valueService.GetUserCount();

            return Ok(count);
        }



        //get user by email
        [HttpGet("{email}")]
        public ActionResult Get(string email)
        {
            bool success = _valueService.GetUserbyId(email);

            if (success)
            {
                return Ok(true);
            }
            else
            {
                return NoContent();

            }
        }


        //Post New User
        [HttpPost("post")]
        public ActionResult Post(Cred cred)
        {
            bool success = _valueService.AddNewUser(cred);

            if (success)
            {
                return Ok(true);
            }
            else
            {
                return BadRequest();
            }
        }

        // Edit User 

        [HttpPut("put")]
        public ActionResult PutUser(Cred cred)
        {
            bool success = _valueService.UpdateUser(cred);

            if (success)
            {
                return Ok(true);
            }
            else
            {
                return NoContent();

            }
        }



        //Delete User

        [HttpDelete("{email}")]
        public ActionResult DeleteUser(string email)
        {
            bool success = _valueService.DeleteUsers(email);

            if (success)
            {
                return Ok(true);
            }
            else
            {
                return NoContent();

            }
        }

        // To Edit User credential 

        [HttpPut("put/cred")]
        public ActionResult PutCred(Cred cred)
        {
            bool success = _valueService.UpdateUserCred(cred);

            if (success)
            {
                return Ok(true);
            }
            else
            {
                return NoContent();

            }
        }
        /*

        // Edit User 

        [HttpPut("put")]
        public ActionResult PutUser(Cred cred)
        {
            bool success = _valueService.UpdateUser(cred);

            if (success)
            {
                return Ok(true);
            }
            else
            {
                return NoContent();

            }
        }
*/


    }

}