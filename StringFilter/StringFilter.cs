using System;
using System.Collections.Generic;
using System.Linq;

namespace StringFilter
{
    public class StringFilter
    {
        /// <summary>
        /// The assignment wasn't clear enough as most often happens with real requirements.
        /// Firstly, "NUnit" is third party library in my point of view (open source library)
        /// and shouldn't be included. Strainge, isn't it?.. So, here is the link
        /// with included NUnit where you could just launch solution, compile and run tests:
        /// git clone https://github.com/povilaspanavas/HireFSOStringSearch.git
        /// Secondly, there isn't data about incoming string lists. For example, if there
        /// is lots of small strings, but small count of strings with length of 6, we
        /// could do different search (cycle trough only strings of length 6 ant try to 
        /// disassemble them, then search for disassembled parts).
        /// If that would be too slow, we could have dictionaries, where key would be first
        /// letter, and value hashset containing all the words starting with that letter.
        /// Thirdly, no mentioning if data is case sensitive? Current implementation is,
        /// but it's easy to change.
        /// </summary>
        /// <param name="strList"></param>
        /// <returns></returns>
        public List<string> Filter(List<string> strList)
        {
            if (strList == null)
                throw new ArgumentNullException("strList");
            if (strList.Count == 0)
                return new List<string>();
            var hashSet = new HashSet<string>(); // list.Contain is slow for large lists
            var resultHashSet = new HashSet<string>(); // results
            foreach (var str in strList)
            {
                if (str.Length != 6) // the less data the better
                    continue;
                hashSet.Add(str); 
            }
            // check A + B, A + C, A + D...
            // later check B + C, B + D, B + E
            for (int i = 0; i < strList.Count; i++)
            {
                if (strList[i].Length >= 6) // technically it wouldn't be two strings if the first is length of 6
                    continue;
                for (int j = i + 1; j < strList.Count; j++)
                {
                    // A + B
                    var concatStr = strList[i] + strList[j];
                    if (concatStr.Length != 6) // we are limited to 6 symbols
                        continue;
                    if (IsFit(concatStr, hashSet, resultHashSet))
                        resultHashSet.Add(concatStr);
                    
                    // B + A
                    concatStr = strList[j] + strList[i];
                    if (IsFit(concatStr, hashSet, resultHashSet))
                        resultHashSet.Add(concatStr);
                }
            }
            return resultHashSet.ToList();
        }

        private bool IsFit(string str, ICollection<string> hashSet, ICollection<string> resultHashSet)
        {
            return hashSet.Contains(str) && resultHashSet.Contains(str) == false;
        }
    }
}
