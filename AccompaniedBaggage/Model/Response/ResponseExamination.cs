namespace AccompaniedBaggage.Model.Response
{
    public class ResponseExamination
    {
        public int Examination_id { get; set; }
        public int? Receive_id { get; set; }
        public DateTime? Examination_date { get; set; }
        public string? Colour { get; set; }
        public string? Colour_code { get; set; }
        public decimal? Weight_kg { get; set; }
        public string? Size { get; set; }
        public decimal? Length { get; set; }
        public decimal? Width { get; set; }
        public decimal? Height { get; set; }
        public string? Cargo_type { get; set; }
        public string? Stored_location { get; set; }
        public string? Remarks { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
