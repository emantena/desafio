using DesafioB3.Repository.Interfaces;
using DesafioB3.Service.Interfaces;
using DesafioB3.Service.ViewModels.Response;

namespace DesafioB3.Service;

public class StatusService : IStatusService
{
	private readonly IStatusRepository _statusRepository;

	public StatusService(IStatusRepository statusRepository)
	{
		_statusRepository = statusRepository;
	}

	public async Task<BaseResponse> GetStatusAsync()
	{
		var response = new BaseResponse();

		response.AddValue(await _statusRepository.GetAllStatusAsync());

		return response;
	}
}