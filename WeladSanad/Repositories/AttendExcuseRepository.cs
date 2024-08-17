namespace WeladSanad.Repositories
{
    public class AttendExcuseRepository : IAttendExcuseRepository
    {
        private readonly MyContext _context;

        public AttendExcuseRepository(MyContext context)
        {
            _context = context;
        }

        public async Task<List<Attend>> GetAttendTypes()
        {
            return await _context.Attends.ToListAsync();    
        }

        public async Task<Attend> GetAttendType(int Id)
        {
            return await _context.Attends.FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task AddAttendType(Attend AttendType)
        {
            await _context.Attends.AddAsync(AttendType);
        }

        public async Task DeleteAttendType(int Id)
        {
            var AttendType = await _context.Attends.FirstOrDefaultAsync(x => x.Id == Id);
            _context.Attends.Remove(AttendType);
        }

        public async Task UpdateAttendType(int AttTypeId, Attend AttendType)
        {
            var AttendTypeToUpdate = await _context.Attends.FirstOrDefaultAsync(x => x.Id == AttTypeId);
            AttendTypeToUpdate.Type = AttendType.Type;
        }

        public async Task<bool?> SaveChanges()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
