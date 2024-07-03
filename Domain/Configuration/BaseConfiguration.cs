using System;
using Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Configuration
{
	public abstract class BaseConfiguration<T> : IEntityTypeConfiguration<T> where T:BaseEntity
	{
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(m => m.CreateDate).HasDefaultValue(DateTime.Now);
        }
    }
}

