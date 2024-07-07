// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

var inputFile = "input.csv";
var outputFile = "output.csv";
var output2File = "output2.csv";

// 入力ファイル作成
// MakeInputFile(inputFile);

// 入力ファイル読み取り
var inputCsv = ReadCsv(inputFile);
var input = inputCsv.ToArray();

// input = Enumerable.Range(0, 100)
//     .Select(value => new Complex(value, 0))
//     .ToArray();

// FFT
FFT.Convert(input);

// 変換後ファイル書き込み
WriteCsv(outputFile, input);

Console.WriteLine("convert");

// IFFT
FFT.Inverse(input);

// 逆変換後ファイル書き込み
WriteCsv(output2File, input);

Console.WriteLine("inverse");

/// <summary>
/// ファイル読み取り
/// </summary>
/// <param name="filePath"></param>
/// <returns></returns>
static IReadOnlyList<Complex> ReadCsv(string filePath)
{
    using var reader = new StreamReader(filePath);

    string csvLine;
    string[] columns;
    List<Complex> result = [];

    while (false == reader.EndOfStream)
    {
        try
        {
            csvLine = reader.ReadLine() ?? string.Empty;
            columns = csvLine.Split(',');

            result.Add(
                new Complex
                {
                    Real = double.Parse(columns[1])
                });

        }
        catch (Exception e)
        {
            System.Diagnostics.Debug.WriteLine(e);
        }
    }

    return result;
}

/// <summary>
/// ファイル書き込み
/// </summary>
/// <param name="filePath"></param>
/// <param name="output"></param>
static void WriteCsv(string filePath, IEnumerable<Complex> output)
{
    var csv = output
        .Select((value, index) => $"{index},{value.Real},{value.Imaginary}");

    File.WriteAllLines(filePath, csv);
}

/// <summary>
/// 入力ファイル作成
/// </summary>
/// <param name="filePath"></param>
static void MakeInputFile(string filePath)
{
    var length = 100;

    using var writer = new StreamWriter(filePath);

    for (int i = 0; i < length; i++)
    {
        // 波形
        var y = ToSinWave(1, i, length);
        y += ToSinWave(16, i, length);
        y += ToSinWave(4, i, length);

        writer.WriteLine($"{i},{y}");
    }
}

/// <summary>
/// sin(2πωt/n)
/// </summary>
/// <param name="omega"></param>
/// <returns></returns>
static double ToSinWave(double omega, double t, int length)
{
    return Math.Sin(2 * Math.PI * omega * t / length);
}
