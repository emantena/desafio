using DesafioB3.Service.ViewModels.Response;

namespace DesafioB3.Service.Interfaces;

public interface IStatusService
{
	Task<BaseResponse> GetStatusAsync();
}
