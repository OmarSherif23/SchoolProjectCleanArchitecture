using SchoolProject.Data.Entities;
using SchoolProject.Data.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Service.Abstracts
{
    public interface IStudentService
    {
        public Task<List<Student>> GetStudentsListAsync();
        public Task<Student> GetStudentByIDWithIncudeAsync(int Id);
        public Task<Student> GetStudentByIDAsync(int Id);
        public Task<string> AddAsync(Student student);
        public Task<string> EditAsync(Student student);
        public Task<string> DeleteAsync(Student student);
        public Task<bool> IsNameExist(string name);
        public Task<bool> IsNameEnExistExcludeSelf(string nameEn,int id);
        public Task<bool> IsNameArExistExcludeSelf(string nameAr,int id);
        public IQueryable<Student> GetStudentsQuerable();
        public IQueryable<Student> FilterStudentPaginatedQuerable(StudentOrderingEnum order , string search);
    }
}
