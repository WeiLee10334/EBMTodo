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
    [RoutePrefix("api/back/EBMProjectWorking")]
    public class EBMProjectWorkingController : BaseApiController<EBMProjectWorkingViewModel, EBMProjectWorkingQueryModel, ApplicationDbContext>
    {
        public override IQueryable<EBMProjectWorkingViewModel> SetOrderBy(IQueryable<EBMProjectWorkingViewModel> query, string orderby, bool reverse)
        {
            if (!string.IsNullOrEmpty(orderby))
            {
                query = reverse ? query.OrderBy($"{orderby} descending") : query.OrderBy(orderby);
                return query;
            }
            else
            {
                return query.OrderBy("RecordDateTime descending");
            }
        }
        public override IQueryable<EBMProjectWorkingViewModel> ExtraFilter(IQueryable<EBMProjectWorkingViewModel> query, EBMProjectWorkingQueryModel model)
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
        public override IQueryable<EBMProjectWorkingViewModel> SetDateTimeRange(IQueryable<EBMProjectWorkingViewModel> query, DateTime? Start, DateTime? End)
        {
            Start = Start == null ? DateTime.MinValue : Start;
            End = End == null ? DateTime.MaxValue : End;
            return query.Where(x => x.RecordDateTime >= Start && x.RecordDateTime <= End);
        }
    }
    public class EBMProjectWorkingViewModel : IQueryableViewModel<EBMProjectWorkingViewModel>
    {
        public EBMProjectWorkingViewModel()
        {

        }
        public IQueryable<EBMProjectWorkingViewModel> GetQueryable(ApplicationDbContext context)
        {
            return context.EBMProjectWorking.Select(x => new EBMProjectWorkingViewModel()
            {
                PWID = x.PWID.ToString(),
                PID = x.PID.ToString(),
                ProjectName = x.EBMProject.ProjectName,
                Target = x.Target,
                Description = x.Description,
                WorkingHour = x.WokingHour,
                WorkingType = x.workingType.ToString(),
                RecordDateTime = x.RecordDateTime,
                Id = x.Id,
                UserName = x.ApplicationUser.UserName
            });
        }

        public EBMProjectWorkingViewModel Create(ApplicationDbContext context, EBMProjectWorkingViewModel model)
        {
            throw new NotImplementedException();
        }

        public EBMProjectWorkingViewModel Update(ApplicationDbContext context, EBMProjectWorkingViewModel model)
        {
            throw new NotImplementedException();
        }

        public void Delete(ApplicationDbContext context, EBMProjectWorkingViewModel model)
        {
            throw new NotImplementedException();
        }

        public string PWID { set; get; }

        public string PID { set; get; }

        public string ProjectName { set; get; }

        public string Target { set; get; }

        public string Description { set; get; }

        public decimal WorkingHour { set; get; }

        public string WorkingType { set; get; }

        public DateTime RecordDateTime { set; get; }

        public string Id { set; get; }

        public string UserName { set; get; }
    }
    public class EBMProjectWorkingQueryModel : PagingQueryModel
    {
        public string PID { set; get; }

        public string Id { set; get; }
    }
}
