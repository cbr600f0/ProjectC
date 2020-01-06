using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectC.Pages
{
    public static class LetterValue
    {
        public static IDictionary<char, int> dict = new Dictionary<char, int>();

        static LetterValue()
        {
            dict.Add('A', 1);
            dict.Add('B', 3);
            dict.Add('C', 5);
            dict.Add('D', 2);
            dict.Add('E', 1);
            dict.Add('F', 4);
            dict.Add('G', 3);
            dict.Add('H', 4);
            dict.Add('I', 4);
            dict.Add('J', 4);
            dict.Add('K', 3);
            dict.Add('L', 3);
            dict.Add('M', 3);
            dict.Add('N', 1);
            dict.Add('O', 1);
            dict.Add('P', 3);
            dict.Add('Q', 9);
            dict.Add('R', 2);
            dict.Add('S', 2);
            dict.Add('T', 2);
            dict.Add('U', 4);
            dict.Add('V', 4);
            dict.Add('W', 5);
            dict.Add('X', 8);
            dict.Add('Y', 8);
            dict.Add('Z', 4);
        }
    }

    public class GetValues
    {
        public int WordWorth(string word)
        {
            int totalWordWorth = 0;
            word = word.ToUpper();
            foreach (char letter in word)
            {
                if (LetterValue.dict.ContainsKey(letter))
                {
                    var currentCharacterOfWord = LetterValue.dict.First(x => x.Key == letter);
                    totalWordWorth += currentCharacterOfWord.Value;
                }
            }
            return totalWordWorth;
        }

        public int LetterWorth(char letter)
        {
            if (LetterValue.dict.ContainsKey(letter))
            {
                return LetterValue.dict.First(x => x.Key == letter).Value;
            }
            return 0;
        }
    }
}
