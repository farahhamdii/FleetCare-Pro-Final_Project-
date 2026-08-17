using FleetCarePro.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FleetCarePro.Infrastructure.Persistence.Configurations;

public class ServiceLineItemConfiguration : IEntityTypeConfiguration<ServiceLineItem>
{
    public void Configure(EntityTypeBuilder<ServiceLineItem> builder)
    {
        builder.HasKey(li => li.Id);

        builder.Property(li => li.Description).IsRequired().HasMaxLength(500);

        builder.Property(li => li.Cost).HasPrecision(18, 2);

        builder.HasOne(li => li.ServiceRecord).WithMany(sr => sr.ServiceLineItems)
            .HasForeignKey(li => li.ServiceRecordId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(li => li.ServiceCategory) .WithMany(sc => sc.ServiceLineItems)
            .HasForeignKey(li => li.ServiceCategoryId) //to
            .OnDelete(DeleteBehavior.Restrict);
    }
}