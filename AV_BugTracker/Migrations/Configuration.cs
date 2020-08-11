namespace AV_BugTracker.Migrations
{
	using System;
	using System.Data.Entity;
	using System.Data.Entity.Migrations;
	using System.Linq;
	using Microsoft.AspNet.Identity;
	using Microsoft.AspNet.Identity.EntityFramework;
	using AV_BugTracker.Models;

	internal sealed class Configuration : DbMigrationsConfiguration<AV_BugTracker.Models.ApplicationDbContext>
	{
		public Configuration()
		{
			AutomaticMigrationsEnabled = true;
		}


		protected override void Seed(AV_BugTracker.Models.ApplicationDbContext context)
		{
			var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

			if (!context.Roles.Any(r => r.Name == "Admin"))
			{
				roleManager.Create(new IdentityRole { Name = "Admin" });
			}

			if (!context.Roles.Any(r => r.Name == "ProjectManager"))
			{
				roleManager.Create(new IdentityRole { Name = "ProjectManager" });
			}

			if (!context.Roles.Any(r => r.Name == "Developer"))
			{
				roleManager.Create(new IdentityRole { Name = "Developer" });
			}

			if (!context.Roles.Any(r => r.Name == "Submitter"))
			{
				roleManager.Create(new IdentityRole { Name = "Submitter" });
			}

			if (!context.Roles.Any(r => r.Name == "New User"))
			{
				roleManager.Create(new IdentityRole { Name = "New User" });
			}


			var userManager = new UserManager<ApplicationUser>(
				new UserStore<ApplicationUser>(context));


			if (!context.Users.Any(u => u.Email == "avelez9882@gmail.com"))
			{
				userManager.Create(new ApplicationUser()
				{
					Email = "avelez9882@gmail.com",
					UserName = "avelez9882@gmail.com",
					FirstName = "Angelica",
					LastName = "Velez",
					DisplayName = "Angelica",

				}, "Jupiter92!");

				var userId = userManager.FindByEmail("avelez9882@gmail.com").Id;

				userManager.AddToRole(userId, "Admin");
			}


			context.SaveChanges();

			#region TicketType Seed 
			context.TicketTypes.AddOrUpdate(
				tt => tt.Name,
				new TicketType() { Name = "Software" },
				new TicketType() { Name = "Hardware" },
				new TicketType() { Name = "UI" },
				new TicketType() { Name = "Defect" },
				new TicketType() { Name = "Other" },
				new TicketType() { Name = "Feature Request  " }
				);
			#endregion

			#region TicketPriority Seed
			context.TicketPriorities.AddOrUpdate(
				tp => tp.Name,
				new TicketPriority() { Name = "Low" },
				new TicketPriority() { Name = "Medium" },
				new TicketPriority() { Name = "High" },
				new TicketPriority() { Name = "On Hold" }
				);
			#endregion

			#region TicketStatus Seed
			context.TicketStatuses.AddOrUpdate(
				ts => ts.Name,
				new TicketStatus() { Name = "Open" },
				new TicketStatus() { Name = "Assigned" },
				new TicketStatus() { Name = "Resolved" },
				new TicketStatus() { Name = "Reopened" },
				new TicketStatus() { Name = "Archived" }
				);
			#endregion

			#region Project Seed 
			context.Projects.AddOrUpdate(
				p => p.Name,
				new Project() { Name = "Seed 1", Created = DateTime.Now.AddDays(-60), IsArchived = true },
				new Project() { Name = "Seed 2", Created = DateTime.Now.AddDays(-45) },
				new Project() { Name = "Seed 3", Created = DateTime.Now.AddDays(-30) },
				new Project() { Name = "Seed 4", Created = DateTime.Now.AddDays(-15) },
				new Project() { Name = "Seed 5", Created = DateTime.Now.AddDays(-7) }
			);
			#endregion

			context.SaveChanges();
		}

	}

}
