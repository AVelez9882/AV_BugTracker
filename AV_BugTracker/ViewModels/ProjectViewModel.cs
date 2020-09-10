using AV_BugTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AV_BugTracker.ViewModels
{
	public class ProjectViewModel
	{
		public List<Project> AllProjects { get; set; }
		public List<Project> MyProjects { get; set; }
	}
}