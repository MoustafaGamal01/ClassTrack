﻿using ClassTrack.DataAccessLayer.Models.Attendence;

namespace ClassTrack.DataAccessLayer.Repositories.IRepository.IAttendenceRepos
{
    public interface IAttendenceRepository
    {
        Task<StudentAttend> GetAttendenceById(int id);
        Task<List<StudentAttend>> GetAttendences();
        Task AddAttendence(StudentAttend attendence);
        Task UpdateAttendence(int stdAtt, StudentAttend attendence);
        Task DeleteAttendence(int id);
        Task<List<StudentAttend>> Search(string searchMonthYear);
        Task<List<StudentAttend>> GetAttendenceByStudentId(int StudentId);
        Task<bool?> SaveChanges();
    }
}
