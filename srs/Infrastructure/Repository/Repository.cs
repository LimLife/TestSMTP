using Application.Interfaces;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class Repository : IRepository
    {
        private readonly DBData _context;
        public Repository(DBData context)
        {
            _context = context;
        }

        /// <summary>
        /// Add to database Message 
        /// </summary>
        /// <param name="message"></param>
        public async Task AddMessage(Message message)
        {
            await _context.Messages.AddAsync(message);
            await _context.SaveChangesAsync();
        }
        /// <summary>
        /// Gets all error and status messages
        /// </summary>
        /// <returns>
        /// List Message
        /// </returns>
        public async Task<List<Message>> GetAllMessage()
        {
            return await _context.Messages.AsNoTracking()
                .Include(filed => filed.Failed)
                .Include(result => result.Result)
                .Include(resipinets => resipinets.Recipients)
                .ToListAsync();
        }
    }
}
