using EBMTodo.Controllers.Api.Models;
using EBMTodo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EBMTodo.Controllers.Api.Services
{
    public class DataService
    {
        private ApplicationDbContext db;
        public DataService()
        {
            this.db = new ApplicationDbContext();
        }
        //public IEnumerable<EBMPWorkingViewModel> getData(IQueryable<EBMPWorkingViewModel> query)
        //{
        //    this.db.EBMProjectWorking.Where(query)
        //}
    }
}