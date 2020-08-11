using AV_BugTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.Validation;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;


namespace AV_BugTracker.Helpers
{
	public class ProjectHelper
	{
		ApplicationDbContext db = new ApplicationDbContext();
		UserRoleHelper roleHelper = new UserRoleHelper();

		public bool IsUserOnProject(string userId, int projectId)
		{
			Project project = db.Projects.Find(projectId);
			var flag = project.Users.Any(u => u.Id == userId);
			return (flag);
		}

		public ICollection<Project> ListUserProjects(string userId)
		{
			ApplicationUser user = db.Users.Find(userId);

			var projects = user.Projects.ToList();
			return (projects);
		}

		public void AddUserToProject(string userId, int projectId)
		{
			if (!IsUserOnProject(userId, projectId))
			{
				Project proj = db.Projects.Find(projectId);
				var newUser = db.Users.Find(userId);

				proj.Users.Add(newUser);
				db.SaveChanges();
			}
		}

		public void RemoveUserFromProject(string userId, int projectId)
		{
			if (IsUserOnProject(userId, projectId))
			{
				Project proj = db.Projects.Find(projectId);
				var delUser = db.Users.Find(userId);

				proj.Users.Remove(delUser);
				db.Entry(proj).State = EntityState.Modified;
				db.SaveChanges();
			}
		}

		public ICollection<ApplicationUser> UsersOnProject(int projectId)
		{
			return db.Projects.Find(projectId).Users;
		}

		public ICollection<ApplicationUser> UsersNotOnProject(int projectId)
		{
			return db.Users.Where(u => u.Projects.All(p => p.Id != projectId)).ToList();

		}

		public List<ApplicationUser> ListUsersonProjectInRole(int projectId, string roleName)
		{
			var userList = UsersOnProject(projectId);
			var resultList = new List<ApplicationUser>();
			foreach (var user in userList)
			{
				if (roleHelper.IsUserInRole(user.Id, roleName))
				{
					resultList.Add(user);
				}
			}

			return resultList;
		}
	}
}