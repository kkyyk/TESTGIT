using Microsoft.AspNetCore.Mvc.Rendering;
using TESTMVCCORE.Models.DB;
using TESTMVCCORE.Models.Home;
using TESTMVCCORE.Service.Base;

namespace TESTMVCCORE.Service
{
	public class PersonaService : ServiceBase
	{
		private readonly DBContext _dbContext;
		private readonly IConfiguration _conf;
		public PersonaService(DBContext context,
			IConfiguration configuration
			) : base(context)
		{
			_dbContext = context;
			_conf = configuration;
		}

		/// <summary>
		/// 取下拉選單
		/// </summary>
		/// <param name="model"></param>
		public void GetDDL(ref Model_Add model)
		{
			model.ddl_Type.Add(new SelectListItem
			{
				Text = "請選擇",
				Value = ""
			});
			model.ddl_Type.AddRange(_dbContext.Type.Select(x => new SelectListItem
			{
				Text = x.TypeName,
				Value = x.Type1
			}).ToList());

        }

	}
}
