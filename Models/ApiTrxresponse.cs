namespace SchoolRegistrationForm.Models
{
    public class ApiTrxresponse
    {
        public string MerchantTransactionId { get; set; }
        public string MemberContactNumber { get; set; }
        public int Status { get; set; }
        public string Remarks { get; set; }
        public string TrackerId { get; set; }
        public string OrderId { get; set; }
        public string Message { get; set; }
        public string responseMessage { get; set; }
        public string Details { get; set; }
        public int ReponseCode { get; set; }
        public bool status { get; set; }
        public string ios_version { get; set; }
        public string AndroidVersion { get; set; }
    }
}
