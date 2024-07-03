using System;
using Domain.Entity;
using Repository.Data;
using Repository.Repositories.Interface;

namespace Repository.Repositories
{
	public class TeacherRepository : BaseRepository<Teacher>,ITeacherRepository
	{
        public TeacherRepository(AppDbContext context) : base(context) { }

    }
}

