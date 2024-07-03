using System;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Configuration
{
	public class TeacherConfiguration : BaseConfiguration<Teacher>
	{
        public override void Configure(EntityTypeBuilder<Teacher> builder)
        {
            builder.Property(m => m.Name).IsRequired().HasMaxLength(100);
            builder.Property(m => m.Surname).IsRequired().HasMaxLength(100);
            builder.Property(m => m.Email).IsRequired().HasMaxLength(250).HasAnnotation("EmailAddress", true);
            builder.Property(m => m.Age).IsRequired();
            builder.Property(m => m.Sallary).IsRequired();
            builder.HasCheckConstraint("CK_Teacher_Age", "[Age] >= 21");
        }
    }
}

