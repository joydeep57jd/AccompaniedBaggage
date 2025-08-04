
using AccompaniedBaggage.Model.Request;
using AccompaniedBaggage.Model.Response;
using Azure.Core;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SezApi.Data;
using SezApi.Model.Response;
using System.Data;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
namespace AccompaniedBaggage.Services
{
    public class CWCservice
    {
        private readonly AccompaniedBaggageDbContext _dbContext;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly ILogger<CWCservice> _logger;
        public CWCservice(HttpClient httpClient, IConfiguration configuration, ILogger<CWCservice> logger, AccompaniedBaggageDbContext db)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _dbContext = db;
            _logger = logger;
        }

        public async Task<ResponseCWCapi> PostInvoiceToCWCAsync(RequestCWCapi request)
        {
            try
            {
                string url = _configuration["CWCApi:BaseUrl"];
                string user = _configuration["CWCApi:UserId"];
                string pwd = _configuration["CWCApi:Password"];

                string json = JsonSerializer.Serialize(request);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                _logger.LogError($"Posting to CWC API: {url} with data: {json}");
                string credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{user}:{pwd}"));
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);

                var httpResponse = await _httpClient.PostAsync(url, content);
                var responseJson = await httpResponse.Content.ReadAsStringAsync();

                if (!httpResponse.IsSuccessStatusCode)
                {
                    throw new Exception($"CWC API returned error: {httpResponse.StatusCode} - {responseJson}");
                }

                var result = JsonSerializer.Deserialize<ResponseCWCapi>(responseJson, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while calling CWC API: {ex.Message}", ex);
            }
        }

        public async Task<AddEditResponse> GetInvoiceDataFromSPAsync(GetInvoiceDtlforSAPRequest request, int invId)
        {
            AddEditResponse response = new AddEditResponse();

            try
            {
                var model = new RequestCWCapi
                {
                    REQUEST1 = new List<Request1>()
                };

                await using var conn = _dbContext.Database.GetDbConnection();
                await using var cmd = conn.CreateCommand();
                if (conn.State != ConnectionState.Closed)
                    await conn.CloseAsync();
                cmd.CommandText = "GetInvoiceDtlforSAP";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@in_InvoiceNo", request.InvoiceNo ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@in_IsIRN", request.IsIRN));

                await conn.OpenAsync();
                try
                {
                    await using var reader = await cmd.ExecuteReaderAsync();

                    Header? header = null;
                    var itemList = new List<Item>();

                    if (await reader.ReadAsync())
                    {
                        header = new Header
                        {
                            LINE_NO = reader["LINE_NO"]?.ToString() ?? string.Empty,
                            HEADER_TXT = reader["HEADER_TXT"]?.ToString() ?? string.Empty,
                            REF_DOC_NO = reader["REF_DOC_NO"]?.ToString() ?? string.Empty,
                            COMP_CODE = reader["COMP_CODE"]?.ToString() ?? string.Empty,
                            DOC_DATE = reader["DOC_DATE"]?.ToString() ?? string.Empty,
                            PSTNG_DATE = reader["PSTNG_DATE"]?.ToString() ?? string.Empty,
                            FISC_YEAR = reader["FISC_YEAR"]?.ToString() ?? string.Empty,
                            DOC_TYPE = reader["DOC_TYPE"]?.ToString() ?? string.Empty,
                            IRN_NO = reader["IRN_NO"]?.ToString() ?? string.Empty,
                            QR_CODE = reader["QR_CODE"]?.ToString() ?? string.Empty,
                            IRN_ACKN_NO = reader["IRN_ACKN_NO"]?.ToString() ?? string.Empty,
                            IRN_ACKN_DATE = reader["IRN_ACKN_DATE"]?.ToString() ?? string.Empty
                        };
                    }

                    if (await reader.NextResultAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            itemList.Add(new Item
                            {
                                LINE_NO = reader["LINE_NO"]?.ToString() ?? string.Empty,
                                ITEMNO_ACC = reader["ITEMNO_ACC"]?.ToString() ?? string.Empty,
                                GL_ACCOUNT = reader["GL_ACCOUNT"]?.ToString() ?? string.Empty,
                                PROFITSEG = reader["PROFITSEG"]?.ToString() ?? string.Empty,
                                C_CTR_AREA = reader["C_CTR_AREA"]?.ToString() ?? string.Empty,
                                VENDOR_NO = reader["VENDOR_NO"]?.ToString() ?? string.Empty,
                                CUSTOMER = reader["CUSTOMER"]?.ToString() ?? string.Empty,
                                CUST_RECON_ACCOUNT = reader["CUST_RECON_ACCOUNT"]?.ToString() ?? string.Empty,
                                SP_GL_IND = reader["SP_GL_IND"]?.ToString() ?? string.Empty,
                                WBS_ELEMENT = reader["WBS_ELEMENT"]?.ToString() ?? string.Empty,
                                COSTCENTER = reader["COSTCENTER"]?.ToString() ?? string.Empty,
                                ORDERID = reader["ORDERID"]?.ToString() ?? string.Empty,
                                PROFITCENTER = reader["PROFITCENTER"]?.ToString() ?? string.Empty,
                                ALLOC_NUMBER = reader["ALLOC_NUMBER"]?.ToString() ?? string.Empty,
                                ITEM_TEXT = reader["ITEM_TEXT"]?.ToString() ?? string.Empty,
                                BUSINESSPLACE = reader["BUSINESSPLACE"]?.ToString() ?? string.Empty,
                                SECTION_CODE = reader["SECTION_CODE"]?.ToString() ?? string.Empty,
                                DT_CT_INDICATOR = reader["DT_CT_INDICATOR"]?.ToString() ?? string.Empty,
                                AMT_DOCCUR = reader["AMT_DOCCUR"]?.ToString() ?? string.Empty,
                                DOC_CURRENCY = reader["DOC_CURRENCY"]?.ToString() ?? string.Empty,
                                AMT_LOCCUR = reader["AMT_LOCCUR"]?.ToString() ?? string.Empty,
                                TAX_CODE = reader["TAX_CODE"]?.ToString() ?? string.Empty,
                                HSN_SAC = reader["HSN_SAC"]?.ToString() ?? string.Empty,
                                WITHHOLD_TAX_TYPE = reader["WITHHOLD_TAX_TYPE"]?.ToString() ?? string.Empty,
                                WITHHOLD_TAX_CODE = reader["WITHHOLD_TAX_CODE"]?.ToString() ?? string.Empty,
                                TDS_BASE_AMOUNT = reader["TDS_BASE_AMOUNT"]?.ToString() ?? string.Empty,
                                FUND = reader["FUND"]?.ToString() ?? string.Empty,
                                VALUE_DATE = reader["VALUE_DATE"]?.ToString() ?? string.Empty,
                                SALES_ORDER = reader["SALES_ORDER"]?.ToString() ?? string.Empty,
                                SALES_ORDER_ITEM = reader["SALES_ORDER_ITEM"]?.ToString() ?? string.Empty,
                                PLACE_OF_SUPPLY = reader["PLACE_OF_SUPPLY"]?.ToString() ?? string.Empty
                            });
                        }
                    }

                    model.REQUEST1.Add(new Request1
                    {
                        HEADER = header!,
                        ITEM = itemList
                    });
                }
                finally
                {
                    if (conn.State != ConnectionState.Closed)
                        await conn.CloseAsync();
                }

                ResponseCWCapi? sapResponse;
                try
                {
                    sapResponse = await PostInvoiceToCWCAsync(model);
                }
                catch (Exception ex)
                {
                    response.Response = $"Error posting to CWC API: {ex.Message}";
                    return response;
                }

                if (sapResponse?.Response1 != null)
                {
                    try
                    {
                        await using var saveConn = _dbContext.Database.GetDbConnection();
                        await using var saveCmd = saveConn.CreateCommand();

                        saveCmd.CommandText = "InsertSapResponse";
                        saveCmd.CommandType = CommandType.StoredProcedure;

                        saveCmd.Parameters.Add(new SqlParameter("@InvoiceId", invId));
                        saveCmd.Parameters.Add(new SqlParameter("@InvoiceNo", request.InvoiceNo ?? string.Empty));
                        saveCmd.Parameters.Add(new SqlParameter("@SAP_DOC_NUMBER", sapResponse.Response1.SAPDocNumber ?? string.Empty));
                        saveCmd.Parameters.Add(new SqlParameter("@REF_DOC_NO", sapResponse.Response1.RefDocNo ?? string.Empty));
                        saveCmd.Parameters.Add(new SqlParameter("@STATUS", sapResponse.Response1.Status ?? string.Empty));
                        saveCmd.Parameters.Add(new SqlParameter("@REMARK", sapResponse.Response1.Remark ?? string.Empty));
                        saveCmd.Parameters.Add(new SqlParameter("@CreatedBy", 0));
                        saveCmd.Parameters.Add(new SqlParameter("@Module", "CWC"));

                        await saveConn.OpenAsync();
                        try
                        {
                            await saveCmd.ExecuteNonQueryAsync();
                        }
                        finally
                        {
                            if (saveConn.State != ConnectionState.Closed)
                                await saveConn.CloseAsync();
                        }

                        if (!string.IsNullOrEmpty(sapResponse.Response1.Status))
                        {
                            await using var updConn = _dbContext.Database.GetDbConnection();
                            await using var updCmd = updConn.CreateCommand();

                            updCmd.CommandText = "UpdateClaimSAPStatus";
                            updCmd.CommandType = CommandType.StoredProcedure;

                            updCmd.Parameters.Add(new SqlParameter("@Claim_id", invId));
                            updCmd.Parameters.Add(new SqlParameter("@SAP_DOC_NUMBER", sapResponse.Response1.SAPDocNumber ?? string.Empty));

                            await updConn.OpenAsync();
                            try
                            {
                                await updCmd.ExecuteNonQueryAsync();
                            }
                            finally
                            {
                                if (updConn.State != ConnectionState.Closed)
                                    await updConn.CloseAsync();
                            }
                        }

                        response.Response = "success";
                        return response;
                    }
                    catch (Exception ex)
                    {
                        response.Response = $"Error saving SAP response: {ex.Message}";
                        return response;
                    }
                }

                response.Response = "No response from CWC API";
                return response;
            }
            catch (Exception ex)
            {
                response.Response = $"General error: {ex.Message}";
                return response;
            }
        }


        public async Task<AddEditResponse> GetReceiptDataFromSPAsync(GetCashReceiptDtlforSAPRequest request, int cashReceiptId)
        {
            AddEditResponse response = new AddEditResponse();

            try
            {
                var model = new RequestCWCapiReceipt
                {
                    REQUEST = new List<Request2>()
                };

                await using var conn = _dbContext.Database.GetDbConnection();
                await using var cmd = conn.CreateCommand();
                if (conn.State != ConnectionState.Closed)
                    await conn.CloseAsync();

                cmd.CommandText = "GetReceiptDtlforSAP";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@in_ReceiptNo", request.inReceiptNo ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@in_IsIRN", request.IsIRN));

                await conn.OpenAsync();

                HeaderReceipt? header = null;
                var itemList = new List<ItemReceipt>();

                await using var reader = await cmd.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    header = new HeaderReceipt
                    {
                        DOC_NO = reader["DOC_NO"]?.ToString() ?? string.Empty,
                        USERNAME = reader["USERNAME"]?.ToString() ?? string.Empty,
                        HEADER_TXT = reader["HEADER_TXT"]?.ToString() ?? string.Empty,
                        COMP_CODE = reader["COMP_CODE"]?.ToString() ?? string.Empty,
                        DOC_DATE = reader["DOC_DATE"]?.ToString() ?? string.Empty,
                        PSTNG_DATE = reader["PSTNG_DATE"]?.ToString() ?? string.Empty,
                        FISC_YEAR = reader["FISC_YEAR"]?.ToString() ?? string.Empty,
                        FIS_PERIOD = reader["FIS_PERIOD"]?.ToString() ?? string.Empty,
                        DOC_TYPE = reader["DOC_TYPE"]?.ToString() ?? string.Empty,
                        REF_DOC_NO = reader["REF_DOC_NO"]?.ToString() ?? string.Empty,
                        CURRENCY = reader["CURRENCY"]?.ToString() ?? string.Empty,
                        NAME = reader["NAME"]?.ToString() ?? string.Empty,
                        NAME_2 = reader["NAME_2"]?.ToString() ?? string.Empty,
                        NAME_3 = reader["NAME_3"]?.ToString() ?? string.Empty,
                        NAME_4 = reader["NAME_4"]?.ToString() ?? string.Empty,
                        POSTL_CODE = reader["POSTL_CODE"]?.ToString() ?? string.Empty,
                        CITY = reader["CITY"]?.ToString() ?? string.Empty,
                        COUNTRY = reader["COUNTRY"]?.ToString() ?? string.Empty,
                        STREET = reader["STREET"]?.ToString() ?? string.Empty,
                        TAX_NO_1 = reader["TAX_NO_1"]?.ToString() ?? string.Empty,
                        TAX_NO_2 = reader["TAX_NO_2"]?.ToString() ?? string.Empty,
                        TAX_NO_3 = reader["TAX_NO_3"]?.ToString() ?? string.Empty,
                        TAX_NO_4 = reader["TAX_NO_4"]?.ToString() ?? string.Empty
                    };
                }

                if (await reader.NextResultAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        itemList.Add(new ItemReceipt
                        {
                            DOC_NO = reader["DOC_NO"]?.ToString() ?? string.Empty,

                            // GL section
                            GL_ITEMNO_ACC = reader["GL_ITEMNO_ACC"]?.ToString() ?? string.Empty,
                            GL_ACCOUNT = reader["GL_ACCOUNT"]?.ToString() ?? string.Empty,
                            GL_ITEM_TEXT = reader["GL_ITEM_TEXT"]?.ToString() ?? string.Empty,
                            GL_TAX_CODE = reader["GL_TAX_CODE"]?.ToString() ?? string.Empty,
                            GL_REF_KEY_1 = reader["GL_REF_KEY_1"]?.ToString() ?? string.Empty,
                            GL_REF_KEY_2 = reader["GL_REF_KEY_2"]?.ToString() ?? string.Empty,
                            GL_REF_KEY_3 = reader["GL_REF_KEY_3"]?.ToString() ?? string.Empty,
                            GL_PROFIT_CTR = reader["GL_PROFIT_CTR"]?.ToString() ?? string.Empty,
                            GL_COSTCENTER = reader["GL_COSTCENTER"]?.ToString() ?? string.Empty,
                            GL_DT_CT_INDICATOR = reader["GL_DT_CT_INDICATOR"]?.ToString() ?? string.Empty,
                            GL_AMT_DOCCUR = reader["GL_AMT_DOCCUR"]?.ToString() ?? string.Empty,

                            // Customer section
                            CUST_ITEMNO_ACC = reader["CUST_ITEMNO_ACC"]?.ToString() ?? string.Empty,
                            CUSTOMER = reader["CUSTOMER"]?.ToString() ?? string.Empty,
                            RECON_GL_ACCOUNT = reader["RECON_GL_ACCOUNT"]?.ToString() ?? string.Empty,
                            CUST_REF_KEY_1 = reader["CUST_REF_KEY_1"]?.ToString() ?? string.Empty,
                            CUST_REF_KEY_2 = reader["CUST_REF_KEY_2"]?.ToString() ?? string.Empty,
                            CUST_REF_KEY_3 = reader["CUST_REF_KEY_3"]?.ToString() ?? string.Empty,
                            CUST_SP_GL_IND = reader["CUST_SP_GL_IND"]?.ToString() ?? string.Empty,
                            CUST_ALLOC_NMBR = reader["CUST_ALLOC_NMBR"]?.ToString() ?? string.Empty,
                            CUST_BUSINESSPLACE = reader["CUST_BUSINESSPLACE"]?.ToString() ?? string.Empty,
                            CUST_SECTIONCODE = reader["CUST_SECTIONCODE"]?.ToString() ?? string.Empty,
                            CUST_AMT_DOCCUR = reader["CUST_AMT_DOCCUR"]?.ToString() ?? string.Empty,
                            CUST_PROFIT_CTR = reader["CUST_PROFIT_CTR"]?.ToString() ?? string.Empty,
                            CUST_PAYMT_REF = reader["CUST_PAYMT_REF"]?.ToString() ?? string.Empty
                        });
                    }
                }

                model.REQUEST.Add(new Request2
                {
                    HEADER = header!,
                    ITEM = itemList
                });

                ResponseCWCapiReceipt? sapResponse;
                try
                {
                    sapResponse = await PostReceiptToCWCAsync(model);
                }
                catch (Exception ex)
                {
                    response.Response = $"Error posting to CWC API: {ex.Message}";
                    return response;
                }

                if (sapResponse?.Response != null)
                {
                    try
                    {
                        await using var saveConn = _dbContext.Database.GetDbConnection();
                        await using var saveCmd = saveConn.CreateCommand();

                        saveCmd.CommandText = "InsertSapResponse";
                        saveCmd.CommandType = CommandType.StoredProcedure;

                        saveCmd.Parameters.Add(new SqlParameter("@InvoiceId", cashReceiptId));
                        saveCmd.Parameters.Add(new SqlParameter("@InvoiceNo", request.inReceiptNo ?? string.Empty));
                        saveCmd.Parameters.Add(new SqlParameter("@SAP_DOC_NUMBER", sapResponse.Response.SAPDocNumber ?? string.Empty));
                        saveCmd.Parameters.Add(new SqlParameter("@REF_DOC_NO", sapResponse.Response.RefDocNo ?? string.Empty));
                        saveCmd.Parameters.Add(new SqlParameter("@STATUS", sapResponse.Response.Status ?? string.Empty));
                        saveCmd.Parameters.Add(new SqlParameter("@REMARK", sapResponse.Response.Remark ?? string.Empty));
                        saveCmd.Parameters.Add(new SqlParameter("@CreatedBy", 0));
                        saveCmd.Parameters.Add(new SqlParameter("@Module", "CWC"));

                        await saveConn.OpenAsync();
                        try
                        {
                            await saveCmd.ExecuteNonQueryAsync();
                        }
                        finally
                        {
                            if (saveConn.State != ConnectionState.Closed)
                                await saveConn.CloseAsync();
                        }

                        if (!string.IsNullOrEmpty(sapResponse.Response.Status))
                        {
                            await using var updConn = _dbContext.Database.GetDbConnection();
                            await using var updCmd = updConn.CreateCommand();

                            updCmd.CommandText = "UpdateCashReceiptSAPStatus";
                            updCmd.CommandType = CommandType.StoredProcedure;

                            updCmd.Parameters.Add(new SqlParameter("@CashReceiptId", cashReceiptId));
                            updCmd.Parameters.Add(new SqlParameter("@SAP_DOC_NUMBER", sapResponse.Response.SAPDocNumber ?? string.Empty));

                            await updConn.OpenAsync();
                            try
                            {
                                await updCmd.ExecuteNonQueryAsync();
                            }
                            finally
                            {
                                if (updConn.State != ConnectionState.Closed)
                                    await updConn.CloseAsync();
                            }
                        }

                        response.Response = "success";
                        return response;
                    }
                    catch (Exception ex)
                    {
                        response.Response = $"Error saving SAP response: {ex.Message}";
                        return response;
                    }
                }

                response.Response = "No response from CWC API";
                return response;
            }
            catch (Exception ex)
            {
                response.Response = $"General error: {ex.Message}";
                return response;
            }
        }

        public async Task<ResponseCWCapiReceipt> PostReceiptToCWCAsync(RequestCWCapiReceipt request)
        {
            try
            {
                string url = _configuration["CWCApi:CustomerReceiptUrl"];
                string user = _configuration["CWCApi:UserId"];
                string pwd = _configuration["CWCApi:Password"];

                string json = JsonSerializer.Serialize(request);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                string credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{user}:{pwd}"));
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);

                var httpResponse = await _httpClient.PostAsync(url, content);
                var responseJson = await httpResponse.Content.ReadAsStringAsync();

                if (!httpResponse.IsSuccessStatusCode)
                {
                    throw new Exception($"CWC API returned error: {httpResponse.StatusCode} - {responseJson}");
                }

                var result = JsonSerializer.Deserialize<ResponseCWCapiReceipt>(responseJson, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while calling CWC API: {ex.Message}", ex);
            }
        }

    }
}