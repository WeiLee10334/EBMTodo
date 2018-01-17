using EBMTodo.Models;
using EBMTodo.Models.Base.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EBMTodo.Areas.Back.Controllers
{
    public class EBMProjectOnlineController : ApiController
    {
    }
    public class EBMProjectOnlineViewModel
    {
        public EBMProjectOnlineViewModel()
        {

        }
        public static IQueryable<EBMProjectOnlineViewModel> GetQueryable(ApplicationDbContext context)
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
