using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using Application.Common;
using Domain.Entity;

namespace WebUI.Controllers
{
    [Route("api/")]
    [ApiController]
    public class EmailController : ControllerBase
    {

        private readonly IRepository _repository;
        private readonly IEmailService _emailService;
        public EmailController(IRepository repository, IEmailService emailService)
        {
            _repository = repository;
            _emailService = emailService;
        }
        /// <summary>
        /// Sends all messages including : Result,FiledMessage
        /// </summary>
        /// <returns>List Message to object </returns>
        [Route("mails")]
        [HttpGet]
        public async Task<List<Message>> GetMessages()
        {
            return await _repository.GetAllMessage();
        }
        /// <summary>
        /// Accepts a json object with subject body and recipients fields
        /// </summary>
        /// <returns>
        /// Returns a code with the result of the operation
        /// </returns>
        [Route("mails")]
        [HttpPost]
        public async Task<IActionResult> SendEmail([FromBody] EmailModel email)
        {
            await _emailService.Send(email);
            return Ok();
        }
    }
}
