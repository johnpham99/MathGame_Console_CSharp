namespace MathGame.Models;

internal class GameInfo
{
    public DateTime Date { get; set; }
    
    public int Score { get; set; }
    
    public GameType Type { get; set; }
    
    public Difficulty Difficulty { get; set; }
    
    public double SecondsElapsed { get; set; }
}

internal enum GameType
{
    Addition,
    Subtraction,
    Multiplication,
    Division,
    Random
}

internal enum Difficulty
{
    Easy,
    Normal,
    Hard
}