using LanguageExt;
using static LanguageExt.Prelude;

//part 1
const string rootPath = @"C:\Users\Igor\RiderProjects\Aoc2023\SecondDay";
var lines = toList(File.ReadAllLines($@"{rootPath}\input1.txt"));

var maxColorsValues = new HashMap<string, int>(new[] { ("red", 12), ("green", 13), ("blue", 14) } );

var sum = lines.Map(x =>
{
    var records = x.Split(':');
    var isNotValid = records[1].Split(';').Any(y =>
    {
        return y.Split(',').Any(z =>
        {
            var values = z.Trim().Split(' ');
            return int.Parse(values[0]) > maxColorsValues[values[1]];
        });
    });
    return isNotValid ? 0 : int.Parse(records[0].Split(' ').Last());
}).Sum();

Console.WriteLine(sum);

// part 2
var max = lines.Map(x =>
{
    var records = x.Split(':');
    var localMax = new Dictionary<string, int>()
    {
        { "red", 1 },
        { "green", 1 },
        { "blue", 1 }
    };
    foreach (var pair in records[1].Split(';'))
    {
        foreach (var z in pair.Split(','))
        {
            var values = z.Trim().Split(' ');
            if (int.Parse(values[0]) > localMax[values[1]])
            {
                localMax[values[1]] = int.Parse(values[0]);
            }
        };
    }
    return localMax.Select(x => x.Value).Aggregate((current, next) => current * next);
}).Sum();

Console.WriteLine(max);