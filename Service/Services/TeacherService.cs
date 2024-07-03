using System;
using System.Linq.Expressions;
using Domain.Entity;
using Repository.Repositories.Interface;
using Service.Services.Interface;

namespace Service.Services
{
	public class TeacherService : ITeacherService
	{
        private readonly ITeacherRepository _teacherRepo;
		public TeacherService(ITeacherRepository teacherRepo)
		{
            _teacherRepo = teacherRepo;
		}

        public async Task Create(Teacher entity)
        {
            if (await _teacherRepo.IsExist(m => m.Email == entity.Email)) throw new FormatException();
            await _teacherRepo.Create(entity);
        }

        public async Task Delete(Teacher entiy)
        {
            await _teacherRepo.Delete(entiy);
        }

        public async Task<List<Teacher>> GetAll(Expression<Func<Teacher, bool>> predicate = null, params string[] includes)
        {
           return await _teacherRepo.GetAll(predicate, includes);
        }

        public async Task<Teacher> GetBy(Expression<Func<Teacher, bool>> predicate = null, params string[] includes)
        {
            return await _teacherRepo.GetEntity(predicate, includes);
        }

        public async Task<bool> IsExist(Expression<Func<Teacher, bool>> predicate = null)
        {
            return await _teacherRepo.IsExist(predicate);
        }

        public async Task Update(Teacher entity)
        {
            if (await _teacherRepo.IsExist(m => m.Id != entity.Id && m.Email == entity.Email)) throw new FormatException();
            entity.UpdateDate = DateTime.Now;
          await  _teacherRepo.Update(entity);
        }
    }
}

