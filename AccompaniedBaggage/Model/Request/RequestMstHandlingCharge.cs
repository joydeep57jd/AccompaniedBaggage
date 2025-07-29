namespace AccompaniedBaggage.Model.Request
{
    public class RequestMstHandlingCharge
    {
        public int HandlingChargeId { get; set; }
        public string? Category { get; set; }
        public decimal? Amount { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public string? SacCode { get; set; }
    }
}
