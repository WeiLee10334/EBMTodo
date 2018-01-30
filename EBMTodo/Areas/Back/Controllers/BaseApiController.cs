using EBMTodo.Areas.Back.Models;
using EBMTodo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EBMTodo.Areas.Back.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T">ViewModel</typeparam>
    /// <typeparam name="U">QueryModel extendable override ExtraFilter</typeparam>
    /// <typeparam name="TRepo">db</typeparam>
    public abstract class BaseApiController<T, U, TRepo> : ApiController
        where T : IQueryableViewModel<T>, new()
        where U : PagingQueryModel
        where TRepo : ApplicationDbContext, new()
    {
        private T Model;
        private TRepo db;
        public BaseApiController()
        {
            db = new TRepo();
            Model = new T();
        }
        /// <summary>
        /// default orderby require defined
        /// </summary>
        /// <param name="query"></param>
        /// <param name="orderby"></param>
        /// <param name="reverse"></param>
        /// <returns></returns>
        public abstract IQueryable<T> SetOrderBy(IQueryable<T> query, string orderby, bool reverse);
        /// <summary>
        /// datetimerange hook
        /// </summary>
        /// <param name="query"></param>
        /// <param name="Start"></param>
        /// <param name="End"></param>
        /// <returns></returns>
        public virtual IQueryable<T> SetDateTimeRange(IQueryable<T> query, DateTime? Start, DateTime? End)
        {
            return query;
        }
        /// <summary>
        /// search hook
        /// </summary>
        /// <param name="query"></param>
        /// <param name="filters"></param>
        /// <returns></returns>
        public virtual IQueryable<T> SetFilter(IQueryable<T> query, Dictionary<string, string> filters)
        {
            foreach (var filter in filters)
            {
                var prop = typeof(T).GetProperty(filter.Key);
                if (prop != null && !string.IsNullOrEmpty(filter.Value))
                {
                    if (prop.PropertyType == typeof(string))
                    {
                        query = query.Where(filter.Key + ".Contains(@0)", filter.Value);
                    }
                }
            }
            return query;
        }
        /// <summary>
        /// extra filter setting hook
        /// </summary>
        /// <param name="query"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public virtual IQueryable<T> ExtraFilter(IQueryable<T> query, U model)
        {
            return query;
        }

        [Route("GetList")]
        [HttpPost]
        public virtual IHttpActionResult GetList(U model)
        {
            try
            {
                var dataset = Model.GetQueryable(db);
                var query = dataset;
                var DateTimeFilters = new Dictionary<string, DateTime>();
                query = SetFilter(query, model.Filters);
                query = SetDateTimeRange(query, model.Start, model.End);
                query = ExtraFilter(query, model);
                var data = SetOrderBy(query, model.OrderBy, model.Reverse);
                var result = new PagingViewModel<T>()
                {
                    Skip = model.Skip,
                    Length = model.Length,
                    Total = data.Count(),
                    Data = data.Skip(model.Skip).Take(model.Length).ToList()
                };
                return Ok(result);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.NotAcceptable, e.Message);
            }
        }
        [Route("Create")]
        [HttpPost]
        public virtual IHttpActionResult Create(T model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    return Ok(Model.Create(db, model));
                }
                catch (Exception e)
                {
                    return Content(HttpStatusCode.NotAcceptable, e.Message);
                }
            }
            else
            {
                return Content(HttpStatusCode.NotAcceptable, "格式錯誤");
            }
        }
        [Route("Update")]
        [HttpPost]
        public virtual IHttpActionResult Update(T model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    return Ok(Model.Update(db, model));
                }
                catch (Exception e)
                {
                    return Content(HttpStatusCode.NotAcceptable, e.Message);
                }
            }
            else
            {
                return Content(HttpStatusCode.NotAcceptable, "格式錯誤");
            }
        }
        [Route("Delete")]
        [HttpPost]
        public virtual IHttpActionResult Delete(T model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Model.Delete(db, model);
                    return Ok();
                }
                catch (Exception e)
                {
                    return Content(HttpStatusCode.NotAcceptable, e.Message);
                }
            }
            else
            {
                return Content(HttpStatusCode.NotAcceptable, "格式錯誤");
            }
        }
    }
    public interface IQueryableViewModel<T>
    {
        IQueryable<T> GetQueryable(ApplicationDbContext context);
        T Create(ApplicationDbContext context, T model);
        T Update(ApplicationDbContext context, T model);
        void Delete(ApplicationDbContext context, T model);
    }
}
