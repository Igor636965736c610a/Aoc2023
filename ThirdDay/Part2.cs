namespace ThirdDay;

public class Part2
{
    public void Execute()
    {
        const string rootPath = @"C:\Users\Igor\RiderProjects\Aoc2023\ThirdDay";
        var file = new StreamReader($"{rootPath}/input.txt");
        Console.WriteLine(Solve(file));
        
    }
    int Solve(StreamReader file)
    {
        var context = new Context(null, null, null);
        string? lastLine = null;
        int sum = 0;
        
        while (file.ReadLine() is {} line)
        {
            var currentNums = GetNumsFromLine(line);
            context = new Context(context.CurrentLineNumbers, context.NextLineNumbers, currentNums);
            sum += HandleContext(context, lastLine);
            lastLine = line;
        }
    
        context = new Context(context.CurrentLineNumbers, context.NextLineNumbers, null);
        sum += HandleContext(context, null);
    
        return sum;
    }
    int HandleContext(Context context, string? line)
    {
        if (context.CurrentLineNumbers is null)
        {
            return 0;
        }
    
        ReadOnlySpan<char> lineSpan;
        if (line is not null)
        {
            lineSpan = line.AsSpan();
        }
        else
        {
            lineSpan = new ReadOnlySpan<char>();
        }
        var startAt = 0;
        var sum = 0;
    
        while (ReadStar(lineSpan, startAt) is {} read)
        {
            startAt = read.endIndex;
            var numbers = GetNumbersFromArea(context, read.starPosition);
            if (numbers.Length == 2)
            {
                sum += numbers.Select(x => x.Result).Aggregate((x, y) => x * y);
            }
        }
        return sum;
    }
    NumberDef? ReadNumber(ReadOnlySpan<char> text, int startAt)
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
    
        return new NumberDef(int.Parse(text[index..endIndex]), index, endIndex);
    }
    NumberDef[] GetNumsFromLine(string line)
    {
        var lineSpan = line.AsSpan();
        var startAt = 0;
        var nums = new List<NumberDef>();
    
        while (ReadNumber(lineSpan, startAt) is {} read)
        {
            startAt = read.EndIndex;
            var end = read.EndIndex - 1;
            var readNumber = read with { EndIndex = end };
            nums.Add(readNumber);
        }
    
        return nums.ToArray();
    }
    StarDef? ReadStar(ReadOnlySpan<char> text, int startAt)
    {
        var index = startAt;
    
        for (int i = index; i < text.Length; i++)
        {
            if (text[i] == '*')
            {
                return new StarDef(i, i + 1);
            }
        }
    
        return null;
    }
    NumberDef[] GetNumbersFromArea(Context context, int starPosition)
    {
        return GetNumbersFromLine(context.PreviousLineNumbers)
            .Concat(GetNumbersFromLine(context.NextLineNumbers))
            .Concat(GetNumbersFromLine(context.CurrentLineNumbers))
            .Where(x =>
            {
                var start = x.StartIndex;
                var end = x.EndIndex;
                return (start >= starPosition - 1 && start <= starPosition + 1) ||
                       (end >= starPosition - 1 && end <= starPosition + 1);
            })
            .ToArray();
    }
    
    NumberDef[] GetNumbersFromLine(NumberDef[]? numberDef)
    {
        if (numberDef is {} number)
        {
            return number;
        }
    
        return new NumberDef[] { };
    }
    
    record struct Context(NumberDef[]? PreviousLineNumbers, NumberDef[]? CurrentLineNumbers, NumberDef[]? NextLineNumbers);
    
    record struct NumberDef(int Result, int StartIndex, int EndIndex);
    
    record struct StarDef(int starPosition, int endIndex);
}