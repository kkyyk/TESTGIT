namespace TESTMVCCORE.Models.QueryParams
{
	public class Model_List_QueryParams
	{
		public int? Age { get; set; } //年齡

		public string? NumType { get; set; } //查詢方式(0:小於 1:大於)
		public string? Type { get; set; } //種類

		public string? NameKeyword { get; set; } //名字關鍵字
	}
}
