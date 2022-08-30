using System;
using System.Linq;

namespace InterviewPrep
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(isPalindrom("ABABCDCBAAA"));
            //PrintTriangle(4);  
        }

        public static void PrintTriangle(int length)
        {
            for (int i = 1; i <= length; i++)
            {
                for (int j = 1; j <= i; j++)
                {
                    Console.Write("*");
                }
                Console.Write('\n');
            }
        }

        public static string isPalindrom(string str)
        {
            if (str == null || str.Length == 0) return null;
            int startIndex = 0;
            int middleLength = 0;
            int longest = 0;
            if (str.Length < 2)
            {
                return str;
            }
            else
            {
                for (int i = 0; i < str.Length; i++)
                {
                    // Even number of Characters
                    middleLength = FindPalindrome(str, i, i);
                    if (middleLength > longest)
                    {
                        longest = middleLength;
                        startIndex = i - middleLength / 2;
                    }
                    // Odd Number of Character Length
                    middleLength = FindPalindrome(str, i, i + 1);
                    if (middleLength > longest)
                    {
                        longest = middleLength;
                        startIndex = i - middleLength / 2 + 1;
                    }
                }
                return str.Substring(startIndex, longest);
            }
        }

        public static int FindPalindrome(string SubStr, int left, int right)
        {
            int length = 0;
            while (left >= 0 && right < SubStr.Length)
            {
                if (SubStr[left] != SubStr[right])
                {
                    break;
                }

                if (left == right)
                {
                    length++;
                }
                else
                {
                    length += 2;
                }

                left--;
                right++;
            }
            return length;
        }

    }
}
