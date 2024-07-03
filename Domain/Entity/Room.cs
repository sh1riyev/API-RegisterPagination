using System;
using Domain.Common;

namespace Domain.Entity
{
	public class Room :BaseEntity
	{
		public string Name { get; set; }
		public int SeatCount { get; set; }
		public ICollection<Group> Groups { get; set; }
	}
}

