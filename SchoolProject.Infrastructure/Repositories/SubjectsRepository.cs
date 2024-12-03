using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.Abstracts;
using SchoolProject.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Infrastructure.Repositories
{
    public class SubjectsRepository : GenericRepositoryAsync<Subjects>, ISubjectsRepository
    {
        #region Fields
        private DbSet<Subjects> subjects;
        #endregion

        #region Constructors
        public SubjectsRepository(ApplicationDBContext dbContext) : base(dbContext)
        {
            subjects = dbContext.Set<Subjects>();
        }
        #endregion

        #region Handle Functions
        #endregion
    }
}
