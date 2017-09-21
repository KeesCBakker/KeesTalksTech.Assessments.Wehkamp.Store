using System;

namespace KeesTalksTech.Assessments.Wehkamp.Store.Shopping
{
    public class BasketServiceException : Exception
    {
        public BasketServiceException()
        {
        }

        public BasketServiceException(string message) : base(message)
        {
        }

        public BasketServiceException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
