using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Infrastructure.Data.Configuration
{
    public class FailConfiguration : IEntityTypeConfiguration<Fail>
    {
        public void Configure(EntityTypeBuilder<Fail> builder)
        {
            builder.ToTable("Fail");

            builder.Property(p => p.Id)
                    .IsRequired();

            builder.Property(p => p.Responsible)
                    .IsRequired()
                    .HasMaxLength(100);

            builder.Property(p => p.DateRegistered)
                    .IsRequired();


        }
    }
}
