using EBMTodo.Areas.Back.Models;
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
    [RoutePrefix("api/back/EBMUser")]
    public class EBMUserController : ApiController
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
        [Route("GetList")]
        [HttpPost]
        public IHttpActionResult GetList(EBMUserQueryModel model)
        {
            try
            {
                var dataset = EBMUserViewModel.GetQueryable(db);
                model.OrderBy = typeof(EBMUserViewModel).GetProperty(model.OrderBy) == null ?
                (model.Reverse ? "UserName descending" : "UserName") :
                (model.Reverse ? model.OrderBy + " descending" : model.OrderBy);
                var query = dataset;

                foreach (var filter in model.Filters)
                {
                    var prop = typeof(EBMUserViewModel).GetProperty(filter.Key);
                    if (prop != null && prop.PropertyType == typeof(string))
                    {
                        query = query.Where("@0.Contains(@1)", filter.Key, filter.Value);
                    }
                }

                var data = query;
                var result = new PagingViewModel<EBMUserViewModel>()
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
        [Route("Create")]
        [HttpPost]
        public IHttpActionResult Create(EBMUserViewModel model)
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
        [Route("Update")]
        [HttpPost]
        public IHttpActionResult Update(EBMUserViewModel model)
        {
            var data = db.Users.Find(model.Id);
            if (data != null)
            {
                try
                {
                    data.UserName = model.UserName;
                    data.LineID = model.UID;
                    db.Entry(data).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return Ok(model);
                }
                catch (Exception e)
                {
                    return Content(HttpStatusCode.NotAcceptable, e.Message);
                }
            }
            return BadRequest("not exist");
        }
        [Route("Delete")]
        [HttpPost]
        public IHttpActionResult Delete(EBMUserViewModel model)
        {
            var data = db.Users.Find(model.Id);
            if (data != null)
            {
                try
                {
                    db.Entry(data).State = System.Data.Entity.EntityState.Deleted;
                    db.SaveChanges();
                    return Ok();
                }
                catch (Exception e)
                {
                    return Content(HttpStatusCode.NotAcceptable, e.Message);
                }
            }
            return BadRequest("not exist");
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
        public IHttpActionResult GetLineList(EBMUserQueryModel model)
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
                    if (prop != null && prop.PropertyType == typeof(string))
                    {
                        query = query.Where("@0.Contains(@1)", filter.Key, filter.Value);
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
    public class EBMUserViewModel
    {
        public EBMUserViewModel()
        {

        }
        public static IQueryable<EBMUserViewModel> GetQueryable(ApplicationDbContext context)
        {
            return context.Users.Select(x => new EBMUserViewModel()
            {
                Id = x.Id,
                UserName = x.UserName,
                Email = x.Email,
                UID = x.LineID
            });
        }
        public string Id { set; get; }

        public string Email { set; get; }

        public string UserName { set; get; }

        public string UID { set; get; }
    }
    public class EBMUserQueryModel : PagingQueryModel
    {
    }
}
