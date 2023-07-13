using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration
{
    public class ConfigurationMessage : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.HasKey(key => key.ID);
            builder.Property(subject => subject.Subject);
            builder.Property(body => body.Body);
            builder.Property(time => time.DateSend).HasColumnType(typeName: "TEXT");
            builder.HasMany(filed => filed.Failed).WithOne(mes => mes.Message).HasForeignKey(fk => fk.MessageID);
            builder.HasMany(result => result.Result).WithOne(mes => mes.Message).HasForeignKey(fk => fk.MessageID);
            builder.HasMany(recipinet => recipinet.Recipients).WithOne(recipinets => recipinets.Message).HasForeignKey(fk => fk.MessageID);
        }
    }
}
