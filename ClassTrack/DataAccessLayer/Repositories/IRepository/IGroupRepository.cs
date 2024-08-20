using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using ClassTrack.DataAccessLayer.Models;

namespace ClassTrack.DataAccessLayer.Repositories.IRepository
{
    public interface IGroupRepository
    {
        Task<Group> GetGroupById(int groupId);

        Task<List<Group>> GetAllGroups();

        Task AddGroup(Group group);

        Task DeleteGroup(int groupId);

        Task UpdateGroup(int GroupId, Group group);

        Task<List<Group>> GetGroupsByUserId(string UserId);

        Task<bool?> SaveChanges();
    }
}
