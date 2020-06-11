using System;

namespace Urly.Domain
{
    public class Link
    {
        public Link(string fullUrl)
        {
            FullUrl = fullUrl;
            Created = DateTime.UtcNow;
        }

        public int Id { get; private set; }

        public string FullUrl { get; private set; }

        public DateTime Created { get; private set; }
    }
}
