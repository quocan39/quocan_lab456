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

      
        public ActionResult Create()
        {
            var ViewModel = new CourseViewModel
            {
                Categories = _dbContext.Categories.ToList()
            };
            return View(ViewModel);
        }
        // GET: Courses
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CourseViewModel ViewModel)
        {
            var course = new Course
            {
                LecturerId = User.Identity.GetUserId(),
                dateTime = ViewModel.GetDateTime(),
                CategoryId = ViewModel.Category,
                Place = ViewModel.Place        
            };
            _dbContext.Courses.Add(course);
            _dbContext.SaveChanges();

            return RedirectToAction("Index","Home");
        }
    }
}