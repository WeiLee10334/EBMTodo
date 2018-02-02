using EBMTodo.Areas.Back.Models;
using EBMTodo.Filters;
using EBMTodo.Models;
using EBMTodo.Models.Base.Enum;
using EBMTodo.Models.Todo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EBMTodo.Areas.Back.Controllers
{
    [ValidateViewModelAttribute]
    [RoutePrefix("api/back/EBMProjectOnline")]
    public class EBMProjectOnlineController : BaseApiController<EBMProjectOnlineViewModel, EBMProjectOnlineQueryModel, ApplicationDbContext>
    {
        public override IQueryable<EBMProjectOnlineViewModel> SetOrderBy(IQueryable<EBMProjectOnlineViewModel> query, string orderby, bool reverse)
        {
            if (!string.IsNullOrEmpty(orderby))
            {
                query = reverse ? query.OrderBy($"{orderby} descending") : query.OrderBy(orderby);
                return query;
            }
            else
            {
                return query.OrderBy("CreateDateTime descending");
            }
        }
        public override IQueryable<EBMProjectOnlineViewModel> SetDateTimeRange(IQueryable<EBMProjectOnlineViewModel> query, DateTime? Start, DateTime? End)
        {
            Start = Start == null ? DateTime.MinValue : Start;
            End = End == null ? DateTime.MaxValue : End;
            return query.Where(x => x.CreateDateTime >= Start && x.CreateDateTime <= End);
        }
    }
    public class EBMProjectOnlineQueryModel : PagingQueryModel
    {

    }
    public class EBMProjectOnlineViewModel : IQueryableViewModel<EBMProjectOnlineViewModel>
    {
        public EBMProjectOnlineViewModel()
        {

        }
        public IQueryable<EBMProjectOnlineViewModel> GetQueryable(ApplicationDbContext context)
        {
            return context.EBMProjectOnline.Select(x => new EBMProjectOnlineViewModel()
            {
                POID = x.POID.ToString(),
                ApplyDateTime = x.ApplyDateTime,
                ApplyName = x.ApplyName,
                HandleDateTime = x.HandleDateTime,
                HandleName = x.HandleName,
                ResolveDateTime = x.ResolveDateTime,
                ResponseName = x.ResponseName,
                title = x.title,
                Description = x.Description,
                CompleteRate = x.CompleteRate,
                ApplyDepartment = x.ApplyDepartment,
                CreateDateTime = x.CreateDateTime,
                Memo = x.Memo,
                OnlineCategories = x.OnlineCategories,
            });
        }

        public EBMProjectOnlineViewModel Create(ApplicationDbContext context, EBMProjectOnlineViewModel model)
        {
            var data = new EBMProjectOnline()
            {
                ApplyDateTime = model.ApplyDateTime,
                ApplyDepartment = model.ApplyDepartment,
                ApplyName = model.ApplyName,
                CompleteRate = model.CompleteRate,
                CreateDateTime = DateTime.Now,
                Description = model.Description,
                HandleDateTime = model.HandleDateTime,
                HandleName = model.HandleName,
                OnlineCategories = model.OnlineCategories,
                ResolveDateTime = model.ResolveDateTime,
                ResponseName = model.ResponseName,
                title = model.title
            };
            context.EBMProjectOnline.Add(data);
            context.SaveChanges();
            model.POID = data.POID.ToString();
            model.CreateDateTime = data.CreateDateTime;
            return model;
        }

        public EBMProjectOnlineViewModel Update(ApplicationDbContext context, EBMProjectOnlineViewModel model)
        {
            var data = context.EBMProjectOnline.Find(Guid.Parse(model.POID));
            if (data != null)
            {
                data.ApplyDateTime = model.ApplyDateTime;
                data.ApplyDepartment = model.ApplyDepartment;
                data.ApplyName = model.ApplyName;
                data.CompleteRate = model.CompleteRate;
                data.Description = model.Description;
                data.HandleDateTime = model.HandleDateTime;
                data.HandleName = model.HandleName;
                data.OnlineCategories = model.OnlineCategories;
                data.ResolveDateTime = model.ResolveDateTime;
                data.ResponseName = model.ResponseName;
                data.title = model.title;
                context.Entry(data).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
                return model;
            }
            return null;
        }

        public void Delete(ApplicationDbContext context, EBMProjectOnlineViewModel model)
        {
            var data = context.EBMProjectOnline.Find(Guid.Parse(model.POID));
            if (data != null)
            {
                context.Entry(data).State = System.Data.Entity.EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public string POID { set; get; }

        public DateTime CreateDateTime { set; get; }

        public DateTime ApplyDateTime { set; get; }

        public string ApplyDepartment { set; get; }

        public string ApplyName { set; get; }

        public string Description { set; get; }

        public DateTime? HandleDateTime { set; get; }

        public string HandleName { set; get; }

        public DateTime? ResolveDateTime { set; get; }

        public string ResponseName { set; get; }

        public string Memo { set; get; }

        public string title { set; get; }

        public int CompleteRate { set; get; }

        public OnlineCategories OnlineCategories { set; get; }
    }
}
