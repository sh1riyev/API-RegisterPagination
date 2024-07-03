using System;
using System.Linq.Expressions;
using Domain.Entity;
using Repository.Repositories.Interface;
using Service.Services.Interface;

namespace Service.Services
{
	public class EducationService : IEducationService
	{
        private readonly IEducationRepository _educationRepo;
		public EducationService(IEducationRepository educationRepo)
		{
            _educationRepo = educationRepo;
		}

        public async Task Create(Education entity)
        {
            if (await _educationRepo.IsExist(m => m.Name == entity.Name)) throw new FormatException();
            await _educationRepo.Create(entity);
        }

        public async Task Delete(Education entiy)
        {
            await _educationRepo.Delete(entiy);
        }

        public async Task<List<Education>> GetAll(Expression<Func<Education, bool>> predicate = null, params string[] includes)
        {
           return await _educationRepo.GetAll(predicate, includes);
        }

        public async Task<Education> GetBy(Expression<Func<Education, bool>> predicate = null, params string[] includes)
        {
            return await _educationRepo.GetEntity(predicate, includes);
        }

        public async Task<bool> IsExist(Expression<Func<Education, bool>> predicate = null)
        {
            return await _educationRepo.IsExist(predicate);
        }

        public async Task Update(Education entity)
        {
            if (await _educationRepo.IsExist(m => m.Id != entity.Id && m.Name == entity.Name)) throw new FormatException();
            entity.UpdateDate = DateTime.Now;
            await _educationRepo.Update(entity);
        }
    }
}

