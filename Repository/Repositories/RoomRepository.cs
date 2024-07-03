using System;
using Domain.Entity;
using Repository.Data;
using Repository.Repositories.Interface;

namespace Repository.Repositories
{
	public class RoomRepository : BaseRepository<Room>,IRoomRepository
	{
        public RoomRepository(AppDbContext context) : base(context) { }

    }
}

