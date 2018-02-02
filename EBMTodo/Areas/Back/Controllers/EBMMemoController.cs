using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EBMTodo.Areas.Back.Models;
using EBMTodo.Models;
using EBMTodo.Models.Base.Enum;
using System.Linq.Dynamic;
using EBMTodo.Models.Todo;
using EBMTodo.Filters;

namespace EBMTodo.Areas.Back.Controllers
{
    [ValidateViewModelAttribute]
    [RoutePrefix("api/back/EBMMemo")]
    public class EBMMemoController : BaseApiController<EBMMemoViewModel, EBMMemoQueryModel, ApplicationDbContext>
    {
        public override IQueryable<EBMMemoViewModel> SetOrderBy(IQueryable<EBMMemoViewModel> query, string orderby, bool reverse)
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
        public override IQueryable<EBMMemoViewModel> SetDateTimeRange(IQueryable<EBMMemoViewModel> query, DateTime? Start, DateTime? End)
        {
            Start = Start == null ? DateTime.MinValue : Start;
            End = End == null ? DateTime.MaxValue : End;
            return query.Where(x => x.CreateDateTime >= Start && x.CreateDateTime <= End);
        }
    }
    public class EBMMemoQueryModel : PagingQueryModel
    {

    }
    public class EBMMemoViewModel : IQueryableViewModel<EBMMemoViewModel>
    {
        public EBMMemoViewModel()
        {

        }
        public IQueryable<EBMMemoViewModel> GetQueryable(ApplicationDbContext context)
        {
            return context.Memo.Select(x => new EBMMemoViewModel()
            {
                MID = x.MID,
                CreateDateTime = x.CreateDateTime,
                Tag = x.Tag,
                Content = x.Content,
                ProgressingFlag = x.ProgressingFlag,
                memo = x.memo,
                memoType = x.memoType,
                LineUID = x.LineUID
            });
        }

        public EBMMemoViewModel Create(ApplicationDbContext context, EBMMemoViewModel model)
        {
            var data = new Memo()
            {
                CreateDateTime=DateTime.Now,
                Tag=model.Tag,
                Content=model.Content,
                ProgressingFlag=model.ProgressingFlag,
                memo=model.memo,
                memoType=model.memoType,
                LineUID=model.LineUID             
            };
            context.Memo.Add(data);
            context.SaveChanges();
            model.MID = data.MID;
            model.CreateDateTime = data.CreateDateTime;
            return model;
        }

        public void Delete(ApplicationDbContext context, EBMMemoViewModel model)
        {
            var data = context.Memo.Find(model.MID);
            if (data != null)
            {
                context.Entry(data).State = System.Data.Entity.EntityState.Deleted;
                context.SaveChanges();
            }
        }


        public EBMMemoViewModel Update(ApplicationDbContext context, EBMMemoViewModel model)
        {
            var data = context.Memo.Find(model.MID);
            if (data != null)
            {
                data.Tag = model.Tag;
                data.Content = model.Content;
                data.ProgressingFlag = model.ProgressingFlag;
                data.memo = model.memo;
                data.memoType = model.memoType;
                data.LineUID = model.LineUID;
                context.SaveChanges();
                return model;
            }
            return null;
        }

        public int MID { set; get; }

        public DateTime CreateDateTime { set; get; }

        public string Tag { set; get; }

        public string Content { set; get; }

        public bool ProgressingFlag { set; get; }

        public MemoType memoType { set; get; }

        public string LineUID { set; get; }

        public string memo { set; get; }
    }
}
