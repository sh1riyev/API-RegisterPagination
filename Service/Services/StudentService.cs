using System;
using System.Linq.Expressions;
using Domain.Entity;
using Repository.Repositories.Interface;
using Service.Services.Interface;

namespace Service.Services
{
	public class StudentService : IStudentService
	{
        private readonly IStudentRepository _studentRepo;
		public StudentService(IStudentRepository studentRepo)
		{
            _studentRepo = studentRepo;
		}

        public async Task Create(Student entity)
        {
            if (await _studentRepo.IsExist(m => m.Email == entity.Email)) throw new FormatException();
            await _studentRepo.Create(entity);
        }

        public async Task Delete(Student entiy)
        {
            await _studentRepo.Delete(entiy);
        }

        public async Task<List<Student>> GetAll(Expression<Func<Student, bool>> predicate = null, params string[] includes)
        {
            return await _studentRepo.GetAll(predicate, includes);
        }

        public async Task<Student> GetBy(Expression<Func<Student, bool>> predicate = null, params string[] includes)
        {
            return await _studentRepo.GetEntity(predicate, includes);
        }

        public async Task<bool> IsExist(Expression<Func<Student, bool>> predicate = null)
        {
            return await _studentRepo.IsExist(predicate);
        }

        public async Task Update(Student entity)
        {
            if (await _studentRepo.IsExist(m => m.Id != entity.Id && m.Email == entity.Email)) throw new FormatException();
            entity.UpdateDate = DateTime.Now;
            await _studentRepo.Update(entity);
        }
    }
}

