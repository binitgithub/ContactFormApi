using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactFormApi.Models;
using ContactFormApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContactFormApi.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactFormController : ControllerBase
    {
        private readonly EmailService _emailService;

        public ContactFormController(EmailService emailService)
        {
            _emailService = emailService;
        }
        [HttpPost]
        public async Task<IActionResult> SubmitContactForm([FromBody] ContactFormModel contactForm)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _emailService.SendEmailAsync(contactForm);
                return Ok(new { Message = "Email sent successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error sending email.", Error = ex.Message });
            }
        }
    }
}