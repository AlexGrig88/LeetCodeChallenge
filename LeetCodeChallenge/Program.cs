using System;

namespace LeetCodeChallenge
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Solution.LengthOfLongestSubstring("abcabcbb"));
        }
    }


    public static class Solution
    {
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
