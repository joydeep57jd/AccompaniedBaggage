namespace AccompaniedBaggage.Model.Response
{
    public class UnclaimedReceiptDto
    {
        public int? ReceiptId { get; set; }
        public string? ReceiptNo { get; set; }
        public string? From { get; set; }
        public string? DrNo { get; set; }
        public string? AocNo { get; set; }
        public DateTime? DateOfLanding { get; set; }
        public string? PassportNo { get; set; }
        public string? FlightNo { get; set; }
        public bool? IsSealed { get; set; }
        public string? Remarks { get; set; }
        public string? ReasonForDetention { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }

        public DateTime? ReceiptDate { get; set; }
    }
}
