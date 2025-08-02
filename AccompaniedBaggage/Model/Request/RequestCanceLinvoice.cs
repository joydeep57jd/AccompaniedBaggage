namespace AccompaniedBaggage.Model.Request
{
    public class RequestCanceLinvoice
    {
        public int? Id { get; set; } 
        public int? invId { get; set; }
        public string? InvoiceNo { get; set; }
        public string? Remarks { get; set; }
        public string? cancelReason { get; set; }
        public DateTime? CancelledDate { get; set; }
        public string? Amount { get; set; }
    }
}
