namespace MerchantProfile.Api.Models.Enums
{
    public class APIStatus
    {
        public static readonly APIStatus SERVER_ERROR = new APIStatus("SERVER_ERROR", "Server errors !");

        public static readonly APIStatus CLIENT_ERROR = new APIStatus("CLIENT_ERROR", "Client errors !");

        public static readonly APIStatus ORDER_NOT_FOUND = new APIStatus("ORDER_NOT_FOUND", "Order not found");

        public static readonly APIStatus USER_INVALID = new APIStatus("USER_INVALID", "User invalid");

        public static readonly APIStatus USER_NOT_FOUND = new APIStatus("USER_NOT_FOUND", "User not found");

        public static readonly APIStatus EVENT_NOT_FOUND = new APIStatus("EVENT_NOT_FOUND", "Event not found");

        public static readonly APIStatus EVENT_OUT_OF_STOCK = new APIStatus("EVENT_OUT_OF_STOCK", "Event is out of stock");

        public static readonly APIStatus EVENT_INVALID = new APIStatus("EVENT_INVALID", "Event is unavailable because of status invalid or expired for event");

        public string Status { get; }
        public string Message { get; }

        private APIStatus(string status, string message)
        {
            Status = status;
            Message = message;
        }
    }
}
