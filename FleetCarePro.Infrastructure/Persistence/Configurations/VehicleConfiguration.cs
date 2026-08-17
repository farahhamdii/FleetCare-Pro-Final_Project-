using FleetCarePro.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FleetCarePro.Infrastructure.Persistence.Configurations;

public class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
{
    public void Configure(EntityTypeBuilder<Vehicle> builder)
    {
        builder.HasKey(v => v.Id);

        builder.Property(v => v.VIN).IsRequired() .HasMaxLength(17);

        builder.HasIndex(v => v.VIN).IsUnique();

        builder.Property(v => v.LicensePlate).IsRequired().HasMaxLength(20);

        builder.Property(v => v.Make) .IsRequired().HasMaxLength(50);

        builder.Property(v => v.Model).IsRequired().HasMaxLength(50);

        builder.Property(v => v.PurchasePrice)
            .HasPrecision(18, 2);

        builder.Property(v => v.VehicleImageURL)
            .HasMaxLength(500);

        builder.HasOne(v => v.Driver).WithMany(u => u.AssignedVehicles).HasForeignKey(v => v.DriverId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(v => v.ServiceRecords).WithOne(sr => sr.Vehicle).HasForeignKey(sr => sr.VehicleId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}