const string rootPath = @"C:\Users\Igor\RiderProjects\Aoc2023\ThirdDay";

var sum = 0;
using (var reader = new StreamReader($"{rootPath}/input.txt"))
{
    var lines = new string?[3]
    {
        null,
        null,
        null
    };
    string? currentLine;
    do
    {
        currentLine = reader.ReadLine();
        lines[0] = lines[1];
        lines[1] = lines[2];
        lines[2] = currentLine;

        sum = EnumerateLine(lines, sum);
    } while (currentLine is not null);
}

Console.WriteLine(sum);

int EnumerateLine(string?[] area, int sum)
{
    if (area[1] is null)
    {
        return sum;
    }
    var i = 0;
    var currentLine = area[1]!.GetEnumerator();
    while (currentLine.MoveNext())
    {
        if (char.IsDigit(currentLine.Current))
        {
            var digitBuffer = GetDigitBuffer(ref i, currentLine);
            if (CheckNumberArea(digitBuffer, area))
            {
                sum += digitBuffer.Number;
            }
        }
        i++;
    }
    return sum;
}

DigitBuffer GetDigitBuffer(ref int startMainIndex, CharEnumerator currentLine)
{
    var startIndexLocal = startMainIndex;
    var endIndex = startMainIndex;
    var number = (currentLine.Current - '0').ToString();
    while (currentLine.MoveNext() && char.IsDigit(currentLine.Current))
    {
        endIndex++;
        startMainIndex++;
        number += currentLine.Current - '0';
    }
    var digitBuffer = new DigitBuffer(int.Parse(number), startIndexLocal, endIndex);
    startMainIndex++;
    return digitBuffer;
}

bool CheckNumberArea(DigitBuffer digitBuffer, string?[] area)
{
    bool leftShift = digitBuffer.Start - 1 >= 0;
    bool rightShift = digitBuffer.End + 1  < area[1]!.Length;
    if (rightShift)
    {
        var point = area[1]![digitBuffer.End + 1];
        if (Check(point))
            return true;
    }
    if (leftShift)
    {
        var point = area[1]![digitBuffer.Start - 1];
        if (Check(point))
            return true;
    }
    var start = leftShift ? digitBuffer.Start - 1 : digitBuffer.Start;
    var end = rightShift ? digitBuffer.End + 1 : digitBuffer.End;
    if (area[0] is not null)
    {
        for (var i = start; i <= end; i++)
        {
            if (Check(area[0]![i]))
                return true;
        }
    }
    if (area[2] is not null)
    {
        for (var i = start; i <= end; i++)
        {
            if (Check(area[2]![i]))
                return true;
        }
    }
    return false;
}
bool Check(char point)
{
    return !char.IsDigit(point) && point != '.';
}

record DigitBuffer(int Number, int Start, int End);