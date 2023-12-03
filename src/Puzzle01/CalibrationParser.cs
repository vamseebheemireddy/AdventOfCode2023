// CalibrationParser

// Parse calibration file to return sum of codes

namespace Puzzle01;

using System.Text.RegularExpressions;
public enum ParsingMode
{
    Numeric,
    Semantic,
}

public class CalibrationParser
{
    private string CalibrationFilePath { get; set; }

    public CalibrationParser(string calibrationFilePath)
    {
        CalibrationFilePath = calibrationFilePath;
    }

    public int ParseCalibrationFile(ParsingMode mode)
    {
        var calibrationValue = 0;
        var searchPattern = @"one|two|three|four|five|six|seven|eight|nine|\d";
        var reverseSearchPattern = @"eno|owt|eerht|ruof|evif|xis|neves|thgie|enin|\d";

        if (mode == ParsingMode.Numeric)
        {
            searchPattern = @"\d";
            reverseSearchPattern = @"\d";
        }

        foreach(var line in File.ReadLines(CalibrationFilePath))
        {
            var reversedLine = new string(line.Reverse().ToArray());
            var firstMatch = Regex.Match(line, searchPattern);
            var lastMatch = Regex.Match(reversedLine, reverseSearchPattern);
            if (firstMatch.Success && lastMatch.Success)
                calibrationValue += 10 * ReplaceWordWithNumber(firstMatch) + ReplaceWordWithNumber(lastMatch);
        }

        return calibrationValue;
    }

    public static int ReplaceWordWithNumber(Match match)
    {
        var number = match.Value switch
        {
            "one" or "eno" or "1" => 1,
            "two" or "owt" or "2" => 2,
            "three" or "eerht" or "3" => 3,
            "four" or "ruof" or "4" => 4,
            "five" or "evif" or "5" => 5,
            "six" or "xis" or "6" => 6,
            "seven" or "neves" or "7" => 7,
            "eight" or "thgie" or "8" => 8,
            "nine" or "enin" or "9" => 9,
            _ => throw new Exception("Error with string matching")
        };
        return number;
    }
}

class Program
{
    static void Main(string[] args)
    {
        var calibrationFile = new CalibrationParser(args[0]);

        var calibrationValueNumeric = calibrationFile.ParseCalibrationFile(ParsingMode.Numeric);
        var calibrationValueSemantic = calibrationFile.ParseCalibrationFile(ParsingMode.Semantic);

        Console.WriteLine($@"Calibration file result  with numeric parsing is: {calibrationValueNumeric}");
        Console.WriteLine($@"Calibration file result  with semantic parsing is: {calibrationValueSemantic}");
    }
}
