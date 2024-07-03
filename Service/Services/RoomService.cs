using System;
using System.Linq.Expressions;
using Domain.Entity;
using Repository.Repositories.Interface;
using Service.Services.Interface;

namespace Service.Services
{
	public class RoomService : IRoomService
	{
        private readonly IRoomRepository _roomRepo;
		public RoomService(IRoomRepository roomRepo)
		{
            _roomRepo = roomRepo;
		}

        public async Task Create(Room entity)
        {
            if (await _roomRepo.IsExist(m => m.Name == entity.Name)) throw new FormatException();
            await _roomRepo.Create(entity);
        }

        public async Task Delete(Room entiy)
        {
            await _roomRepo.Delete(entiy);
        }

        public async Task<List<Room>> GetAll(Expression<Func<Room, bool>> predicate = null, params string[] includes)
        {
           return await _roomRepo.GetAll(predicate, includes);
        }

        public async Task<Room> GetBy(Expression<Func<Room, bool>> predicate = null, params string[] includes)
        {
           return await _roomRepo.GetEntity(predicate, includes);        }

        public async Task<bool> IsExist(Expression<Func<Room, bool>> predicate = null)
        {
           return await _roomRepo.IsExist(predicate);
        }
        public async Task Update(Room entity)
        {
            if (await _roomRepo.IsExist(m => m.Id != entity.Id && m.Name == entity.Name)) throw new FormatException();
            entity.UpdateDate = DateTime.Now;
            await _roomRepo.Update(entity);
        }
    }
}

