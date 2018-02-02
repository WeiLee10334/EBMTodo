using EBMTodo.Areas.Back.Models;
using EBMTodo.Filters;
using EBMTodo.Models;
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
    [RoutePrefix("api/back/EBMProjectTodoList")]
    public class EBMProjectTodoListController : BaseApiController<EBMTodoListViewModel, EBMTodoListQueryModel, ApplicationDbContext>
    {

        public override IQueryable<EBMTodoListViewModel> SetFilter(IQueryable<EBMTodoListViewModel> query, Dictionary<string, string> filters)
        {
            return base.SetFilter(query, filters);
        }

        public override IQueryable<EBMTodoListViewModel> SetDateTimeRange(IQueryable<EBMTodoListViewModel> query, DateTime? Start, DateTime? End)
        {
            Start = Start == null ? DateTime.MinValue : Start;
            End = End == null ? DateTime.MaxValue : End;
            return query.Where(x => x.ApplyDateTime >= Start && x.ApplyDateTime <= End);
        }

        public override IQueryable<EBMTodoListViewModel> SetOrderBy(IQueryable<EBMTodoListViewModel> query, string orderby, bool reverse)
        {
            if (!string.IsNullOrEmpty(orderby))
            {
                query = reverse ? query.OrderBy($"{orderby} descending") : query.OrderBy(orderby);
                return query;
            }
            else
            {
                return query.OrderBy("ApplyDateTime descending");
            }
        }

    }
    public class EBMTodoListViewModel : IQueryableViewModel<EBMTodoListViewModel>
    {
        public EBMTodoListViewModel()
        {

        }
        public IQueryable<EBMTodoListViewModel> GetQueryable(ApplicationDbContext context)
        {
            return context.EBMProjectTodoList.Select(x => new EBMTodoListViewModel()
            {
                PTLID = x.PTLID.ToString(),
                ApplyDateTime = x.ApplyDateTime,
                ApplyName = x.ApplyName,
                title = x.title,
                Description = x.Description,
                CompleteRate = x.CompleteRate,
                PMID = x.PMID.ToString(),
                MemberTitle = x.EBMProjectMember == null ? null : x.EBMProjectMember.title,
                Memo = x.Memo,
                Tag = x.Tag,
                PID = x.EBMProjectMember == null ? null : x.EBMProjectMember.PID.ToString(),
                ProjectName = x.EBMProjectMember == null ? null : x.EBMProjectMember.EBMProject.ProjectName
            });
        }

        public EBMTodoListViewModel Create(ApplicationDbContext context, EBMTodoListViewModel model)
        {
            var data = new EBMProjectTodoList()
            {
                title = model.title,
                CreateDateTime = DateTime.Now,
                ApplyDateTime = model.ApplyDateTime,
                ApplyName = model.ApplyName,
                Description = model.Description,
                PMID = Guid.Parse(model.PMID),
                CompleteRate = model.CompleteRate,
                Tag = model.Tag,
                Memo = model.Memo
            };
            context.EBMProjectTodoList.Add(data);
            context.SaveChanges();
            model.PTLID = data.PTLID.ToString();
            model.ApplyDateTime = data.ApplyDateTime;
            return model;
        }

        public EBMTodoListViewModel Update(ApplicationDbContext context, EBMTodoListViewModel model)
        {

            var data = context.EBMProjectTodoList.Find(Guid.Parse(model.PTLID));
            if (data != null)
            {
                data.title = model.title;
                data.ApplyDateTime = model.ApplyDateTime;
                data.ApplyName = model.ApplyName;
                data.CompleteRate = model.CompleteRate;
                data.Description = model.Description;
                data.Tag = model.Tag;
                data.Memo = model.Memo;
                data.PMID = Guid.Parse(model.PMID);
                context.Entry(data).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
                model.MemberTitle = data.EBMProjectMember.title;
                return model;
            }
            return null;
        }

        public void Delete(ApplicationDbContext context, EBMTodoListViewModel model)
        {
            var data = context.EBMProjectTodoList.Find(Guid.Parse(model.PTLID));
            if (data != null)
            {
                context.Entry(data).State = System.Data.Entity.EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public string PTLID { set; get; }

        public DateTime ApplyDateTime { set; get; }

        public string ApplyName { set; get; }

        public string title { set; get; }

        public string Description { set; get; }

        public int CompleteRate { set; get; }

        public string PMID { set; get; }

        public string MemberTitle { set; get; }

        public string Tag { set; get; }

        public string Memo { set; get; }

        public string PID { set; get; }

        public string ProjectName { set; get; }
    }
    public class EBMTodoListQueryModel : PagingQueryModel
    {
        public string PID { set; get; }

        public string PMID { set; get; }
    }
}
