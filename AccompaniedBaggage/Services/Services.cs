using AccompaniedBaggage.Model.Request;
using AccompaniedBaggage.Model.Response;
using Azure.Core;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SezApi.Data;
using SezApi.Model.Request;
using SezApi.Model.Response;
using System.Data;
namespace SezApi.Services
{
    public class Services : IServices
    {
        private readonly AccompaniedBaggageDbContext _db;

        public Services(AccompaniedBaggageDbContext db)
        {
            _db = db;
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
                throw new ApplicationException("Failed to execute SP_AddEditMstGodown", ex);
            }

            return response;
        }

    }
}
