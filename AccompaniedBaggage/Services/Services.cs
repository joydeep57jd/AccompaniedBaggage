using AccompaniedBaggage.Model.Request;
using AccompaniedBaggage.Model.Response;
using Azure.Core;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SezApi.Controllers;
using SezApi.Data;
using SezApi.Model.Request;
using SezApi.Model.Response;
using System.Data;
namespace SezApi.Services
{
    public class Services : IServices
    {
        private readonly AccompaniedBaggageDbContext _db;
        private readonly ILogger<Services> _logger;
        public Services(AccompaniedBaggageDbContext db, ILogger<Services> logger)
        {
            _db = db;
            _logger = logger;
        }

        public async Task<Response<List<ResponseMstEximTraderMaster>>> GetMstParty(int? page, int? size, string? partyType)
        {
            var response = new Response<List<ResponseMstEximTraderMaster>>();

            try
            {

                var conn = _db.Database.GetDbConnection();
                await conn.OpenAsync();

                using var command = conn.CreateCommand();
                command.CommandText = "GetMstEximTraderMasterPaged";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@partyType", partyType ?? (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("@page", page));
                command.Parameters.Add(new SqlParameter("@size", size));

                using var reader = await command.ExecuteReaderAsync();

                int totalCount = 0;
                if (await reader.ReadAsync())
                {
                    totalCount = reader.GetInt32(0);
                }

                await reader.NextResultAsync();

                var data = new List<ResponseMstEximTraderMaster>();
                while (await reader.ReadAsync())
                {
                    data.Add(new ResponseMstEximTraderMaster
                    {
                        TraderId = reader.GetInt32(reader.GetOrdinal("TraderId")),
                        OperationType = reader["OperationType"] as string,
                        EximTraderName = reader["EximTraderName"] as string,
                        EximTraderAlias = reader["EximTraderAlias"] as string,
                        Address = reader["Address"] as string,
                        CountryName = reader["CountryName"] as string,
                        StateName = reader["StateName"] as string,
                        CityName = reader["CityName"] as string,
                        Pincode = reader["Pincode"] as string,
                        PhoneNo = reader["PhoneNo"] as string,
                        FaxNo = reader["FaxNo"] as string,
                        ContactPerson = reader["ContactPerson"] as string,
                        EmailId = reader["EmailId"] as string,
                        MobileNo = reader["MobileNo"] as string,
                        PAN = reader["PAN"] as string,
                        AadhaarNo = reader["AadhaarNo"] as string,
                        GSTNo = reader["GSTNo"] as string,
                        TAN = reader["TAN"] as string,
                        SapCustomerNo = reader["SapCustomerNo"] as string,
                        PartyCode = reader["PartyCode"] as string,

                        CountryId = reader["CountryId"] as int?,
                        StateId = reader["StateId"] as int?,
                        StateCode = reader["StateCode"] as string,

                        isImporter = reader["isImporter"] as bool?,
                        isExporter = reader["isExporter"] as bool?,
                        isShipline = reader["isShipline"] as bool?,
                        isCHA = reader["isCHA"] as bool?,
                        IsForWarder = reader["IsForWarder"] as bool?,
                        isRent = reader["isRent"] as bool?,
                        isBidder = reader["isBidder"] as bool?
                    });

                }

                response.Data = data;
                response.Status = true;
                response.TotalCount = totalCount;
            }
            catch (Exception ex)
            {
                _logger.LogError("StackTrace: {StackTrace}", ex.StackTrace);
                response.Data = new List<ResponseMstEximTraderMaster>();
                response.Status = false;
                response.Message = $"Error: {ex.Message}";
            }

            return response;
        }

        public async Task<Response<List<ResponseMstSac>>> GetMstSac(int? sacId, int? page, int? size)
        {
            var response = new Response<List<ResponseMstSac>>();

            try
            {
                var conn = _db.Database.GetDbConnection();
                await conn.OpenAsync();

                using var command = conn.CreateCommand();
                command.CommandText = "GetMstSac";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@SacId", sacId ?? (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("@page", page ?? (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("@size", size ?? (object)DBNull.Value));

                using var reader = await command.ExecuteReaderAsync();

                int totalCount = 0;
                if (await reader.ReadAsync())
                {
                    totalCount = reader.GetInt32(0);
                }

                await reader.NextResultAsync();

                var data = new List<ResponseMstSac>();
                while (await reader.ReadAsync())
                {
                    data.Add(new ResponseMstSac
                    {
                        SacId = reader.GetInt32(reader.GetOrdinal("SacId")),
                        BranchId = reader["BranchId"] as int?,
                        SacCode = reader["SacCode"] as string,
                        Description = reader["Description"] as string,
                        Gst = reader["Gst"] as decimal?,
                        Cess = reader["Cess"] as decimal?,
                        CreatedBy = reader["CreatedBy"] as int?,
                        CreatedOn = reader["CreatedOn"] as DateTime?,
                        UpdatedBy = reader["UpdatedBy"] as int?,
                        UpdatedOn = reader["UpdatedOn"] as DateTime?
                    });
                }

                response.Data = data;
                response.TotalCount = totalCount;
                response.Status = true;
            }
            catch (Exception ex)
            {
                _logger.LogError("StackTrace: {StackTrace}", ex.StackTrace);
                response.Data = new List<ResponseMstSac>();
                response.Status = false;
                response.Message = $"Error: {ex.Message}";
            }

            return response;
        }

        public async Task<AddEditResponse> AddEditMstSac(RequestMstSac request)
        {
            var response = new AddEditResponse();

            try
            {
                var conn = _db.Database.GetDbConnection();
                await conn.OpenAsync();

                using var command = conn.CreateCommand();
                command.CommandText = "SP_AddMstSac";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@SacId", request.SacId));
                command.Parameters.Add(new SqlParameter("@BranchId", request.BranchId ?? (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("@SacCode", request.SacCode ?? (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("@Description", request.Description ?? (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("@Gst", request.Gst ?? (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("@Cess", request.Cess ?? (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("@CreatedBy", request.CreatedBy ?? (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("@UpdatedBy", request.UpdatedBy ?? (object)DBNull.Value));

                using var reader = await command.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    response = new AddEditResponse
                    {
                        Response = reader["Response"] as string
                    };
                }
                else
                {
                    response.Response = "No response from SP";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("StackTrace: {StackTrace}", ex.StackTrace);
                throw new ApplicationException("Failed to execute SP_AddMstSac", ex);
            }

            return response;
        }

        public async Task<AddEditResponse> AddEditmstParty(RequestMstEximTraderMaster request)
        {
            var response = new AddEditResponse();

            try
            {
                var conn = _db.Database.GetDbConnection();
                await conn.OpenAsync();

                using var command = conn.CreateCommand();
                command.CommandText = "SP_AddMstEximTraderMaster";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@TraderId", request.TraderId));
                command.Parameters.Add(new SqlParameter("@OperationType", request.OperationType ?? (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("@EximTraderName", request.EximTraderName ?? (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("@EximTraderAlias", request.EximTraderAlias ?? (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("@Address", request.Address ?? (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("@CountryName", request.CountryName ?? (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("@StateName", request.StateName ?? (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("@CityName", request.CityName ?? (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("@Pincode", request.Pincode ?? (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("@PhoneNo", request.PhoneNo ?? (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("@FaxNo", request.FaxNo ?? (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("@ContactPerson", request.ContactPerson ?? (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("@EmailId", request.EmailId ?? (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("@MobileNo", request.MobileNo ?? (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("@PAN", request.PAN ?? (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("@AadhaarNo", request.AadhaarNo ?? (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("@GSTNo", request.GSTNo ?? (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("@TAN", request.TAN ?? (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("@SapCustomerNo", request.SapCustomerNo ?? (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("@PartyCode", request.PartyCode ?? (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("@CountryId", request.CountryId ?? (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("@StateId", request.StateId ?? (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("@StateCode", request.StateCode ?? (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("@isImporter", request.isImporter ?? (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("@isExporter", request.isExporter ?? (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("@isShipline", request.isShipline ?? (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("@isCHA", request.isCHA ?? (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("@IsForWarder", request.IsForWarder ?? (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("@isRent", request.isRent ?? (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("@isBidder", request.isBidder ?? (object)DBNull.Value));

                using var reader = await command.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    response = new AddEditResponse
                    {
                        Response = reader["Response"] as string
                    };
                }
                else
                {
                    response.Response = "No response from SP";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("StackTrace: {StackTrace}", ex.StackTrace);
                throw new ApplicationException("Failed to execute SP_AddMstEximTraderMaster", ex);
            }

            return response;
        }

        public async Task<Response<List<ResponseMstStorageCharge>>> GetMstStorageCharge(int? chargeId, int? page, int? size)
        {
            var response = new Response<List<ResponseMstStorageCharge>>();

            try
            {
                var conn = _db.Database.GetDbConnection();
                await conn.OpenAsync();

                using var command = conn.CreateCommand();
                command.CommandText = "GetMstStorageCharge";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@ChargeId", chargeId ?? (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("@page", page ?? (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("@size", size ?? (object)DBNull.Value));

                using var reader = await command.ExecuteReaderAsync();

                int totalCount = 0;
                if (await reader.ReadAsync())
                {
                    totalCount = reader.GetInt32(0);
                }

                await reader.NextResultAsync();

                var data = new List<ResponseMstStorageCharge>();
                while (await reader.ReadAsync())
                {
                    data.Add(new ResponseMstStorageCharge
                    {
                        ChargeId = reader.GetInt32(reader.GetOrdinal("ChargeId")),
                        Category = reader["Category"] as string,
                        Amount = reader["Amount"] as decimal?,
                        FromDay = reader.GetInt32(reader.GetOrdinal("FromDay")),
                        ToDay = reader.GetInt32(reader.GetOrdinal("ToDay")),
                        CreatedBy = reader["CreatedBy"] as int?,
                        CreatedDate = reader["CreatedDate"] as DateTime?,
                        UpdatedBy = reader["UpdatedBy"] as int?,
                        UpdatedDate = reader["UpdatedDate"] as DateTime?
                    });
                }

                response.Data = data;
                response.TotalCount = totalCount;
                response.Status = true;
            }
            catch (Exception ex)
            {
                _logger.LogError("StackTrace: {StackTrace}", ex.StackTrace);
                response.Data = new List<ResponseMstStorageCharge>();
                response.Status = false;
                response.Message = $"Error: {ex.Message}";
            }

            return response;
        }

        public async Task<AddEditResponse> AddEditMstStorageCharge(RequestMstStorageCharge request)
        {
            var response = new AddEditResponse();

            try
            {
                var conn = _db.Database.GetDbConnection();
                await conn.OpenAsync();

                using var command = conn.CreateCommand();
                command.CommandText = "SP_AddEditMstStorageCharge";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@ChargeId", request.ChargeId));
                command.Parameters.Add(new SqlParameter("@Category", request.Category ?? (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("@Amount", request.Amount ?? (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("@FromDay", request.FromDay));
                command.Parameters.Add(new SqlParameter("@ToDay", request.ToDay));
                command.Parameters.Add(new SqlParameter("@CreatedBy", request.CreatedBy ?? (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("@UpdatedBy", request.UpdatedBy ?? (object)DBNull.Value));

                using var reader = await command.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    response = new AddEditResponse
                    {
                        Response = reader["Response"] as string
                    };
                }
                else
                {
                    response.Response = "No response from SP";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("StackTrace: {StackTrace}", ex.StackTrace);
                throw new ApplicationException("Failed to execute SP_AddEditMstStorageCharge", ex);
            }

            return response;
        }

        public async Task<Response<List<ResponseMstHandlingCharge>>> GetMstHandlingCharge(int? handlingChargeId, int? page, int? size)
        {
            var response = new Response<List<ResponseMstHandlingCharge>>();

            try
            {
                var conn = _db.Database.GetDbConnection();
                await conn.OpenAsync();

                using var command = conn.CreateCommand();
                command.CommandText = "GetMstHandlingCharge";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@HandlingChargeId", handlingChargeId ?? (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("@page", page ?? (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("@size", size ?? (object)DBNull.Value));

                using var reader = await command.ExecuteReaderAsync();

                int totalCount = 0;
                if (await reader.ReadAsync())
                {
                    totalCount = reader.GetInt32(0);
                }

                await reader.NextResultAsync();

                var data = new List<ResponseMstHandlingCharge>();
                while (await reader.ReadAsync())
                {
                    data.Add(new ResponseMstHandlingCharge
                    {
                        HandlingChargeId = reader.GetInt32(reader.GetOrdinal("HandlingChargeId")),
                        Category = reader["Category"] as string,
                        Amount = reader["Amount"] as decimal?,
                        CreatedBy = reader["CreatedBy"] as int?,
                        CreatedDate = reader["CreatedDate"] as DateTime?,
                        UpdatedBy = reader["UpdatedBy"] as int?,
                        UpdatedDate = reader["UpdatedDate"] as DateTime?
                    });
                }

                response.Data = data;
                response.TotalCount = totalCount;
                response.Status = true;
            }
            catch (Exception ex)
            {
                _logger.LogError("StackTrace: {StackTrace}", ex.StackTrace);
                response.Data = new List<ResponseMstHandlingCharge>();
                response.Status = false;
                response.Message = $"Error: {ex.Message}";
            }

            return response;
        }

        public async Task<AddEditResponse> AddEditMstHandlingCharge(RequestMstHandlingCharge request)
        {
            var response = new AddEditResponse();

            try
            {
                var conn = _db.Database.GetDbConnection();
                await conn.OpenAsync();

                using var command = conn.CreateCommand();
                command.CommandText = "SP_AddEditMstHandlingCharge";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@HandlingChargeId", request.HandlingChargeId));
                command.Parameters.Add(new SqlParameter("@Category", request.Category ?? (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("@Amount", request.Amount ?? (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("@CreatedBy", request.CreatedBy ?? (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("@UpdatedBy", request.UpdatedBy ?? (object)DBNull.Value));

                using var reader = await command.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    response = new AddEditResponse
                    {
                        Response = reader["Response"] as string
                    };
                }
                else
                {
                    response.Response = "No response from SP";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("StackTrace: {StackTrace}", ex.StackTrace);
                throw new ApplicationException("Failed to execute SP_AddEditMstHandlingCharge", ex);
            }

            return response;
        }

        public async Task<Response<List<ResponseMstGodown>>> GetMstGodown(int? godownId, int? page, int? size)
        {
            var response = new Response<List<ResponseMstGodown>>();

            try
            {
                var conn = _db.Database.GetDbConnection();
                await conn.OpenAsync();

                using var command = conn.CreateCommand();
                command.CommandText = "GetMstGodown";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@GodownId", godownId ?? (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("@page", page ?? (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("@size", size ?? (object)DBNull.Value));

                using var reader = await command.ExecuteReaderAsync();

                int totalCount = 0;
                if (await reader.ReadAsync())
                {
                    totalCount = reader.GetInt32(0);
                }

                await reader.NextResultAsync();

                var data = new List<ResponseMstGodown>();
                while (await reader.ReadAsync())
                {
                    data.Add(new ResponseMstGodown
                    {
                        GodownId = reader.GetInt32(reader.GetOrdinal("GodownId")),
                        GodownName = reader["GodownName"] as string,
                        LocationAlias = reader["LocationAlias"] as string,
                        CreatedBy = reader["CreatedBy"] as int?,
                        CreatedDate = reader["CreatedDate"] as DateTime?,
                        UpdatedBy = reader["UpdatedBy"] as int?,
                        UpdatedDate = reader["UpdatedDate"] as DateTime?
                    });
                }

                response.Data = data;
                response.TotalCount = totalCount;
                response.Status = true;
            }
            catch (Exception ex)
            {
                _logger.LogError("StackTrace: {StackTrace}", ex.StackTrace);
                response.Status = false;
                response.Message = $"Error: {ex.Message}";
                response.Data = new List<ResponseMstGodown>();
            }

            return response;
        }

        public async Task<AddEditResponse> AddEditGodownAsync(RequestMstGodown request)
        {
            var response = new AddEditResponse();

            try
            {
                var conn = _db.Database.GetDbConnection();
                await conn.OpenAsync();

                using var command = conn.CreateCommand();
                command.CommandText = "SP_AddMstGodown";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@GodownId", request.GodownId));
                command.Parameters.Add(new SqlParameter("@GodownName", request.GodownName ?? (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("@LocationAlias", request.LocationAlias ?? (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("@CreatedBy", request.CreatedBy ?? (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("@UpdatedBy", request.UpdatedBy ?? (object)DBNull.Value));

                using var reader = await command.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    response.Response = reader["Response"] as string;
                }
                else
                {
                    response.Response = "No response from SP";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("StackTrace: {StackTrace}", ex.StackTrace);
                throw new ApplicationException("Failed to execute SP_AddEditMstGodown", ex);
            }

            return response;
        }

        public async Task<Response<List<ResponseExamination>>> GetExaminationAsync(int? examinationId, int? page, int? size)
        {
            var response = new Response<List<ResponseExamination>>();

            try
            {
                var conn = _db.Database.GetDbConnection();
                await conn.OpenAsync();

                using var command = conn.CreateCommand();
                command.CommandText = "SP_GetExamination";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@Examination_id", examinationId ?? (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("@page", page ?? (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("@size", size ?? (object)DBNull.Value));

                using var reader = await command.ExecuteReaderAsync();

                int totalCount = 0;
                if (await reader.ReadAsync())
                {
                    totalCount = reader.GetInt32(0);
                }

                await reader.NextResultAsync();

                var data = new List<ResponseExamination>();
                while (await reader.ReadAsync())
                {
                    data.Add(new ResponseExamination
                    {
                        Examination_id = reader.GetInt32(reader.GetOrdinal("Examination_id")),
                        Receive_id = reader["Receive_id"] as int?,
                        Examination_date = reader["Examination_date"] as DateTime?,
                        Colour = reader["Colour"] as string,
                        Colour_code = reader["Colour_code"] as string,
                        Weight_kg = reader["Weight_kg"] as decimal?,
                        Size = reader["Size"] as string,
                        Length = reader["Length"] as decimal?,
                        Width = reader["Width"] as decimal?,
                        Height = reader["Height"] as decimal?,
                        Cargo_type = reader["Cargo_type"] as string,
                        Stored_location = reader["Stored_location"] as string,
                        Remarks = reader["Remarks"] as string,
                        CreatedDate = reader["CreatedDate"] as DateTime?,
                        CreatedBy = reader["CreatedBy"] as string,
                        UpdatedDate = reader["UpdatedDate"] as DateTime?,
                        UpdatedBy = reader["UpdatedBy"] as string,
                        ReceiptNo = reader["ReceiptNo"] as string
                    });
                }

                response.Data = data;
                response.TotalCount = totalCount;
                response.Status = true;
            }
            catch (Exception ex)
            {
                _logger.LogError("StackTrace: {StackTrace}", ex.StackTrace);
                response.Status = false;
                response.Message = $"Error: {ex.Message}";
                response.Data = new List<ResponseExamination>();
            }

            return response;
        }

        public async Task<Response<AddEditResponse>> AddEditExaminationAsync(RequestExamination request)
        {
            var response = new Response<AddEditResponse>();

            try
            {
                var conn = _db.Database.GetDbConnection();
                await conn.OpenAsync();

                using var command = conn.CreateCommand();
                command.CommandText = "SP_AddEditExamination";
                command.CommandType = CommandType.StoredProcedure;

                void AddParameter(string name, object value)
                {
                    var param = command.CreateParameter();
                    param.ParameterName = name;
                    param.Value = value ?? DBNull.Value;
                    command.Parameters.Add(param);
                }

                AddParameter("@Examination_id", request.Examination_id);
                AddParameter("@Receive_id", request.Receive_id);
                AddParameter("@Examination_date", request.Examination_date);
                AddParameter("@Colour", request.Colour);
                AddParameter("@Colour_code", request.Colour_code);
                AddParameter("@Weight_kg", request.Weight_kg);
                AddParameter("@Size", request.Size);
                AddParameter("@Length", request.Length);
                AddParameter("@Width", request.Width);
                AddParameter("@Height", request.Height);
                AddParameter("@Cargo_type", request.Cargo_type);
                AddParameter("@Stored_location", request.Stored_location);
                AddParameter("@Remarks", request.Remarks);
                AddParameter("@CreatedBy", request.CreatedBy);
                AddParameter("@UpdatedBy", request.UpdatedBy);

                int resultId = 0;

                using var reader = await command.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    resultId = Convert.ToInt32(reader[0]);
                }

                response.Data = new AddEditResponse
                {
                    Response = request.Examination_id == 0 ? "Inserted successfully" : "Updated successfully"
                };
                response.Status = true;
            }
            catch (Exception ex)
            {
                _logger.LogError("StackTrace: {StackTrace}", ex.StackTrace);
                response.Data = new AddEditResponse
                {
                    Response = $"Error: {ex.Message}"
                };
                response.Status = false;
            }

            return response;
        }

        public async Task<Response<List<ResponseReceiptDetails>>> GetReceiptDetails(int? ReceiptId, int? page, int? size, bool? forExamStor)
        {
            var response = new Response<List<ResponseReceiptDetails>>();

            try
            {
                var conn = _db.Database.GetDbConnection();
                await conn.OpenAsync();

                using var command = conn.CreateCommand();
                command.CommandText = "Sp_GetReceiptDetails";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@ReceiptId", ReceiptId ?? (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("@page", page ?? (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("@size", size ?? (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("@forExamStor", forExamStor ?? (object)DBNull.Value));

                using var reader = await command.ExecuteReaderAsync();

                int totalCount = 0;
                if (await reader.ReadAsync())
                {
                    totalCount = reader.GetInt32(0);
                }

                await reader.NextResultAsync();

                var data = new List<ResponseReceiptDetails>();
                while (await reader.ReadAsync())
                {
                    data.Add(new ResponseReceiptDetails
                    {
                        ReceiptId = reader.GetInt32(reader.GetOrdinal("ReceiptId")),
                        ReceiptNo = reader["ReceiptNo"] as string,
                        ReceiptDate = reader["ReceiptDate"] as DateTime?,
                        From = reader["From"] as string,
                        DrNo = reader["DrNo"] as string,
                        AocNo = reader["AocNo"] as string,
                        DateOfLanding = reader["DateOfLanding"] as DateTime?,
                        PassportNo = reader["PassportNo"] as string,
                        FlightNo = reader["FlightNo"] as string,
                        IsSealed = reader["IsSealed"] as string,
                        Remarks = reader["Remarks"] as string,
                        ReasonForDetention = reader["ReasonForDetention"] as string,
                        CreatedDate = reader["CreatedDate"] as DateTime?,
                        UpdatedDate = reader["UpdatedDate"] as DateTime?,
                        CreatedBy = reader["CreatedBy"] as string,
                        UpdatedBy = reader["UpdatedBy"] as string
                    });
                }

                response.Data = data;
                response.TotalCount = totalCount;
                response.Status = true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetReceiptDetailsAsync: {Message}", ex.Message);
                response.Status = false;
                response.Message = $"Error: {ex.Message}";
                response.Data = new List<ResponseReceiptDetails>();
            }

            return response;
        }

        public async Task<Response<AddEditResponse>> AddEditReceiptDetailsAsync(RequestReceiptDetails request)
        {
            var response = new Response<AddEditResponse>();

            try
            {
                var conn = _db.Database.GetDbConnection();
                await conn.OpenAsync();

                using var command = conn.CreateCommand();
                command.CommandText = "SP_AddEditReceiptDetails";
                command.CommandType = CommandType.StoredProcedure;

                void AddParameter(string name, object value)
                {
                    var param = command.CreateParameter();
                    param.ParameterName = name;
                    param.Value = value ?? DBNull.Value;
                    command.Parameters.Add(param);
                }

                AddParameter("@ReceiptId", request.ReceiptId);
                AddParameter("@ReceiptDate", request.ReceiptDate);
                AddParameter("@From", request.From);
                AddParameter("@DrNo", request.DrNo);
                AddParameter("@AocNo", request.AocNo);
                AddParameter("@DateOfLanding", request.DateOfLanding);
                AddParameter("@PassportNo", request.PassportNo);
                AddParameter("@FlightNo", request.FlightNo);
                AddParameter("@IsSealed", request.IsSealed);
                AddParameter("@Remarks", request.Remarks);
                AddParameter("@ReasonForDetention", request.ReasonForDetention);
                AddParameter("@CreatedBy", request.CreatedBy);
                AddParameter("@UpdatedBy", request.UpdatedBy);

                int resultId = 0;

                using var reader = await command.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    resultId = Convert.ToInt32(reader[0]);
                }

                response.Data = new AddEditResponse
                {
                    Response = request.ReceiptId == 0 ? "Inserted successfully" : "Updated successfully"
                };
                response.Status = true;
            }
            catch (Exception ex)
            {
                _logger.LogError("StackTrace: {StackTrace}", ex.StackTrace);
                response.Data = new AddEditResponse
                {
                    Response = $"Error: {ex.Message}"
                };
                response.Status = false;
            }

            return response;
        }

        public async Task<Response<List<ResponseBaggageClaim>>> GetBaggageClaimAsync(int? claimId, int? page, int? size)
        {
            var response = new Response<List<ResponseBaggageClaim>>();

            try
            {
                var conn = _db.Database.GetDbConnection();
                await conn.OpenAsync();

                using var command = conn.CreateCommand();
                command.CommandText = "GetAB_Baggage_claim";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@Claim_id", claimId ?? (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("@page", page ?? (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("@size", size ?? (object)DBNull.Value));

                using var reader = await command.ExecuteReaderAsync();

                int totalCount = 0;
                if (await reader.ReadAsync())
                {
                    totalCount = reader.GetInt32(0);
                }

                await reader.NextResultAsync();

                var data = new List<ResponseBaggageClaim>();
                while (await reader.ReadAsync())
                {
                    data.Add(new ResponseBaggageClaim
                    {
                        Claim_id = reader.GetInt32(reader.GetOrdinal("Claim_id")),
                        Claim_no = reader["Claim_no"] as string,
                        Claim_date = reader["Claim_date"] as DateTime?,
                        Party_id = reader["Party_id"] as decimal?,
                        Party_name = reader["Party_name"] as string,
                        Passport_no = reader["Passport_no"] as string,
                        Name = reader["Name"] as string,
                        AOC_DR_no = reader["AOC_DR_no"] as string,
                        Receive_id = reader["Receive_id"] as int?,
                        Baggage_receipt_no = reader["Baggage_receipt_no"] as string,
                        Baggage_receipt_date = reader["Baggage_receipt_date"] as DateTime?,
                        Remarks = reader["Remarks"] as string,
                        CreatedBy = reader["CreatedBy"] as string,
                        CreatedDate = reader["CreatedDate"] as DateTime?,
                        UpdatedBy = reader["UpdatedBy"] as string,
                        UpdatedDate = reader["UpdatedDate"] as DateTime?,
                        ReceiptNo = reader["ReceiptNo"] as string
                    });
                }

                response.Data = data;
                response.TotalCount = totalCount;
                response.Status = true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetBaggageClaimAsync: {Message}", ex.Message);
                response.Status = false;
                response.Message = $"Error: {ex.Message}";
                response.Data = new List<ResponseBaggageClaim>();
            }

            return response;
        }

        public async Task<Response<AddEditResponse>> AddEditBaggageClaimAsync(RequestBaggageClaim request)
        {
            var response = new Response<AddEditResponse>();

            try
            {
                var conn = _db.Database.GetDbConnection();      
                await conn.OpenAsync();

                using var command = conn.CreateCommand();
                command.CommandText = "Sp_AddEditBaggageClaim";
                command.CommandType = CommandType.StoredProcedure;

                void AddParam(string name, object? value)
                {
                    var param = command.CreateParameter();
                    param.ParameterName = name;
                    param.Value = value ?? DBNull.Value;
                    command.Parameters.Add(param);
                }

                AddParam("@Claim_id", request.Claim_id);

                var claimNoParam = new SqlParameter("@Claim_no", SqlDbType.VarChar, 50)
                {
                    Direction = ParameterDirection.InputOutput,
                    Value = request.Claim_no ?? (object)DBNull.Value
                };
                command.Parameters.Add(claimNoParam);

                AddParam("@Claim_date", request.Claim_date);
                AddParam("@Party_id", request.Party_id);
                AddParam("@Party_name", request.Party_name);
                AddParam("@Passport_no", request.Passport_no);
                AddParam("@Name", request.Name);
                AddParam("@AOC_DR_no", request.AOC_DR_no);
                AddParam("@Receive_id", request.Receive_id);
                AddParam("@Baggage_receipt_no", request.Baggage_receipt_no);
                AddParam("@Baggage_receipt_date", request.Baggage_receipt_date);
                AddParam("@Remarks", request.Remarks);
                AddParam("@CreatedBy", request.CreatedBy);
                AddParam("@UpdatedBy", request.UpdatedBy);

                int insertedId = 0;

                using var reader = await command.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    insertedId = Convert.ToInt32(reader["Claim_id"]);
                }

                var outputClaimNo = claimNoParam.Value?.ToString();

                response.Data = new AddEditResponse
                {
                    Response = request.Claim_id == 0 ? "Inserted successfully" : "Updated successfully"
                };
                response.Status = true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in AddEditBaggageClaimAsync: {Message}", ex.Message);
                response.Data = new AddEditResponse
                {
                    Response = $"Error: {ex.Message}"
                };
                response.Status = false;
            }

            return response;
        }

        public async Task<Response<AddEditResponse>> AddEditPaymentReceiptAsync(PaymentReceipt request)
        {
            var response = new Response<AddEditResponse>();

            try
            {
                var conn = _db.Database.GetDbConnection();
                await conn.OpenAsync();

                using var command = conn.CreateCommand();
                command.CommandText = "Sp_AddEditPaymentReceipt";
                command.CommandType = CommandType.StoredProcedure;

                // Helper method to add parameter
                void AddParam(string name, object? value)
                {
                    var param = command.CreateParameter();
                    param.ParameterName = name;
                    param.Value = value ?? DBNull.Value;
                    command.Parameters.Add(param);
                }

                AddParam("@ReceiptId", request.ReceiptId);

                var receiptNoParam = new SqlParameter("@ReceiptNo", SqlDbType.VarChar, 100)
                {
                    Direction = ParameterDirection.InputOutput,
                    Value = request.ReceiptNo ?? (object)DBNull.Value
                };
                command.Parameters.Add(receiptNoParam);

                AddParam("@PartyId", request.PartyId);
                AddParam("@PartyName", request.PartyName);
                AddParam("@InvoiceId", request.InvoiceId);
                AddParam("@InvoiceNo", request.InvoiceNo);
                AddParam("@Amount", request.Amount);
                AddParam("@ModeOfPayment", request.ModeOfPayment);
                AddParam("@Remarks", request.Remarks);
                    AddParam("@CreatedBy", request.CreatedBy);
                    AddParam("@UpdatedBy", request.UpdatedBy);

                int insertedId = 0;
                using var reader = await command.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    insertedId = Convert.ToInt32(reader["ReceiptId"]);
                }

                var outputReceiptNo = receiptNoParam.Value?.ToString();

                response.Data = new AddEditResponse
                {
                    //  Id = insertedId,
                    // ReceiptNo = outputReceiptNo,
                    Response = request.ReceiptId == 0 ? $"Inserted successfully Id = {insertedId} , ReceiptNo= {outputReceiptNo}" : "Updated successfully"
                };
                response.Status = true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in AddEditPaymentReceiptAsync: {Message}", ex.Message);

                response.Data = new AddEditResponse
                {
                    Response = $"Error: {ex.Message}"
                };
                response.Status = false;
            }

            return response;
        }

        public async Task<Response<List<PaymentReceipt>>> GetPaymentReceiptAsync(int? receiptId, int? page, int? size)
        {
            var response = new Response<List<PaymentReceipt>>();

            try
            {
                var conn = _db.Database.GetDbConnection();
                await conn.OpenAsync();

                using var command = conn.CreateCommand();
                command.CommandText = "GetPaymentReceipt";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@ReceiptId", receiptId ?? (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("@page", page ?? (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("@size", size ?? (object)DBNull.Value));

                using var reader = await command.ExecuteReaderAsync();

                int totalCount = 0;
                if (await reader.ReadAsync())
                {
                    totalCount = reader.GetInt32(0);
                }

                await reader.NextResultAsync();

                var data = new List<PaymentReceipt>();
                while (await reader.ReadAsync())
                {
                    data.Add(new PaymentReceipt
                    {
                        ReceiptId = reader.GetInt32(reader.GetOrdinal("ReceiptId")),
                        ReceiptNo = reader["ReceiptNo"] as string,
                        PartyId = reader["PartyId"] as int?,
                        PartyName = reader["PartyName"] as string,
                        InvoiceId = reader["InvoiceId"] as int?,
                        InvoiceNo = reader["InvoiceNo"] as string,
                        Amount = reader["Amount"] as decimal? ?? 0,
                        ModeOfPayment = reader["ModeOfPayment"] as string ?? string.Empty,
                        Remarks = reader["Remarks"] as string,
                        CreatedDate = reader["CreatedDate"] as DateTime?,
                        CreatedBy = reader["CreatedBy"] as string,
                        UpdatedDate = reader["UpdatedDate"] as DateTime?,
                        UpdatedBy = reader["UpdatedBy"] as string
                    });
                }

                response.Data = data;
                response.TotalCount = totalCount;
                response.Status = true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetPaymentReceiptAsync: {Message}", ex.Message);
                response.Status = false;
                response.Message = $"Error: {ex.Message}";
                response.Data = new List<PaymentReceipt>();
            }

            return response;
        }


        public async Task<Response<List<UnclaimedReceiptDto>>> UnclaimedReceiptDto()
        {
            var response = new Response<List<UnclaimedReceiptDto>>();

            try
            {
                var conn = _db.Database.GetDbConnection();
                await conn.OpenAsync();

                using var command = conn.CreateCommand();
                command.CommandText = "Sp_GetUnclaimedReceiptDetails";
                command.CommandType = CommandType.StoredProcedure;

                using var reader = await command.ExecuteReaderAsync();

                var data = new List<UnclaimedReceiptDto>();

                while (await reader.ReadAsync())
                {
                    data.Add(new UnclaimedReceiptDto
                    {
                        ReceiptId = reader.GetInt32(reader.GetOrdinal("ReceiptId")),
                        ReceiptNo = reader["ReceiptNo"] as string,
                        From = reader["From"] as string,
                        DrNo = reader["DrNo"] as string,
                        AocNo = reader["AocNo"] as string,
                        DateOfLanding = reader["DateOfLanding"] as DateTime?,
                        PassportNo = reader["PassportNo"] as string,
                        FlightNo = reader["FlightNo"] as string,
                        IsSealed = reader["IsSealed"] as bool?,
                        Remarks = reader["Remarks"] as string,
                        ReasonForDetention = reader["ReasonForDetention"] as string,
                        CreatedDate = reader["CreatedDate"] as DateTime?,
                        UpdatedDate = reader["UpdatedDate"] as DateTime?,
                        CreatedBy = reader["CreatedBy"] as string,
                        UpdatedBy = reader["UpdatedBy"] as string,
                        ReceiptDate = reader["ReceiptDate"] as DateTime?
                    });
                }

                response.Data = data;
                response.TotalCount = data.Count;
                response.Status = true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetUnclaimedReceiptDetailsAsync: {Message}", ex.Message);
                response.Status = false;
                response.Message = $"Error: {ex.Message}";
                response.Data = new List<UnclaimedReceiptDto>();
            }

            return response;
        }

        public async Task<Response<AddEditResponse>> AddEditDeliveryAsync(RequestDelivery request)
        {
            var response = new Response<AddEditResponse>();

            try
            {
                var conn = _db.Database.GetDbConnection();
                await conn.OpenAsync();

                using var command = conn.CreateCommand();
                command.CommandText = "Sp_AddEdit_Delivery";
                command.CommandType = CommandType.StoredProcedure;

                void AddParam(string name, object? value)
                {
                    var param = command.CreateParameter();
                    param.ParameterName = name;
                    param.Value = value ?? DBNull.Value;
                    command.Parameters.Add(param);
                }

                AddParam("@DeliveryId", request.DeliveryId);
                AddParam("@ReceiptId", request.ReceiptId);
                AddParam("@ReceiptNo", request.ReceiptNo);
                AddParam("@DeliveryDate", request.DeliveryDate);
                AddParam("@DeliveryTime", request.DeliveryTime);
                AddParam("@CustomOfficeId", request.CustomOfficeId);
                AddParam("@Remarks", request.Remarks);
                AddParam("@CreatedBy", request.CreatedBy);
                AddParam("@UpdatedBy", request.UpdatedBy);

                int insertedId = 0;
                using var reader = await command.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    insertedId = Convert.ToInt32(reader["DeliveryId"]);
                }

                response.Data = new AddEditResponse
                {
                    Response = request.DeliveryId == 0
                        ? $"Inserted successfully. DeliveryId = {insertedId}"
                        : "Updated successfully"
                };
                response.Status = true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in AddEditDeliveryAsync: {Message}", ex.Message);

                response.Data = new AddEditResponse
                {
                    Response = $"Error: {ex.Message}"
                };
                response.Status = false;
            }

            return response;
        }

        public async Task<Response<List<ResponseDelivery>>> GetDeliveryAsync(int? deliveryId, int? receiptId, int? page, int? size)
        {
            var response = new Response<List<ResponseDelivery>>();

            try
            {
                var conn = _db.Database.GetDbConnection();
                await conn.OpenAsync();

                using var command = conn.CreateCommand();
                command.CommandText = "Sp_Get_Delivery";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@DeliveryId", deliveryId ?? (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("@ReceiptId", receiptId ?? (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("@page", page ?? (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("@size", size ?? (object)DBNull.Value));

                using var reader = await command.ExecuteReaderAsync();

                int totalCount = 0;
                if (await reader.ReadAsync())
                {
                    totalCount = reader.GetInt32(0);
                }

                await reader.NextResultAsync();

                var data = new List<ResponseDelivery>();
                while (await reader.ReadAsync())
                {
                    data.Add(new ResponseDelivery
                    {
                        DeliveryId = reader.GetInt32(reader.GetOrdinal("DeliveryId")),
                        ReceiptId = reader.GetInt32(reader.GetOrdinal("ReceiptId")),
                        ReceiptNo = reader["ReceiptNo"] as string,
                        DeliveryDate = reader["DeliveryDate"] as DateTime?,
                        DeliveryTime = reader["DeliveryTime"] as TimeSpan?,
                        CustomOfficeId = reader["CustomOfficeId"] as string,
                        Remarks = reader["Remarks"] as string,
                        CreatedDate = reader["CreatedDate"] as DateTime?,
                        CreatedBy = reader["CreatedBy"] as string,
                        UpdatedDate = reader["UpdatedDate"] as DateTime?,
                        UpdatedBy = reader["UpdatedBy"] as string
                    });
                }

                response.Data = data;
                response.TotalCount = totalCount;
                response.Status = true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetDeliveryAsync: {Message}", ex.Message);
                response.Status = false;
                response.Message = $"Error: {ex.Message}";
                response.Data = new List<ResponseDelivery>();
            }

            return response;
        }

    }
}
