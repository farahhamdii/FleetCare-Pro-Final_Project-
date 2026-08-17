using FleetCarePro.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FleetCarePro.Infrastructure.Persistence.Configurations;

public class ServiceCenterConfiguration : IEntityTypeConfiguration<ServiceCenter>
{
    public void Configure(EntityTypeBuilder<ServiceCenter> builder)
    {
        builder.HasKey(sc => sc.Id);

        builder.Property(sc => sc.Name).IsRequired().HasMaxLength(150);

        builder.Property(sc => sc.PhoneNumber).IsRequired().HasMaxLength(20);

        builder.Property(sc => sc.Email).IsRequired().HasMaxLength(150);

        builder.Property(sc => sc.Address).IsRequired().HasMaxLength(300);

        builder.HasMany(sc => sc.ServiceRecords).WithOne(sr => sr.ServiceCenter).HasForeignKey(sr => sr.ServiceCenterId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}