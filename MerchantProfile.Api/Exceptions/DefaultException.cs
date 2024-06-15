using MerchantProfile.Api.Models.Enums;

namespace MerchantProfile.Api.Exceptions
{
    public class DefaultException : Exception
    {
        public string Status { get; private set; }
        public string ErrorMessage { get; private set; }

        // Constructor với status và message
        public DefaultException(string status, string message) : base(message)
        {
            Status = status;
            ErrorMessage = message;
        }

        // Constructor với APIStatus
        public DefaultException(APIStatus apiStatus) : base(apiStatus.Message)
        {
            Status = apiStatus.Status;
            ErrorMessage = apiStatus.Message;
        }
    }
}
