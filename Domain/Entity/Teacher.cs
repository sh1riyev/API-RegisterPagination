using System;
using Domain.Common;

namespace Domain.Entity
{
	public class Teacher :BaseEntity
	{
		public string Name { get; set; }
		public string Surname { get; set; }
		public string Email { get; set; }
        public int Age { get; set; }
		public decimal Sallary { get; set; }
		public ICollection<GroupTeacher> GroupTeachers { get; set; }
    }
}

