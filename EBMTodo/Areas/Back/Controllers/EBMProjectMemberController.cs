using EBMTodo.Areas.Back.Models;
using EBMTodo.Filters;
using EBMTodo.Models;
using EBMTodo.Models.Todo;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Dynamic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EBMTodo.Areas.Back.Controllers
{
    [ValidateViewModelAttribute]
    [RoutePrefix("api/back/EBMProjectMember")]
    public class EBMProjectMemberController : BaseApiController<EBMProjectMemberViewModel, EBMProjectMemberQueryModel, ApplicationDbContext>
    {
        public override IQueryable<EBMProjectMemberViewModel> ExtraFilter(IQueryable<EBMProjectMemberViewModel> query, EBMProjectMemberQueryModel model)
        {
            if (!string.IsNullOrEmpty(model.PID))
            {
                query = query.Where(x => x.PID == model.PID);
            }
            if (!string.IsNullOrEmpty(model.Id))
            {
                query = query.Where(x => x.Id == model.Id);
            }
            return query;
        }

        public override IQueryable<EBMProjectMemberViewModel> SetDateTimeRange(IQueryable<EBMProjectMemberViewModel> query, DateTime? Start, DateTime? End)
        {
            Start = Start == null ? DateTime.MinValue : Start;
            End = End == null ? DateTime.MaxValue : End;
            return query.Where(x => x.CreateDateTime >= Start && x.CreateDateTime <= End);
        }

        public override IQueryable<EBMProjectMemberViewModel> SetFilter(IQueryable<EBMProjectMemberViewModel> query, Dictionary<string, string> filters)
        {
            return base.SetFilter(query, filters);
        }

        public override IQueryable<EBMProjectMemberViewModel> SetOrderBy(IQueryable<EBMProjectMemberViewModel> query, string orderby, bool reverse)
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
    }
    public class EBMProjectMemberViewModel : IQueryableViewModel<EBMProjectMemberViewModel>
    {
        public EBMProjectMemberViewModel()
        {

        }
        public IQueryable<EBMProjectMemberViewModel> GetQueryable(ApplicationDbContext context)
        {
            return context.EBMProjectMember.Select(x => new EBMProjectMemberViewModel()
            {
                PMID = x.PMID.ToString(),
                CreateDateTime = x.CreateDateTime,
                title = x.title,
                Id = x.Id,
                UserName = x.ApplicationUser.UserName,
                PID = x.PID.ToString(),
                ProjectName = x.EBMProject.ProjectName
            });
        }

        public EBMProjectMemberViewModel Create(ApplicationDbContext context, EBMProjectMemberViewModel model)
        {
            var data = new EBMProjectMember()
            {
                title = model.title,
                CreateDateTime = DateTime.Now,
                PID = Guid.Parse(model.PID)
            };
            context.EBMProjectMember.Add(data);
            context.SaveChanges();
            model.PMID = data.PMID.ToString();
            model.CreateDateTime = data.CreateDateTime;
            return model;
        }

        public EBMProjectMemberViewModel Update(ApplicationDbContext context, EBMProjectMemberViewModel model)
        {
            var data = context.EBMProjectMember.Find(Guid.Parse(model.PMID));
            if (data != null)
            {

                data.title = model.title;
                data.Id = model.Id;
                data.PID = Guid.Parse(model.PID);
                context.Entry(data).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
                return model;
            }
            return null;
        }

        public void Delete(ApplicationDbContext context, EBMProjectMemberViewModel model)
        {
            var data = context.EBMProjectMember.Find(Guid.Parse(model.PMID));
            if (data != null)
            {

                context.Entry(data).State = System.Data.Entity.EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public string PMID { set; get; }

        public DateTime? CreateDateTime { set; get; }
        [StringLength(100)]
        public string title { set; get; }

        public string Id { set; get; }

        public string UserName { set; get; }
        [Required]
        public string PID { set; get; }

        public string ProjectName { set; get; }
    }
    public class EBMProjectMemberQueryModel : PagingQueryModel
    {
        public string PID { set; get; }

        public string Id { set; get; }
    }
}
