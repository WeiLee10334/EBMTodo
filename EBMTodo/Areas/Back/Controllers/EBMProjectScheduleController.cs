using EBMTodo.Areas.Back.Models;
using EBMTodo.Filters;
using EBMTodo.Models;
using EBMTodo.Models.Base.Enum;
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
    [RoutePrefix("api/back/EBMProjectSchedule")]
    public class EBMProjectScheduleController : BaseApiController<EBMProjectScheduleViewModel, EBMProjectScheduleQueryModel, ApplicationDbContext>
    {
        public override IQueryable<EBMProjectScheduleViewModel> SetOrderBy(IQueryable<EBMProjectScheduleViewModel> query, string orderby, bool reverse)
        {
            if (!string.IsNullOrEmpty(orderby))
            {
                query = reverse ? query.OrderBy($"{orderby} descending") : query.OrderBy(orderby);
                return query;
            }
            else
            {
                return query.OrderBy("ScheduleDateTime descending");
            }
        }

        public override IQueryable<EBMProjectScheduleViewModel> SetDateTimeRange(IQueryable<EBMProjectScheduleViewModel> query, DateTime? Start, DateTime? End)
        {
            Start = Start == null ? DateTime.MinValue : Start;
            End = End == null ? DateTime.MaxValue : End;
            return query.Where(x => x.ScheduleDateTime >= Start && x.ScheduleDateTime <= End);
        }
    }
    public class EBMProjectScheduleViewModel : IQueryableViewModel<EBMProjectScheduleViewModel>
    {
        public EBMProjectScheduleViewModel()
        {

        }
        public IQueryable<EBMProjectScheduleViewModel> GetQueryable(ApplicationDbContext context)
        {
            return context.EBMProjectSchedule.Select(x => new EBMProjectScheduleViewModel()
            {
                PSID = x.PSID.ToString(),
                ScheduleDateTime = x.ScheduleDateTime,
                Target = x.Target,
                Description = x.Description,
                WokingHour = x.WokingHour,
                scheduleType = x.scheduleType,
                FinishDateTime = x.FinishDateTime,
                LineUID = x.LineUID,
                Title = x.Title,
                ProgressingFlag = x.ProgressingFlag,
                Id = x.Id,
                PID = x.PID.ToString()
            });
        }

        public EBMProjectScheduleViewModel Create(ApplicationDbContext context, EBMProjectScheduleViewModel model)
        {
            var data = new EBMProjectSchedule()
            {
                CreateDateTime = DateTime.Now,
                Title = model.Title,
                scheduleType = model.scheduleType,
                ScheduleDateTime = model.ScheduleDateTime,
                Description = model.Description,
                PID = Guid.Parse(model.PID),
                Id = model.Id,
                Target = model.Target,
                FinishDateTime = model.FinishDateTime,
                WokingHour = model.WokingHour,
                ProgressingFlag = model.ProgressingFlag
            };
            context.EBMProjectSchedule.Add(data);
            context.SaveChanges();
            model.PSID = data.PSID.ToString();
            return model;
        }

        public EBMProjectScheduleViewModel Update(ApplicationDbContext context, EBMProjectScheduleViewModel model)
        {
            var data = context.EBMProjectSchedule.Find(Guid.Parse(model.PSID));
            if (data != null)
            {
                data.CreateDateTime = DateTime.Now;
                data.Title = model.Title;
                data.scheduleType = model.scheduleType;
                data.ScheduleDateTime = model.ScheduleDateTime;
                data.Description = model.Description;
                data.PID = Guid.Parse(model.PID);
                data.Id = model.Id;
                data.Target = model.Target;
                data.FinishDateTime = model.FinishDateTime;
                data.WokingHour = model.WokingHour;
                data.ProgressingFlag = model.ProgressingFlag;
                context.Entry(data).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
                return model;
            }
            return null;
        }

        public void Delete(ApplicationDbContext context, EBMProjectScheduleViewModel model)
        {
            var data = context.EBMProjectSchedule.Find(Guid.Parse(model.PSID));
            if (data != null)
            {

                context.Entry(data).State = System.Data.Entity.EntityState.Deleted;
                context.SaveChanges();
            }

        }

        public string PSID { set; get; }

        public DateTime ScheduleDateTime { set; get; }

        public string Target { set; get; }

        public string Description { set; get; }

        public decimal WokingHour { set; get; }

        public ScheduleType scheduleType { set; get; }

        public DateTime FinishDateTime { set; get; }

        public string LineUID { set; get; }

        public string Title { set; get; }

        public bool ProgressingFlag { set; get; }

        public string Id { set; get; }
        public string UserName { set; get; }

        public string PID { set; get; }
    }
    public class EBMProjectScheduleQueryModel : PagingQueryModel
    {
    }
}
