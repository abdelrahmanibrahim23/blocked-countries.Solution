﻿namespace blocked_countries.Errors
{
    public class ApiValidationErrorResponse:ApiResponse
    {
        public IEnumerable<string> Errors { get; set; }=new List<string>();
        public ApiValidationErrorResponse():base(400)
        {
            
        }
    }
}
