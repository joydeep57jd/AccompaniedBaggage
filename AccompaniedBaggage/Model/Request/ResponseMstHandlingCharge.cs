namespace AccompaniedBaggage.Model.Request
{
    public class ResponseMstHandlingCharge
    {
        public int HandlingChargeId { get; set; }
        public string? Category { get; set; }
        public decimal? Amount { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public string? SacCode { get; set; }

        public string? StorageType { get; set; }
    }
}
