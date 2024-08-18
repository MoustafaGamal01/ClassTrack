using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using WeladSanad.DataAccessLayer.Models;

namespace WeladSanad.DataAccessLayer.Repositories.IRepository
{
    public interface IGroupRepository
    {
        Task<Group> GetGroupById(int groupId);

        Task<List<Group>> GetAllGroups();

        Task AddGroup(Group group);

        Task DeleteGroup(int groupId);

        Task UpdateGroup(int GroupId, Group group);

        Task<bool?> SaveChanges();
    }
}
