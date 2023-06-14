using Microsoft.AspNetCore.Mvc;
using Infoapi.Models;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Infoapi.Services;

namespace Infoapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatentController : ControllerBase
    {
       private readonly IConfiguration _config;
        private readonly Helper _valueService;


        public PatentController(IConfiguration config)
        {
            _config = config;
            _valueService = new Helper(config);

        }
         
         //get patent all data
         [HttpGet("Patent")]
        public async Task<ActionResult<IEnumerable<Patent>>> GetByPatent()
        {
            var pet = await _valueService.GetPatent();
            return Ok(pet);
        }
        /*
        [HttpGet("count")]
        public async Task<ActionResult<int>> GetPatentCount()
        {
            int count = await _valueService.GetpatentCount();

            return Ok(count);
        }
        */

         //get patent by id 
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
           bool success = _valueService.GetPatentbyId(id);

            if (success)
            {
                return Ok(true);
            }
            else
            {
                return NoContent();

            }
        }


        //Post New Patent
        [HttpPost("post")]
        public ActionResult Post(Patent pet)
        {
            bool success = _valueService.AddNewPatentData(pet);

            if (success)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        //Delete Patent
        [HttpDelete("{id}")]
        public ActionResult DeletePatent(int id)
        {
            bool success = _valueService.DeletePatent(id);

            if (success)
            {
                return Ok(true);
            }
            else
            {
                return NoContent();

            }
        }

        // Edit Patent 


        [HttpPut("put")]
        public ActionResult PutPatent(Patent pet)
        {
            bool success = _valueService.UpdatePatent(pet);

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