using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using EBMTodo.Models;
using EBMTodo.Models.Todo;

namespace EBMTodo.Controllers
{
    public class EBMProjects1Controller : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/EBMProjects1
        public IQueryable<EBMProject> GetEBMProject()
        {
            return db.EBMProject;
        }

        // GET: api/EBMProjects1/5
        [ResponseType(typeof(EBMProject))]
        public async Task<IHttpActionResult> GetEBMProject(Guid id)
        {
            EBMProject eBMProject = await db.EBMProject.FindAsync(id);
            if (eBMProject == null)
            {
                return NotFound();
            }

            return Ok(eBMProject);
        }

        // PUT: api/EBMProjects1/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutEBMProject(Guid id, EBMProject eBMProject)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != eBMProject.PID)
            {
                return BadRequest();
            }

            db.Entry(eBMProject).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EBMProjectExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/EBMProjects1
        [ResponseType(typeof(EBMProject))]
        public async Task<IHttpActionResult> PostEBMProject(EBMProject eBMProject)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.EBMProject.Add(eBMProject);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = eBMProject.PID }, eBMProject);
        }

        // DELETE: api/EBMProjects1/5
        [ResponseType(typeof(EBMProject))]
        public async Task<IHttpActionResult> DeleteEBMProject(Guid id)
        {
            EBMProject eBMProject = await db.EBMProject.FindAsync(id);
            if (eBMProject == null)
            {
                return NotFound();
            }

            db.EBMProject.Remove(eBMProject);
            await db.SaveChangesAsync();

            return Ok(eBMProject);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EBMProjectExists(Guid id)
        {
            return db.EBMProject.Count(e => e.PID == id) > 0;
        }
    }
}