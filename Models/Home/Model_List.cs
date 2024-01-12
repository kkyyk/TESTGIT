using TESTMVCCORE.Models.DB;

namespace TESTMVCCORE.Models.Home
{
	public class Model_List
	{
		public Model_List()
		{
			PersonaList = new List<Persona>();
		}
		public List<Persona>? PersonaList { get; set; }
	}
}
