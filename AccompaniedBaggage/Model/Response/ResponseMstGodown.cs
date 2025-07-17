namespace AccompaniedBaggage.Model.Response
{
    public class ResponseMstGodown
    {
        public int GodownId { get; set; }
        public string? GodownName { get; set; }
        public string? LocationAlias { get; set; }

        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
