using Microsoft.AspNetCore.Mvc.Rendering;
using TESTMVCCORE.Models.DB;
using TESTMVCCORE.Models.Extend;
using TESTMVCCORE.Models.QueryParams;

namespace TESTMVCCORE.Models.Home
{
	public class Model_List
	{

		public Model_List()
		{
			PersonaList = new List<PersonaExtend>();



			ddl_SearchNumberType = new List<SelectListItem>();
			ddl_SearchType = new List<SelectListItem>();
		}

		public List<PersonaExtend>? PersonaList { get; set; }

		#region 搜尋
		//public int? Age { get; set; } //年齡

		//public string? SearchType { get; set; } //查詢方式(0:小於 1:大於)

		//public string? NameKeyword { get; set; } //名字關鍵字
		public Model_List_QueryParams? QueryParams { get; set; }
		#endregion
		#region 下拉選單
		public List<SelectListItem> ddl_SearchNumberType { get; set; } //數字類型

		public List<SelectListItem> ddl_SearchType { get; set; } //種類

		#endregion

	}
}
