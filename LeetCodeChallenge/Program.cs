using System;
using System.Text;

namespace LeetCodeChallenge
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Solution.LongestCommonPrefix(new string[] {"ab", "a"}));
        }
    }


    public static class Solution
    {
        /* 66. Plus One
         * You are given a large integer represented as an integer array digits, where each digits[i] is the ith digit of the integer. The digits are ordered from most significant to least significant in left-to-right order. The large integer does not contain any leading 0's.

        Increment the large integer by one and return the resulting array of digits.

        Example 1:

        Input: digits = [1,2,3]
        Output: [1,2,4]
        Explanation: The array represents the integer 123.
        Incrementing by one gives 123 + 1 = 124.
        Thus, the result should be [1,2,4].

        Example 3:

        Input: digits = [9]
        Output: [1,0]
        Explanation: The array represents the integer 9.
        Incrementing by one gives 9 + 1 = 10.
        Thus, the result should be [1,0].

         */
        public static int[] PlusOne(int[] digits)
        {
            int mem = 0;
            for (int i = digits.Length - 1; i >= 0; --i) {
                if (mem == 1 && digits[i] + mem == 10) {
                    digits[i] = 0;
                }
                else if (mem == 1) {
                    digits[i] += 1;
                    mem = 0;
                    break;
                }
                else if (digits[i] + 1 == 10 && mem == 0) {
                    digits[i] = 0;
                    mem = 1;
                }
                else {
                    digits[i] += 1;
                    break;
                }
            }
            if (digits[0] == 10 || mem == 1) {
                var arr = new int[digits.Length + 1];
                arr[0] = 1;
                return arr;
            }
            return digits;
        }

        /* 14. Longest Common Prefix
         * Write a function to find the longest common prefix string amongst an array of strings.
        If there is no common prefix, return an empty string "".  

        Example 1:

        Input: strs = ["flower","flow","flight"]
        Output: "fl"
         */
        public static string LongestCommonPrefix(string[] strs)
        {
            if (strs.Length == 1) return strs[0];
            int minLen = strs.Min(w => w.Length);
            if (minLen == 0) return "";
            var prefix = new StringBuilder("");
            int longPrefixIdx = 0;
            while (longPrefixIdx < minLen) {
                char ch = strs[0][longPrefixIdx];
                foreach (var s in strs) {
                    if (ch != s[longPrefixIdx]) return prefix.ToString();
                }
                ++longPrefixIdx;
                prefix.Append(ch);
            }
            return prefix.ToString();
        }

        /* 9. Palindrome Number
         */
        public static bool IsPalindrome(int x)
        {
            if (x < 0) return false;
            if (x < 10) return true;
            int leftToRightNum = x, rightToLeftNum = 0;
            var rightToLeft = new List<int>();
            while (x > 0) {
                rightToLeft.Add(x % 10);
                x /= 10;
            }
            rightToLeft.Reverse();
            for (int i = 0; i < rightToLeft.Count; ++i) {
                rightToLeftNum += rightToLeft[i] * (int)Math.Pow(10, i);
            }
            return leftToRightNum == rightToLeftNum;

        }

        /*88. Merge Sorted Array
         * You are given two integer arrays nums1 and nums2, sorted in non-decreasing order, and two integers m and n, representing the number of elements in nums1 and nums2 respectively.
            Merge nums1 and nums2 into a single array sorted in non-decreasing order.
            Example 1:

            Input: nums1 = [1,2,3,0,0,0], m = 3, nums2 = [2,5,6], n = 3
            Output: [1,2,2,3,5,6]
            Explanation: The arrays we are merging are [1,2,3] and [2,5,6].
            The result of the merge is [1,2,2,3,5,6] with the underlined elements coming from nums1.
         */
        public static void Merge(int[] nums1, int m, int[] nums2, int n)
        {
            int i = m - 1, j = n - 1;
            int k = m + n - 1;
            while (j >= 0) {
                if (i >= 0 && nums1[i] >= nums2[j]) {
                    nums1[k] = nums1[i--];
                }
                else {
                    nums1[k] = nums2[j--];
                }
                --k;
            }
        }

        /* 13. Roman to Integer
         * Symbol       Value
            I             1
            V             5
            X             10
            L             50
            C             100
            D             500
            M             1000
         */
        public static int RomanToInt(string s)
        {
            var dict = new Dictionary<char, int>() {
                {'I', 1}, {'V', 5}, {'X', 10}, {'L', 50},
                {'C', 100}, {'D', 500}, {'M', 1000}
            };
            int res = dict[s[s.Length - 1]];
            for (int i = s.Length - 2; i >= 0; --i) {
                if (dict[s[i]] < dict[s[i + 1]]) {
                    res -= dict[s[i + 1]];
                    res += (dict[s[i + 1]] - dict[s[i]]);
                }
                else {
                    res += dict[s[i]];
                }
            }
            return res;
    }

        /* 4)
         * The string "PAYPALISHIRING" is written in a zigzag pattern on a given number of rows like this: 
         * (you may want to display this pattern in a fixed font for better legibility)
         
            Example 2:

            Input: s = "PAYPALISHIRING", numRows = 4
            Output: "PINALSIGYAHRPI"
            Explanation:
            P     I    N
            A   L S  I G
            Y A   H R
            P     I
         */
        public static string Convert(string s, int numRows)
        {
            if (numRows == 1) return s;
            var vertical = CreateList(s, numRows, 0);
            var diagonal = CreateList(s, numRows, numRows - 1);
            if (vertical.Count > diagonal.Count) diagonal.Add(new char[numRows]); // выравнить кол-во массивов в списке
            diagonal.ForEach(arr => { arr[0] = '_'; arr[^1] = '_'; Array.Reverse(arr); });
            var sb = new StringBuilder("");
            for (int j = 0; j < numRows; j++)
                for (int i = 0; i < vertical.Count; i++)
                    sb.Append(vertical[i][j]).Append(diagonal[i][j]);
            var res = new StringBuilder("");
            foreach (var c in sb.ToString()) {
                if (c != '_' && c != '\0') {
                    res.Append(c);
                }
            }
            return res.ToString();
        }

        private static List<char[]> CreateList(string s, int numRows, int start)
        {
            var result = new List<char[]>() { new char[numRows] };
            var distance = numRows - 2;
            for (int i = 0, j = 0, k = start; k < s.Length;)    // i - индекс списка, j - индекс внутреннего массива
            {
                if (j == numRows) {
                    j = 0;
                    result.Add(new char[numRows]);
                    ++i;
                    k += distance;
                    continue;
                }
                result[i][j] = s[k++];
                ++j;
            }
            return result;
        }


        /* 3)
         * Given a string s, find the length of the longest substring
        without repeating characters.

        Example 1:

        Input: s = "abcabcbb"
        Output: 3
        Explanation: The answer is "abc", with the length of 3.
         */
        public static int LengthOfLongestSubstring(string s)
        {
            if (string.IsNullOrEmpty(s)) return 0;
            int longestSub = 1;
            var resultList = new List<char>() { s[0] };
            for (int i = 1; i < s.Length; i++)
            {
                if (resultList.Contains(s[i])) {
                    longestSub = resultList.Count > longestSub ? resultList.Count : longestSub;
                    i = i - (resultList.Count - 1);
                    resultList.Clear();
                    resultList.Add(s[i]);
                } 
                else {
                    resultList.Add(s[i]); 
                }
            }
            longestSub = resultList.Count > longestSub ? resultList.Count : longestSub;
            return longestSub;
        }

        /* 2)
         * Given a signed 32-bit integer x, return x with its digits reversed.
         * If reversing x causes the value to go outside the signed 32-bit integer range [-231, 231 - 1], then return 0.
         Assume the environment does not allow you to store 64-bit integers (signed or unsigned).
            Example 1:
            Input: x = 123
            Output: 321

            Example 2:
            Input: x = -123
            Output: -321
        */

        public static int Reverse(int x)
        {
            bool isNegative = x < 0;
            if (x == int.MinValue || x == int.MaxValue) return 0;  // минимальное значение нельзя привратить в мах, т.к. оно на 1 дальше и в любом случае реверс выходит за int32
            x = isNegative ? -x : x;

            string xReversed = new string(x.ToString().Reverse().ToArray());
            if (xReversed.Length == 10 && !Validate(x)) {
                return 0;
            }
            return isNegative ? int.Parse("-" + xReversed) : int.Parse(xReversed);


        }

        private static bool Validate(int x)
        {
            int tenPower = 1_000_000_000;
            while (x > 0 && tenPower > 0) {
                int DIGIT_OF_REVERSE = x % 10;
                int DIGIT_OF_MAX = (int.MaxValue / tenPower) % 10;
                if (DIGIT_OF_MAX > DIGIT_OF_REVERSE) 
                    return true;
                else if (DIGIT_OF_MAX < DIGIT_OF_REVERSE)
                    return false;
                x /= 10;
                tenPower /= 10;
            }
            return true;
        }

        /* 1)
         * Given an array of integers nums and an integer target, return indices of the two numbers such that they add up to target.
            You may assume that each input would have exactly one solution, and you may not use the same element twice.
            You can return the answer in any order.

            Example 1:

            Input: nums = [2,7,11,15], target = 9
            Output: [0,1]
            Explanation: Because nums[0] + nums[1] == 9, we return [0, 1].
         */
        public static int[] TwoSum(int[] nums, int target)
        {
            var dict = new Dictionary<int, int>() { { nums[0], 0 } };
            for (int i = 1; i < nums.Length; i++) {
                int required = target - nums[i];
                if (dict.ContainsKey(required)) {
                    return new int[] { dict[required], i };
                } 
                else {
                    dict[nums[i]] = i;
                }
            }
            return new int[2];
        }
        public static void Test_TwoSum()
        {
            var res = Solution.TwoSum([3, 2, 4], 6);
            Console.WriteLine($"[{res[0]}, {res[1]}]");
        }
    }
}
