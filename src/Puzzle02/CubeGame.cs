// CalibrationParser

// Parse calibration file to return sum of codes

namespace Puzzle2;

using System.Text.RegularExpressions;
public class CubeGame
{
    private string GameFilePath { get; set; }

    public CubeGame(string gameFilePath)
    {
        GameFilePath = gameFilePath;
    }
}

class Program
{
    static void Main(string[] args)
    {
        var gameFile = new CubeGame(args[0]);

    }
}
