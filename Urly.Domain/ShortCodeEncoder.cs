using System.Text;
using Urly.Domain.Exceptions;

namespace Urly.Domain
{
    public class ShortCodeEncoder
    {
        public string Encode(int id)
        {
            if (id < 0)
            {
                throw new InvalidOperationDomainException("Invalid ID.");
            }

            StringBuilder builder = new StringBuilder();

            int value = id;
            while (value > 0)
            {
                char c = Alphabet[value % AlphabetSize];
                builder.Insert(0, c);
                value /= AlphabetSize;
            }

            return builder.ToString();
        }

        public int Decode(string code)
        {
            if (code is null)
            {
                throw new InvalidOperationDomainException("Invalid Code.");
            }

            int result = 0;
            foreach (char c in code)
            {
                result = (result * AlphabetSize) + Alphabet.IndexOf(c);
            }

            return result;
        }

        private static readonly string Alphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private static readonly int AlphabetSize = Alphabet.Length;
    }
}
