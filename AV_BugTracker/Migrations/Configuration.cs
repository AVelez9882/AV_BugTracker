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

		}

	}

}
