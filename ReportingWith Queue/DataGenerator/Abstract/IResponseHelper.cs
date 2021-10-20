namespace ReportingWith_Queue
{
    public interface IResponseHelper
    {
        ResponseModel ResponseGenerator(object requestData, bool success, string errorMessage = "");
    }
}