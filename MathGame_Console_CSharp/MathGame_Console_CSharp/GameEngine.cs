using System.Diagnostics;
using System.Text.RegularExpressions;
using MathGame.Models;
using Microsoft.CognitiveServices.Speech;
using Microsoft.Extensions.Configuration;

namespace MathGame;

internal class GameEngine
{
 internal async Task Game(GameType gameType, Difficulty difficulty, bool useSpeech)
 {
  // Game 
  var score = 0;
  var random = new Random();
  bool randomGame = gameType == GameType.Random;

  var stopwatch = Stopwatch.StartNew();
  for (int i = 0; i < 5; i++)
  {
   if (randomGame)
   {
    gameType = (GameType)random.Next(0, 4);
   }
   
   var numbers = Helpers.GetNumbers(gameType, difficulty);
   var value1 = numbers[0];
   var value2 = numbers[1];
   
   string operatorString = gameType switch
   {
    GameType.Addition => "+",
    GameType.Subtraction => "-",
    GameType.Multiplication => "*",
    GameType.Division => "/",
    _ => throw new ArgumentException($"Invalid game type {gameType}.")
   };

   int correctAnswer = gameType switch
   {
    GameType.Addition => value1 + value2,
    GameType.Subtraction => value1 - value2,
    GameType.Multiplication => value1 * value2,
    GameType.Division => value1 / value2,
    _ => throw new ArgumentException($"Invalid game type {gameType}.")
   };
   
   Console.Clear();
   Console.WriteLine($"{value1} {operatorString} {value2} = ");

   var inputTask = Helpers.GetInput(useSpeech);
   string input = inputTask.Result; 
   
   if (int.Parse(input) == correctAnswer)
   {
    Console.WriteLine("Correct! Press any key to continue.");
    score++;
   }
   else
   {
    Console.WriteLine($"Incorrect. The correct answer was {correctAnswer}. Press any key to continue.");
   }
   
   if (i == 4)
   {
    stopwatch.Stop();
   }
   
   Console.ReadKey();
  }
  Console.Clear();
  Console.WriteLine($"Game over. Your final score is {score}. This took {stopwatch.Elapsed.TotalSeconds} seconds.");
  Console.WriteLine("Press any key to return to the main menu.");
  Console.ReadKey();
  Helpers.AddToHistory(score, randomGame ? GameType.Random : gameType, difficulty, stopwatch.Elapsed.TotalSeconds);
 }
}