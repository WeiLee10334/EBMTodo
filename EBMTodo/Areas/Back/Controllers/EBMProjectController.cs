using EBMTodo.Areas.Back.Models;
using EBMTodo.Filters;
using EBMTodo.Models;
using EBMTodo.Models.Todo;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Linq.Dynamic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EBMTodo.Areas.Back.Controllers
{
    public class EBMProjectViewModel : IQueryableViewModel<EBMProjectViewModel>
    {
        public EBMProjectViewModel Create(ApplicationDbContext context, EBMProjectViewModel model)
        {
            var data = new EBMProject()
            {
                CreateDateTime = DateTime.Now,
                IsHode = model.IsHode,
                ProjectName = model.ProjectName,
                ProjectNo = model.ProjectNo
            };
            context.EBMProject.Add(data);
            context.SaveChanges();
            model.PID = data.PID.ToString();
            model.CreateDateTime = data.CreateDateTime;
            return model;
        }

        public void Delete(ApplicationDbContext context, EBMProjectViewModel model)
        {
            var data = context.EBMProject.Find(Guid.Parse(model.PID));
            if (data != null)
            {

                context.Entry(data).State = System.Data.Entity.EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public IQueryable<EBMProjectViewModel> GetQueryable(ApplicationDbContext context)
        {
            return context.EBMProject.Select(x => new EBMProjectViewModel()
            {
                PID = x.PID.ToString(),
                ProjectName = x.ProjectName,
                CreateDateTime = x.CreateDateTime,
                IsHode = x.IsHode,
                ProjectNo = x.ProjectNo,
            });
        }

        public EBMProjectViewModel Update(ApplicationDbContext context, EBMProjectViewModel model)
        {
            var data = context.EBMProject.Find(Guid.Parse(model.PID));
            if (data != null)
            {

                data.ProjectName = model.ProjectName;
                data.IsHode = model.IsHode;
                data.ProjectNo = model.ProjectNo;
                context.Entry(data).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
                return model;
            }
            return null;
        }
        public string PID { set; get; }
        [StringLength(100)]
        public string ProjectName { set; get; }

        public DateTime? CreateDateTime { set; get; }
        [StringLength(100)]
        public string ProjectNo { set; get; }

        public bool IsHode { set; get; }
    }
    public class EBMProjectQueryModel : PagingQueryModel
    {

    }
    [ValidateViewModelAttribute]
    [RoutePrefix("api/back/EBMProject")]
    public class EBMProjectController : BaseApiController<EBMProjectViewModel, EBMProjectQueryModel, ApplicationDbContext>
    {
        public override IQueryable<EBMProjectViewModel> SetDateTimeRange(IQueryable<EBMProjectViewModel> query, DateTime? Start, DateTime? End)
        {
            Start = Start == null ? DateTime.MinValue : Start;
            End = End == null ? DateTime.MaxValue : End;
            return query.Where(x => x.CreateDateTime >= Start && x.CreateDateTime <= End);
        }

        public override IQueryable<EBMProjectViewModel> SetOrderBy(IQueryable<EBMProjectViewModel> query, string orderby, bool reverse)
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
}
