using Microsoft.EntityFrameworkCore;
using ClassTrack.DataAccessLayer.Models;
using ClassTrack.DataAccessLayer.Models.Context;
using ClassTrack.DataAccessLayer.Repositories.IRepository;

namespace ClassTrack.DataAccessLayer.Repositories
{
    public class GroupRepository : IGroupRepository
    {
        private readonly MyContext _context;

        public GroupRepository(MyContext context)
        {
            _context = context;
        }

        public async Task AddGroup(Group group)
        {
            await _context.Groups.AddAsync(group);
        }

        public async Task DeleteGroup(int groupId)
        {
            var group = await _context.Groups.FindAsync(groupId);
            _context.Groups.Remove(group);
        }

        public async Task<List<Group>> GetAllGroups()
        {
            return await _context.Groups.ToListAsync();
        }

        public async Task<Group> GetGroupById(int groupId)
        {
            return await _context.Groups.FirstOrDefaultAsync(g => g.Id == groupId);
        }

        public async Task<List<Group>> GetGroupsByUserId(string UserId)
        {
            return await _context.Groups.Where(g => g.TeacherId == UserId).ToListAsync();
        }

        public async Task<bool?> SaveChanges()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task UpdateGroup(int GroupId, Group group)
        {
            var groupToUpdate = await GetGroupById(GroupId);
            _context.Entry(groupToUpdate).CurrentValues.SetValues(group);

        }
    }
}
