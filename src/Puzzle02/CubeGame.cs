// CalibrationParser

// Parse calibration file to return sum of codes

namespace Puzzle2;

using System.Text.RegularExpressions;

public readonly record struct Draw(uint Red, uint Green, uint Blue);

public record struct Game(uint Id, List<Draw> Draws);
public class CubeGame
{
    private string GameFilePath { get; set; }
    private List<Game> Games { get; set; };

    public CubeGame(string gameFilePath)
    {
        GameFilePath = gameFilePath;
        Games = new List<Game>();
    }

    public void ParseGameFile()
    {
        foreach(var line in File.ReadLines(GameFilePath))
        {
            var gameText = line.Split(":");
            if (!gameText[0].Contains("Game"))
            {
                throw new Exception($"Input file syntax error. Line: {line}");
            } 
            else
            {
                var id = Convert.ToUInt32(gameText[0].Replace("Game ", ""));
            }

            var draws = ParseDraws(gameText[1]);
        }
    }

    private List<Draw> ParseDraws(string gameText)
    {
        var draws = new List<Draw>();
        var drawTexts = gameText.Split(";");
        foreach (var drawText in drawTexts)
        {
            var colourTexts = drawText.Split(",");
            foreach (var colourText in colourTexts)
            {
                switch colourText
            }
        }
        return draws;
        
    }
}

class Program
{
    static void Main(string[] args)
    {
        var gameFile = new CubeGame(args[0]);

    }
}
