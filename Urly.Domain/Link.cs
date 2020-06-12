using System;
using Urly.Domain.Exceptions;

namespace Urly.Domain
{
    public class Link
    {
        public Link(string fullUrl)
        {
            if (string.IsNullOrEmpty(fullUrl))
            {
                throw new InvalidOperationDomainException("Invalid Full URL.");
            }

            if (fullUrl.Length > 500)
            {
                throw new InvalidOperationDomainException("Full URL too long.");
            }

            FullUrl = fullUrl;
            Created = DateTime.UtcNow;
        }

        public int Id { get; private set; }

        public string FullUrl { get; private set; }

        public DateTime Created { get; private set; }
    }
}
