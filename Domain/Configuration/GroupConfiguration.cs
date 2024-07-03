using System;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Configuration
{
	public class GroupConfiguration : BaseConfiguration<Group>
	{
		public override void Configure(EntityTypeBuilder<Group> builder)
		{
			builder.Property(m => m.Name).IsRequired();
			builder.Property(m => m.Capacity).IsRequired();
            builder.HasCheckConstraint("CK_Group_Capacity", "[Capacity] >= 7 AND [Capacity] <= 20");
        }
	}
}

