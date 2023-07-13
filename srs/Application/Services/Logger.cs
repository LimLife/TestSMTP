using Application.Interfaces;
using Domain.Entity;
using Domain.Enums;

namespace Application.Services
{
    public class Logger : ILogger
    {
        private readonly IRepository _repository;
        public Logger(IRepository repository)
        {
            _repository = repository;
        }
        /// <summary>
        /// Logging to the database with parameters
        /// </summary>
        /// <param name="message">
        /// Exception parameter with Catch
        /// </param>
        /// <param name="invalidEmail">
        /// String all invalid emails
        /// </param>
        /// <param name="state">
        /// Result operation if null or empty OK
        /// </param>
        public async Task Log(Message message, string invalidEmail = "", string state = "")
        {
            MessageResult messageResult = string.IsNullOrEmpty(state)
                ? new MessageResult() { State = StateResult.Ok } : new MessageResult() { State = StateResult.Failed };
            FailedMessage failedMessage = string.IsNullOrEmpty(state) 
                ? new FailedMessage() { MessageFiled = "" } : new FailedMessage() { MessageFiled = $"{state} {invalidEmail}" };
            message.Result = new List<MessageResult>() { messageResult };
            message.Failed = new List<FailedMessage>() { failedMessage };

            await _repository.AddMessage(message);
        }
    }
}
