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
            var hashSet = new HashSet<string>(); // lists are slow for lots of data
            var resultHashSet = new HashSet<string>(); // results
            foreach (var str in strList)
            {
                hashSet.Add(str);
            }
            for (int i = 0; i < strList.Count; i++)
            {
                if (strList[i].Length >= 6) // technically it wouldn't be two strings if the first is length of 6
                    continue;

                for (int j = 1; j < strList.Count; j++)
                {
                    var concatStr= strList[i] + strList[j];
                    if (concatStr.Length > 6) // we are limited to 6 symbols
                        continue;
                    if (hashSet.Contains(concatStr) && resultHashSet.Contains(concatStr) == false)
                        resultHashSet.Add(concatStr);
                }
            }
            return new List<string>(hashSet);
        }
    }
}
