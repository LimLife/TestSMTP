using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration
{
    public class ConfigurationFailedMessage : IEntityTypeConfiguration<FailedMessage>
    {
        public void Configure(EntityTypeBuilder<FailedMessage> builder)
        {
            builder.HasKey(x => x.ID);
            builder.Property(prop => prop.MessageFiled);
        }
    }
}
