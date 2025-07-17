using AccompaniedBaggage.Model.Request;
using AccompaniedBaggage.Model.Response;
using SezApi.Model.Request;
using SezApi.Model.Response;

namespace SezApi.Services
{
    public interface IServices
    {
        Task<Response<List<ResponseMstEximTraderMaster>>> GetMstParty(int? page, int? size, string? partyType);
        Task<Response<List<ResponseMstSac>>> GetMstSac(int? sacId, int? page, int? size);
        Task<AddEditResponse> AddEditMstSac(RequestMstSac request);
        Task<AddEditResponse> AddEditmstParty(RequestMstEximTraderMaster request);
        Task<Response<List<ResponseMstStorageCharge>>> GetMstStorageCharge(int? chargeId, int? page, int? size);
        Task<AddEditResponse> AddEditMstStorageCharge(RequestMstStorageCharge request);
        Task<Response<List<ResponseMstHandlingCharge>>> GetMstHandlingCharge(int? handlingChargeId, int? page, int? size);
        Task<AddEditResponse> AddEditMstHandlingCharge(RequestMstHandlingCharge request);
        Task<Response<List<ResponseMstGodown>>> GetMstGodown(int? godownId, int? page, int? size);
        Task<AddEditResponse> AddEditGodownAsync(RequestMstGodown request);
    }
}
