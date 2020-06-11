using Microsoft.VisualStudio.TestTools.UnitTesting;
using Urly.Domain;

namespace Urly.UnitTests.Domain
{
    [TestClass]
    public class ShortCodeEncoderTest
    {
        [DataTestMethod]
        [DataRow(0, "")]
        [DataRow(1, "b")]
        [DataRow(5, "f")]
        [DataRow(26, "A")]
        [DataRow(100, "bM")]
        [DataRow(1000, "qi")]
        [DataRow(3850, "bag")]
        [DataRow(235000, "9iu")]
        public void EncodeIsCorrect(int number, string expected)
        {
            var encoder = new ShortCodeEncoder();

            string result = encoder.Encode(number);

            Assert.AreEqual(expected, result);
        }

        [DataTestMethod]
        [DataRow("", 0)]
        [DataRow("b", 1)]
        [DataRow("f", 5)]
        [DataRow("A", 26)]
        [DataRow("bM", 100)]
        [DataRow("qi", 1000)]
        [DataRow("bag", 3850)]
        [DataRow("9iu", 235000)]
        public void DecodeIsCorrect(string code, int expected)
        {
            var encoder = new ShortCodeEncoder();

            int result = encoder.Decode(code);

            Assert.AreEqual(expected, result);
        }
    }
}
