using Microsoft.AspNetCore.Mvc;
using Infoapi.Models;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Infoapi.Services;

namespace Infoapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeleniumScrapController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly Helper _valueService;


        public SeleniumScrapController(IConfiguration config)
        {
            _config = config;
            _valueService = new Helper(config);


        }
         [HttpGet("Selenium")]
        public async Task<ActionResult<IEnumerable<Selenium>>> GetBySelenium()
        {
            var sel = await _valueService.GetSelenium();
            return Ok(sel);
        }

         [HttpGet("count")]
        public async Task<ActionResult<int>> GetSeleniumCount()
        {
            int count = (int)await _valueService.GetSeleniumCount();

            return Ok(count);
        }


       //get user by id 
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
           bool success = _valueService.GetSeleniumbyId(id);

            if (success)
            {
                return Ok(true);
            }
            else
            {
                return NoContent();

            }
        }

        //Post Selenium Data
        [HttpPost("post")]
        public ActionResult Post(Selenium sel)
        {
            bool success = _valueService.AddNewSeleniumData(sel);

            if (success)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        //Delete Selenium Data 

        [HttpDelete("{id}")]
        public ActionResult DeleteSelenium(int id)
        {
            bool success = _valueService.DeleteSelenium(id);

            if (success)
            {
                return Ok(true);
            }
            else
            {
                return NoContent();

            }
        }

        // Edit Selenium Data

        [HttpPut("put")]
        public ActionResult PutUser(Selenium sel)
        {
            bool success = _valueService.UpdateSelenium(sel);

            if (success)
            {
                return Ok(true);
            }
            else
            {
                return NoContent();

            }
        }



        
        

       

    }

}