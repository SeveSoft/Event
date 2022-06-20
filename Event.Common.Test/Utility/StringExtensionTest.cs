using Event.Common.Utility;
using NUnit.Framework;

namespace Event.Common.Test.Utility
{
    public class StringExtensionTest
    {
        [Test]
        public void TestMd5Hash()
        {
            var expected = "2bdb9e0e0220c27394afda6a1a105eee";
            var md5Hash = "Hallo World".ToMd5Hash();
            Assert.AreEqual(expected, md5Hash, "Hash not the same");
        }
    }
}