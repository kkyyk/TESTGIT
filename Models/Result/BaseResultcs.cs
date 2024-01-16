namespace TESTMVCCORE.Models.Result
{
	public class BaseResult
	{
		public string? Message { get; set; }
		public bool IsSuccess { get; set; }

		public BaseResult()
		{
			Message = "";
			IsSuccess = false;
		}

		public BaseResult(string message, bool isSuccess)
		{
			Message = message;
			IsSuccess = isSuccess;
		}

		public BaseResult(bool isSuccess)
		{
			Message = "";
			IsSuccess = isSuccess;
		}

		public BaseResult(string message)
		{
			Message = message;
			IsSuccess = string.IsNullOrEmpty(message);
		}
	}
}
