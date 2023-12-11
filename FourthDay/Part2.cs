namespace FourthDay;

public class Part2
{
    public void Execute()
    {
        const string rootPath = @"C:\Users\Igor\RiderProjects\Aoc2023\FourthDay";
        var file = new StreamReader($"{rootPath}/input.txt");
        Console.WriteLine(Solve(file));
    }
    
    private int Solve(StreamReader file)
    {
        var copies = new List<Node>();
        int sum = 0;
        while (file.ReadLine() is {} line)
        {
            foreach (var copy in copies)
            {
                copy.Value -= 1;
            }
            
            var numbers = line.Split('|');
            var left = numbers[0].Split(':').Last().Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(x => int.Parse(x));
            
            var right = numbers[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x));
            var count = left.Intersect(right).Count();
            var cardsLine = copies.Select(x => x.Count).Sum() + 1;
            
            sum += cardsLine;
            
            if (count != 0)
            {
                copies.Add(new Node(count, cardsLine));
            }
            copies.RemoveAll(x => x.Value == 0);
        }

        return sum;
    }
}

class Node
{
    public Node(int value, int count)
    {
        Value = value;
        Count = count;
    }
    public int Value { get; set; }
    public int Count { get; }
}