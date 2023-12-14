namespace FiveDay;

public class Part1
{
    public void Execute()
    {
        const string rootPath = @"C:\Users\Igor\RiderProjects\Aoc2023\FiveDay";
        var file = new StreamReader($"{rootPath}/input.txt");
        Console.WriteLine(Solve(file));
    }

    private ulong Solve(StreamReader file)
    {
        var seedsLine = file.ReadLine()!;
        var seeds = seedsLine.Split(":").Last().Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(x => new Seed(ulong.Parse(x), true)).ToList();

        while (file.ReadLine() is { } line)
        {
            if (string.IsNullOrWhiteSpace(line) || char.IsDigit(line.First()))
            {
                continue;
            }

            while (file.ReadLine() is {} innerLine && !string.IsNullOrWhiteSpace(innerLine))
            {
                var nums =  innerLine.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(x => ulong.Parse(x)).ToArray();
                var context = new Context(nums[0], nums[1], nums[2]);
                foreach (var seed in seeds)
                {
                    if (!seed.ToSwap)
                    {
                        continue;
                    }
                    if (seed.Number >= context.Src && seed.Number < context.Src + context.Range)
                    {
                        seed.Number = context.Dst + (seed.Number - context.Src);
                        seed.ToSwap = false;
                    }
                }
            }

            foreach (var seed in seeds)
            {
                seed.ToSwap = true;
            }
        }

        return seeds.Select(x => x.Number).Min();
    }

    private class Seed
    {
        public ulong Number { get; set; }
        public bool ToSwap { get; set; }
        public Seed(ulong number, bool toSwap)
        {
            Number = number;
            ToSwap = toSwap;
        }
    }
    private record struct Context(ulong Dst, ulong Src, ulong Range);
}