using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;

namespace WeladSanad.Repositories.IRepository
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
