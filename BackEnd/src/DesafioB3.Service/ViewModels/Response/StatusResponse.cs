namespace DesafioB3.Service.ViewModels.Response
{
	public class StatusResponse
	{
		public int StatusId { get; set; }
		public string Name { get; set; }

		public StatusResponse(int statusId, string name)
		{
			StatusId = statusId;
			Name = name;
		}
	}
}
