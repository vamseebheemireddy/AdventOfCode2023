// EngineSchematic

// Parse engine schematic file and find sum of valid parts

namespace Puzzle3;

using UtilityExtensions;
using System.Text.RegularExpressions;

public record struct Part(int Line, int Position, int PartNumber);

public class EngineSchematic
{
    private string[] schematic;

    private List<Part> parts;

    public EngineSchematic(string schematicFilePath)
    {
        schematic = (string[])File.ReadLines(schematicFilePath);
        parts = new List<Part>();
    }

    public int FindValidParts()
    {

    }

}

class Program
{
    static void Main(string[] args)
    {
        var engine = new EngineSchematic(args[0]);
        var partTotal = engine.FindValidParts();

        Console.WriteLine($@"Sum of valid part numbers is: {partTotal}");
    }
}
