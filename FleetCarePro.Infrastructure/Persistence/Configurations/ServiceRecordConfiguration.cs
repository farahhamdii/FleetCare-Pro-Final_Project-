using FleetCarePro.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FleetCarePro.Infrastructure.Persistence.Configurations;

public class ServiceRecordConfiguration : IEntityTypeConfiguration<ServiceRecord>
{
    public void Configure(EntityTypeBuilder<ServiceRecord> builder)
    {
        builder.HasKey(sr => sr.Id);

        builder.Property(sr => sr.TotalCost).HasPrecision(18, 2);

        builder.Property(sr => sr.InvoiceDocumentPath).HasMaxLength(500);

        builder.Property(sr => sr.Notes) .HasMaxLength(1000);

        builder.HasOne(sr => sr.Vehicle).WithMany(v => v.ServiceRecords).HasForeignKey(sr => sr.VehicleId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(sr => sr.ServiceCenter).WithMany(sc => sc.ServiceRecords).HasForeignKey(sr => sr.ServiceCenterId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(sr => sr.CreatedByUser).WithMany(u => u.CreatedServiceRecords) .HasForeignKey(sr => sr.CreatedByUserId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(sr => sr.ServiceLineItems).WithOne(li => li.ServiceRecord).HasForeignKey(li => li.ServiceRecordId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}