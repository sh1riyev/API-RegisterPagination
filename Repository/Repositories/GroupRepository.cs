using System;
using Domain.Entity;
using Repository.Data;
using Repository.Repositories.Interface;

namespace Repository.Repositories
{
    public class GroupRepository : BaseRepository<Group>,IGroupRepository
    {
        public GroupRepository(AppDbContext context) : base(context) { }
    }
}

