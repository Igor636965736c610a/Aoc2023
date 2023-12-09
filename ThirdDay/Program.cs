const string rootPath = @"C:\Users\Igor\RiderProjects\Aoc2023\ThirdDay";
var file = new StreamReader($"{rootPath}/input.txt");
Console.WriteLine(Solve(file));

int Solve(StreamReader file)
{
    var context = new Context(null, null, null);
    var sum = 0;

    while (file.ReadLine() is {} line)
    {
        context = new Context(context.CurrentLine, context.NextLine, line);
        sum += HandleContext(context);
    }

    context = new Context(context.CurrentLine, context.NextLine, null);
    sum += HandleContext(context);

    return sum;
}

(int result, int startIndex, int endIndex)? ReadNumber(ReadOnlySpan<char> text, int startAt)
{
    var index = startAt;

    while (text.Length > index && !char.IsDigit(text[index]))
    {
        index++;
    }

    if (text.Length <= index)
    {
        return null;
    }

    var endIndex = index;

    while (text.Length > endIndex && char.IsDigit(text[endIndex]))
    {
        endIndex++;
    }

    return (int.Parse(text[index..endIndex]), index, endIndex);
}

int HandleContext(Context context)
{
    if (context.CurrentLine is null)
    {
        return 0;
    }

    var line = context.CurrentLine.AsSpan();
    var sum = 0;
    var startAt = 0;

    while (ReadNumber(line, startAt) is {} read)
    {
        startAt = read.endIndex;
        var start = Math.Max(0, read.startIndex - 1);
        var end = Math.Min(context.CurrentLine.Length - 1, read.endIndex);

        if (
            SearchLine(context.PreviousLine, start, end) ||
            SearchLine(context.CurrentLine, start, end) ||
            SearchLine(context.NextLine, start, end)
        )
        {
            sum += read.result;
        }
    }

    return sum;
}

bool SearchLine(string? line, int start, int end)
{
    if (line is not {} l)
    {
        return false;
    }

    for (int i = start; i < end + 1; i++)
    {
        var c = l[i];

        if (c != '.' && !char.IsDigit(c))
        {
            return true;
        }
    }

    return false;
}

record struct Context(string? PreviousLine, string? CurrentLine, string? NextLine);