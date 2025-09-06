using System.Linq;
using System.Text.Json;
using System.Collections.Generic;

public class MaxIncreasingSubArrayAsJson
{
    public static string MaxIncreasingSubarrayAsJson(List<int> numbers)
    {
        if (numbers == null || numbers.Count == 0)
        {
            return "[]";
        }
        
        List<int> currentSubarray = new List<int>();
        List<int> maxSubarray = new List<int>();
        
        currentSubarray.Add(numbers[0]);
        maxSubarray.Add(numbers[0]);

        for (int i = 1; i < numbers.Count; i++)
        {
            if (numbers[i] > numbers[i - 1])
            {
                currentSubarray.Add(numbers[i]);
            }
            else
            {
                if (currentSubarray.Sum() > maxSubarray.Sum())
                {
                    maxSubarray = new List<int>(currentSubarray);
                }
            
                currentSubarray.Clear();
                currentSubarray.Add(numbers[i]);
            }
        }
        
        if (currentSubarray.Sum() > maxSubarray.Sum())
        {
            maxSubarray = new List<int>(currentSubarray);
        }
        
        return JsonSerializer.Serialize(maxSubarray);
    }
}