using System;
using System.Collections.Generic;

namespace DegreeOfArray
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] array = new int[] { 1, 2, 3, 4, 5, 1, 7, 9, 1, 2, 5, 3 };

            if (array.Length == 0)
            {
                Console.WriteLine("empty Array");
                return;
            }

            if (array.Length == 1)
            {
                Console.WriteLine("The degree of array is: " + 1 + " and minimum aubarray is same as input array");
                return;
            }

            var elementInfo = GetDegreeAndIndices(array);
            Console.WriteLine("The degree of array is: " + elementInfo.Count);

            var minSubArraywithSameDegree = GetSubArray(array, elementInfo.LeftIndex, elementInfo.RightIndex);
            Console.WriteLine("The minimum sub array with same degree is: ");
            foreach (var i in minSubArraywithSameDegree)
            {
                Console.Write(i + " ,");
            }

            Console.ReadLine();
        }

        public static ElementInfo GetDegreeAndIndices(int[] array)
        {
            var keyValuePairs = GetElementInfos(array);
            var elementInfoList = GetElementInfosWithHighestCount(keyValuePairs);
            var elelemtInfoWithSmallestSubArray = GetSmallestSubArray(elementInfoList);
            return elelemtInfoWithSmallestSubArray;
        }

        private static ElementInfo GetSmallestSubArray(List<ElementInfo> elementInfoList)
        {
            if (elementInfoList.Count == 1)
            {
                return elementInfoList[0];
            }

            var elelemtInfoWithSmallestSubArray = elementInfoList[0];
            int minSubArrayLength = elementInfoList[0].RightIndex - elementInfoList[0].LeftIndex + 1;

            for (var i = 1; i < elementInfoList.Count; i++)
            {
                var subArrayLength = elementInfoList[i].RightIndex - elementInfoList[i].LeftIndex + 1;
                if (subArrayLength < minSubArrayLength)
                {
                    minSubArrayLength = subArrayLength;
                    elelemtInfoWithSmallestSubArray = elementInfoList[i];
                }
            }
            return elelemtInfoWithSmallestSubArray;
        }

        private static List<ElementInfo> GetElementInfosWithHighestCount(Dictionary<int, ElementInfo> keyValuePairs)
        {
            int degree = 0;
            int degreeElement = 0;
            List<ElementInfo> result = new List<ElementInfo>();
            foreach (var kvp in keyValuePairs)
            {
                if (kvp.Value.Count < degree)
                {
                    continue;
                }

                if (kvp.Value.Count == degree)
                {
                    result.Add(kvp.Value);
                }

                if (kvp.Value.Count > degree)
                {
                    degree = kvp.Value.Count;
                    degreeElement = kvp.Key;
                    result = new List<ElementInfo>() { kvp.Value };
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
