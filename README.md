## Description:
Create two functions to encode and then decode a string using the Rail Fence Cipher. This cipher is used to encode a string by placing each character successively in a diagonal along a set of "rails". First start off moving diagonally and down. When you reach the bottom, reverse direction and move diagonally and up until you reach the top rail. Continue until you reach the end of the string. Each "rail" is then read left to right to derive the encoded string.

For example, the string ```"WEAREDISCOVEREDFLEEATONCE"``` could be represented in a three rail system as follows:
```
W       E       C       R       L       T       E
  E   R   D   S   O   E   E   F   E   A   O   C  
    A       I       V       D       E       N
```
The encoded string would be:
```
WECRLTEERDSOEEFEAOCAIVDEN
```
Write a function/method that takes 2 arguments, a string and the number of rails, and returns the ENCODED string.

Write a second function/method that takes 2 arguments, an encoded string and the number of rails, and returns the DECODED string.

For both encoding and decoding, assume number of rails >= 2 and that passing an empty string will return an empty string.

Note that the example above excludes the punctuation and spaces just for simplicity. There are, however, tests that include punctuation. Don't filter out punctuation as they are a part of the string.
### My solution
```C#
using System.Collections.Generic;
using System.Text;

public class RailFenceCipher
{
    public static string Encode(string s, int n)
    {
        List<char?>[] railFence = MakeRailFence(s, n);
        StringBuilder result = new(s.Length);
        
        for (int i = 0; i < n; i++)
        {
            foreach (char? c in railFence[i])
            {
                if (c != null)
                    result.Append(c);
            }
        }

        return result.ToString();
    }

    public static string Decode(string s, int n)
    {
        List<char?>[] railFence = MakeRailFence(s, n);
        StringBuilder result = new(s.Length);
        int index = 0;

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < railFence[i].Count; j++)
            {
                if (railFence[i][j] != null)
                    railFence[i][j] = s[index++];
            }
        }

        (int, int) position = (0, 0);
        int step = 1;
        
        while (result.Length != s.Length)
        {
            if (railFence[position.Item1][position.Item2] == null)
            {
                step *= -1;
                position.Item1 += step;
            }

            result.Append(railFence[position.Item1][position.Item2]);

            if ((position.Item1 == n - 1 && step == 1) || (position.Item1 == 0 && step == -1))
                position.Item2++;

            else position.Item1 += step;
        }

        return result.ToString();
    }

    public static List<char?>[] MakeRailFence(string str, int n)
    {
        List<char?>[] railFence = new List<char?>[n];

        for (int i = 0; i < n; i++)
            railFence[i] = new List<char?>();

        int index = 0, step = 1;

        foreach (char c in str)
        {
            railFence[index].Add(c);

            if ((index == n - 1 && step == 1) || (index == 0 && step == -1))
            {
                railFence[index].Add(null);
                step *= -1;
            }

            index += step;
        }

        return railFence;
    }
}
```
