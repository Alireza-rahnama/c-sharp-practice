using System.Text;
using System.Runtime.InteropServices.ComTypes;
namespace myCSharpPracticeProjects
{
    public class Palindrome
    {
        static void Main(string[] args)
        {
            string input;
            bool shouldContinue = true;
            bool isPalindrome;

            while (shouldContinue)
            {

                Console.WriteLine("Enter a string or exit to quit the program!");

                input = Console.ReadLine();
                string newInput = "";

                string reversedInput = "";


                foreach(char character in  input){
                    if (!char.IsPunctuation(character) && !char.IsWhiteSpace(character))
                    {
                        newInput+=character;   
                    }
                }

                int stringIndex = newInput.Length - 1;

                while (stringIndex >= 0)
                {
                    char character = newInput[stringIndex];

                    if (!char.IsPunctuation(character) && !char.IsWhiteSpace(character))
                    {
                        reversedInput+=character;   
                    }
                    stringIndex--;
                }

                
                isPalindrome = newInput.ToLower().Equals(reversedInput.ToLower()) ? true : false;

                Console.WriteLine(reversedInput.ToLower());
                Console.WriteLine($"is Palindrome: {{{isPalindrome}}}");

                if (input.ToLower() == "quit")
                {
                    shouldContinue = false;
                    break;
                }

            }



        }

    }

}
