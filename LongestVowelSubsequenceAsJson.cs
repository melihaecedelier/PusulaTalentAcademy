using System;
using System.Linq;
using System.Text.Json;
using System.Collections.Generic;

public class LongestVowelSubsequenceAsJson
{
    private static bool IsVowel(char c)
    {
        return "aeiouAEIOU".IndexOf(c) >= 0;
    }

    public static string LongestVowelSubsequenceAsJson(List<string> words)
    {
        if (words == null || words.Count == 0)
        {
            return "[]";
        }

        var results = new List<object>();

        foreach (var word in words)
        {
            string longestSequence = "";
            string currentSequence = "";

            if (string.IsNullOrEmpty(word))
            {
                results.Add(new { word = "", sequence = "", length = 0 });
                continue;
            }

            for (int i = 0; i < word.Length; i++)
            {
                if (IsVowel(word[i]))
                {
                    currentSequence += word[i];
                }
                else
                {
                    if (currentSequence.Length > longestSequence.Length)
                    {
                        longestSequence = currentSequence;
                    }
                    currentSequence = "";
                }
            }

            // Döngü bittikten sonra son diziyi de kontrol etmeyi unutmayın.
            if (currentSequence.Length > longestSequence.Length)
            {
                longestSequence = currentSequence;
            }
            
            results.Add(new {
                word = word,
                sequence = longestSequence,
                length = longestSequence.Length
            });
        }

        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        return JsonSerializer.Serialize(results, options);
    }
}
