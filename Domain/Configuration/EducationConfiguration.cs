using System;
using Domain.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Configuration
{
	public class EducationConfiguration : BaseConfiguration<Education>
	{
        public override void Configure(EntityTypeBuilder<Education> builder)
        {
            builder.Property(m => m.Name).IsRequired().HasMaxLength(150);
        }
    }
}

