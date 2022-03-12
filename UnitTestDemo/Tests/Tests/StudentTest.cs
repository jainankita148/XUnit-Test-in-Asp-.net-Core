using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Entity;
using WebApi.Service;
using Xunit;

namespace Tests
{
    public class StudentTest
    {
        private readonly Mock<IStudentService> studentService;

        public StudentTest()
        {
            studentService = new Mock<IStudentService>();
        }
        [Fact]
        public async Task GetAllStudent_Test()
        {
            // Arrange
            studentService.Setup(srvc => srvc.GetStudentList()).ReturnsAsync(new List<StudentEntity>()
            {
                new StudentEntity()
                {

                    StudentId=1,
                    FirstName=" Juan",
                    LastName="Hoffman",
                    DOB="5/7/1988",
                    Email= "juan.hoffman@example.com"
                },new StudentEntity()
                {
                    StudentId=2,
                    FirstName=" Gary",
                    LastName="Hawkins",
                    DOB="6/5/1985",
                    Email= "gary.hawkins@example.com"
                },new StudentEntity()
                {
                    StudentId=3,
                    FirstName="Claude",
                    LastName="Flores",
                    DOB="12/1/1952",
                    Email= "claude.flores@example.com"
                }
            });
            //Act
            var result = await studentService.Object.GetStudentList();
            //Assert
            Assert.True(result.Count == 3);
        }

        [Fact]
        public async Task AddSTudent_Test()
        {
            //Arrange
            StudentEntity studentEntity = null;
            studentService.Setup(srvc => srvc.AddStudent(It.IsAny<StudentEntity>())).Callback<StudentEntity>(x => studentEntity = x);
            var studData = new StudentEntity
            {
                FirstName = " First Name",
                LastName = "Last Name",
                DOB = "Date of Birth",
                Email = "Email"
            };

            //Act
            await studentService.Object.AddStudent(studData);

            //Assert
            studentService.Verify(x => x.AddStudent(It.IsAny<StudentEntity>()), Times.Once);
            Assert.Equal(studentEntity.FirstName, studData.FirstName);
            Assert.Equal(studentEntity.LastName, studData.LastName);
            Assert.Equal(studentEntity.DOB, studData.DOB);
            Assert.Equal(studentEntity.Email, studData.Email);
        }

        [Fact]
        public async Task UpdateStudent_Test()
        {
            //Arrange
            StudentEntity studentEntity = null;
            studentService.Setup(srvc => srvc.UpdateStudent(It.IsAny<StudentEntity>())).Callback<StudentEntity>(x => studentEntity = x);
            var studData = new StudentEntity
            {
                FirstName = " First Name updated",
                LastName = "Last Name updated",
                DOB = "Date of Birth updated",
                Email = "Email updated",
                StudentId = 2
            };
            //Act
            await studentService.Object.UpdateStudent(studData);
            //Assert
            studentService.Verify(x => x.UpdateStudent(It.IsAny<StudentEntity>()), Times.Once);

            Assert.Equal(studentEntity.FirstName, studData.FirstName);
            Assert.Equal(studentEntity.LastName, studData.LastName);
            Assert.Equal(studentEntity.DOB, studData.DOB);
            Assert.Equal(studentEntity.Email, studData.Email);


        }
        [Fact]
        public async Task DeleteStudent_Test()
        {
            //Arrange
            var studentId = 2;
            studentService.Setup(srvc => srvc.RemoveStudent(studentId));
            //Act
            await studentService.Object.RemoveStudent(studentId);
            //Assert
            studentService.Verify(repo => repo.RemoveStudent(studentId), Times.Once);

        }
    }
}
