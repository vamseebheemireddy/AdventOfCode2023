// Scratchcard

// Parse scratchcard values and find total ppoints

namespace Puzzle04;

using UtilityExtensions;
using System.Text.RegularExpressions;

#pragma warning disable SYSLIB1045 // Convert to 'GeneratedRegexAttribute'.

public class Scratchcard
{
    private List<int> PlayerNumbers;
    private List<int> WinningNumbers;
    private int Points;

    public Scratchcard(List<int> playerNumbers, List<int> winningNumbers, int points)
    {
        PlayerNumbers = playerNumbers;
        WinningNumbers = winningNumbers;
        Points = points;
    }

    public static List<Scratchcard> ParseScratchcardFile(string scratchcardsFilePath)
    {
        var cards = new List<Scratchcard>();

        foreach (var line in File.ReadLines(scratchcardsFilePath))
        {
            var allNumbersText = line.Split(":")[1];
            var winningNumbersText = allNumbersText.Split("|")[0];
            var playerNumbersText = allNumbersText.Split("|")[1];
            var winningNumbers = new List<int>();
            var playerNumbers = new List<int>();

            var winningNumbersMatch = Regex.Match(winningNumbersText, @"\d+");
            while(winningNumbersMatch.Success)
            {
                winningNumbers.Add(Convert.ToInt32(winningNumbersMatch.Value));
                winningNumbersMatch = winningNumbersMatch.NextMatch();
            }

            var playerNumbersMatch = Regex.Match(playerNumbersText, @"\d+");
            while(playerNumbersMatch.Success)
            {
                playerNumbers.Add(Convert.ToInt32(playerNumbersMatch.Value));
                playerNumbersMatch = playerNumbersMatch.NextMatch();
            }

            cards.Add(new Scratchcard(playerNumbers, winningNumbers, 0));
        }

        return cards;
    }

    public int GetPoints()
    {
        Points = 0;
        foreach (var winningNumber in WinningNumbers)
        {
            if (PlayerNumbers.Contains(winningNumber))
            {
                if (Points == 0)
                    Points = 1;
                else
                    Points *= 2; 
            }
        }
        return Points;
    }
}

class Program
{
    static void Main(string[] args)
    {
        var cards = Scratchcard.ParseScratchcardFile(args[0]);
        var totalPoints = cards.Select(x => x.GetPoints()).Sum();
        Console.WriteLine($@"Sum of scratchcard points is: {totalPoints}");
    }
}

#pragma warning restore SYSLIB1045 // Convert to 'GeneratedRegexAttribute'.