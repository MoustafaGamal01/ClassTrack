namespace WeladSanad.Repositories.IRepository
{
    public interface IAttendTypeRepository
    {
        Task<List<Attend>> GetAttendTypes();

        Task<Attend> GetAttendType(int Id);
    
        Task AddAttendType(Attend AttendType);

        Task DeleteAttendType(int Id);

        Task UpdateAttendType(int AttTypeId, Attend AttendType);

        Task<bool?> SaveChanges();
    }
}
