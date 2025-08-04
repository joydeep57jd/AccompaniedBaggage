using System.ComponentModel.DataAnnotations;

namespace AccompaniedBaggage.Model.Request
{
    public class RequestCreditNote
    {
        public int CreditNoteId { get; set; }
        public bool? TaxInvoice { get; set; }
        public bool? BillOfSupply { get; set; }
        public string? InvoiceNo { get; set; }
        public DateTime? CreditNoteDate { get; set; }
        public string? CreditNoteNo { get; set; }
        public int? PartyId { get; set; }
        public int? PayeeId { get; set; }
        public string? GSTNo { get; set; }
        public string? PlaceOfSupply { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? Remarks { get; set; }
        public string? SAP_DOC_NUMBER { get; set; }
        public int IsSAP { get; set; }


        public List<CreditNoteDetail> CreditNoteDetailList { get; set; } = new List<CreditNoteDetail>();
    }

    public class CreditNoteDetail
    {
        [Key]
        public int CreditNoteDetailId { get; set; }

        public int ChargesTypeId { get; set; }     
        public int CreditNoteId { get; set; }      

        public string? ChargeType { get; set; }
        public string? ChargeName { get; set; }
        public string? SACCode { get; set; }

        public int? Quantity { get; set; }

        public decimal? Rate { get; set; }
        public decimal? Inv_Amount { get; set; }
        public decimal? Taxable { get; set; }

        public decimal? IGSTPer { get; set; }
        public decimal? IGSTAmt { get; set; }

        public decimal? CGSTPer { get; set; }
        public decimal? CGSTAmt { get; set; }

        public decimal? SGSTPer { get; set; }
        public decimal? SGSTAmt { get; set; }

        public decimal? Total { get; set; }

        public bool IsActive { get; set; } 
    }
}
