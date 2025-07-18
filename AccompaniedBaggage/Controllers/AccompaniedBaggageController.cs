using AccompaniedBaggage.Model.Request;
using Microsoft.AspNetCore.Mvc;
using SezApi.Model.Request;
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
        public async Task<IActionResult> GetMstSac(int? sacId, int? page, int? size)
        {

            var response = await _services.GetMstSac(sacId, page, size);

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
    }
}
