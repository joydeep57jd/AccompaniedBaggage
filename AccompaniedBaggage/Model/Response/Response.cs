using System.ComponentModel.DataAnnotations;

namespace SezApi.Model.Response
{
    public class Response<T>
    { 
        public bool Status { get; set; }     
        public T Data { get; set; }
        public string Message { get; set; }

        public int TotalCount { get; set; }
    }
    public class ResponseCustom
    {
        [Key]
        public string? Response { get; set; }  // "OK" or "NOT OK"
        public int? Id { get; set; }
        public string? ErrorMessage { get; set; }  // Optional if included in SP
    }
}
