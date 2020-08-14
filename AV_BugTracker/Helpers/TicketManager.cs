using AV_BugTracker.Models;
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
		public List<Ticket> GetMyTickets(string userId)
		{
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

	}
}