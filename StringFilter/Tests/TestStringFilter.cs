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
            Test(1, new List<string> { "aaabbb", "aaa", "bbb" }, "aaabbb");
        }


        [Test]
        public void Test1xItemFitDifferentSide()
        {
            Test(1, new List<string> { "bbbaaa", "aaa", "bbb" }, "bbbaaa");
        }

        [Test]
        public void Test2xItemFit()
        {
            Test(2, new List<string> { "aaabbb", "aaa", "bbb", "123456", "456", "123" },
                "aaabbb", "123456");
        }

        [Test]
        public void Test1xItemFitDifferentStringLength()
        {
            Test(1, new List<string> { "123456", "1", "23456" }, "123456");
        }

        [Test]
        public void TestFitsOnlyOfLength6()
        {
            Test(0, new List<string> { "1234567", "123", "4567" });
        }

        [Test]
        public void TestTwoTheSameStringsOfLength3()
        {
            Test(1, new List<string> { "123456", "123", "456", "123", "456" }, "123456");
        }

        [Test]
        public void TestManyEqualStrings()
        {
            Test(1, new List<string> { "123456", "123", "456", "123", "456", "456", "123456"},
                "123456");
        }

        [Test]
        public void Test2xItemFitCaseInsensitive()
        {
            Test(1, new List<string> { "AAaBBb", "aAa", "Bbb" }, "aaabbb");
        }

        [Test]
        public void Test1xItemFitCaseSensitive()
        {
            var result = new StringFilter(true).Filter(new List<string> { "AAaBBb", "AAa", "BBb" });
            Assert.AreEqual(1, result.Count);
            Assert.IsTrue(result.Contains("AAaBBb"));
        }

        [Test]
        public void Test0xItemFitCaseSensitive()
        {
            var result = new StringFilter(true).Filter(new List<string> { "aaabbb", "aAa", "Bbb" });
            Assert.AreEqual(0, result.Count);
        }

        [Test]
        public void Test0xItemFitTwoStringsOfLength6()
        {
            var result = _stringFilter.Filter(new List<string> { "123456", "123456" });
            Assert.AreEqual(0, result.Count);
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expectedCount">How much elements should be in the result list</param>
        /// <param name="strList">Strings list for a search</param>
        /// <param name="mustContainStrings">List of strings, which should be found</param>
        public void Test(int expectedCount, List<string> strList, params string[] mustContainStrings)
        {
            var result = _stringFilter.Filter(strList);
            Assert.AreEqual(expectedCount, result.Count);

            foreach (var containString in mustContainStrings)
            {
                Assert.IsTrue(result.Contains(containString),
                    string.Format("The list doesn't contain string {0}", containString));                
            }
        }
    }
}
