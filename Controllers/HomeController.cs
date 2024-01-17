using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Diagnostics;
using TESTMVCCORE.Models;
using TESTMVCCORE.Models.DB;
using TESTMVCCORE.Models.Home;
using TESTMVCCORE.Models.Result;
using TESTMVCCORE.Service;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace TESTMVCCORE.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly PersonaService _personaService;

		public HomeController(PersonaService personaService,
			ILogger<HomeController> logger)
		{
			_personaService = personaService;
			_logger = logger;
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult List()
		{
			Model_List model = new Model_List();
			model.PersonaList = _personaService.Query().ToList();
			_personaService.GetListDDL(ref model);
			return View(model);
		}

		[HttpPost]
		public IActionResult List(Model_List model)
		{
            model.PersonaList = _personaService.Query(model.QueryParams).ToList();
			_personaService.GetListDDL(ref model);
            return View(model);
		}


		#region 新增
		public IActionResult Add()
		{
			Model_Add model = new Model_Add();
			_personaService.GetAddDDL(ref model);
			return View(model);
		}

		//[HttpPost]
		//public IActionResult Add(Model_Add model)
		//{
		//	//string result = _personaService.Insert(model.Persona);
		//	string result = _personaService.AddPersona(model);
		//	return RedirectToAction("List");
		//}

		[HttpPost]
		public IActionResult Add(Model_Add model)
		{
			string result = _personaService.AddPersona(model);
			return Json(new BaseResult(result));
		}
		#endregion

		#region 編輯
		public IActionResult Edit(int Id)
		{
			Model_Add model = new Model_Add();
			model = _personaService.Detail(Id);
			//model.Persona = _personaService.Find<Persona>(Id);
			if (model.Persona == null)
			{
				return RedirectToAction("List");
			}
			_personaService.GetAddDDL(ref model);
			return View(model);
		}

		//[HttpPost]
		//public IActionResult Edit(Model_Add model)
		//{
		//	//string result = _personaService.Update(model.Persona);
		//	string result = _personaService.EditPersona(model);
		//	return RedirectToAction("List");
		//}

		[HttpPost]
		public IActionResult Edit(Model_Add model)
		{
			string result = _personaService.EditPersona(model);
			return Json(new BaseResult(result));
		}

		#endregion
		#region 刪除
		//public IActionResult Delete(int Id)
		//{
		//	string result = "";
		//	result = _personaService.DeletePersona(Id);
		//	return RedirectToAction("List");
		//}
		[HttpPost]
		public IActionResult Delete(int Id)
		{
			string result = "";
			result = _personaService.DeletePersona(Id);
			return Json(new BaseResult(result));
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
