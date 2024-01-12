using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Diagnostics;
using TESTMVCCORE.Models;
using TESTMVCCORE.Models.DB;
using TESTMVCCORE.Models.Home;
using TESTMVCCORE.Service;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace TESTMVCCORE.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly AllCommonService _allCommonService;

		public HomeController(AllCommonService allCommonService,
			ILogger<HomeController> logger)
		{
			_allCommonService = allCommonService;
			_logger = logger;
		}

		public IActionResult Index()
		{
			var list = _allCommonService.Lookup<Persona>().ToList();
			return View();
		}

		public IActionResult List()
		{
			Model_List model = new Model_List();
			model.PersonaList = _allCommonService.Lookup<Persona>().ToList();
			return View(model);
		}

		#region 新增
		public IActionResult Add()
		{
			Model_Add model = new Model_Add();
			return View(model);
		}

		[HttpPost]
		public IActionResult Add(Model_Add model)
		{
			string result = _allCommonService.Insert(model.Persona);
			return RedirectToAction("List");
		}
		#endregion

		#region 編輯
		public IActionResult Edit(int Id)
		{
			Model_Add model = new Model_Add();
			model.Persona = _allCommonService.Find<Persona>(Id);
			if(model.Persona == null)
			{
				return RedirectToAction("List");
			}
			return View(model);
		}

		[HttpPost]
		public IActionResult Edit(Model_Add model)
		{
			string result = _allCommonService.Update(model.Persona);
			return RedirectToAction("List");
		}

		#endregion
		#region 刪除
		public IActionResult Delete(int Id)
		{
			string result = "";
			//要刪除的Persona
			var Persona = _allCommonService.Find<Persona>(Id);
			if (Persona != null)
			{
				result = _allCommonService.Delete<Persona>(Persona);
			}
			return RedirectToAction("List");
		}
		#endregion

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
