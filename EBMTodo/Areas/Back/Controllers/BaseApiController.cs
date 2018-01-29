using EBMTodo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EBMTodo.Areas.Back.Controllers
{
    public abstract class BaseApiController<T, TRepo>
        where T : class
        where TRepo : ApplicationDbContext, new()
    {
        private T model;
    }
    public interface IQueryableViewModel<T>
    {
        IQueryable<T> GetQueryable(ApplicationDbContext context);
    }
}
