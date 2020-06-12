﻿namespace Urly.Domain.Exceptions
{
    public class InvalidOperationDomainException : DomainException
    {
        public InvalidOperationDomainException(string message)
            : base(message)
        {
        }
    }
}
