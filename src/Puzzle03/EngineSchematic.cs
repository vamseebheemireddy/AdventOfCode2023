// EngineSchematic

// Parse engine schematic file and find sum of valid parts

namespace Puzzle03;

using UtilityExtensions;
using System.Text.RegularExpressions;

#pragma warning disable SYSLIB1045 // Convert to 'GeneratedRegexAttribute'.

public record struct Part(int Line, int StartPosition, int EndPosition, int PartNumber, List<Gear> Gears);

public record struct Gear(int Line, int Position);

public class EngineSchematic
{
    private readonly string[] Schematic;

    private readonly List<Part> Parts;

    public EngineSchematic(string schematicFilePath)
    {
        Schematic = File.ReadAllLines(schematicFilePath);
        Parts = new List<Part>();
    }

    public int FindValidParts()
    {
        foreach(var (line, index) in Schematic.Enumerated())
        {
            var partMatch = Regex.Match(line, @"\d+");

            while (partMatch.Success)
            {
                var newPart = new Part(index, partMatch.Index, partMatch.Index + partMatch.Length - 1, Convert.ToInt32(partMatch.Value), new List<Gear>());
                if (IsValidPart(newPart))
                {
                    newPart.Gears = FindGears(newPart);
                    Parts.Add(newPart);
                }
                partMatch = partMatch.NextMatch();
            }
        }

        var partsTotal = Parts.Select(x => x.PartNumber).Sum();
        return partsTotal;
    }

    private List<Gear> FindGears(Part part)
    {
        var gears = new List<Gear>();
        var line = Schematic[part.Line];

        if (part.StartPosition > 0)
            if (line.Substring(part.StartPosition - 1, 1) == "*")
                gears.Add(new Gear(part.Line, part.StartPosition - 1));

        if (part.EndPosition < line.Length - 1)
            if (line.Substring(part.EndPosition + 1, 1) == "*")
                gears.Add(new Gear(part.Line, part.EndPosition + 1));

        var leftEdge = part.StartPosition - 1 < 0 ? 0 : part.StartPosition - 1;
        var rightEdge = part.EndPosition + 1 > line.Length - 1 ? line.Length - 1 : part.EndPosition + 1;

        if (part.Line > 0)
            foreach(var (character, index) in Schematic[part.Line - 1].Substring(leftEdge, rightEdge - leftEdge + 1).Enumerated())
            {
                if (character == '*')
                    gears.Add(new Gear(part.Line - 1, index + leftEdge));
            }
            
        if (part.Line < Schematic.Length - 1)
            foreach(var (character, index) in Schematic[part.Line + 1].Substring(leftEdge, rightEdge - leftEdge + 1).Enumerated())
            {
                if (character == '*')
                    gears.Add(new Gear(part.Line + 1, index + leftEdge));
            }

        return gears;
    }

    private bool IsValidPart(Part part)
    {
        var line = Schematic[part.Line];

        if (part.StartPosition > 0)
            if (line.Substring(part.StartPosition - 1, 1) != ".")
                return true;

        if (part.EndPosition < line.Length - 1)
            if (line.Substring(part.EndPosition + 1, 1) != ".")
                return true;

        var leftEdge = part.StartPosition - 1 < 0 ? 0 : part.StartPosition - 1;
        var rightEdge = part.EndPosition + 1 > line.Length - 1 ? line.Length - 1 : part.EndPosition + 1;
        var noSymbol = new string ('.', rightEdge - leftEdge + 1);

        if (part.Line > 0)
            if (!Equals(Schematic[part.Line - 1].Substring(leftEdge, rightEdge - leftEdge + 1), noSymbol))
                return true;

        if (part.Line < Schematic.Length - 1)
            if (!Equals(Schematic[part.Line + 1].Substring(leftEdge, rightEdge - leftEdge + 1), noSymbol))
                return true;

        return false;
    }

    public List<Gear> GetActiveGears()
    {
        var activeGears = new List<Gear>();
        return activeGears;
    }
}

class Program
{
    static void Main(string[] args)
    {
        var engine = new EngineSchematic(args[0]);

        var partTotal = engine.FindValidParts();
        Console.WriteLine($@"Sum of valid part numbers is: {partTotal}");

        var activeGears = engine.GetActiveGears();
        Console.WriteLine($@"Sum of gear ratios is: {gearRatioTotal}");
    }
}

#pragma warning restore SYSLIB1045 // Convert to 'GeneratedRegexAttribute'.