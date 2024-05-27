using System;
using System.Collections.Generic;
using static System.Random;
using System.Text;
using System.Runtime.InteropServices.Marshalling;

namespace HangmanGame
{
    internal class Hang
    {
        private static void printHangman(int wrong)
        {
            //based on number of wrong guesses, the hangman is made more and more
            //stand is built on 0 wrong guesses
            if (wrong == 0)
            {
                Console.WriteLine("\n+---+");
                Console.WriteLine("    |");
                Console.WriteLine("    |");
                Console.WriteLine("    |");
                Console.WriteLine("   ===");
            }

            else if (wrong == 1)
            {
                Console.WriteLine("\n+---+");
                Console.WriteLine("O   |");
                Console.WriteLine("    |");
                Console.WriteLine("    |");
                Console.WriteLine("   ===");
            }

            else if (wrong == 2)
            {
                Console.WriteLine("\n+---+");
                Console.WriteLine("O   |");
                Console.WriteLine("|   |");
                Console.WriteLine("    |");
                Console.WriteLine("   ===");
            }

            else if (wrong == 3)
            {
                Console.WriteLine("\n+---+");
                Console.WriteLine(" O  |");
                Console.WriteLine("/|  |");
                Console.WriteLine("    |");
                Console.WriteLine("   ===");
            }

            else if (wrong == 4)
            {
                Console.WriteLine("\n+---+");
                Console.WriteLine(" O  |");
                Console.WriteLine("/|\\ |");
                Console.WriteLine("    |");
                Console.WriteLine("   ===");
            }

            else if (wrong == 5)
            {
                Console.WriteLine("\n+---+");
                Console.WriteLine(" O  |");
                Console.WriteLine("/|\\ |");
                Console.WriteLine("/   |");
                Console.WriteLine("   ===");
            }

            else if (wrong == 6)
            {
                //6 is the limit for wrong guesses
                Console.WriteLine("\n+---+");
                Console.WriteLine(" O   |");
                Console.WriteLine("/|\\  |");
                Console.WriteLine("/ \\  |");
                Console.WriteLine("    ===");
            }
        }

        private static int printWord(List<char> guessedLetters, String randomWord)
        {
            int counter = 0;
            int correctLetters = 0;
            //spaces out lines for better presentation
            Console.Write("\r\n");
            foreach (char c in randomWord)
            {
                //if the char c is in guessedLetters, it'll be written and correctletters+1
                if (guessedLetters.Contains(c))
                {
                    Console.Write(c + " ");
                    correctLetters += 1;
                }
                else
                {
                    //else a blank is printed
                    Console.Write("  ");
                }
                counter += 1;
            }
            //Console.Write("\r\n");
            return correctLetters;
        }

        private static void printLines(String randomWord)
        {
            Console.Write("\r");
            foreach (char c in randomWord)
            {
                //puts overline on the line bellow to show the amount of letters in word
                Console.OutputEncoding = System.Text.Encoding.Unicode;
                Console.Write("\u0305 ");
            }
        }

        static void Main(string[] args)
        {
            //welcome message
            Console.WriteLine("Welcome to hangman :)");
            Console.WriteLine("-----------------------------------------");
            //chooses a random word in the list between 0 - the number of strings in list
            Random random = new Random();
            List<string> wordDictionary = new List<string> { "sunflower", "house", "build", "strong", "water", "hi", "run", "like", "person", "powerful", "waves", "sprint" };
            int index = random.Next(wordDictionary.Count);
            String randomWord = wordDictionary[index];

            foreach (char x in randomWord)
            {
                Console.Write("_ ");
            }

            int lengthOfWordToGuess = randomWord.Length;
            int amountOfTimesWrong = 0;
            List<char> currentLettersGuessed = new List<char>();
            int currentLettersRight = 0;
            //game continues so long as wrong guesses are under 6 and the word hasn't been guessed yet
            while (amountOfTimesWrong != 6 && currentLettersRight != lengthOfWordToGuess)
            {
                //displays the letters guessed to user so repeats don't happen
                Console.Write("\nLetters guessed so far: ");
                foreach (char letter in currentLettersGuessed)
                {
                    Console.Write(letter + " ");
                }
                // Prompt user for input
                Console.Write("\nGuess a letter: ");
                char letterGuessed = Console.ReadLine()[0];
                // Check if that letter has already been guessed
                if (currentLettersGuessed.Contains(letterGuessed))
                {
                    //if yes, tells user and 
                    Console.Write("\r\n You have already guessed this letter");
                    printHangman(amountOfTimesWrong);
                    currentLettersRight = printWord(currentLettersGuessed, randomWord);
                    printLines(randomWord);
                }

                else
                {
                    // Check if letter is in randomWord
                    bool right = false;
                    for (int i = 0; i < randomWord.Length; i++) { if (letterGuessed == randomWord[i]) { right = true; } }

                    // User is right
                    if (right)
                    {
                        printHangman(amountOfTimesWrong);
                        // Print word with updated letter
                        currentLettersGuessed.Add(letterGuessed);
                        currentLettersRight = printWord(currentLettersGuessed, randomWord);
                        Console.Write("\r\n");
                        printLines(randomWord);
                    }
                    // User was wrong 
                    else
                    {
                        //if a none-alphabet character is put, it will be counted as a turn and listed as an incorrect guess
                        //adds to wrong count
                        amountOfTimesWrong += 1;
                        //adds to the guessed letter list
                        currentLettersGuessed.Add(letterGuessed);
                        // Update the hangman to show how many tries user has left
                        printHangman(amountOfTimesWrong);
                        // Prints word
                        currentLettersRight = printWord(currentLettersGuessed, randomWord);
                        Console.Write("\r\n");
                        printLines(randomWord);
                    }
                }
            }
            Console.WriteLine("\r\nGame is over! Good try and thanks for playing!");
            
        }
    }
}