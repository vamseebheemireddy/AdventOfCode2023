﻿// EngineSchematic

// Parse engine schematic file and find sum of valid parts

namespace Puzzle03;

using UtilityExtensions;
using System.Text.RegularExpressions;

#pragma warning disable SYSLIB1045 // Convert to 'GeneratedRegexAttribute'.

public record struct Part(int Line, int StartPosition, int EndPosition, int PartNumber);

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
                var newPart = new Part(index, partMatch.Index, partMatch.Index + partMatch.Length - 1, Convert.ToInt32(partMatch.Value));
                if (IsValidPart(newPart))
                    Parts.Add(newPart);
                partMatch = partMatch.NextMatch();
            }
        }

        var partsTotal = Parts.Select(x => x.PartNumber).Sum();
        return partsTotal;
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

#pragma warning restore SYSLIB1045 // Convert to 'GeneratedRegexAttribute'.