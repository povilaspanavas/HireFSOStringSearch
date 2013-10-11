using System;
using System.Collections.Generic;
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

        [Test]
        public void Test1xItemFitDifferentStringLength()
        {
            Test(1, new List<string> { "123456", "1", "23456" });
        }

        [Test]
        public void TestFitsOnlyOfLength6()
        {
            Test(0, new List<string> { "1234567", "123", "4567" });
        }

        [Test]
        public void TestTwoTheSameStringsOfLength3()
        {
            Test(1, new List<string> { "123456", "123", "456", "123", "456" });
        }

        [Test]
        public void TestManyEqualStrings()
        {
            Test(1, new List<string> { "123456", "123", "456", "123", "456", "456", "123456" });
        }

        [Test]
        public void TestFromAssignment()
        {
            var result = _stringFilter.Filter(new List<string> { "al", "albums", "aver", "bar", "barely", 
                "be", "befoul", "bums", "by", "cat", "con", "convex", "ely", "foul", "here", 
                "hereby", "jig", "jigsaw", "or", "saw", "tail", "tailor", "vex", "we", "weaver"});
            // Test count
            Assert.AreEqual(8, result.Count);

            // Test values
            Assert.IsTrue(result.Contains("albums"));
            Assert.IsTrue(result.Contains("barely"));
            Assert.IsTrue(result.Contains("befoul"));
            Assert.IsTrue(result.Contains("convex"));
            Assert.IsTrue(result.Contains("hereby"));
            Assert.IsTrue(result.Contains("jigsaw"));
            Assert.IsTrue(result.Contains("tailor"));
            Assert.IsTrue(result.Contains("weaver"));
        }

        public void Test(int expectedCount, List<string> strList)
        {
            Assert.AreEqual(expectedCount, _stringFilter.Filter(strList).Count);
        }
    }
}
