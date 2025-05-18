namespace blocked_countries.Errors
{
    public class ApiExceptionResponse:ApiResponse
    {
        #nullable disable
        public string Details { get; set; }
        public ApiExceptionResponse(int statusCode,string message=null,string details=null):
            base(statusCode,message)
        {

        }
    }
}
