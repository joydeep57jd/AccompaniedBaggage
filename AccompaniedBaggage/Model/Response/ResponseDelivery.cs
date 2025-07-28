namespace AccompaniedBaggage.Model.Response
{
    public class ResponseDelivery
    {
        public int DeliveryId { get; set; }
        public int ReceiptId { get; set; }
        public string? ReceiptNo { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public TimeSpan? DeliveryTime { get; set; }
        public string? CustomOfficeId { get; set; }
        public string? Remarks { get; set; }

        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

    }
}
