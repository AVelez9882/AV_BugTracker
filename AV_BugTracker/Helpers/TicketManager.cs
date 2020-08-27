using AV_BugTracker.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AV_BugTracker.Helpers
{
	public class TicketManager
	{
		private ApplicationDbContext db = new ApplicationDbContext();
		private UserRoleHelper roleHelper = new UserRoleHelper();
		private ProjectHelper projectHelper = new ProjectHelper();
		public List<Ticket> GetMyTickets()
		{
			var userId = HttpContext.Current.User.Identity.GetUserId();
			var tickets = new List<Ticket>();
			var myRole = roleHelper.ListUserRoles(userId).FirstOrDefault();
			List<Ticket> model = new List<Ticket>();
			switch (myRole)
			{
				case "Admin":
					model = db.Tickets.ToList();
					break;
				case "ProjectManager":
				case "Developer":
					model = projectHelper.ListUserProjects(userId).SelectMany(p => p.Tickets).ToList();
					break;
				case "Submitter":
					model = db.Tickets.Where(T => T.SubmitterId == userId).ToList();
					break;
				default:
					return model;
			}
			return model;
		}

		public void ManageTicketNotifications(Ticket oldTicket, Ticket newTicket)
		{
			//scenario 1: a new assignment, oldTicket.DeveloperId = null newTicket.DeveloperId is not
			if (oldTicket.DeveloperId == null && newTicket.DeveloperId != null)
			{
				var newNotification = new TicketNotification()
				{
					TicketId = newTicket.Id,
					UserId = newTicket.DeveloperId,
					Created = DateTime.Now,
					Subject = $"You have been assigned a Ticket Id: {newTicket.Id}",
					Body = $"Heads up {newTicket.Developer.FirstName}, you have been assigned to Ticket Id: {newTicket.Id} titled '{newTicket.Issue}' on Project '{newTicket.Project.Name}'"

				};

				db.TicketNotifications.Add(newNotification);
				db.SaveChanges();
			}

			//scenario 2: unassignment - oldTicket.DeveloperId = was not null, and newTicket.DeveloperId is 
			if (oldTicket.DeveloperId != null && newTicket.DeveloperId == null)
			{
				var unassignNotification = new TicketNotification()
				{
					TicketId = oldTicket.Id,
					UserId = oldTicket.DeveloperId,
					Created = DateTime.Now,
					Subject = $"You have been unassigned from Ticket Id: {oldTicket.Id}",
					Body = $"Heads up, {oldTicket.Developer.FirstName}, you have been unassigned from Ticket Id: {oldTicket.Id} titled '{oldTicket.Issue}' on Project '{oldTicket.Project.Name}'"
				};

				db.TicketNotifications.Add(unassignNotification);
				db.SaveChanges();
			}


			//scenario 3: reassignment - neither old nor new ticket.DeveloperId is null, and they don't match 
			if (oldTicket.DeveloperId != null && newTicket.DeveloperId != null && oldTicket.DeveloperId != newTicket.DeveloperId)
			{
				var unassignNotification = new TicketNotification()
				{
					TicketId = oldTicket.Id,
					UserId = oldTicket.DeveloperId,
					Created = DateTime.Now,
					Subject = $"You have been unassigned from Ticket Id: {oldTicket.Id}",
					Body = $"Heads up, {oldTicket.Developer.FirstName}, you have been unassigned from Ticket Id: {oldTicket.Id} titled '{oldTicket.Issue}' on Project '{oldTicket.Project.Name}'"
				};

				var assignNotification = new TicketNotification()
				{
					TicketId = newTicket.Id,
					UserId = newTicket.DeveloperId,
					Created = DateTime.Now,
					Subject = $"You have been assigned to Ticket Id: {newTicket.Id}",
					Body = $"Heads up, {newTicket.Developer.FirstName}, you have been assigned to Ticket Id: {newTicket.Id} titled '{newTicket.Issue}' on Project '{newTicket.Project.Name}'"
				};

				db.TicketNotifications.Add(unassignNotification);
				db.TicketNotifications.Add(assignNotification);
				db.SaveChanges();
			}
		}


		public void AttachmentNotifications(Ticket ticket)
		{
			//scenario 1: a new assignment, oldTicket.DeveloperId = null newTicket.DeveloperId is not
			var newNotification = new TicketNotification()
			{
				TicketId = ticket.Id,
				UserId = ticket.DeveloperId,
				Created = DateTime.Now,
				Subject = $"Attachment Added on the {ticket.Project.Name} project.",
				Body = $"Heads up {ticket.Developer.FirstName}, Ticket Id: {ticket.Id} titled '{ticket.Issue}' on Project '{ticket.Project.Name}' has a new attachment."

			};

			db.TicketNotifications.Add(newNotification);
			db.SaveChanges();

		}


		public void CommentNotifications(Ticket ticket)
		{
			//scenario 1: a new assignment, oldTicket.DeveloperId = null newTicket.DeveloperId is not
			var newNotification = new TicketNotification()
			{
				TicketId = ticket.Id,
				UserId = ticket.DeveloperId,
				Created = DateTime.Now,
				Subject = $"Comment Added on the {ticket.Project.Name} project.",
				Body = $"Heads up {ticket.Developer.FirstName}, Ticket Id: {ticket.Id} titled '{ticket.Issue}' on Project '{ticket.Project.Name}' has a new comment."

			};

			db.TicketNotifications.Add(newNotification);
			db.SaveChanges();

		}
	}
}