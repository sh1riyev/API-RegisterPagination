using System;
namespace Service.DTOs.Teacher
{
	public class TeacherCreateDto
	{
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public decimal Sallary { get; set; }
        public List<int> GroupId { get; set; }
    }
}

