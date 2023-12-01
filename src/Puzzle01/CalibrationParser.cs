// CalibrationParser

// Parse calibration file to return sum of codes

namespace Puzzle1;

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
        var lineDigits = new List<int>();

        foreach(var line in File.ReadLines(CalibrationFilePath))
        {
            var parsedLine = (mode == ParsingMode.Semantic) ? ParseLineSemanticly(line) : line;

            foreach(char character in parsedLine)
            {
                if(Char.IsDigit(character))
                    lineDigits.Add( (int)Char.GetNumericValue(character) );
            }
            if (lineDigits.Count > 0)
                calibrationValue += lineDigits.Last() + 10 * lineDigits.First();
            lineDigits.Clear();
        }

        return calibrationValue;
    }

    public static string ParseLineSemanticly(string line)
    {
        var regexSearchString = "one|two|three|four|five|six|seven|eight|nine";
        var regexSearch = new Regex(regexSearchString);
        Match match;
        do
        {
            match = Regex.Match(line, regexSearchString);
            line = regexSearch.Replace(line, ReplaceWordWithNumber, 1);
        } while (match.Success);
        return line;
    }

    public static string ReplaceWordWithNumber(Match match)
    {
        var number = match.Value switch
        {
            "one" => "1",
            "two" => "2",
            "three" => "3",
            "four" => "4",
            "five" => "5",
            "six" => "6",
            "seven" => "7",
            "eight" => "8",
            "nine" => "9",
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
