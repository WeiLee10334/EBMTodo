using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EBMTodo.Controllers
{
    [RoutePrefix("ngapp/{any}")]
    public class ngAppController : Controller
    {
        // GET: ngApp
        [Route]
        public ActionResult Index()
        {
            var result = new FilePathResult($"~/ngApp/index.html", "text/html");
            return result;
        }
    }
}