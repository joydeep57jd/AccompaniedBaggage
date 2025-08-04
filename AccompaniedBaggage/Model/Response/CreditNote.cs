namespace AccompaniedBaggage.Model.Response
{
    public class CreditNote
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

        public bool? IsYard { get; set; }
        public bool? IsImport { get; set; }

        public string? SAP_DOC_NUMBER { get; set; } 

        public int? IsSAP { get; set; }

        public decimal? Taxable_Amt { get; set; }
        public decimal? IGST_Amt { get; set; }
        public decimal? CGST_Amt { get; set; }
        public decimal? SGST_Amt { get; set; }
        public decimal? Total_Amt { get; set; }
        public decimal? Roundoff_Amt { get; set; }
        public decimal? Net_Amt { get; set; }
    }
}
