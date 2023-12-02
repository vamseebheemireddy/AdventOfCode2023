// CalibrationParser

// Parse calibration file to return sum of codes

namespace Puzzle2;

using System.Text.RegularExpressions;

#pragma warning disable SYSLIB1045 // Convert to 'GeneratedRegexAttribute'.

public record struct Draw(int Red, int Green, int Blue);

public record struct Game(int Id, List<Draw> Draws);
public class CubeGame
{
    private string GameFilePath { get; set; }
    private List<Game> Games { get; set; }

    public CubeGame(string gameFilePath)
    {
        GameFilePath = gameFilePath;
        Games = new List<Game>();
    }

    public void ParseGameFile()
    {
        foreach(var line in File.ReadLines(GameFilePath))
        {
            int id;
            var gameText = line.Split(":");
            if (!gameText[0].Contains("Game"))
            {
                throw new Exception($"Input file syntax error. Line: {line}");
            } 
            else
            {
                id = Convert.ToInt32(gameText[0].Replace("Game ", ""));
            }

            var draws = ParseDraws(gameText[1]);
            Games.Add(new Game(id, draws));
        }
    }

    private static List<Draw> ParseDraws(string gameText)
    {
        var draws = new List<Draw>();
        var drawTexts = gameText.Split(";");

        foreach (var drawText in drawTexts)
        {
            var draw = new Draw(Red: 0, Green: 0, Blue: 0);
            var colourTexts = drawText.Split(",");

            foreach (var colourText in colourTexts)
            {
                var colour = Regex.Match(colourText, @"red|green|blue").Value;
                switch (colour)
                {
                    case "red": 
                        draw.Red = Convert.ToInt32(Regex.Match(colourText, @"\d+").Value);
                        break;
                    case "green": 
                        draw.Green = Convert.ToInt32(Regex.Match(colourText, @"\d+").Value);
                        break;
                    case "blue": 
                        draw.Blue = Convert.ToInt32(Regex.Match(colourText, @"\d+").Value);
                        break;
                    default:
                        throw new Exception($@"Failed to find number of colour {colour} in entry {colourText}.");
                }
            }
            draws.Add(draw);
        }

        return draws;
    }

    public List<int> FindValidGames(Draw totalCubes)
    {
        var validGames = new List<int>();
        foreach (var game in Games)
        {
            if (AllDrawsValid(game.Draws, totalCubes))
                validGames.Add(game.Id);
        }
        return validGames;
    }

    private static bool AllDrawsValid(List<Draw> draws, Draw totalCubes)
    {
        foreach (var draw in draws)
        {
            if (draw.Red > totalCubes.Red || draw.Green > totalCubes.Green || draw.Blue > totalCubes.Blue)
                return false;
        }
        return true;
    }
}

class Program
{
    static void Main(string[] args)
    {
        var totalCubes = new Draw(Red: Convert.ToInt32(args[1]), Green: Convert.ToInt32(args[2]), Blue: Convert.ToInt32(args[3]));
        var cubeGame = new CubeGame(args[0]);
        cubeGame.ParseGameFile();
        var validGames = cubeGame.FindValidGames(totalCubes);
        Console.WriteLine($@"Sum of valid game IDs is: {validGames.Sum()}");
    }
}

#pragma warning restore SYSLIB1045 // Convert to 'GeneratedRegexAttribute'.