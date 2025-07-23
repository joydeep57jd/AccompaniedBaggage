namespace AccompaniedBaggage.Model.Request
{
    public class RequestBaggageClaim
    {
        public int Claim_id { get; set; } = 0;  
        public string? Claim_no { get; set; } 
        public DateTime? Claim_date { get; set; }
        public decimal? Party_id { get; set; }
        public string? Party_name { get; set; }
        public string? Passport_no { get; set; }
        public string? Name { get; set; }
        public string? AOC_DR_no { get; set; }
        public int? Receive_id { get; set; }
        public string? Baggage_receipt_no { get; set; }
        public DateTime? Baggage_receipt_date { get; set; }
        public string? Remarks { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
