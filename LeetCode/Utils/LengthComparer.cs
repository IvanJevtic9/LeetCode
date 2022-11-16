using System.Collections;

namespace LeetCode.Utils
{
    public class LengthComparer : IComparer
    {
        public int Compare(object x, object y)
        {
            string a = Convert.ToString(x);
            string b = Convert.ToString(y);

            return (new CaseInsensitiveComparer()).Compare(a.Length, b.Length);

        }
    }
}
