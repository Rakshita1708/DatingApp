namespace API.Errors
{
    public class ApiException
    {
        public ApiException(int statusCode, string message,string details)
        {
            StatusCode = statusCode;
            Message = message;
            Details = details;
        }
        //this is going to contain response of what we are going to sendback to the client when we have
        public int StatusCode { get; set; }
        public string Message { get; set; }
    
        public string  Details { get; set; }
    
    }

}