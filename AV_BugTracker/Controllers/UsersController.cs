﻿using AV_BugTracker.Helpers;
using AV_BugTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AV_BugTracker.Controllers
{
    public class UsersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private UserRoleHelper roleHelper = new UserRoleHelper();
        // GET: Users
        [Authorize]
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        [Authorize(Roles = "Admin")]
        public ActionResult ManageUserRole(string id)
        {
            var userRole = roleHelper.ListUserRoles(id).FirstOrDefault();
            ViewBag.RoleName = new SelectList(db.Roles, "Name", "Name", userRole);

            return View(db.Users.Find(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ManageUserRole(string id, string roleName)
        {
            foreach (var role in roleHelper.ListUserRoles(id))
            {
                roleHelper.RemoveUserFromRole(id, role);
            }

            if (!string.IsNullOrEmpty(roleName))
            {
                roleHelper.AddUserToRole(id, roleName);
            }

            db.SaveChanges();
            return RedirectToAction("ManageUserRole", new { id });
        }
    }
}