namespace blocked_countries.Errors
{
    public class ApiResponse
    {
        #nullable disable
        public int StatusCode { get; set; }
        public string ErrorMessage { get; set; }
        
        public ApiResponse(int statusCode,string errorMessage=null) { 
            StatusCode=statusCode;
            ErrorMessage=errorMessage??GetMessageFromStatus(statusCode);
        }
         
        public string GetMessageFromStatus(int statusCode) {
            return statusCode switch
            {
                400 => "BadRequst",
                401 => "Not Authorized",
                404 => "NotFound",
                500 => "Server Request ",
                _ =>null
            };
        }
    }
}
