using EBMTodo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace EBMTodo.Controllers.Api.Models
{
    public class EBMPWorkingRepository<EBMPWorkingViewModel> : IRepository<EBMPWorkingViewModel>
        where EBMPWorkingViewModel : class
    {
        private ApplicationDbContext db;
        public EBMPWorkingRepository()
        {
            this.db = new ApplicationDbContext();
        }
        public void Create(EBMPWorkingViewModel instance)
        {
            throw new NotImplementedException();
        }

        public void Delete(EBMPWorkingViewModel instance)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public EBMPWorkingViewModel Get(Expression<Func<EBMPWorkingViewModel, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IQueryable<EBMPWorkingViewModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void Update(EBMPWorkingViewModel instance)
        {
            throw new NotImplementedException();
        }
    }
}