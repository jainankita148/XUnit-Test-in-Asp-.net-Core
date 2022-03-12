using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entity;

namespace WebApi.Service
{
    public class StudentService: IStudentService
    {
        private readonly ApplicationDBContext _dbContext;

        public StudentService(ApplicationDBContext dBContext)
        {
            _dbContext = dBContext;
        }

        public async Task AddStudent(StudentEntity studentEntity)
        {
            await _dbContext.StudentEntities.AddAsync(studentEntity);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<List<StudentEntity>> GetStudentList()
        {
            var studentList = await _dbContext.StudentEntities.ToListAsync();
            return studentList;
        }
        public async Task<StudentEntity> GetById(int studentId)
        {
            var studentData = await _dbContext.StudentEntities.FirstOrDefaultAsync(z => z.StudentId == studentId);
            return studentData;
        }
        public async Task UpdateStudent(StudentEntity studentEntity)
        {
            _dbContext.Entry(studentEntity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();

        }
        public async Task RemoveStudent(int studentId)
        {
            var alreadyExists = await _dbContext.StudentEntities.FirstOrDefaultAsync(z => z.StudentId == studentId);
            if (alreadyExists != null)
            {
                _dbContext.StudentEntities.Remove(alreadyExists);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
