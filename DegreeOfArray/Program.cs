using System;
using System.Collections.Generic;

namespace DegreeOfArray
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] array = new int[] {1,2,3,4,5,6,7,8,9,1,2,1,1,2,2,1,4,6 };
            var elementInfo = GetDegreeAndIndices(array);
            Console.WriteLine("The degree of array is: " + elementInfo.Count);

            var subArraywithSameDegree = GetSubArray(array, elementInfo.LeftIndex, elementInfo.RightIndex);
            Console.WriteLine("The sub array with same degree is: ");
            foreach (var i in subArraywithSameDegree)
            {
                Console.Write(i + " ,");
            }

            Console.ReadLine();
        }

        public static ElementInfo GetDegreeAndIndices(int[] array)
        {
            var keyValuePairs = GetElementInfos(array);
            var result = GetElementInfoWithHighestCount(keyValuePairs);
            return result;
        }

        private static ElementInfo GetElementInfoWithHighestCount(Dictionary<int, ElementInfo> keyValuePairs)
        {
            int degree = 0;
            int degreeElement = 0;
            ElementInfo result = new ElementInfo();
            foreach (var kvp in keyValuePairs)
            {
                if (kvp.Value.Count > degree)
                {
                    degree = kvp.Value.Count;
                    degreeElement = kvp.Key;
                    result = kvp.Value;
                }
            }

            return result;
        }

        private static Dictionary<int, ElementInfo> GetElementInfos(int[] array)
        {
            Dictionary<int, ElementInfo> keyValuePairs = new Dictionary<int, ElementInfo>();
            for (int i = 0; i < array.Length; i++)
            {
                if (keyValuePairs.ContainsKey(array[i]))
                {
                    var elementInfo = keyValuePairs[array[i]];
                    elementInfo.Count++;
                    elementInfo.RightIndex = i;
                }
                else
                {
                    var elementInfo = new ElementInfo() { Count = 1, LeftIndex = i, RightIndex = i };
                    keyValuePairs.Add(array[i], elementInfo);
                }
            }

            return keyValuePairs;
        }

        private static int[] GetSubArray(int[] arr, int start, int end)
        {
            int[] result = new int[end - start + 1];
            int x = 0;
            for (int i = start; i <= end; i++)
            {
                result[x] = arr[i];
                x++;
            }
            return result;
        }
    }
}
