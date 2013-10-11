using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringFilter
{
    public class StringFilter
    {
        public List<string> Filter(List<string> strList)
        {
            if (strList == null)
                throw new ArgumentNullException("strList");
            if (strList.Count == 0)
                return new List<string>();
            var hashSet = new HashSet<string>(); // lists are slow for lots of data
            var resultHashSet = new HashSet<string>(); // results
            foreach (var str in strList)
            {
                if (str.Length != 6) // the less data the better
                    continue;
                hashSet.Add(str); 
            }
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
