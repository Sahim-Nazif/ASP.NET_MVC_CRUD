using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_CRUD.Models;

namespace MVC_CRUD.Controllers
{
    public class MoviesController : Controller
    {
        private readonly ApplicationDbContext _db;

        public MoviesController( ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }

        #region API Calls
        [HttpGet]
        public async Task<IActionResult>GetAll()
        {
            return Json(new { data = await _db.Movies.ToListAsync() });
        }
        [HttpDelete]
        public async Task<IActionResult>Delete( int id)
        {
            //retrieving the movie from DB
            var movieFromDB = await _db.Movies.FirstOrDefaultAsync(m => m.Id == id);
            if (movieFromDB==null)
            {
                return Json(new { success = false, message = "Error Occured when Deleting Movie" });

            }
            _db.Movies.Remove(movieFromDB);
            await _db.SaveChangesAsync();
            return Json(new { success = true, message = "Movie Deleted Successfully!" });
        }
        #endregion
    }
}