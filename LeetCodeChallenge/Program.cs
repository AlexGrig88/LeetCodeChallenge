﻿namespace LeetCodeChallenge
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            var res = Solution.TwoSum([3, 2, 4], 6);
            Console.WriteLine($"[{res[0]}, {res[1]}]");
        }
    }


    public static class Solution
    {
        /*
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
    }
}
