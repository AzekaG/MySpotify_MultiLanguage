using Microsoft.AspNetCore.Mvc.Filters;
using System.Globalization;

namespace MySpotify.Filters
{
	public class CultureAttribute : Attribute, IActionFilter
	{
		public void OnActionExecuted(ActionExecutedContext filterContext) { }
	
		public void OnActionExecuting(ActionExecutingContext filterContext)
		{
			string? cultureName = null;
			var cultureCookie = filterContext.HttpContext.Request.Cookies["lang"];
			if (cultureCookie != null)
			{
				cultureName = cultureCookie;
			}
			else cultureName = "uk";
		

			List<string> cultures = new List<string>() {"ru", "en", "uk"};
		
		
			if(!cultures.Contains(cultureName)) 
			{
				cultureName = "uk";
			}


			Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(cultureName);

			Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(cultureName);
		}
	
	}
}
