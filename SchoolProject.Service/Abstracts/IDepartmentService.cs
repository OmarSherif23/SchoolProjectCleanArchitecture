using SchoolProject.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Service.Abstracts
{
    public interface IDepartmentService
    {
        //public Task<Department> GetDepartmentByIDWithIncudeAsync(int Id);
        public Task<Department> GetDepartmentByIDAsync(int Id);
    }
}
