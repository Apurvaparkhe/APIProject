using Microsoft.AspNetCore.Mvc;
using Infoapi.Models;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Infoapi.Services;
using System.Data;
using Serilog;

namespace Infoapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FirstController : ControllerBase
    {

        private readonly IConfiguration _config;
        private readonly Helper _valueService;
        //private readonly ILogger<Helper> _logger;
        private readonly ILogger<FirstController> _logger;

        public FirstController(IConfiguration config, ILogger<FirstController> _logger)
        {
            _config = config;
            _valueService = new Helper(config);
            this._logger = _logger;
        }

        [HttpGet("value")]
        public string Greetings()
        {

            return "Hello there from api project";


        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Value>>> GetStudents()
        {
            try
            {

                this._logger.LogInformation("|Log ||Testing");
                // Log.Information("Product trigegerred");
                var values = await _valueService.GetValuesAsync();
                return Ok(values);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                return StatusCode(500); // Internal Server Error
            }


        }




        [HttpGet("count/{tableName}")]
        public async Task<ActionResult<int>> GetTableCount(string tableName)
        {
            try
            {
                int t = Convert.ToInt32(tableName);
                int count = -1;
                string connectionString = _config.GetConnectionString("DefaultConnection");
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    Console.WriteLine("connection is open in sql");
                    using (SqlCommand cmd = new SqlCommand("GetTableCountss", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@TableName", t);
                        SqlParameter returnParameter = cmd.Parameters.Add("@ReturnVal", SqlDbType.Int);
                        returnParameter.Direction = ParameterDirection.ReturnValue;
                        await cmd.ExecuteNonQueryAsync();
                        count = (int)returnParameter.Value;
                    }

                }
                return Ok(count);


                /* if (count == -1)
                 {
                     return BadRequest("Invalid table name");
                 }*/
            }
            catch (Exception ex)
            {
                this._logger.LogInformation("|Log ||Testing", ex);
                Log.Information("Product trigegerred");
                return null;

            }


        }








        [HttpGet("Cred")]
        public async Task<ActionResult<IEnumerable<Cred>>> GetByCred()
        {
            try
            {

                var creds = await _valueService.GetCred();
                return Ok(creds);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                return StatusCode(500); // Internal Server Error
            }


        }



        [HttpPost("Check")]
        public IActionResult CheckCredentials(Cred cred)
        {
            if (_valueService == null)
            {
                Console.WriteLine("Error: _valueService cannot be null");
                // return BadRequest("Internal server error");
                return Ok(false);
            }

            var rolename = _valueService.CheckCred(cred)?.Trim() ?? "";
            Console.WriteLine(rolename);

            if (!string.IsNullOrEmpty(rolename))
            {
                var authResponse = new AuthResponse { RoleName = rolename };
                return Ok(authResponse);
            }
            else
            {
                // return BadRequest("Wrong credentials");
                return Ok(false);
            }
        }




        public class AuthResponse
        {
            public string? RoleName { get; set; }
        }



        [HttpPost("post")]
        public ActionResult Post(Value value)
        {
            try
            {
                bool success = _valueService.AddValue(value);

                if (success)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred ");
                return StatusCode(500); // Internal Server Error
            }
        }

        [HttpPut("put")]
        public ActionResult Put(Value value)
        {
            try
            {
                bool success = _valueService.UpdateValue(value);

                if (success)
                {
                    return Ok(true);
                }
                else
                {
                    return NoContent();

                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred ");
                return StatusCode(500); // Internal Server Error
            }

        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                bool success = _valueService.DeleteValue(id);

                if (success)
                {
                    return Ok(true);
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the value with ID {Id}", id);
                return StatusCode(500); // Internal Server Error
            }
        }
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            try
            {
                bool success = _valueService.GetbyId(id);

                if (success)
                {
                    return Ok(true);
                }
                else
                {
                    return NoContent();

                }
            }

            catch (Exception ex)
            {
                this._logger.LogError(ex, "An error occurred while retrieving the value with ID {Id}", id);
                return StatusCode(500); // Internal Server Error
            }
        }



    }
}
