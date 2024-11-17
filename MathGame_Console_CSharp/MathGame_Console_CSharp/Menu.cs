using MathGame.Models;

namespace MathGame;

internal class Menu
{
    private GameEngine gameClass = new();

    internal void ShowMenu(string name, DateTime date)
    {
        Console.Clear();
        Console.WriteLine($"Hello {name}. It's {date}. This is a math game.");
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
        Console.WriteLine("\n");

        var gameRunning = true;

        while (gameRunning)
        {
            Console.Clear();
            Console.WriteLine(@$"What game would you like to play today? Choose from the option below:
V - View Previous Games
A - Addition
S - Subtraction
M - Multiplication
D - Division
R - Random
Q - Quit the program");
            Console.WriteLine("---------------------------------------------");

            var gameSelected = Console.ReadLine();

            switch (gameSelected.Trim().ToLower())
            {
                case "v":
                    Helpers.PrintGames();
                    break;
                case "a":
                    ChooseDifficulty(GameType.Addition);
                    break;
                case "s":
                    ChooseDifficulty(GameType.Subtraction);
                    break;
                case "m":
                    ChooseDifficulty(GameType.Multiplication);
                    break;
                case "d":
                    ChooseDifficulty(GameType.Division);
                    break;
                case "r":
                    ChooseDifficulty(GameType.Random);
                    break;
                case "q":
                    Console.WriteLine("Goodbye!");
                    gameRunning = false;
                    break;
                default:
                    Console.WriteLine("Please choose a valid option");
                    break;
            }
        }
        
    }

    private void ChooseDifficulty(GameType gameType)
    {
        Console.Clear();
        Console.WriteLine($"""
                           Choose a difficulty:
                           E - Easy
                           N - Normal
                           H - Hard
                           """);
        Console.WriteLine("---------------------------------------------");
        
        var input = Console.ReadLine();
        while (string.IsNullOrEmpty(input) || 
               (input.Trim().ToLower() != "e" && 
                input.Trim().ToLower() != "n" && 
                input.Trim().ToLower() != "h"))
        {
            Console.WriteLine("Please choose a valid difficulty");
            input = Console.ReadLine();
        }

        Difficulty difficulty =  input switch
        {
            "e" => Difficulty.Easy,
            "n" => Difficulty.Normal,
            "h" => Difficulty.Hard,
            _ => throw new ArgumentException("Invalid difficulty.")
        };
        
        VoiceOption(gameType, difficulty);
    }

    private async void VoiceOption(GameType gameType, Difficulty difficulty)
    {
        Console.Clear();
        Console.WriteLine($"""
                           Would you like to try using your voice? (Y/N):
                           """);
        Console.WriteLine("---------------------------------------------");
        
        var input = Console.ReadLine();
        while (string.IsNullOrEmpty(input) ||
               (input.Trim().ToLower() != "y" &&
                input.Trim().ToLower() != "n"))
        {
            Console.WriteLine("Please enter 'Y' or 'N'");
            input = Console.ReadLine();
        }

        await gameClass.Game(gameType, difficulty, input.Trim().ToLower() == "y" ? true : false);
    }
}