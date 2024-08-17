namespace WeladSanad.Repositories.IRepository
{
    public interface IAttendenceRepository
    {
        Task<StudentAttend> GetAttendenceById(int id);
        Task<List<StudentAttend>> GetAttendences();
        Task AddAttendence(StudentAttend attendence);
        Task UpdateAttendence(int stdAtt, StudentAttend attendence);
        Task DeleteAttendence(int id);
        Task<List<StudentAttend>> Search(string searchMonthYear);
        Task<bool?> SaveChanges();
    }
}
