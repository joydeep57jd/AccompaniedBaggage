namespace AccompaniedBaggage.Model.Request
{
    public class RequestMstGodown
    {
        public int GodownId { get; set; }
        public string? GodownName { get; set; }
        public string? LocationAlias { get; set; }

        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
