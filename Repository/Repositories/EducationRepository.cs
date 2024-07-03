using System;
using Domain.Entity;
using Repository.Data;
using Repository.Repositories.Interface;

namespace Repository.Repositories
{
	public class EducationRepository : BaseRepository<Education>,IEducationRepository
	{
        public EducationRepository(AppDbContext context) : base(context) { }

    }
}

