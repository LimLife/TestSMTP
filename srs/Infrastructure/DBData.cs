using Domain.Entity;
using Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class DBData : DbContext
    {
        public DbSet<Message> Messages { get; set; }
        public DbSet<FailedMessage> FailedMessages { get; set; }
        public DbSet<MessageResult> MessageResults { get; set; }
        public DbSet<Recipient> Recipients { get; set; }
        public DBData(DbContextOptions<DBData> op) : base(op)
        {
            Database.EnsureCreatedAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ConfigurationMessage());
            modelBuilder.ApplyConfiguration(new ConfigurationFailedMessage());
            modelBuilder.ApplyConfiguration(new ConfigurationResultMessage());
        }
    }
}
