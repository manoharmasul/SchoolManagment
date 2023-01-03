using Microsoft.AspNetCore.Mvc;

namespace SchoolManagment.Models
{
    public class BaseResponseStatus
    {
        /// <summary>
        /// Status Code for the response
        /// </summary>
        public string StatusCode { get; set; }

        /// <summary>
        /// Status Message for the response
        /// </summary>
        public string StatusMessage { get; set; }

        /// <summary>
        /// Response Data to be shared
        /// </summary>
        public object ResponseData { get; set; }
        public object ResponseData1 { get; set; }
        public object ResponseData2 { get; set; }

        public static implicit operator BaseResponseStatus(int v)
        {
            throw new NotImplementedException();
        }

        public static explicit operator BaseResponseStatus(ActionResult v)
        {
            throw new NotImplementedException();
        }
    }
}

