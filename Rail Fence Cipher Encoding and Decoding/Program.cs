using System.Collections.Generic;
using System.Text;

public class RailFenceCipher
{
    public static void Main()
    {   
        // Tests
        var t1 = Encode("WEAREDISCOVEREDFLEEATONCE", 3);
        // ...should return "WECRLTEERDSOEEFEAOCAIVDEN"

        var t2 = Decode("WECRLTEERDSOEEFEAOCAIVDEN", 3);
        // ...should return "WEAREDISCOVEREDFLEEATONCE"
    }

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