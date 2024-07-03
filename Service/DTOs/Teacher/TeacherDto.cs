using System;
namespace Service.DTOs.Teacher
{
	public class TeacherDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public decimal Sallary { get; set; }
        public ICollection<string> GroupName { get; set; }
        public ICollection<string> EducationName { get; set; }
    }
}

