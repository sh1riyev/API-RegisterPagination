using System;
using Domain.Common;

namespace Domain.Entity
{
	public class Education :BaseEntity
	{
		public string Name { get; set; }
		public ICollection<Group> Groups { get; set; }
	}
}

