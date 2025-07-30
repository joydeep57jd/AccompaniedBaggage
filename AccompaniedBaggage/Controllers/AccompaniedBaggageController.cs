using AccompaniedBaggage.Model.Request;
using AccompaniedBaggage.Model.Response;
using Microsoft.AspNetCore.Mvc;
using SezApi.Model.Request;
using SezApi.Model.Response;
using SezApi.Services;
namespace SezApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccompaniedBaggageController : Controller
    {
        private readonly IServices _services;

        public AccompaniedBaggageController(IServices services)
        {
            _services = services;
        }

        [HttpGet("GetmstParty")]
        public async Task<IActionResult> GetmstParty(int? page, int? size, string? partyType)
        {

            var response = await _services.GetMstParty(page, size, partyType);

            if (response.Data == null || !response.Data.Any())
            {
                return NotFound(new { message = "No entries found." });
            }

            return Ok(response);
        }

        [HttpPost("AddEditmstParty")]
        public async Task<IActionResult> AddEditmstParty(RequestMstEximTraderMaster request)
        {
            if (request == null)
            {
                return BadRequest("Request data is required.");
            }
            try
            {
                var result = await _services.AddEditmstParty(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetMstSac")]
        public async Task<IActionResult> GetMstSac(int? sacId, int? page, int? size, bool? ForStoragePage, bool? ForHandlingPage)
        {

            var response = await _services.GetMstSac(sacId, page, size, ForStoragePage, ForHandlingPage);

            if (response.Data == null || !response.Data.Any())
            {
                return NotFound(new { message = "No entries found." });
            }

            return Ok(response);
        }

        [HttpPost("AddEditMstSac")]
        public async Task<IActionResult> AddEditMstSac(RequestMstSac request)
        {
            if (request == null)
            {
                return BadRequest("Request data is required.");
            }
            try
            {
                var result = await _services.AddEditMstSac(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetMstStorageCharge")]
        public async Task<IActionResult> GetMstStorageCharge(int? chargeId, int? page, int? size)
        {

            var response = await _services.GetMstStorageCharge(chargeId, page, size);

            if (response.Data == null || !response.Data.Any())
            {
                return NotFound(new { message = "No entries found." });
            }

            return Ok(response);
        }

        [HttpPost("AddEditMstStorageCharge")]
        public async Task<IActionResult> AddEditMstStorageCharge(RequestMstStorageCharge request)
        {
            if (request == null)
            {
                return BadRequest("Request data is required.");
            }
            try
            {
                var result = await _services.AddEditMstStorageCharge(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetMstHandlingCharge")]
        public async Task<IActionResult> GetMstHandlingCharge(int? handlingChargeId, int? page, int? size)
        {

            var response = await _services.GetMstHandlingCharge(handlingChargeId, page, size);

            if (response.Data == null || !response.Data.Any())
            {
                return NotFound(new { message = "No entries found." });
            }

            return Ok(response);
        }

        [HttpPost("AddEditMstHandlingCharge")]
        public async Task<IActionResult> AddEditMstHandlingCharge(RequestMstHandlingCharge request)
        {
            if (request == null)
            {
                return BadRequest("Request data is required.");
            }
            try
            {
                var result = await _services.AddEditMstHandlingCharge(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetMstGodown")]
        public async Task<IActionResult> GetMstGodown(int? godownId, int? page, int? size)
        {

            var response = await _services.GetMstGodown(godownId, page, size);

            if (response.Data == null || !response.Data.Any())
            {
                return NotFound(new { message = "No entries found." });
            }

            return Ok(response);
        }

        [HttpPost("AddEditGodownAsync")]
        public async Task<IActionResult> AddEditGodownAsync(RequestMstGodown request)
        {
            if (request == null)
            {
                return BadRequest("Request data is required.");
            }
            try
            {
                var result = await _services.AddEditGodownAsync(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpGet("GetExaminationAsync")]
        public async Task<IActionResult> GetExaminationAsync(int? examinationId, int? page, int? size)
        {

            var response = await _services.GetExaminationAsync(examinationId, page, size);

            if (response.Data == null || !response.Data.Any())
            {
                return NotFound(new { message = "No entries found." });
            }

            return Ok(response);
        }

        [HttpPost("AddEditExaminationAsync")]
        public async Task<IActionResult> AddEditExaminationAsync(RequestExamination request)
        {
            if (request == null)
            {
                return BadRequest("Request data is required.");
            }
            try
            {
                var result = await _services.AddEditExaminationAsync(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetReceiptDetails")]
        public async Task<IActionResult> GetReceiptDetails(int? ReceiptId, int? page, int? size,bool? forExamStor,DateTime? FromreceiptDate,DateTime? ToreceiptDate)
        {

            var response = await _services.GetReceiptDetails(ReceiptId, page, size, forExamStor, FromreceiptDate, ToreceiptDate);

            if (response.Data == null || !response.Data.Any())
            {
                return NotFound(new { message = "No entries found." });
            }

            return Ok(response);
        }


        [HttpPost("AddEditReceiptDetailsAsync")]
        public async Task<IActionResult> AddEditReceiptDetailsAsync(RequestReceiptDetails request)
        {
            if (request == null)
            {
                return BadRequest("Request data is required.");
            }
            try
            {
                var result = await _services.AddEditReceiptDetailsAsync(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetBaggageClaimAsync")]
        public async Task<IActionResult> GetBaggageClaimAsync(int? claimId, int? page, int? size, int? Party_id, bool? ForPaymentReceipt)
        {

            var response = await _services.GetBaggageClaimAsync(claimId, page, size, Party_id, ForPaymentReceipt);

            if (response.Data == null || !response.Data.Any())
            {
                return NotFound(new { message = "No entries found." });
            }

            return Ok(response);
        }


        [HttpPost("AddEditBaggageClaimAsync")]
        public async Task<IActionResult> AddEditBaggageClaimAsync(RequestBaggageClaim request)
        {
            if (request == null)
            {
                return BadRequest("Request data is required.");
            }
            try
            {
                var result = await _services.AddEditBaggageClaimAsync(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("AddEditPaymentReceiptAsync")]
        public async Task<IActionResult> AddEditPaymentReceiptAsync(PaymentReceipt request)
        {
            if (request == null)
            {
                return BadRequest("Request data is required.");
            }
            try
            {
                var result = await _services.AddEditPaymentReceiptAsync(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetPaymentReceiptAsync")]
        public async Task<IActionResult> GetPaymentReceiptAsync(int? receiptId, int? page, int? size,bool? ForDelhivery)
        {

            var response = await _services.GetPaymentReceiptAsync(receiptId, page, size, ForDelhivery);

            if (response.Data == null || !response.Data.Any())
            {
                return NotFound(new { message = "No entries found." });
            }

            return Ok(response);
        }

        [HttpGet("UnclaimedReceiptDto")]
        public async Task<IActionResult> UnclaimedReceiptDto()
        {
            var response = await _services.UnclaimedReceiptDto();
            if (response.Data == null || !response.Data.Any())
            {
                return NotFound(new { message = "No entries found." });
            }
            return Ok(response);
        }

        [HttpGet("GetDeliveryAsync")]
        public async Task<IActionResult> GetDeliveryAsync(int? deliveryId, int? receiptId, int? page, int? size)
        {
            var response = await _services.GetDeliveryAsync(deliveryId, receiptId, page, size);

            if (response.Data == null || !response.Data.Any())
            {
                return NotFound(new { message = "No entries found." });
            }

            return Ok(response);
        }

        [HttpPost("AddEditDelivery")]
        public async Task<IActionResult> AddEditDelivery(RequestDelivery request)
        {
            if (request == null)
            {
                return BadRequest("Request data is required.");
            }
            try
            {
                var result = await _services.AddEditDeliveryAsync(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetHandlingChargesCalc")]
        public async Task<IActionResult> GetHandlingChargesCalc(string customType, string receiptNo, int partyId)
        {
            try
            {
                var result = await _services.GetHandlingChargesCalcAsync(customType, receiptNo, partyId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetInvoiceChargesAsync")]
        public async Task<IActionResult> GetInvoiceChargesAsync(int? id, int? inoviceId, int? page, int? size)
        {
            try
            {
                var result = await _services.GetInvoiceChargesAsync(id, inoviceId, page, size);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetBaggageClaimReportAsync")]
        public async Task<IActionResult> GetBaggageClaimReportAsync(DateTime? fromDate, DateTime? toDate)
        {
            try
            {
                var result = await _services.GetBaggageClaimReportAsync(fromDate, toDate);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetMstCompanyAsync")]
        public async Task<IActionResult> GetMstCompanyAsync(int? companyId)
        {
            try
            {
                var result = await _services.GetMstCompanyAsync(companyId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(200, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetStorageChargesCalcAsync")]
        public async Task<IActionResult> GetStorageChargesCalcAsync(string receiptNo, int partyId, DateTime claimDate)
        {   try
            {
                var result = await _services.GetStorageChargesCalcAsync(receiptNo, partyId, claimDate);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(200, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetDeliveryReportAsync")]
        public async Task<IActionResult> GetDeliveryReportAsync(DateTime? fromDate, DateTime? toDate)
        {
            try
            {
                var result = await _services.GetBaggageDeliveryReportAsync(fromDate, toDate);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetStockRegisterReportAsync")]
        public async Task<IActionResult> GetStockRegisterReportAsync(
    DateTime? fromReceiptDate,
    DateTime? toReceiptDate,
    DateTime? fromExaminationDate,
    DateTime? toExaminationDate)
        {
            try
            {
                var result = await _services.GetStockRegisterReportAsync(fromReceiptDate, toReceiptDate, fromExaminationDate, toExaminationDate);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}
