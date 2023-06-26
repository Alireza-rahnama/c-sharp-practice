
namespace myCSharpPracticeProjects
{
    ///<summary>
    ///this code will accept a string from the terminal and checks wether it 
    ///it reads the same backward as forward. It ignores any special character or white spaces.
    ///</summary>
    class Palindrome
    {
        ///<summary>
        ///main method to execute the palindrom logic.
        ///</summary>

        ///<param name="args">
        ///an array of strings reading from the terminal
        ///</param>
        static void Main(string[] args)
        {
            string input;
            bool shouldContinue = true;
            bool isPalindrome;

            while (shouldContinue)
            {

                Console.WriteLine("Enter  a string or exit to quit the program!");

                input = Console.ReadLine();

                if (input.ToLower() == "quit")
                {
                    shouldContinue = false;
                    break;
                }

                string newInput = "";
                string reversedInput = "";

                foreach (char character in input)
                {
                    if (!char.IsPunctuation(character) && !char.IsWhiteSpace(character))
                    {
                        newInput += character;
                    }
                }

                int stringIndex = newInput.Length - 1;

                while (stringIndex >= 0)
                {
                    char character = newInput[stringIndex];

                    if (!char.IsPunctuation(character) && !char.IsWhiteSpace(character))
                    {
                        reversedInput += character;
                    }
                    stringIndex--;
                }

                isPalindrome = newInput.ToLower().Equals(reversedInput.ToLower()) ? true : false;

                Console.WriteLine(reversedInput.ToLower());
                Console.WriteLine($"is Palindrome: {{{isPalindrome}}}");
            }
        }

    }

}
