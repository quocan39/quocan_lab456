using Microsoft.AspNet.Identity;
using Nguyenquocan_lab456.DTO;
using Nguyenquocan_lab456.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Nguyenquocan_lab456.Controllers
{
    public class UnFollowingController : ApiController
    {
        private readonly ApplicationDbContext _dbContext;
        public UnFollowingController()
        {
            _dbContext = new ApplicationDbContext();
        }
        [HttpPost]
        public IHttpActionResult UnFollow(FollowingDto followingDto)
        {
            var userId = User.Identity.GetUserId();
            _dbContext.Followings.Remove(_dbContext.Followings.FirstOrDefault(a => a.FollowerId == userId && a.FolloweeId == followingDto.FolloweeId));
            _dbContext.SaveChanges();
            return Ok();
        }
    }
}
