namespace AccompaniedBaggage.Model.Response
{
    public class ResponseMstStorageCharge
    {
        public int ChargeId { get; set; }
        public string? Category { get; set; }
        public decimal? Amount { get; set; }
        public int FromDay { get; set; }
        public int ToDay { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
