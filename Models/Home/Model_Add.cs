using Microsoft.AspNetCore.Mvc.Rendering;
using TESTMVCCORE.Models.DB;

namespace TESTMVCCORE.Models.Home
{
	public class Model_Add
	{
        public Model_Add()
        {
            ddl_Type = new List<SelectListItem>();
        }

        public Persona Persona { get; set; } = null!;

        public List<PersonaDetail>? PersonaDetailList { get; set; }

        #region 下拉選單
        public List<SelectListItem> ddl_Type { get; set; }
        #endregion
    }
}
