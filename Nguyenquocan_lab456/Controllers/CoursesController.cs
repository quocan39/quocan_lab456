using Microsoft.AspNet.Identity;
using Nguyenquocan_lab456.Models;
using Nguyenquocan_lab456.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        // GET: Courses
        [Authorize]
        public ActionResult Create()
        {
            var ViewModel = new CourseViewModel
            {
                Categories = _dbContext.Categories.ToList(),
                Heading = "Add Course"
            };
            return View(ViewModel);
        }

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
        [Authorize]
        public ActionResult Attending()
        {
            var userId = User.Identity.GetUserId();

            var courses = _dbContext.Attendances
                .Where(a => a.AttendeeId == userId)
                .Select(a => a.Course)
                .Include(l => l.Lecturer)
                .Include(l => l.Category)
                .ToList();

            var viewModel = new CourseViewModel
            {
                UpcommingCourse = courses,
                ShowAction = User.Identity.IsAuthenticated
            };
            return View(viewModel);
        }
        [Authorize]
        public ActionResult Mine()
        {
            var userId = User.Identity.GetUserId();
            var courses = _dbContext.Courses
                .Where(c => c.LecturerId == userId && c.dateTime > DateTime.Now)
                .Include(l => l.Lecturer)
                .Include(c => c.Category)
                .ToList();

            return View(courses);
        }
        [Authorize]
        public ActionResult Edit(int id)
        {
            var userId = User.Identity.GetUserId();
            var courses = _dbContext.Courses.Single(c => c.Id == id && c.LecturerId == userId);

            var viewModel = new CourseViewModel
            {
                Categories = _dbContext.Categories.ToList(),
                Date = courses.dateTime.ToString("dd/M/yyyy"),
                Time = courses.dateTime.ToString("HH:mm"),
                Category = courses.CategoryId,
                Place = courses.Place,
                Heading = "Edit Course",
                Id = courses.Id
            };
            return View("Create", viewModel);
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(CourseViewModel ViewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewModel.Categories = _dbContext.Categories.ToList();
                return View("Create", ViewModel);
            }
            var userId = User.Identity.GetUserId();
            var courses = _dbContext.Courses.Single(c => c.Id == ViewModel.Id && c.LecturerId == userId);

            courses.Place = ViewModel.Place;
            courses.dateTime = ViewModel.GetDateTime();
            courses.CategoryId = ViewModel.Category;

            _dbContext.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
        [Authorize]
        public ActionResult Following()
        {
            var userId = User.Identity.GetUserId();
            var followings = _dbContext.Followings
                .Where(a => a.FollowerId == userId)
                .Select(a => a.Followee)
                .ToList();
            return View(followings);
        }
    }
}