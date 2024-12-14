using WordFinder.Tools;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to the Matrix Word Finder!");
        Console.WriteLine("Please enter the number of rows for the matrix (1-64):");

        // Get the number of rows
        int numberOfRows = GetValidIntegerInput(1, 64);

        // Get the number of columns
        Console.WriteLine("Please enter the number of columns for the matrix (1-64):");
        int numberOfColumns = GetValidIntegerInput(1, 64);

        // Confirm matrix size
        Console.WriteLine($"You have defined a {numberOfRows}x{numberOfColumns} matrix.");

        // Ask if the user wants to provide a list of words
        Console.WriteLine("Would you like to provide a list of words to randomly place in the matrix? (yes/no)");
        bool provideWords = GetYesOrNoInput();

        List<string> words = new List<string>();

        if (provideWords)
        {
            Console.WriteLine("Please enter the words separated by commas (e.g., apple,banana,grape):");
            words = GetWordListInput();

            Console.WriteLine("You have provided the following words:");
            foreach (var word in words)
            {
                Console.WriteLine($"- {word}");
            }
        }
        else
        {
            Console.WriteLine("You chose not to provide any words.");
        }

        var matrixGenerator = new MatrixGenerator();
        if (words.Any())
        {
            matrixGenerator.ValidWords = words;
        }

        while (true)
        {          
            var matrix = matrixGenerator.Generate(rows: numberOfRows, columns: numberOfColumns, maxInsertions: 10);

            Console.WriteLine("Here is the generated matrix:");
            foreach (var row in matrix)
            {
                Console.WriteLine(string.Join(" ", row.ToCharArray()));
            }
           
            Console.WriteLine("Do you accept this matrix? (yes/no)");
            if (GetYesOrNoInput())
            {
                Console.WriteLine("Matrix accepted!");

                var wordFinder = new WordFinder.Core.WordFinder(matrix);

                while (true)
                {                    
                    Console.WriteLine("Enter the list of words to search for, separated by commas:");
                    var searchWords = GetWordListInput();

                    var result = wordFinder.Find(searchWords);
                    
                    Console.WriteLine("Search results:");
                    if (result.Any())
                    {
                        foreach (var word in result)
                        {
                            Console.WriteLine($"- {word}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No words found in the matrix.");
                    }
                   
                    Console.WriteLine("Would you like to search for another set of words? (yes/no)");
                    if (!GetYesOrNoInput())
                    {
                        Console.WriteLine("Exiting word search...");
                        break; // Exit the loop if user don't want to search for more words
                    }
                }

                break; // Exit the main loop if the matrix was accepted
            }
        }          
    }

    static int GetValidIntegerInput(int min, int max)
    {
        int value;
        while (true)
        {
            Console.Write("Enter a number: ");
            string input = Console.ReadLine();

            if (int.TryParse(input, out value) && value >= min && value <= max)
            {
                return value; // Input is valid, return the value
            }

            Console.WriteLine($"Invalid input. Please enter a valid integer between {min} and {max}.");
        }
    }

    static bool GetYesOrNoInput()
    {
        while (true)
        {
            Console.Write("Enter 'yes' or 'no': ");
            string input = Console.ReadLine()?.Trim().ToLower();

            if (input == "yes")
            {
                return true;
            }
            else if (input == "no")
            {
                return false;
            }

            Console.WriteLine("Invalid input. Please enter 'yes' or 'no'.");
        }
    }

    static List<string> GetWordListInput()
    {
        while (true)
        {
            Console.Write("Enter words separated by commas: ");
            string input = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(input))
            {
                var words = new List<string>(input.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries));

                // Trim whitespace from each word
                for (int i = 0; i < words.Count; i++)
                {
                    words[i] = words[i].Trim();
                }

                if (words.Count > 0)
                {
                    return words;
                }
            }

            Console.WriteLine("Invalid input. Please enter at least one word separated by commas.");
        }
    }
}