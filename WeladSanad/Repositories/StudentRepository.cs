using Microsoft.EntityFrameworkCore;
using WeladSanad.Models;
using WeladSanad.Repositories.IRepository;

namespace WeladSanad.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly MyContext _context;

        // implement this interface
        public StudentRepository(MyContext context)
        {
            _context = context;
        }

        public async Task AddStudent(Student student)
        {
            await _context.Students.AddAsync(student);
        }

        public async Task DeleteStudent(int id)
        {
            var std = await GetStudentById(id);
            if (std == null)
            {
                throw new Exception("Student not found");
            }
            _context.Students.Remove(std);
        }

        public async Task<Student> GetStudentById(int id)
        {
            return await _context.Students.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<List<Student>> GetStudents()
        {
           return await _context.Students.ToListAsync();
        }

        public async Task<bool?> SaveChanges()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task UpdateStudent(int stdId, Student student)
        {
            var std = await GetStudentById(stdId);
            if (std == null)
            {
                throw new Exception("Student not found");
            }
            _context.Entry(std).CurrentValues.SetValues(student);
        }
    }
}
