using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Entity;

namespace WebApi.Service
{
    public interface IStudentService
    {
        Task AddStudent(StudentEntity studentEntity);
        Task<List<StudentEntity>> GetStudentList();
        Task<StudentEntity> GetById(int studentId);
        Task UpdateStudent(StudentEntity studentEntity);
        Task RemoveStudent(int studentId);
    }
}