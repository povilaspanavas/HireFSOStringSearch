using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace StringFilter.Tests
{
    [TestFixture]
    public class TestStringFilter
    {
        private readonly StringFilter _stringFilter = new StringFilter();

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void TestNullParameter()
        {
            _stringFilter.Filter(null);
        }

        [Test]
        public void TestEmptyList()
        {
            Test(0, new List<string>());
        }

        [Test]
        public void TestOneItem()
        {
            Test(0, new List<string> {"asadfd"});
        }

        public void Test(int expectedCount, List<string> strList)
        {
            Assert.AreEqual(expectedCount, _stringFilter.Filter(strList).Count);
        }
    }
}
