using System;
using System.Linq;

namespace InterviewPrep
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = Console.ReadLine();
            //ModifyVariableName(input);
            CountOccurances(input);
            
            // Object Refrence Equal To
            B objB = new B();
            objB.a = 100;
            objB.b = 200;

            B objC = new B();
            objC.a = 200;
            objB = objC;
            Console.WriteLine(objB.a + objB.b);

            string a = "   ABC TBF    ";
            //a = a.Replace(' ', string.Empty);
            string[] b = a.Split(' ');
            string result= null;
            for (int i = 0; i < b.Length; i++)
            {
                if(b[i]!=string.Empty)
                {
                    result += b[i];
                    if(i!= b.Length-1)
                    {
                        result += ' ';
                    }
                }
            }
           }

        protected static string ModifyVariableName(string input)
        {
            input = input.Trim();
            if (string.IsNullOrEmpty(input)) return input;
            String result = "";

            for (int i = 0; i < input.Length; i++)
            {
                if (char.IsUpper(input[i]))
                {
                    result += "_" + char.ToLower(input[i]);

                }
                else if (input[i] == '_')
                {
                    i++;
                    result += char.ToUpper(input[i]);
                }
                else
                {
                    result += input[i];
                }
            }
            return result;
        }

        protected static string CountOccurances(string input)
        {
            input = input.Trim();
            if (string.IsNullOrEmpty(input)) return input;
            string result = "";
            input = string.Concat(input.OrderBy(c => c));
            while (input.Length > 0)
            {
                int count = 0;
                for (int i = 0; i < input.Length; i++)
                {
                    if (input[0] == input[i])
                    {
                        count++;
                    }
                    else break;
                }
                result += input[0] + count.ToString();
                input = input.Replace(input[0].ToString(), "");
            }
            return result;
        }
    }

   public class B
    {
        public int a;
        public int b;

    }
}
