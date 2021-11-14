using System;

namespace Banks.Tools
{
    public class CentralBankException : Exception
    {
        public CentralBankException()
        {
        }

        public CentralBankException(string message)
            : base(message)
        {
        }

        public CentralBankException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}