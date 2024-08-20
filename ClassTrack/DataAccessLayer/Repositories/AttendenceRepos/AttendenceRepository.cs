using System.Globalization;
using ClassTrack.DataAccessLayer.Models.Attendence;
using ClassTrack.DataAccessLayer.Models.Context;
using ClassTrack.DataAccessLayer.Repositories.IRepository.IAttendenceRepos;

namespace ClassTrack.DataAccessLayer.Repositories.AttendenceRepos
{
    public class AttendenceRepository : IAttendenceRepository
    {
        private readonly MyContext _context;
        public AttendenceRepository(MyContext context)
        {
            _context = context;
        }

        public async Task AddAttendence(StudentAttend attendence)
        {
            await _context.StudentAttends.AddAsync(attendence);
        }

        public async Task DeleteAttendence(int Id)
        {
            var attendence = await _context.StudentAttends.FindAsync(Id);
            _context.StudentAttends.Remove(attendence);
        }

        public async Task<StudentAttend> GetAttendenceById(int Id)
        {
            return await _context.StudentAttends.FirstOrDefaultAsync(att => att.Id == Id);
        }

        public async Task<List<StudentAttend>> GetAttendences()
        {
            return await _context.StudentAttends.ToListAsync();
        }

        public async Task<bool?> SaveChanges()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<StudentAttend>> Search(string searchMonthYear) // Expected format: "08-2024"
        {
            if (!DateTime.TryParseExact(searchMonthYear, "MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime searchDate))
            {
                throw new ArgumentException("Invalid date format. Expected format: MM-yyyy.");
            }

            var result = _context.StudentAttends
                .Where(s => s.Date.Year == searchDate.Year && s.Date.Month == searchDate.Month)
                .ToList();

            return await Task.FromResult(result);
        }

        public async Task UpdateAttendence(int StdAtt, StudentAttend stdAttendence)
        {
            var stdAtt = await _context.StudentAttends.FirstOrDefaultAsync(s => s.Id == StdAtt);
            if (stdAtt != null)
            {
                if (stdAttendence.Description != null) stdAtt.Description = stdAttendence.Description;
                if (stdAttendence.Description != null) stdAtt.AttendId = stdAttendence.AttendId;
            }

        }
    }
}
