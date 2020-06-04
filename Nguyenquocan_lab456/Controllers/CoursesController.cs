using Microsoft.AspNet.Identity;
using Nguyenquocan_lab456.Models;
using Nguyenquocan_lab456.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nguyenquocan_lab456.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        public CoursesController()
        {
            _dbContext = new ApplicationDbContext();
        }

        public object ViewModel { get; private set; }

        // GET: Courses
        [Authorize]
        [HttpPost]
        public ActionResult Create(CourseViewModel Viewmodel)
        {
            var course = new Course
            {
                LecturerId = User.Identity.GetUserId(),
                DateTime = Viewmodel.GetDateTime(),
                CategoryId = Viewmodel.Category,
                Place = Viewmodel.Place        
            };
            _dbContext.Courses.Add(course);
            _dbContext.SaveChanges();

            return RedirectToAction("Index","Home");
        }
    }
}