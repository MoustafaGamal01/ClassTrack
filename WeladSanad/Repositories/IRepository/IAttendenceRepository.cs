namespace WeladSanad.Repositories.IRepository
{
    public interface IAttendenceRepository
    {
        Task<StudentAttend> GetAttendenceById(int Id);
        Task<List<StudentAttend>> GetAttendences();
        Task AddAttendence(StudentAttend attendence);
        Task UpdateAttendence(int StdAtt, StudentAttend attendence);
        Task DeleteAttendence(int Id);
        Task<bool?> SaveChanges();
    }
}
