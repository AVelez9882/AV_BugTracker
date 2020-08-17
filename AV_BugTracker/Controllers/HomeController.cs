using AV_BugTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace AV_BugTracker.Controllers
{
	public class HomeController : Controller
	{
		private ApplicationDbContext db = new ApplicationDbContext();

		[Authorize]
		public ActionResult Index()
		{
			return View(db.Users.Find(User.Identity.GetUserId()));
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}