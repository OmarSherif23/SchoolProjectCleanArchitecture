using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Data.Helpers;
using SchoolProject.Infrastructure.Abstracts;
using SchoolProject.Infrastructure.Data;
using SchoolProject.Service.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Azure.Core.HttpHeader;

namespace SchoolProject.Service.Implementations
{
    public class StudentService : IStudentService
    {
        #region Fields
        private readonly IStudentRepository _studentRepository;
        #endregion

        #region Constructors
        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }
        #endregion

        #region Handle Functions
        public async Task<List<Student>> GetStudentsListAsync()
        {
            return await _studentRepository.GetStudentsListAsync();
        }
        public async Task<Student> GetStudentByIDWithIncudeAsync(int Id)
        {
            //var student = await _studentRepository.GetByIdAsync(Id);
            var student = _studentRepository.GetTableNoTracking().Include(d => d.Department).Where(s => s.StudID == Id).FirstOrDefault();
            return student; 
        }

        public async Task<string> AddAsync(Student student)
        {
            await _studentRepository.AddAsync(student);
            return "Success";
        }

        public async Task<bool> IsNameExist(string nameEn)
        {
            var student = _studentRepository.GetTableNoTracking().Where(s => s.NameEn.Equals(nameEn)).FirstOrDefault();
            if (student == null)
                return false;
            return true;
        }

        public async Task<bool> IsNameEnExistExcludeSelf(string nameEn, int id)
        {
            var student = await _studentRepository.GetTableNoTracking().Where(s=> s.NameEn.Equals(nameEn)&!s.StudID.Equals(id)).FirstOrDefaultAsync();
            if (student == null)
                return false;
            return true;
        }

        public async Task<bool> IsNameArExistExcludeSelf(string nameAr, int id)
        {
            var student = await _studentRepository.GetTableNoTracking().Where(s => s.NameAr.Equals(nameAr) & !s.StudID.Equals(id)).FirstOrDefaultAsync();
            if (student == null)
                return false;
            return true;
        }

        public async Task<string> EditAsync(Student student)
        {
            await _studentRepository.UpdateAsync(student);
            return "Success";
        }

        public async Task<string> DeleteAsync(Student student)
        {
            var trans = _studentRepository.BeginTransaction();
            try
            {
                await _studentRepository.DeleteAsync(student);
                await trans.CommitAsync();
                return "Success";
            }
            catch
            {
               await trans.RollbackAsync();
                return "Failed";
            }
        }

        public async Task<Student> GetStudentByIDAsync(int Id)
        {
            var student = await _studentRepository.GetByIdAsync(Id);
            return student;
        }

        public IQueryable<Student> GetStudentsQuerable()
        {
            return _studentRepository.GetTableNoTracking().Include(d => d.Department).AsQueryable();
        }

        public IQueryable<Student> FilterStudentPaginatedQuerable(StudentOrderingEnum order , string search)
        {
            var querable = _studentRepository.GetTableNoTracking().Include(d => d.Department).AsQueryable();
            if (search != null)
            {
                querable = querable.Where(e => e.NameEn.Contains(search) || e.Address.Contains(search));
            }
            switch(order)
            {
                case StudentOrderingEnum.StudID:
                    querable = querable.OrderBy(s => s.StudID);
                    break;

                case StudentOrderingEnum.Name:
                    querable = querable.OrderBy(s => s.NameEn);
                    break;
                    
                case StudentOrderingEnum.Address:
                    querable = querable.OrderBy(s => s.Address);
                    break; 

                case StudentOrderingEnum.DepartmentName:
                    querable = querable.OrderBy(s => s.Department.DNameEn);
                    break;

                default:
                    querable = querable.OrderBy(s => s.StudID);
                    break;
            }
            return querable;
        }
        #endregion
    }
}
