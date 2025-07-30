namespace AccompaniedBaggage.Model.Response
{
    public class ResponseDeliveryReport
    {
        public int? DeliveryId { get; set; }
        public int? ReceiptId { get; set; }
        public string? ReceiptNo { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public string? CustomOfficeId { get; set; }
        public string? Remarks { get; set; }
        public string? DeliveryTime { get; set; }

        public string? BaggageReceiptNo { get; set; }
        public DateTime? BaggageReceiptDate { get; set; }
        public string? BaggageFrom { get; set; }
    }
}
