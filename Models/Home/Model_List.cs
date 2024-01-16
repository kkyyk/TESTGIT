using TESTMVCCORE.Models.DB;
using TESTMVCCORE.Models.Extend;

namespace TESTMVCCORE.Models.Home
{
	public class Model_List
	{
		public Model_List()
		{
			PersonaList = new List<PersonaExtend>();
		}

		public List<PersonaExtend>? PersonaList { get; set; }

	}
}
