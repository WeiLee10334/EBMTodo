using EBMTodo.Controllers.Api.Models;
using EBMTodo.Models;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EBMTodo.Controllers.Api
{
    [RoutePrefix("api/memo")]
    public class MemoController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        [HttpPost]
        [Route("getData")]
        public IHttpActionResult getData(MemoQueryModel para)
        {
            para.start = para.start == null ? DateTime.Now.Date.AddDays(-7) : para.start.Value.Date;
            para.end = para.end == null ? DateTime.Now.Date.AddDays(1) : para.end.Value.Date.AddDays(1);
            var model = db.Memo.Select(x => new MemoViewModel
            {
                MID = x.MID,
                CreateDateTime = x.CreateDateTime,
                memo = x.memo,
                Content = x.Content,
                memoType = x.memoType.ToString(),
                ProgressingFlag = x.ProgressingFlag,
                LineUID = x.LineUID,
                Tag = x.Tag,
                WorkerName = db.LineUser.FirstOrDefault(y => y.UID == x.LineUID).Name
            });
            var predicate = PredicateBuilder.New<MemoViewModel>(true);
            predicate = predicate.And(x => x.CreateDateTime >= para.start && x.CreateDateTime <= para.end);

            if (para.UIDs != null && para.UIDs.Count > 0)
            {
                var orPredicate = PredicateBuilder.New<MemoViewModel>();
                foreach (var id in para.UIDs)
                {
                    orPredicate = orPredicate.Or(x => x.LineUID == id);
                }
                predicate = predicate.And(orPredicate);
            }
            var data = model.Where(predicate).ToList();
            return Ok(data);
        }
        [HttpPost]
        [Route("getDataByTime")]
        public IHttpActionResult getDataByTime(MemoQueryModel para)
        {
            para.start = para.start == null ? DateTime.Now.Date.AddDays(-7) : para.start.Value.Date;
            para.end = para.end == null ? DateTime.Now.Date.AddDays(1) : para.end.Value.Date.AddDays(1);
            var model = db.Memo.Select(x => new MemoViewModel
            {
                MID = x.MID,
                CreateDateTime = x.CreateDateTime,
                memo = x.memo,
                Content = x.Content,
                memoType = x.memoType.ToString(),
                ProgressingFlag = x.ProgressingFlag,
                LineUID = x.LineUID,
                Tag = x.Tag,
                WorkerName = db.LineUser.FirstOrDefault(y => y.UID == x.LineUID).Name
            });
            var predicate = PredicateBuilder.New<MemoViewModel>(true);
            predicate = predicate.And(x => x.CreateDateTime >= para.start && x.CreateDateTime <= para.end);

            if (para.UIDs != null && para.UIDs.Count > 0)
            {
                var orPredicate = PredicateBuilder.New<MemoViewModel>();
                foreach (var id in para.UIDs)
                {
                    orPredicate = orPredicate.Or(x => x.LineUID == id);
                }
                predicate = predicate.And(orPredicate);
            }

            var data = model.Where(predicate).ToList()
                .GroupBy(x =>
                {
                    switch (para.groupby)
                    {
                        case "day":
                            return x.CreateDateTime.ToString("yyyy-MM-dd");
                        case "week":
                            return x.CreateDateTime.FirstDayOfWeek().ToString("yyyy-MM-dd") + "~" + x.CreateDateTime.LastDayOfWeek().ToString("yyyy-MM-dd");
                        case "month":
                            DateTime date = x.CreateDateTime;
                            var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
                            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
                            return firstDayOfMonth.ToString("yyyy-MM-dd") + "~" + lastDayOfMonth.ToString("yyyy-MM-dd");
                        default:
                            return x.CreateDateTime.ToString("yyyy-MM-dd");
                    }
                })
               .Select(x => new
               {
                   date = x.Key,
                   list = x.ToList()
               }).OrderBy(x => x.date).ToList();
            return Ok(data);
        }
        [HttpPost]
        [Route("getDataByUID")]
        public IHttpActionResult getDataByUID(MemoQueryModel para)
        {
            //預設一個禮拜
            para.start = para.start == null ? DateTime.Now.Date.AddDays(-7) : para.start.Value.Date;
            para.end = para.end == null ? DateTime.Now.Date.AddDays(1) : para.end.Value.Date.AddDays(1);
            var model = db.Memo.Select(x => new MemoViewModel
            {
                MID = x.MID,
                CreateDateTime = x.CreateDateTime,
                memo = x.memo,
                Content = x.Content,
                memoType = x.memoType.ToString(),
                ProgressingFlag = x.ProgressingFlag,
                LineUID = x.LineUID,
                Tag = x.Tag,
                WorkerName = db.LineUser.FirstOrDefault(y => y.UID == x.LineUID).Name
            });
            var predicate = PredicateBuilder.New<MemoViewModel>(true);
            predicate = predicate.And(x => x.CreateDateTime >= para.start && x.CreateDateTime <= para.end);

            if (para.UIDs != null && para.UIDs.Count > 0)
            {
                var orPredicate = PredicateBuilder.New<MemoViewModel>();
                foreach (var id in para.UIDs)
                {
                    orPredicate = orPredicate.Or(x => x.LineUID == id);
                }
                predicate = predicate.And(orPredicate);
            }

            var data = model.Where(predicate).ToList()
                .GroupBy(x => new
                {
                    UID = x.LineUID,
                    Name = x.WorkerName
                })
               .Select(x => new
               {
                   lineUID = x.Key.UID,
                   workerName = x.Key.Name,
                   list = x.ToList()
               }).ToList();
            return Ok(data);
        }
    }
    public class MemoQueryModel
    {
        public DateTime? start { set; get; }
        public DateTime? end { set; get; }
        public List<string> UIDs { set; get; }
        public string groupby { set; get; }
    }
}
