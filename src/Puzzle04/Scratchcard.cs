// Scratchcard

// Parse scratchcard values and find total ppoints

namespace Puzzle04;

using UtilityExtensions;
using System.Text.RegularExpressions;

#pragma warning disable SYSLIB1045 // Convert to 'GeneratedRegexAttribute'.

public class Scratchcard
{
    public readonly int Id;
    public int Copies;
    public readonly int Matches;

    public Scratchcard(int id, int matches)
    {
        Id = id;
        Copies = 1;
        Matches = matches;
    }

    public static List<Scratchcard> ParseScratchcardFile(string scratchcardsFilePath)
    {
        var cards = new List<Scratchcard>();

        foreach (var (line, id) in File.ReadAllLines(scratchcardsFilePath).Enumerated())
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

            var matches = 0;
            foreach (var winningNumber in  winningNumbers)
                if (playerNumbers.Contains(winningNumber))
                    matches += 1;

            cards.Add(new Scratchcard(id, matches));
        }

        return cards;
    }
}

class Program
{
    static void Main(string[] args)
    {
        var cards = Scratchcard.ParseScratchcardFile(args[0]);
        var totalPoints = cards.Select(x => x.Matches == 0 ? 0 : 1 << x.Matches - 1).Sum();
        Console.WriteLine($@"Sum of scratchcard points is: {totalPoints}");

        foreach(var (card, id) in cards.Enumerated())
        {
            var startCopy = id < cards.Count - 1 ? id + 1 : -1;
            var endCopy = id + card.Matches < cards.Count ? id + card.Matches : cards.Count - 1;
            if (startCopy != -1)
                foreach (var copyId in Enumerable.Range(startCopy, endCopy - startCopy + 1))
                    cards[copyId].Copies += card.Copies;
        }
        var totalCards = cards.Select(x => x.Copies).Sum();
        Console.WriteLine($@"Total scratchcard copies is: {totalCards}");
    }
}

#pragma warning restore SYSLIB1045 // Convert to 'GeneratedRegexAttribute'.