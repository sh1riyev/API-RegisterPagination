using System;
using System.Linq.Expressions;
using Domain.Entity;
using Repository.Repositories.Interface;
using Service.Services.Interface;

namespace Service.Services
{
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository _groupRepo;
        public GroupService(IGroupRepository groupRepo)
        {
            _groupRepo = groupRepo;
        }


        public async Task Create(Group entity)
        {
            if (await _groupRepo.IsExist(m => m.Name == entity.Name)) throw new FormatException();
            await _groupRepo.Create(entity);
        }

        public async Task Delete(Group entiy)
        {
            await _groupRepo.Delete(entiy);
        }

        public async Task<List<Group>> GetAll(Expression<Func<Group, bool>> predicate = null, params string[] includes)
        {
            return await _groupRepo.GetAll(predicate, includes);
        }

        public async Task<Group> GetBy(Expression<Func<Group, bool>> predicate = null, params string[] includes)
        {
            return await _groupRepo.GetEntity(predicate, includes);
        }

        public async Task<bool> IsExist(Expression<Func<Group, bool>> predicate = null)
        {
            return await _groupRepo.IsExist(predicate);
        }

        public async Task Update(Group entity)
        {
            if (await _groupRepo.IsExist(m => m.Id != entity.Id && m.Name == entity.Name)) throw new FormatException();
            entity.UpdateDate = DateTime.Now;
            await _groupRepo.Update(entity);
        }
    }

}