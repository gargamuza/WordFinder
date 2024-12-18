﻿using System.Text;

namespace WordFinder.Tools
{
    public class MatrixGenerator
    {
        public List<string> ValidWords = new List<string>
        {
            "apple", "banana", "orange", "grape", "cherry", "peach", "melon", "berry", "kiwi", "lemon"
        };

        public List<string> Generate(int rows, int columns, int maxInsertions)
        {
            var random = new Random();
            var matrix = new char[rows, columns];

            // Initialize the array with whitespace
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < columns; col++)
                {
                    matrix[row, col] = ' ';
                }
            }

            // Insert valid words at random
            for (int i = 0; i < maxInsertions; i++)
            {
                string validWord = ValidWords[random.Next(ValidWords.Count)];
                bool placed = false;

                int attempts = 0;
                const int maxAttempts = 100;

                while (!placed && attempts < maxAttempts)
                {
                    attempts++;
                    bool placeHorizontally = random.Next(0, 2) == 0;

                    if (placeHorizontally)
                    {
                        if (validWord.Length > columns)
                        {
                            // It is not possible to place horizontally                         
                            break;
                        }

                        // Try to place horizontally
                        int startRow = random.Next(0, rows);
                        int startCol = random.Next(0, columns - validWord.Length + 1);

                        // Check if positions are available
                        bool canPlace = true;
                        for (int k = 0; k < validWord.Length; k++)
                        {
                            if (matrix[startRow, startCol + k] != ' ')
                            {
                                canPlace = false;
                                break;
                            }
                        }

                        if (canPlace)
                        {
                            for (int k = 0; k < validWord.Length; k++)
                            {
                                matrix[startRow, startCol + k] = validWord[k];
                            }
                            placed = true;
                        }
                    }
                    else
                    {
                        if (validWord.Length > rows)
                        {
                            // It is not possible to place vertically                          
                            break;
                        }

                        // Try to place vertically
                        int startRow = random.Next(0, rows - validWord.Length + 1);
                        int startCol = random.Next(0, columns);

                        // Check if positions are available
                        bool canPlace = true;
                        for (int k = 0; k < validWord.Length; k++)
                        {
                            if (matrix[startRow + k, startCol] != ' ')
                            {
                                canPlace = false;
                                break;
                            }
                        }

                        if (canPlace)
                        {
                            for (int k = 0; k < validWord.Length; k++)
                            {
                                matrix[startRow + k, startCol] = validWord[k];
                            }
                            placed = true;
                        }
                    }
                }
            }

            // Fill empty spaces with random characters
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < columns; col++)
                {
                    if (matrix[row, col] == ' ')
                    {
                        matrix[row, col] = (char)random.Next('a', 'z' + 1);
                    }
                }
            }

            // Convert each row of the array to a string and add it to the word list
            var result = new List<string>();
            for (int row = 0; row < rows; row++)
            {
                var sb = new StringBuilder();
                for (int col = 0; col < columns; col++)
                {
                    sb.Append(matrix[row, col]);
                }
                result.Add(sb.ToString());
            }

            return result;
        }
    }
}
