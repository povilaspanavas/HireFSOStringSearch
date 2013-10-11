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
        private StringFilter _stringFilter = new StringFilter();

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void TestNullParameter()
        {
            _stringFilter.Filter(null);
        }

        [Test]
        public void TestEmptyList()
        {
            Assert.AreEqual(0, _stringFilter.Filter(new List<string>()).Count);
        }
    }
}
