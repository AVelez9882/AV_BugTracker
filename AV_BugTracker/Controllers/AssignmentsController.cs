using AV_BugTracker.Helpers;
using AV_BugTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace AV_BugTracker.Controllers
{
	public class AssignmentsController : Controller
	{

		private ApplicationDbContext db = new ApplicationDbContext();
		private UserRoleHelper roleHelper = new UserRoleHelper();
		private ProjectHelper projectHelper = new ProjectHelper();

		// GET: Assignments
		[Authorize(Roles = "Admin")]
		public ActionResult ManageRoles()
		{

			//use my view bag to hold a multi select list of users in the system 
			//new multi select list(the data, "Id", "Email")
			ViewBag.UserIds = new MultiSelectList(db.Users, "Id", "Email");

			ViewBag.RoleName = new SelectList(db.Roles, "Name", "Name");



			return View(db.Users.ToList());

		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult ManageRoles(List<string> userIds, string roleName)
		{
			if (userIds == null)
			{
				return RedirectToAction("ManageRoles");
			}

			foreach (var userId in userIds)
			{
				//determine if this user occupies a role, if so remove them from that role 
				foreach (var role in roleHelper.ListUserRoles(userId).ToList())
				{
					roleHelper.RemoveUserFromRole(userId, role);
				}


				//if a role was selected add the user to it 
				if (!string.IsNullOrEmpty(roleName))
				{
					roleHelper.AddUserToRole(userId, roleName);
				}
			}

			return RedirectToAction("ManageRoles");
		}

		[Authorize(Roles = "Admin")]
		public ActionResult ManageUserRoles()
		{
			return View();
		}

		//GET: Assign Projects to Users 
		[Authorize(Roles = "Admin, ProjectManager")]
		public ActionResult ManageProjectUsers()
		{
			ViewBag.UserIds = new MultiSelectList(db.Users, "Id", "Email");
			ViewBag.ProjectIds = new MultiSelectList(db.Projects, "Id", "Name");


			return View(db.Users.ToList());
		}

		//POST
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult ManageProjectUsers(List<string> userIds, List<int> projectIds)
		{
			if (userIds == null || projectIds == null)
			{

				return RedirectToAction("ManageProjectUsers");
			}

			foreach (var userId in userIds)
			{
				foreach (var projectId in projectIds)
				{
					if (projectHelper.IsUserOnProject(userId, projectId))
					{
						projectHelper.RemoveUserFromProject(userId, projectId);
					}
					else 
					{
						projectHelper.AddUserToProject(userId, projectId);
					}
				}
			}

			return RedirectToAction("ManageProjectUsers");
		}






		//GET
		[Authorize(Roles = "Admin, ProjectManager")]
		public ActionResult ManageUserProjects(string Id)
		{
			var myProjectIds = projectHelper.ListUserProjects(Id);

			ViewBag.UserId = Id;
			ViewBag.ProjectIds = new MultiSelectList(db.Projects, "Id", "Name", myProjectIds);

			return View();
		}


		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult ManageUserProjects(string userId, List<int> projectIds)
		{
			foreach (var project in projectHelper.ListUserProjects(userId).ToList())
			{
				projectHelper.RemoveUserFromProject(userId, project.Id);
			}

			if (projectIds != null)
			{
				foreach (var projectId in projectIds)
				{
					projectHelper.AddUserToProject(userId, projectId);
				}
			}

			return RedirectToAction("ManageUserProjects");

		}
	}




}
