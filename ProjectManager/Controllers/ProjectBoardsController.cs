using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ProjectManager.DAL;
using ProjectManager.Models;

namespace ProjectManager.Controllers
{
    public class ProjectBoardsController : ApiController
    {
        private PMContext db = new PMContext();

        // GET: api/ProjectBoards
        public IQueryable<ProjectBoard> GetBoards()
        {
            return db.Boards;
        }

        // GET: api/ProjectBoards/5
        [ResponseType(typeof(ProjectBoard))]
        public IHttpActionResult GetProjectBoard(int id)
        {
            ProjectBoard projectBoard = db.Boards.Find(id);
            if (projectBoard == null)
            {
                return NotFound();
            }

            return Ok(projectBoard);
        }

        // PUT: api/ProjectBoards/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProjectBoard(int id, ProjectBoard projectBoard)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != projectBoard.Id)
            {
                return BadRequest();
            }

            db.Entry(projectBoard).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectBoardExists(id))
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

        // POST: api/ProjectBoards
        [ResponseType(typeof(ProjectBoard))]
        public IHttpActionResult PostProjectBoard(ProjectBoard projectBoard)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Boards.Add(projectBoard);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = projectBoard.Id }, projectBoard);
        }

        // DELETE: api/ProjectBoards/5
        [ResponseType(typeof(ProjectBoard))]
        public IHttpActionResult DeleteProjectBoard(int id)
        {
            ProjectBoard projectBoard = db.Boards.Find(id);
            if (projectBoard == null)
            {
                return NotFound();
            }

            db.Boards.Remove(projectBoard);
            db.SaveChanges();

            return Ok(projectBoard);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProjectBoardExists(int id)
        {
            return db.Boards.Count(e => e.Id == id) > 0;
        }
    }
}