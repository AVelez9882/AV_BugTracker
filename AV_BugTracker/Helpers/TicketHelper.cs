using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AV_BugTracker.Models;
using AV_BugTracker.Helpers;
using Microsoft.AspNet.Identity;

namespace AV_BugTracker.Helpers
{
	public class TicketHelper
	{
		private UserRoleHelper roleHelper = new UserRoleHelper();
		private ApplicationDbContext db = new ApplicationDbContext();

		public bool CanEditTicket(int ticketId)
		{
			var userId = HttpContext.Current.User.Identity.GetUserId();
			var myRole = roleHelper.ListUserRoles(userId).FirstOrDefault();
			switch (myRole)
			{
				case "Admin":
					return true;
				case "ProjectManager":
					var user = db.Users.Find(userId);
					return user.Projects.SelectMany(p => p.Tickets).Any(t => t.Id == ticketId);
				case "Developer":
				case "Submitter":
					var ticket = db.Tickets.Find(ticketId);
					if (ticket.DeveloperId == userId || ticket.SubmitterId == userId)
					{
						return true;
					}
					else
					{
						return false;
					}
				default:
					return false;
			}
		}


		public bool CanMakeComment(int ticketId)
		{
			var userId = HttpContext.Current.User.Identity.GetUserId();
			var myRole = roleHelper.ListUserRoles(userId).FirstOrDefault();
			switch (myRole)
			{
				case "Admin":
					return true;
				case "ProjectManager":
					var user = db.Users.Find(userId);
					return user.Projects.SelectMany(p => p.Tickets).Any(t => t.Id == ticketId);
				case "Developer":
				case "Submitter":
					var ticket = db.Tickets.Find(ticketId);
					if (ticket.DeveloperId == userId || ticket.SubmitterId == userId)
					{
						return true;
					}
					else 
					{
						return false;
					}
				default:
					return false;
			}
		}
	}
}