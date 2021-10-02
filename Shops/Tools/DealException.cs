using System;

namespace Shops.Tools
{
    public class DealException : Exception
    {
        public DealException()
        {
        }

        public DealException(string message)
            : base(message)
        {
        }

        public DealException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}