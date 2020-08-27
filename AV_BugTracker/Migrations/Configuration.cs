namespace AV_BugTracker.Migrations
{
	using System;
	using System.Data.Entity;
	using System.Data.Entity.Migrations;
	using System.Linq;
	using Microsoft.AspNet.Identity;
	using Microsoft.AspNet.Identity.EntityFramework;
	using AV_BugTracker.Models;
	using System.Web.Configuration;
	using System.Collections.Generic;
	using AV_BugTracker.Helpers;

	internal sealed class Configuration : DbMigrationsConfiguration<AV_BugTracker.Models.ApplicationDbContext>
	{
		private ProjectHelper projectHelper = new ProjectHelper();
		private UserRoleHelper roleHelper = new UserRoleHelper();
		Random random = new Random();

		public Configuration()
		{
			AutomaticMigrationsEnabled = true;
		}

		protected override void Seed(AV_BugTracker.Models.ApplicationDbContext context)
		{

			var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
			#region Role Creation
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
			#endregion

			var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
			var demoPassword = WebConfigurationManager.AppSettings["DemoPassword"];
			//var demoPMPassword = WebConfigurationManager.AppSettings["DemoPMPassword"];
			//var demoDevPassword = WebConfigurationManager.AppSettings["DemoDevPassword"];
			//var demoSubPassword = WebConfigurationManager.AppSettings["DemoSubPassword"];
			//var adminEmail = WebConfigurationManager.AppSettings["DemoAdminEmail"];

			#region User Creation
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
				var adminId = userManager.FindByEmail("avelez9882@gmail.com").Id;
				userManager.AddToRole(adminId, "Admin");
			}

			if (!context.Users.Any(u => u.Email == "DemoAdminEmail@mailinator.com"))
			{
				userManager.Create(new ApplicationUser()
				{
					Email = "DemoAdminEmail@mailinator.com",
					UserName = "DemoAdminEmail@mailinator.com",
					FirstName = "Anne",
					LastName = "Weber",
					DisplayName = "Anne",
					AvatarPath = "/Avatars/default_avatar.png"

				}, demoPassword);
			}

			if (!context.Users.Any(u => u.Email == "DemoPMEmail@mailinator.com"))
			{
				userManager.Create(new ApplicationUser()
				{
					Email = "DemoPMEmail@mailinator.com",
					UserName = "DemoPMEmail@mailinator.com",
					FirstName = "Reeva",
					LastName = "Smart",
					DisplayName = "Reeva",
					AvatarPath = "/Avatars/default_avatar.png"

				}, demoPassword);
			}

			if (!context.Users.Any(u => u.Email == "DemoDevEmail@mailinator.com"))
			{
				userManager.Create(new ApplicationUser()
				{
					Email = "DemoDevEmail@mailinator.com",
					UserName = "DemoDevEmail@mailinator.com",
					FirstName = "Francesca",
					LastName = "Martins",
					DisplayName = "Francesca",
					AvatarPath = "/Avatars/default_avatar.png"

				}, demoPassword);
			}

			if (!context.Users.Any(u => u.Email == "DemoSubEmail@mailinator.com"))
			{
				userManager.Create(new ApplicationUser()
				{
					Email = "DemoSubEmail@mailinator.com",
					UserName = "DemoSubEmail@mailinator.com",
					FirstName = "Gianni",
					LastName = "Tang",
					DisplayName = "Gianni",
					AvatarPath = "/Avatars/default_avatar.png"

				}, demoPassword);
			}

			if (!context.Users.Any(u => u.Email == "DemoPM2Email@mailinator.com"))
			{
				userManager.Create(new ApplicationUser()
				{
					Email = "DemoPM2Email@mailinator.com",
					UserName = "DemoPM2Email@mailinator.com",
					FirstName = "Geraint",
					LastName = "Wyatt",
					DisplayName = "Geraint",
					AvatarPath = "/Avatars/default_avatar.png"

				}, demoPassword);
			}

			if (!context.Users.Any(u => u.Email == "DemoPM3Email@mailinator.com"))
			{
				userManager.Create(new ApplicationUser()
				{
					Email = "DemoPM3Email@mailinator.com",
					UserName = "DemoPM3Email@mailinator.com",
					FirstName = "Simra",
					LastName = "Hussain",
					DisplayName = "Simra",
					AvatarPath = "/Avatars/default_avatar.png"

				}, demoPassword);
			}

			if (!context.Users.Any(u => u.Email == "DemoDev2Email@mailinator.com"))
			{
				userManager.Create(new ApplicationUser()
				{
					Email = "DemoDev2Email@mailinator.com",
					UserName = "DemoDev2Email@mailinator.com",
					FirstName = "Kimberly",
					LastName = "Andrews",
					DisplayName = "Kimberly",
					AvatarPath = "/Avatars/default_avatar.png"

				}, demoPassword);
			}

			if (!context.Users.Any(u => u.Email == "DemoDev3Email@mailinator.com"))
			{
				userManager.Create(new ApplicationUser()
				{
					Email = "DemoDev3Email@mailinator.com",
					UserName = "DemoDev3Email@mailinator.com",
					FirstName = "Tiegan",
					LastName = "Rivera",
					DisplayName = "Tiegan",
					AvatarPath = "/Avatars/default_avatar.png"

				}, demoPassword);
			}

			if (!context.Users.Any(u => u.Email == "DemoSub2Email@mailinator.com"))
			{
				userManager.Create(new ApplicationUser()
				{
					Email = "DemoSub2Email@mailinator.com",
					UserName = "DemoSub2Email@mailinator.com",
					FirstName = "Gracie",
					LastName = "Velazquez",
					DisplayName = "Gracie",
					AvatarPath = "/Avatars/default_avatar.png"

				}, demoPassword);
			}

			if (!context.Users.Any(u => u.Email == "DemoSub3Email@mailinator.com"))
			{
				userManager.Create(new ApplicationUser()
				{
					Email = "DemoSub3Email@mailinator.com",
					UserName = "DemoSub3Email@mailinator.com",
					FirstName = "Tyreese",
					LastName = "Adams",
					DisplayName = "Tyreese",
					AvatarPath = "/Avatars/default_avatar.png"

				}, demoPassword);
			}
			#endregion
			context.SaveChanges();

			#region Role Assignment
			var admin2Id = userManager.FindByEmail("DemoAdminEmail@mailinator.com").Id;
			userManager.AddToRole(admin2Id, "Admin");
			var pmId = userManager.FindByEmail("DemoPMEmail@mailinator.com").Id;
			userManager.AddToRole(pmId, "ProjectManager");
			var devId = userManager.FindByEmail("DemoDevEmail@mailinator.com").Id;
			userManager.AddToRole(devId, "Developer");
			var subId = userManager.FindByEmail("DemoSubEmail@mailinator.com").Id;
			userManager.AddToRole(subId, "Submitter");
			var pm2Id = userManager.FindByEmail("DemoPM2Email@mailinator.com").Id;
			userManager.AddToRole(pm2Id, "ProjectManager");
			var dev2Id = userManager.FindByEmail("DemoDev2Email@mailinator.com").Id;
			userManager.AddToRole(dev2Id, "Developer");
			var sub2Id = userManager.FindByEmail("DemoSub2Email@mailinator.com").Id;
			userManager.AddToRole(sub2Id, "Submitter");
			var pm3Id = userManager.FindByEmail("DemoPM3Email@mailinator.com").Id;
			userManager.AddToRole(pm3Id, "ProjectManager");
			var dev3Id = userManager.FindByEmail("DemoDev3Email@mailinator.com").Id;
			userManager.AddToRole(dev3Id, "Developer");
			var sub3Id = userManager.FindByEmail("DemoSub3Email@mailinator.com").Id;
			userManager.AddToRole(sub3Id, "Submitter");
			#endregion
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
				new Project() { Name = "Seed 1", Description = "This is Seed 1 Project", Created = DateTime.Now.AddDays(-60), IsArchived = true },
				new Project() { Name = "Seed 2", Description = "This is Seed 2 Project", Created = DateTime.Now.AddDays(-45) },
				new Project() { Name = "Seed 3", Description = "This is Seed 3 Project", Created = DateTime.Now.AddDays(-30) },
				new Project() { Name = "Seed 4", Description = "This is Seed 4 Project", Created = DateTime.Now.AddDays(-15) },
				new Project() { Name = "Seed 5", Description = "This is Seed 5 Project", Created = DateTime.Now.AddDays(-7) }
			);
			#endregion

			context.SaveChanges();

			#region Ticket Seed
			List<Ticket> ticketList = new List<Ticket>();
			List<ApplicationUser> projectManagers = new List<ApplicationUser>();
			List<ApplicationUser> developers = new List<ApplicationUser>();
			List<ApplicationUser> submitters = new List<ApplicationUser>();
			projectManagers.AddRange(roleHelper.UsersInRole("ProjectManager"));
			developers.AddRange(roleHelper.UsersInRole("Developer"));
			submitters.AddRange(roleHelper.UsersInRole("Submitter"));

			#region Assigning Users to Projects by Role
			foreach (var project in context.Projects)
			{
				foreach (var user in roleHelper.UsersInRole("Admin"))
				{
					projectHelper.AddUserToProject(user.Id, project.Id);
				}
				projectHelper.AddUserToProject(projectManagers[random.Next(projectManagers.Count)].Id, project.Id);
				//Developer assignment 
				var firstDev = developers[random.Next(developers.Count)].Id;
				var secondDev = developers[random.Next(developers.Count)].Id;
				while (firstDev == secondDev)
				{
					secondDev = developers[random.Next(developers.Count)].Id;
				}
				projectHelper.AddUserToProject(firstDev, project.Id);
				projectHelper.AddUserToProject(secondDev, project.Id);
				//Submitter assignment
				var firstSub = submitters[random.Next(submitters.Count)].Id;
				var secondSub = submitters[random.Next(submitters.Count)].Id;
				while (firstSub == secondSub)
				{
					secondSub = submitters[random.Next(submitters.Count)].Id;
				}
				projectHelper.AddUserToProject(firstSub, project.Id);
				projectHelper.AddUserToProject(secondSub, project.Id);
			}
			#endregion


			foreach (var project in context.Projects.ToList())
			{
				projectManagers = projectHelper.ListUsersonProjectInRole(project.Id, "ProjectManager");
				developers = projectHelper.ListUsersonProjectInRole(project.Id, "Developer");
				submitters = projectHelper.ListUsersonProjectInRole(project.Id, "Submitter");
				foreach (var type in context.TicketTypes.ToList())
				{
					foreach (var status in context.TicketStatuses.ToList())
					{
						foreach (var priority in context.TicketPriorities.ToList())
						{
							var developerId = developers[random.Next(developers.Count)].Id;
							if (status.Name == "Open")
							{
								developerId = null;
							}
							var resolved = false;
							var archived = false;
							if (status.Name == "Resolved")
							{
								resolved = true;
							}
							if (status.Name == "Archived" || project.IsArchived)
							{
								archived = true;
							}
							var newTicket = new Ticket()
							{
								ProjectId = project.Id,
								TicketPriorityId = priority.Id,
								TicketTypeId = type.Id,
								TicketStatusId = status.Id,
								SubmitterId = submitters[random.Next(submitters.Count)].Id,
								DeveloperId = developers[random.Next(developers.Count)].Id,
								Created = DateTime.Now,
								Issue = $"This is a seeded ticket of type {type.Name} on {project.Name}",
								IssueDescription = $"This is a description for ticket of type {type.Name} with a status of {status.Name} and priority {priority.Name}",
								IsResolved = resolved,
								IsArchived = archived
							};
							ticketList.Add(newTicket);
						}
					}
				}
			}


		context.Tickets.AddRange(ticketList);
		context.SaveChanges();
		#endregion
		}

}

}