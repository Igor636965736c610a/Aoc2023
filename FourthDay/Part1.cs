namespace FourthDay;

public class Part1
{
    public void Execute()
    {
        const string rootPath = @"C:\Users\Igor\RiderProjects\Aoc2023\FourthDay";
        var file = new StreamReader($"{rootPath}/input.txt");
        Console.WriteLine(Solve(file));
    }

    private int Solve(StreamReader file)
    {
        int sum = 0;
        while (file.ReadLine() is {} line)
        {
            var numbers = line.Split('|');
            var left = numbers[0].Split(':').Last().Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(x => int.Parse(x));
            var right = numbers[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x));
            var count = left.Intersect(right).Count();
            if (count != 0)
            { 
                sum += (int)Math.Pow(2, count - 1);
            }
        }

        return sum;
    }
}