using TESTMVCCORE.Models.DB;
using TESTMVCCORE.Service.Base;

namespace TESTMVCCORE.Service
{
	public class AllCommonService : ServiceBase
	{
		private readonly DBContext _dbContext;
		private readonly IConfiguration _conf;
		public AllCommonService(DBContext context,
			IConfiguration configuration
			) : base(context)
		{
			_dbContext = context;
			_conf = configuration;
		}

		//回傳該Table最大編號+1
		public int GetNewId<T>() where T : class
		{
			int result = 1;
			var list = Lookup<T>().ToList();
			if (list.Count > 0)
			{
				result = list.Count() + 1;
			}
			return result;

		}
	}
}
