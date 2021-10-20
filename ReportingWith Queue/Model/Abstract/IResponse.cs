namespace ReportingWith_Queue
{
    public interface IResponse
    {
        public bool SuccessStatus { get; set; }
        public string ErrorMessage { get; set; }
    }
}