using System;

namespace ValetaxTestTree.Application.Exceptions
{
    public class SecureException : Exception
    {
        public SecureException(string message) : base(message) { }
    }
}
