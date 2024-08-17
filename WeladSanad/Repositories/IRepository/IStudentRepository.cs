using WeladSanad.Models;

namespace WeladSanad.Repositories.IRepository
{
    public interface IStudentRepository
    {
        Task<Student> GetStudentById(int Id);
        Task<List<Student>> GetStudents();
        Task AddStudent(Student Student);
        Task UpdateStudent(int StudentId, Student Student);
        Task DeleteStudent(int Id);
        Task<List<Student>> GetStudentsByGroupId(int GroupId);
        Task<List<Student>> GetDeletedStudents();
        Task<List<Student>> Search(string Name);
        Task<bool?> SaveChanges();
    }
}
