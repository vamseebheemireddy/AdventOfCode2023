// CalibrationParser

// Parse calibration file to return sum of codes

namespace Puzzle1;

public class CalibrationParser
{
    private string CalibrationFilePath { get; set; }

    public CalibrationParser(string calibrationFilePath)
    {
        CalibrationFilePath = calibrationFilePath;
    }

    public void ParseCalibrationFile()
    {
        var calibrationValue = 0;
        var lineDigits = new List<int>();
        foreach(var line in File.ReadLines(CalibrationFilePath))
        {
            foreach(char character in line)
            {
                if(Char.IsDigit(character))
                    lineDigits.Add( (int)Char.GetNumericValue(character) );
            }
            if (lineDigits.Count > 0)
                calibrationValue += lineDigits.Last() + 10 * lineDigits.First();
            lineDigits.Clear();
        }
        Console.WriteLine($@"Calibration file result is: {calibrationValue}");
    }
}

class Program
{
    static void Main(string[] args)
    {
        var calibrationFile = new CalibrationParser(args[0]);
        calibrationFile.ParseCalibrationFile();
    }
}
