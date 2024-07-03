using System;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Configuration
{
	public class RoomConfiguration : BaseConfiguration<Room>
	{
        public override void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.Property(m => m.Name).IsRequired().HasMaxLength(100);
            builder.Property(m => m.SeatCount).IsRequired();

            builder.HasCheckConstraint("CK_Room_SeatCount", "[SeatCount] >= 1 AND [SeatCount] <= 30");
        }
    }
}

