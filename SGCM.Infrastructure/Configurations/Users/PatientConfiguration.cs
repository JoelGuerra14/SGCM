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