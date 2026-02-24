using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SGCM.Shared.Domain.Entities;
using SGCM.Shared.Domain.ValueObjects;

namespace SGCM.Infrastructure.Configurations.Users;

public sealed class PatientConfiguration : IEntityTypeConfiguration<Patient>
{
    public void Configure(EntityTypeBuilder<Patient> builder)
    {
        builder.ToTable("Patients");
        builder.HasKey(p => p.Id);

        builder.Property(u => u.FirstName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(u => u.LastName)
            .IsRequired()
            .HasMaxLength(100);
        builder.Property(p => p.Phone).HasMaxLength(20);
        builder.Property(p => p.Address).HasMaxLength(250);
        builder.Property(p => p.BloodType).HasMaxLength(5);
        builder.Property(p => p.Allergies).HasMaxLength(500);
        builder.Property(p => p.MedicalConditions).HasMaxLength(500);
        builder.Property(p => p.EmergencyContactName).HasMaxLength(100);
        builder.Property(p => p.EmergencyContactPhone).HasMaxLength(20);
        builder.Property(p => p.PolicyNumber).HasMaxLength(50);

        builder.Property(p => p.InsurerId)
            .HasConversion(new StronglyTypedIdConverter<InsurerId>(guid => InsurerId.From(guid))!)
            .HasColumnType("uniqueidentifier")
            .IsRequired(false);

        // Relación con Insurer
        builder.HasOne(p => p.Insurer)
            .WithMany()
            .HasForeignKey(p => p.InsurerId)
            .OnDelete(DeleteBehavior.SetNull);
        #region Properties

        builder.Property(p => p.Id)
            .HasConversion(new StronglyTypedIdConverter<PatientId>(guid => PatientId.From(guid)))
            .HasColumnType("uniqueidentifier");

        builder.Property(p => p.FirstName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.LastName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.UserId)
            .IsRequired();


        #endregion

        #region Relationships

        builder.HasOne(p => p.User)
            .WithOne(u => u.Patient)
            .HasForeignKey<Patient>(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        #endregion
    }
}