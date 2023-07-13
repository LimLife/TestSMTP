using Domain.Entity;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration
{
    public class ConfigurationResultMessage : IEntityTypeConfiguration<MessageResult>
    {
        public void Configure(EntityTypeBuilder<MessageResult> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(prop => prop.State).HasConversion(state => state.ToString(), state => (StateResult)Enum.Parse(typeof(StateResult), state));
        }
    }
}
