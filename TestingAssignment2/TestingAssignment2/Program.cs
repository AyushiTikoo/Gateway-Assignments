using System;

namespace TestingAssignment2
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputStr = Console.ReadLine();

            // Case 1  
            Console.WriteLine("If Lowercase Characters Present Convert To Uppercase : " + inputStr.ConvertUpperCase());

            // Case 2 
            Console.WriteLine("If Uppercase Characters Present Convert To Lowercase : " + inputStr.ConvertLowerCase());

            // Case 3  
            Console.WriteLine("Title Case : " + inputStr.ConvertTitleCase());

            // Case 4  
            Console.WriteLine("To Check If Whole String Is In Lower Case Or Not : " + inputStr.CheckLowerCase());

            // Case 5 
            Console.WriteLine("First Letter Uppercase :" + inputStr.FirstUpperLetter());

            // Case 6 
            Console.WriteLine("To Check If Whole String Is In Upper Case Or Not : " + inputStr.CheckUpperCase());

            // Case 7  
            Console.WriteLine("Number Validation :" + inputStr.NumberValidation());

            // Case 8 
            Console.WriteLine("Last Character Remove : " + inputStr.LastCharacterRemove());

            // Case 9 
            Console.WriteLine("Word Count : " + inputStr.WordCount());

            // Case 10  
            Console.WriteLine("String To Int : " + inputStr.StringToInt());
        }
    }
}
