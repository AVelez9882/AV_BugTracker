using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AV_BugTracker.Models;

namespace AV_BugTracker.Helpers
{
	public class UserNameHelper
	{
		private ApplicationDbContext db = new ApplicationDbContext();

		public string GetFullName(string userId)
		{
			var user = db.Users.Find(userId);
			var firstName = user.FirstName;
			var lastName = user.LastName;
			return firstName + "" + lastName;
		}

		public string GetFirstName(string userId)
		{
			var user = db.Users.Find(userId);
			var firstName = user.FirstName;
			return firstName;
		}

		public string LastNameFirst(string userId)
		{
			var user = db.Users.Find(userId);
			return user.FullName;
		}
	}

}