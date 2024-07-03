using System;
using Domain.Common;

namespace Domain.Entity
{
	public class Student : BaseEntity
	{
		public string Name { get; set; }
		public string Surname { get; set; }
		public string Address { get; set; }
		public string Email { get; set; }
		public int Age { get; set; }
		public ICollection<GroupStudent> GroupStudents { get; set; }
	}
}

