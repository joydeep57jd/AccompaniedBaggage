using System.Text.Json.Serialization;

namespace AccompaniedBaggage.Model.Response
{
    public class CWCresponse
    {
    }
    public class ResponseCWCapi
    {
        [JsonPropertyName("RESPONSE1")]
        public Response1 Response1 { get; set; }
    }

    public class ResponseCWCapiReceipt
    {
        [JsonPropertyName("RESPONSE")]
        public Response1 Response { get; set; }
    }

    public class Response1
    {
        [JsonPropertyName("SAP_DOC_NUMBER")]
        public string SAPDocNumber { get; set; }

        [JsonPropertyName("REF_DOC_NO")]
        public string RefDocNo { get; set; }

        [JsonPropertyName("STATUS")]
        public string Status { get; set; }

        [JsonPropertyName("REMARK")]
        public string Remark { get; set; }
    }


    public class GetInvoiceDtlforSAPRequest
    {
        public string InvoiceNo { get; set; }
        public int IsIRN { get; set; }
    }

    public class GetCashReceiptDtlforSAPRequest
    {
        public string inReceiptNo { get; set; }
        public int IsIRN { get; set; }
        //public bool YardInvoice { get; set; }
    }
}
