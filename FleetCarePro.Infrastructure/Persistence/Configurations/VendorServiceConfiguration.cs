using FleetCarePro.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FleetCarePro.Infrastructure.Persistence.Configurations;

public class VendorServiceConfiguration : IEntityTypeConfiguration<VendorService>
{
    public void Configure(EntityTypeBuilder<VendorService> builder)
    {
        builder.HasKey(vs => new
        {
            vs.ServiceCenterId,
            vs.ServiceCategoryId
        });

        builder.HasOne(vs => vs.ServiceCenter).WithMany(sc => sc.VendorServices).HasForeignKey(vs => vs.ServiceCenterId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(vs => vs.ServiceCategory).WithMany(sc => sc.VendorServices).HasForeignKey(vs => vs.ServiceCategoryId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}