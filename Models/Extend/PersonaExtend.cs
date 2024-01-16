using TESTMVCCORE.Models.DB;

namespace TESTMVCCORE.Models.Extend
{
	public class PersonaExtend
	{
		public Persona Persona { get; set; } = null!;
		public List<PersonaDetail>? PersonaDetailList { get; set; }
	}
}
