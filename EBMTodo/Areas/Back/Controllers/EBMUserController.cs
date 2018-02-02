using EBMTodo.Areas.Back.Models;
using EBMTodo.Filters;
using EBMTodo.Models;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Dynamic;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;


namespace EBMTodo.Areas.Back.Controllers
{
    [ValidateViewModelAttribute]
    [RoutePrefix("api/back/EBMUser")]
    public class EBMUserController : BaseApiController<EBMUserViewModel, EBMUserQueryModel, ApplicationDbContext>
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                var context = Request.Properties["MS_HttpContext"] as HttpContextWrapper;
                if (context != null)
                {
                    return _userManager ?? HttpContextBaseExtensions.GetOwinContext(context.Request).GetUserManager<ApplicationUserManager>();
                }
                return null;
            }
            private set
            {
                _userManager = value;
            }
        }

        [Route("Create")]
        [HttpPost]
        public override IHttpActionResult Create(EBMUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = new ApplicationUser { UserName = model.UserName, Email = model.Email };
                    string password = "a123456";
                    var result = UserManager.CreateAsync(user, password).Result;
                    if (result.Succeeded)
                    {
                        model.Id = user.Id;
                        var line = db.LineUser.FirstOrDefault(x => x.UID == model.UID);
                        if (line != null)
                        {
                            model.LineUserName = line.Name;
                        }
                        return Ok(model);
                    }
                    return Content(HttpStatusCode.NotAcceptable, result.Errors);

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
        [Route("ChangePassword")]
        [HttpPost]
        public IHttpActionResult ChangePassword(ChangePasswordModel model)
        {
            var user = UserManager.FindByIdAsync(model.Id).Result;
            if (user != null)
            {
                UserManager.ResetPasswordAsync(user.Id, UserManager.GeneratePasswordResetTokenAsync(user.Id).Result, model.Password);
                return Ok();
            }
            return Content(HttpStatusCode.NotAcceptable, "格式錯誤");
        }
        [Route("Register")]
        [HttpPost]
        public IHttpActionResult Register(EBMUserRegisterViewModel model)
        {
            try
            {
                var user = new ApplicationUser { UserName = model.UserName, Email = model.Email };
                var result = UserManager.CreateAsync(user, model.Password).Result;
                if (result.Succeeded)
                {
                    return Ok();
                }
                return Content(HttpStatusCode.NotAcceptable, result.Errors);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.NotAcceptable, e.Message);
            }
        }
        [Route("GetLineList")]
        [HttpPost]
        public IHttpActionResult GetLineList(PagingQueryModel model)
        {
            try
            {
                var dataset = EBMLineUserViewModel.GetQueryable(db);
                model.OrderBy = typeof(EBMLineUserViewModel).GetProperty(model.OrderBy) == null ?
                (model.Reverse ? "UserName descending" : "UserName") :
                (model.Reverse ? model.OrderBy + " descending" : model.OrderBy);
                var query = dataset;

                foreach (var filter in model.Filters)
                {
                    var prop = typeof(EBMLineUserViewModel).GetProperty(filter.Key);
                    if (prop != null && prop.PropertyType == typeof(string) && !string.IsNullOrEmpty(filter.Value))
                    {
                        query = query.Where(filter.Key + ".Contains(@0)", filter.Value);
                    }
                }

                var data = query;
                var result = new PagingViewModel<EBMLineUserViewModel>()
                {
                    Skip = model.Skip,
                    Length = model.Length,
                    Total = data.Count(),
                    Data = data.OrderBy(model.OrderBy).Skip(model.Skip).Take(model.Length).ToList()
                };
                return Ok(result);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.NotAcceptable, e.Message);
            }
        }

        public override IQueryable<EBMUserViewModel> SetOrderBy(IQueryable<EBMUserViewModel> query, string orderby, bool reverse)
        {
            if (!string.IsNullOrEmpty(orderby))
            {
                query = reverse ? query.OrderBy($"{orderby} descending") : query.OrderBy(orderby);
                return query;
            }
            else
            {
                return query.OrderBy("UserName descending");
            }
        }
    }
    public class ChangePasswordModel
    {
        [Required]
        public string Id { set; get; }
        [Required]
        [StringLength(20, MinimumLength = 6)]
        public string Password { set; get; }
    }
    public class EBMUserRegisterViewModel
    {
        public string Email { set; get; }
        public string UserName { set; get; }
        public string Password { set; get; }
    }
    public class EBMLineUserViewModel
    {
        public EBMLineUserViewModel()
        {

        }
        public static IQueryable<EBMLineUserViewModel> GetQueryable(ApplicationDbContext context)
        {
            return context.LineUser.Select(x => new EBMLineUserViewModel()
            {
                UID = x.UID,
                UserName = x.Name
            });
        }
        public string UID { set; get; }

        public string UserName { set; get; }
    }
    public class EBMUserQueryModel : PagingQueryModel
    {

    }
    public class EBMUserViewModel : IQueryableViewModel<EBMUserViewModel>
    {
        public EBMUserViewModel()
        {

        }
        public IQueryable<EBMUserViewModel> GetQueryable(ApplicationDbContext context)
        {
            return context.Users.Select(x => new EBMUserViewModel()
            {
                Id = x.Id,
                UserName = x.UserName,
                Email = x.Email,
                UID = x.LineID,
                LineUserName = context.LineUser.FirstOrDefault(y => y.UID == x.LineID) == null ? "" : context.LineUser.FirstOrDefault(y => y.UID == x.LineID).Name
            });
        }

        public EBMUserViewModel Create(ApplicationDbContext context, EBMUserViewModel model)
        {
            throw new NotImplementedException();
        }

        public EBMUserViewModel Update(ApplicationDbContext context, EBMUserViewModel model)
        {
            var data = context.Users.Find(model.Id);
            if (data != null)
            {
                data.UserName = model.UserName;
                data.LineID = model.UID;
                context.Entry(data).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
                model.LineUserName = context.LineUser.FirstOrDefault(x => x.UID == model.UID).Name;
                return model;
            }
            return null;
        }

        public void Delete(ApplicationDbContext context, EBMUserViewModel model)
        {
            var data = context.Users.Find(model.Id);
            if (data != null)
            {
                context.Entry(data).State = System.Data.Entity.EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public string Id { set; get; }

        public string Email { set; get; }

        public string UserName { set; get; }

        public string UID { set; get; }

        public string LineUserName { set; get; }
    }
}
