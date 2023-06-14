using System.Net.Mail;
using Microsoft.AspNetCore.Mvc;
using Infoapi.Models;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Infoapi.Services;
using SendingEmails;

namespace Infoapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly Helper _valueService;
        private readonly IEmailSender _emailSender;



        public EmailController(IConfiguration config, IEmailSender emailSender)
        {
            _config = config;
            _valueService = new Helper(config);
            _emailSender = emailSender;


        }

        public class EmailData
        {
            public string? ToEmail { get; set; }
            public string? Subject { get; set; }
            public string? Body { get; set; }
        }

        [HttpGet("sendCred")]
        public async Task<ActionResult<IEnumerable<Cred>>> GetByCred()
        {
            var creds = await _valueService.GetCred();
            return Ok(creds);
        }

        [HttpPost("send")]
        public async Task<IActionResult> Index([FromBody] EmailData emailData)
        {


            string? email = emailData.ToEmail;
            string? subject = emailData.Subject;
            string? message = emailData.Body;
            if (email != null && subject != null && message != null)
            {
                await _emailSender.SendEmailAsync(email, subject, message);
                return Ok(true);

            }
            else
            {
                return Ok(false);
            }


        }

        /*
                [HttpPost("send")]
                public IActionResult SendEmail([FromBody] EmailData emailData)
                {
                    try
                    {
                        Console.WriteLine($"ToEmail: {emailData.ToEmail}, Subject: {emailData.Subject}, Body: {emailData.Body}");

                        MailMessage message = new MailMessage();

                        message.From = new MailAddress("apurvambparkhe@gmail.com");

                        message.To.Add(emailData.ToEmail);
                        Console.WriteLine(emailData.ToEmail);

                        message.Subject = emailData.Subject;
                        Console.WriteLine(emailData.Subject);

                        message.Body = emailData.Body;
                        Console.WriteLine(emailData.Body);

                        SmtpClient smtpClient = new SmtpClient();

                        smtpClient.Host = "smtp.gmail.com";
                        smtpClient.Port = 587;

                        smtpClient.Credentials = new System.Net.NetworkCredential("apurvambparkhe@gmail.com", "Apurva#321");

                        smtpClient.EnableSsl = true;

                        smtpClient.Send(message);

                        return Ok();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"An error occurred while sending the email: {ex.Message}\nStack Trace: {ex.StackTrace}");

                        return StatusCode(500, ex.Message);
                    }
                }

        */




    }
}