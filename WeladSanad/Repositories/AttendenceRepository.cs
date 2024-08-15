namespace WeladSanad.Repositories
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
        
        public async Task UpdateAttendence(int StdAtt, StudentAttend stdAttendence)
        {
            var stdAtt = await _context.StudentAttends.FindAsync(StdAtt);
            _context.Entry(stdAtt).CurrentValues.SetValues(stdAttendence);
        }
    }
}
