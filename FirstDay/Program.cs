const string rootPath = @"C:\Users\Igor\RiderProjects\Aoc2023\FirstDay";

#region WeatherMachine

var input = File.ReadAllText(@$"{rootPath}\input.txt");
var output = 0;
foreach (var parenthesis in input) 
{
    if (parenthesis == '(')
    {
        output++;
    }

    if (parenthesis == ')')
    {
        output--;
    }
}
//part 1
Console.WriteLine(output);

output = 0;
var position = 1;
foreach (var parenthesis in input) 
{
    if (parenthesis == '(')
    {
        output++;
    }
    if (parenthesis == ')')
    {
        output--;
    }
    if (output < 0)
    {
        //part 2
        Console.WriteLine(position);
        break;
    }
    position ++;
}

#endregion

var input2 = File.ReadLines($@"{rootPath}\input2.txt");

var sum = input2.Select(x =>
{
    string result = new string(x.Where(char.IsDigit).ToArray());
    return int.Parse($"{result.First()}{result.Last()}");
}).Sum();
//part 1
Console.WriteLine(sum);

var sum2 = 0;
foreach (var s in input2)
{
    var numbers = new List<int>();
    for (int i = 0; i < s.Length; i++)
    {
        var local = s.Substring(i);
        var n = GetNumber(local);
        if (n is not null)
        {
            numbers.Add(n.Value);
        }
    }

    if (numbers.Count > 0)
    {
        var resultNumber = int.Parse($"{numbers.First()}{numbers.Last()}");   
        sum2 += resultNumber;
    }

}
// part 2
Console.WriteLine(sum2);

const string one = "one";
const string two = "two";
const string three = "three";
const string four = "four";
const string five = "five";
const string six = "six";
const string seven = "seven";
const string eight = "eight";
const string nine = "nine";
int? GetNumber(string line)
{
    char? localN = line.FirstOrDefault();
    if (localN is not null && char.IsDigit(localN.Value))
    {
        return localN - '0';
    }
    
    if (line.StartsWith(one))
        return 1;
    
    if (line.StartsWith(two))
        return 2;

    if (line.StartsWith(three))
        return 3;

    if (line.StartsWith(four))
        return 4;

    if (line.StartsWith(five))
        return 5;

    if (line.StartsWith(six))
        return 6;

    if (line.StartsWith(seven))
        return 7;

    if (line.StartsWith(eight))
        return 8;

    if (line.StartsWith(nine))
        return 9;
    
    return null;
}