using AccompaniedBaggage.Model.Request;
using AccompaniedBaggage.Model.Response;
using SezApi.Model.Request;
using SezApi.Model.Response;

namespace SezApi.Services
{
    public interface IServices
    {
        Task<Response<List<ResponseMstEximTraderMaster>>> GetMstParty(int? page, int? size, string? partyType);
        Task<Response<List<ResponseMstSac>>> GetMstSac(int? sacId, int? page, int? size,bool? ForStoragePage,bool? ForHandlingPage);
        Task<AddEditResponse> AddEditMstSac(RequestMstSac request);
        Task<AddEditResponse> AddEditmstParty(RequestMstEximTraderMaster request);
        Task<Response<List<ResponseMstStorageCharge>>> GetMstStorageCharge(int? chargeId, int? page, int? size);
        Task<AddEditResponse> AddEditMstStorageCharge(RequestMstStorageCharge request);
        Task<Response<List<ResponseMstHandlingCharge>>> GetMstHandlingCharge(int? handlingChargeId, int? page, int? size);
        Task<AddEditResponse> AddEditMstHandlingCharge(RequestMstHandlingCharge request);
        Task<Response<List<ResponseMstGodown>>> GetMstGodown(int? godownId, int? page, int? size);
        Task<AddEditResponse> AddEditGodownAsync(RequestMstGodown request);
        Task<Response<List<ResponseExamination>>> GetExaminationAsync(int? examinationId, int? page, int? size);
        Task<Response<AddEditResponse>> AddEditExaminationAsync(RequestExamination request);

        Task<Response<List<ResponseReceiptDetails>>> GetReceiptDetails(int? ReceiptId, int? page, int? size, bool? forExamStor, DateTime? FromreceiptDate, DateTime? ToreceiptDate);
        Task<Response<AddEditResponse>> AddEditReceiptDetailsAsync(RequestReceiptDetails request);

        Task<Response<List<ResponseBaggageClaim>>> GetBaggageClaimAsync(int? claimId, int? page, int? size, int? Party_id, bool? ForPaymentReceipt);
        Task<Response<AddEditResponse>> AddEditBaggageClaimAsync(RequestBaggageClaim request);
        Task<Response<AddEditResponse>> AddEditPaymentReceiptAsync(PaymentReceipt request);
        Task<Response<List<PaymentReceipt>>> GetPaymentReceiptAsync(int? receiptId, int? page, int? size, bool? ForDelhivery);

        Task<Response<List<UnclaimedReceiptDto>>> UnclaimedReceiptDto();
        Task<Response<AddEditResponse>> AddEditDeliveryAsync(RequestDelivery request);
        Task<Response<List<ResponseDelivery>>> GetDeliveryAsync(int? deliveryId, int? receiptId, int? page, int? size);

        Task<ResponseHandlingCharge?> GetHandlingChargesCalcAsync(string customType, string receiptNo, int partyId,string StorageType);

        Task<InvoiceChargeListResponse> GetInvoiceChargesAsync(int? id, int? inoviceId, int? page, int? size);
        Task<List<BaggageClaimReportResponse>> GetBaggageClaimReportAsync(DateTime? fromDate, DateTime? toDate);
        Task<List<ResponseMstCompany>> GetMstCompanyAsync(int? companyId);

        Task<ResponseStorageChargesCalc?> GetStorageChargesCalcAsync(string receiptNo, int partyId, DateTime claimDate);

        Task<Response<List<ResponseDeliveryReport>>> GetBaggageDeliveryReportAsync(DateTime? fromDate, DateTime? toDate);

        Task<Response<List<StockRegisterReportRow>>> GetStockRegisterReportAsync(
    DateTime? fromReceiptDate,
    DateTime? toReceiptDate,
    DateTime? fromExaminationDate,
    DateTime? toExaminationDate);
    }
}
