using System;
using Domain.Entity;
using Repository.Data;
using Repository.Repositories.Interface;

namespace Repository.Repositories
{
	public class StudentRepository : BaseRepository<Student>,IStudentRepository
	{
        public StudentRepository(AppDbContext context) : base(context) { }
    }
}

