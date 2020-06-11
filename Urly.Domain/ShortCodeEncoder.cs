using System.Text;

namespace Urly.Domain
{
    public class ShortCodeEncoder
    {
        public string Encode(int number)
        {
            StringBuilder builder = new StringBuilder();

            int value = number;
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
