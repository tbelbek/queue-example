namespace ReportingWith_Queue
{
    public class ResponseModel : IDataObject, IResponse
    {
        public object RequestModel { get; set; }
        public bool SuccessStatus { get; set; }
        public string ErrorMessage { get; set; }
    }
}