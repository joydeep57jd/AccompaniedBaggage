namespace AccompaniedBaggage.Model.Request
{
    public class RequestMstEximTraderMaster
    {
        public int TraderId { get; set; }
        public string? OperationType { get; set; }
        public string? EximTraderName { get; set; }
        public string? EximTraderAlias { get; set; }
        public string? Address { get; set; }
        public string? CountryName { get; set; }
        public string? StateName { get; set; }
        public string? CityName { get; set; }
        public string? Pincode { get; set; }
        public string? PhoneNo { get; set; }
        public string? FaxNo { get; set; }
        public string? ContactPerson { get; set; }
        public string? EmailId { get; set; }
        public string? MobileNo { get; set; }
        public string? PAN { get; set; }
        public string? AadhaarNo { get; set; }
        public string? GSTNo { get; set; }
        public string? TAN { get; set; }
        public string? SapCustomerNo { get; set; }
        public string? PartyCode { get; set; }
        public int? CountryId { get; set; }
        public int? StateId { get; set; }
        public string? StateCode { get; set; }
        public bool? isImporter { get; set; }
        public bool? isExporter { get; set; }
        public bool? isShipline { get; set; }
        public bool? isCHA { get; set; }
        public bool? IsForWarder { get; set; }
        public bool? isRent { get; set; }
        public bool? isBidder { get; set; }
    }
}
