using LeetCode.Models;
using LeetCode.Utils;
using System.Collections;
using System.Text;

namespace LeetCode
{
    public static class Solutions
    {
        #region Zuehkle - task 1: Find largest common substring in string 
        public static string GetLargestCommonSubstring(string stringSequence)
        {
            string maxSubstring = "";
            string substringS = "";
            string stringS = "";

            int substringPointer = 0;
            int currentSubStringPointer = 0;

            for (int i = 0; i < stringSequence.Length; i++)
            {
                stringS += stringSequence[i];

                substringPointer = substringS.Length != 0 ? substringPointer : stringS.Length - 1;

                while (currentSubStringPointer < substringPointer)
                {
                    if (stringSequence[i] == stringS[currentSubStringPointer])
                    {
                        currentSubStringPointer++;
                        substringS = substringS + stringSequence[i];
                        if (maxSubstring.Length < substringS.Length)
                        {
                            maxSubstring = substringS.Substring(0);
                        }
                        break;
                    }
                    else if (stringSequence[i] != stringS[currentSubStringPointer] && substringS.Length != 0)
                    {
                        substringS += stringSequence[i];

                        while (substringS.Length > 0)
                        {
                            var indexOf = stringS.Substring(0, substringPointer).IndexOf(substringS);

                            if (indexOf == -1)
                            {
                                if (substringS.Length == 1)
                                {
                                    substringS = "";
                                    currentSubStringPointer = 0;
                                    break;
                                }

                                substringS = substringS.Substring(1);
                                substringPointer++;
                            }
                            else
                            {
                                currentSubStringPointer = indexOf + substringS.Length;
                                if (maxSubstring.Length < substringS.Length)
                                {
                                    maxSubstring = substringS.Substring(0);
                                }
                                break;
                            }
                        }

                        break;
                    }
                    else
                    {
                        currentSubStringPointer++;
                    }
                }
                currentSubStringPointer = substringS.Length == 0 || currentSubStringPointer == substringPointer ? 0 : currentSubStringPointer;
            }

            return maxSubstring;
        }
        #endregion

        #region Problem14: LongestCommonPrefix
        public static string LongestCommonPrefix(string[] strs)
        {
            StringBuilder commonString = new StringBuilder();
            if (strs.Length == 0)
                return commonString.ToString();

            IComparer lengthComparer = new LengthComparer();
            Array.Sort(strs, lengthComparer);

            for (int i = 0; i < strs[0].Length; i++)
            {
                bool contains = true;
                for (int j = 1; j < strs.Length; j++)
                {
                    contains = strs[0][i] == strs[j][i];
                    if (!contains)
                        break;
                }

                if (contains)
                    commonString.Append(strs[0][i]);
                else
                    break;
            }

            return commonString.ToString();
        }

        #endregion
        #region Problem15: ThreeSum - solution 1
        public static IList<IList<int>> ThreeSum1(int[] nums)
        {
            IList<IList<int>> result = new List<IList<int>>();

            if (nums.Length > 3000 || nums.Length < 3)
            {
                return result;
            }

            Array.Sort(nums);

            int pointerOne = 0;
            int pointerTwo = 1;
            while (true)
            {
                bool flag2 = true;
                for (int i = pointerTwo + 1; i < nums.Length; ++i)
                {
                    if (nums[pointerOne] + nums[pointerTwo] + nums[i] == 0)
                    {
                        var triples = new List<int> { nums[pointerOne], nums[pointerTwo], nums[i] };
                        result.Add(triples);

                        while (pointerTwo < nums.Length - 1 && nums[pointerTwo] == nums[pointerTwo + 1])
                        {
                            ++pointerTwo;
                            flag2 = false;
                        }

                        if (flag2)
                        {
                            ++pointerTwo;
                            flag2 = true;
                        }
                        i = pointerTwo;

                        if (pointerTwo == nums.Length - 1)
                            break;
                    }
                    else if (nums[pointerOne] + nums[pointerTwo] + nums[i] > 0)
                    {
                        break;
                    }
                    else
                    {
                        while (i < nums.Length - 1 && nums[i] == nums[i + 1])
                            ++i;
                    }
                }
                while (pointerOne < nums.Length - 2 && nums[pointerOne] == nums[pointerOne + 1])
                {
                    ++pointerOne;
                }

                ++pointerOne;
                pointerTwo = pointerOne + 1;

                if (pointerOne == nums.Length - 2)
                    break;
            }

            return result;
        }
        #endregion
        #region Problem15: ThreeSum - faster solution 
        public static IList<IList<int>> ThreeSum(int[] nums)
        {
            if (nums.Length > 3000 || nums.Length < 3)
            {
                return new List<IList<int>>();
            }
            IList<IList<int>> result = new List<IList<int>>();

            Array.Sort(nums);
            for (int i = 0; i < nums.Length - 2; i++)
            {
                if (i > 0 && nums[i] == nums[i - 1])
                    continue;

                int j = i + 1, k = nums.Length - 1;
                while (j < k)
                {
                    if (nums[i] + nums[j] + nums[k] > 0)
                    {
                        --k;
                        while (j < k && nums[k] == nums[k + 1])
                            --k;

                    }
                    else if (nums[i] + nums[j] + nums[k] < 0)
                    {
                        ++j;
                        while (j < k && nums[j - 1] == nums[j])
                            ++j;
                    }
                    else
                    {
                        result.Add(new List<int> { nums[i], nums[j], nums[k] });
                        ++j;
                        --k;
                        while (j < k && nums[j - 1] == nums[j])
                            ++j;
                        while (j < k && nums[k] == nums[k + 1])
                            --k;
                    }
                }
            }

            return result;
        }
        #endregion
        #region Problem16: ThreeSumClosest
        public static int ThreeSumClosest(int[] nums, int target)
        {
            if (nums.Length < 3 || nums.Length > 1000)
            {
                return -1;
            }

            int result = -1;
            int diffMin = 20000;
            Array.Sort(nums);
            for (int i = 0; i < nums.Length - 2; i++)
            {
                int j = i + 1, k = nums.Length - 1;
                while (j < k)
                {
                    int diff = target - (nums[i] + nums[j] + nums[k]);
                    if (diff > 0)
                    {
                        if (Math.Abs(diff) < diffMin)
                        {
                            result = nums[i] + nums[j] + nums[k];
                            diffMin = Math.Abs(diff);
                        }
                        ++j;
                    }
                    else if (diff < 0)
                    {
                        if (Math.Abs(diff) < diffMin)
                        {
                            result = nums[i] + nums[j] + nums[k];
                            diffMin = Math.Abs(diff);
                        }
                        --k;
                    }
                    else
                    {
                        return nums[i] + nums[j] + nums[k];
                    }
                }

                while (i < nums.Length - 2 && nums[i] == nums[i + 1]) ++i;
            }

            return result;
        }
        #endregion
        #region Problem17: LatterCombinations
        public static IList<string> LetterCombinations(string digits)
        {
            if (digits.Length == 0)
            {
                return new List<string>();
            }
            List<string> allCombinations = new List<string>();
            GetAllCombination(digits, "", digits.Length, ref allCombinations);

            return allCombinations;
        }

        private static void GetAllCombination(string digits, string currentCombination, int maxLength, ref List<string> allCombinations)
        {
            string[] numberLetters = new string[]
            {
                "abc",
                "def",
                "ghi",
                "jkl",
                "mno",
                "pqrs",
                "tuv",
                "wxyz"
            };

            if (maxLength == currentCombination.Length)
            {
                allCombinations.Add(currentCombination);
            }
            else
            {
                foreach (var letter in numberLetters[Convert.ToInt32(digits[0].ToString()) - 2])
                {
                    currentCombination += letter;
                    GetAllCombination(digits.Substring(1), currentCombination, maxLength, ref allCombinations);
                    currentCombination = currentCombination.Remove(currentCombination.Length - 1);
                }
            }
        }
        #endregion
        #region Problem18: 4Sum
        public static IList<IList<int>> FourSum(int[] nums, int target)
        {
            var result = new List<IList<int>>();

            if (nums.Length < 4 || nums.Length > 200) return new List<IList<int>>();

            Array.Sort(nums);

            var pointer1 = 0;
            var pointer2 = 1;
            var pointer3 = 2;

            while (true)
            {
                for (int i = pointer3 + 1; i < nums.Length; ++i)
                {
                    if (i - 1 != pointer3 && nums[i - 1] == nums[i]) continue;
                    if ((nums[pointer1] + nums[pointer2] + nums[pointer3] + nums[i]) == target)
                    {
                        result.Add(new List<int>()
                        {
                            nums[pointer1],nums[pointer2],nums[pointer3],nums[i]
                        });
                    }

                    if ((nums[pointer1] + nums[pointer2] + nums[pointer3] + nums[i]) > target) break;
                }

                var startCiclus = false;
                bool? breakSearch = null;

                while (pointer3 != nums.Length - 2)
                {
                    pointer3++;
                    if (nums[pointer3 - 1] != nums[pointer3])
                    {
                        startCiclus = true;
                        breakSearch = false;
                        break;
                    }
                }

                if (startCiclus) continue;
                else if (breakSearch.HasValue) break;

                while (pointer2 != nums.Length - 3)
                {
                    pointer2++;
                    if (nums[pointer2 - 1] != nums[pointer2])
                    {
                        startCiclus = true;
                        breakSearch = false;
                        pointer3 = pointer2 + 1;
                        break;
                    }
                }

                if (startCiclus) continue;
                else if (breakSearch.HasValue) break;

                while (pointer1 != nums.Length - 4)
                {
                    pointer1++;
                    if (nums[pointer1 - 1] != nums[pointer1])
                    {
                        startCiclus = true;
                        pointer2 = pointer1 + 1;
                        pointer3 = pointer2 + 1;
                        break;
                    }
                }

                if (startCiclus) continue;
                break;
            }

            return result;

        }
        #endregion
        #region Problem18. 4Sum - Faster solution
        public static IList<IList<int>> FourSumFaster(int[] nums, int target)
        {
            var result = new List<IList<int>>();

            if (nums.Length < 4 || nums.Length > 200) return new List<IList<int>>();

            Array.Sort(nums);

            for (int i = 0; i < nums.Length - 3; ++i)
            {
                if (i > 0 && nums[i - 1] == nums[i]) continue;
                for (int j = i + 1; j < nums.Length - 2; ++j)
                {
                    if (j > i + 1 && nums[j - 1] == nums[j]) continue;
                    int k = j + 1, k1 = nums.Length - 1;

                    while (k < k1)
                    {
                        if (nums[i] + nums[j] + nums[k] + nums[k1] > target)
                        {
                            --k1;
                            while (k < k1 && nums[k1] == nums[k1 + 1]) --k1;
                        }
                        else if (nums[i] + nums[j] + nums[k] + nums[k1] < target)
                        {
                            ++k;
                            while (k < k1 && nums[k] == nums[k - 1]) ++k;
                        }
                        else
                        {
                            result.Add(new List<int>() { nums[i], nums[j], nums[k], nums[k1] });
                            ++k;
                            --k1;
                            while (k < k1 && nums[k] == nums[k - 1]) ++k;
                            while (k < k1 && nums[k1] == nums[k1 + 1]) --k1;
                        }
                    }

                }



            }

            return result;
        }
        #endregion
        #region Problem19 - RemoveNthFromEnd
        public static ListNode RemoveNthFromEnd(ListNode head, int n)
        {
            if (n < 1 || n > 30)
                return null;

            var nodes = new List<ListNode>();
            var count = 0;

            while (head != null)
            {
                if (count == 30)
                {
                    break;
                }

                nodes[count] = head;
                head = head.next;
                ++count;
            }

            if (count == n)
            {
                return nodes[0].next;
            }

            nodes[count - n - 1].next = nodes[count - n].next;

            return nodes[0];
        }
        #endregion
        #region Problem20 - Valid Parentheses
        public static bool IsValid(string s)
        {
            if (s.Length == 0 || s.Length > 10000)
            {
                return false;
            }

            Stack<char> stack = new Stack<char>();

            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == '(' || s[i] == '{' || s[i] == '[')
                {
                    stack.Push(s[i]);
                }
                else if (s[i] == ')' || s[i] == '}' || s[i] == ']')
                {
                    if (stack.Count == 0)
                    {
                        return false;
                    }

                    var startPar = stack.Pop();

                    if ((startPar == '(' && s[i] != ')') ||
                        (startPar == '{' && s[i] != '}') ||
                        (startPar == '[' && s[i] != ']'))
                    {
                        return false;
                    }
                }
            }

            if (stack.Count != 0)
            {
                return false;
            }

            return true;
        }
        #endregion
        #region Problem21 - Merge Two Sorted List
        public static ListNode MergeTwoLists(ListNode list1, ListNode list2)
        {
            if (list1 == null)
                return list2;
            if (list2 == null)
                return list1;

            if (list2.val > list1.val)
            {
                list1.next = MergeTwoLists(list1.next, list2);

                return list1;
            }

            list2.next = MergeTwoLists(list1, list2.next);
            return list2;
        }
        #endregion
        #region Problem22 - Generate Parentheses

        private static List<string> result = new List<string>();
        private static int maxN;
        public static IList<string> GenerateParenthesis(int n)
        {
            maxN = n;
            GenerateAndCheck(new char[n * 2], 0, 0, 0);
            return result;
        }
        private static void GenerateAndCheck(char[] str, int opened, int closed, int index)
        {
            if (opened == closed && opened == maxN)
            {
                result.Add(new string(str));
                return;
            }

            if (opened < maxN)
            {
                str[index] = '(';
                GenerateAndCheck(str, ++opened, closed, ++index);
                --index;
                --opened;
            }
            if (closed < opened)
            {
                str[index] = ')';
                GenerateAndCheck(str, opened, ++closed, ++index);
                --closed;
                --index;
            }
        }
        #endregion
        #region Problem23 - Merge k sorted list
        public static ListNode MergeKLists(ListNode[] lists)
        {
            if (lists == null || lists.Length == 0)
                return null;

            return Merge(lists, 0, lists.Length - 1);
        }

        private static ListNode Merge(ListNode[] lists, int i, int j)
        {
            if (j == i)
                return lists[i];
            else
            {
                int mid = i + (j - i) / 2;

                ListNode left = Merge(lists, i, mid),
                         right = Merge(lists, mid + 1, j);

                return Merge(left, right);
            }
        }

        private static ListNode Merge(ListNode list1, ListNode list2)
        {
            ListNode dummy = new ListNode(0),
                     cur = dummy;

            while (list1 != null && list2 != null)
            {
                if (list1.val <= list2.val)
                {
                    cur.next = list1;
                    list1 = list1.next;
                }
                else
                {
                    cur.next = list2;
                    list2 = list2.next;
                }

                cur = cur.next;
            }

            if (list1 != null)
                cur.next = list1;

            if (list2 != null)
                cur.next = list2;

            return dummy.next;
        }
        #endregion
        #region Problem224: Calculate
        public static int Calculate(string s)
        {
            int global = 0;
            Stack<string> expression = new Stack<string>();

            char currentOperation = 'n';
            int currentValue = 0;

            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == ' ') continue;
                else if (s[i] == '+' || s[i] == '-') currentOperation = s[i];
                else if (s[i] == '(')
                {
                    currentOperation = currentOperation != 'n' ? currentOperation : '+';

                    expression.Push(currentValue.ToString());
                    expression.Push(currentOperation.ToString());

                    currentOperation = '+';
                    currentValue = 0;
                }
                else if (s[i] == ')')
                {
                    int cur;
                    switch (expression.Pop())
                    {
                        case "+":
                            cur = Convert.ToInt32(expression.Pop());
                            currentValue = cur + currentValue;
                            break;
                        case "-":
                            cur = Convert.ToInt32(expression.Pop());
                            currentValue = cur - currentValue;
                            break;
                    }
                }
                else
                {
                    currentOperation = currentOperation != 'n' ? currentOperation : '+';
                    var value = Convert.ToInt32(s[i].ToString());

                    while (i + 1 < s.Length && char.IsDigit(s[i + 1]))
                    {
                        ++i;
                        value = value * 10 + Convert.ToInt32(s[i].ToString());
                    }
                    switch (currentOperation)
                    {
                        case '+':
                            currentValue += value;
                            break;
                        case '-':
                            currentValue -= value;
                            break;
                    }
                }
            }

            if (currentValue != 0)
            {
                global += currentValue;
            }
            return global;
        }
        #endregion
    }
}
