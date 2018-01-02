using EBMTodo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EBMTodo.Controllers.Api
{
    [RoutePrefix("api/EBMProject")]
    public class EBMProjectController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        [Route("GetList")]
        public IHttpActionResult GetList()
        {

            return Ok();
        }
    }
}
