using System;
using Microsoft.AspNetCore.Mvc;

namespace CourseApp.Controllers
{
	[Route("api/[controller]/[action]")]
	public class BaseController : Controller
	{
		public BaseController()
		{
		}
	}
}

