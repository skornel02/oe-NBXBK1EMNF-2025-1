using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poc_password_cracking;

public static class PasswordGenerator
{
    public static string ToString(int[] boxes, string[] allowedCharacters)
    {
        var result = string.Empty;

        foreach (var box in boxes)
        {
            result += allowedCharacters[box];
        }

        return result;
    }

    public static IEnumerable<string> GeneratePassword(int length, string[] allowedCharacters, bool printPercentage = true)
    {
        var characterBoxes = new int[length];

        long totalIterations = (long)Math.Pow(allowedCharacters.Length, length);
        var percentageTreshold = totalIterations / 20 + 1;
        var nextThreshold = percentageTreshold;

        for (long currentIteration = 0;;++currentIteration)
        {
            if (printPercentage && currentIteration >= nextThreshold)
            {
                var percent = (currentIteration * 100) / totalIterations;
                Console.WriteLine($"Generated {percent}% ({currentIteration:N0} of {totalIterations:N0})");
                nextThreshold += percentageTreshold;
            }

            characterBoxes[0]++;

            for (int i = 0; i < characterBoxes.Length; i++)
            {
                if (characterBoxes[i] >= allowedCharacters.Length)
                {
                    if (i + 1 >= characterBoxes.Length)
                    {
                        yield break;
                    }
                    characterBoxes[i] = 0;
                    characterBoxes[i + 1]++;
                }
            }

            yield return ToString(characterBoxes, allowedCharacters);
        }
    }

}
