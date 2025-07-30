namespace AccompaniedBaggage.Model.Response
{
    public class StockRegisterReportRow
    {
        public string? ReceiptNo { get; set; }
        public DateTime? ReceiptDate { get; set; }
        public string? From { get; set; }
        public string? FlightNo { get; set; }
        public DateTime? DateOfLanding { get; set; }
        public string? AocNo { get; set; }
        public string? PassportNo { get; set; }
        public string? Colour { get; set; }
        public string? ColourCode { get; set; }
        public decimal? WeightKg { get; set; }
        public string? Size { get; set; }
        public string? StoredLocation { get; set; }
        public decimal? Length { get; set; }
        public decimal? Height { get; set; }
        public DateTime? ExaminationDate { get; set; }
    }
}
