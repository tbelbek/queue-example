namespace ReportingWith_Queue
{
    public class ResponseHelper : IResponseHelper
    {
        public ResponseModel ResponseGenerator(object requestData, bool success, string errorMessage = "")
        {
            return new ResponseModel()
            {
                ErrorMessage = errorMessage,
                SuccessStatus = success,
                RequestModel = requestData
            };
        }
    }
}