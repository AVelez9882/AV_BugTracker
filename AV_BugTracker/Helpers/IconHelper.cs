using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace AV_BugTracker.Helpers
{
	public class IconHelper
	{
		public string GetIcon(string fileExtension)
		{
			var fileExtensions = WebConfigurationManager.AppSettings["AllowableExtensions"].Split(',');
			var imageExtenstions = WebConfigurationManager.AppSettings["AllowableImageExtensions"].Split(',');
			foreach (var extension in fileExtensions.Concat(imageExtenstions))
			{
				if (extension == fileExtension)
				{
					return $"/Images/{extension.TrimStart(',')}.png";
				}
			}
			return "/Images/default.png";
		}
	}
}