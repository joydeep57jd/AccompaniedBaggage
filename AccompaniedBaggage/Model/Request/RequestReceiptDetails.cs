namespace AccompaniedBaggage.Model.Request
{
    public class RequestReceiptDetails
    {
        public int ReceiptId { get; set; } // For Edit, 0 for Add
        public DateTime? ReceiptDate { get; set; }
        public string? From { get; set; } // (A)irline or (C)ustom
        public string? DrNo { get; set; }
        public string? AocNo { get; set; }
        public DateTime? DateOfLanding { get; set; }
        public string? PassportNo { get; set; }
        public string? FlightNo { get; set; }
        public string? IsSealed { get; set; } // 'Y' or 'N'
        public string? Remarks { get; set; }
        public string? ReasonForDetention { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
