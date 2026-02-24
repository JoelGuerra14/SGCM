using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SGCM.Shared.Domain.Entities;
using SGCM.Shared.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGCM.Infrastructure.Configurations.Insurers
{
    public sealed class InsurerConfiguration : IEntityTypeConfiguration<Insurer>
    {
        public void Configure(EntityTypeBuilder<Insurer> builder)
        {
            builder.ToTable("Insurers");
            builder.HasKey(i => i.Id);

            builder.Property(i => i.Id)
                .HasConversion(new StronglyTypedIdConverter<InsurerId>(guid => InsurerId.From(guid)))
                .HasColumnType("uniqueidentifier");

            builder.Property(i => i.Name).IsRequired().HasMaxLength(150);
            builder.Property(i => i.ContactPhone).HasMaxLength(20);
            builder.Property(i => i.ContactEmail).HasMaxLength(100);
            builder.Property(i => i.Address).HasMaxLength(250);
            builder.Property(i => i.Website).HasMaxLength(250);
        }
    }
}
