﻿using System.Web;
using System.Web.Mvc;

namespace AV_BugTracker
{
	public class FilterConfig
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
		}
	}
}
