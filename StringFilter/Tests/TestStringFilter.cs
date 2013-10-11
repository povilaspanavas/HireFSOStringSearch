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
        public void Test1xItem()
        {
            Test(0, new List<string> {"asadfd"});
        }

        [Test]
        public void Test2xItem()
        {
            Test(0, new List<string> { "asadfd", "sad" });
        }

        [Test]
        public void Test1xItemFit()
        {
            Test(1, new List<string> { "aaabbb", "aaa", "bbb" });
        }

        [Test]
        public void Test1xItemFitDifferentSide()
        {
            Test(1, new List<string> { "bbbaaa", "aaa", "bbb" });
        }

        [Test]
        public void Test2xItemFit()
        {
            Test(2, new List<string> { "aaabbb", "aaa", "bbb", "123456", "456", "123" });
        }

        public void Test(int expectedCount, List<string> strList)
        {
            Assert.AreEqual(expectedCount, _stringFilter.Filter(strList).Count);
        }
    }
}
