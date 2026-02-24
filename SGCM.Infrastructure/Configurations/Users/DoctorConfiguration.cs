using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SGCM.Shared.Domain.Entities;
using SGCM.Shared.Domain.ValueObjects;

namespace SGCM.Infrastructure.Configurations.Users;

public sealed class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
{
    public void Configure(EntityTypeBuilder<Doctor> builder)
    {
        builder.ToTable("Doctors");
        builder.HasKey(d => d.Id);

        #region Properties

        builder.Property(d => d.Id)
            .HasConversion(new StronglyTypedIdConverter<DoctorId>(guid => DoctorId.From(guid)))
            .HasColumnType("uniqueidentifier");

        builder.Property(d => d.FirstName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(d => d.LastName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(d => d.UserId)
            .IsRequired();

        #endregion

        #region Relations

        builder.HasOne(d => d.User)
            .WithOne(u => u.Doctor)
            .HasForeignKey<Doctor>(d => d.UserId)
            .OnDelete(DeleteBehavior.Cascade);        

        #endregion
    }
}