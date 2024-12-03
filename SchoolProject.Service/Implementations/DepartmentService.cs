using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.Abstracts;
using SchoolProject.Service.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Service.Implementations
{
    public class DepartmentService : IDepartmentService
    {
        #region
        private readonly IDepartmentRepository _departmentRepository;
        #endregion

        #region
        public DepartmentService(IDepartmentRepository departmentRepository) 
        {
            _departmentRepository = departmentRepository;
        }
        #endregion

        #region
        #endregion

        public async Task<Department> GetDepartmentByIDAsync(int Id)
        {
            var student = await _departmentRepository.GetTableNoTracking().Where(d => d.DID.Equals(Id))
                                                                                    .Include(x => x.DepartmentSubjects).ThenInclude(x => x.Subject)
                                                                                    .Include(x => x.Students)
                                                                                    .Include(x => x.Instructors)
                                                                                    .Include(x => x.Instructor)
                                                                                    .FirstOrDefaultAsync();
            return student;
        }
    }
}
