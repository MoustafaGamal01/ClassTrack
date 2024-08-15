using WeladSanad.Models;

namespace WeladSanad.Repositories.IRepository
{
    public interface IStudentRepository
    {
        Task<Student> GetStudentById(int id);
        Task<List<Student>> GetStudents();
        Task AddStudent(Student student);
        Task UpdateStudent(int StudentId, Student student);
        Task DeleteStudent(int id);
        Task<List<Student>> GetStudentsByGroupId(int groupId);
        Task<bool?> SaveChanges();
    }
}
