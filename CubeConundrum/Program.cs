class Program
{
	public static void Main()
	{
		try
		{
			using var inputFile = new StreamReader("../../../input.csv");

			var results = new List<int>();
			var nextLine = inputFile.ReadLine();
			while (nextLine is not null)
			{
				results.Add(ProccessInputLine(nextLine));
				nextLine = inputFile.ReadLine();
			}
			
			Console.WriteLine($"RESULT: {results.Sum()}");
		}
		catch (IOException e)
        {
            Console.WriteLine("The file could not be read:");
            Console.WriteLine(e.Message);
        }

	}

	private static int ProccessInputLine(string inputLine)
	{
		var gameNum = int.Parse(inputLine.Split(':')[0].Split(' ')[1]);
		var sets = inputLine.Split(':')[1].Split(";");
		var minRequiredColors = new Dictionary<string, int>() {{"red", 0}, {"blue", 0}, {"green", 0}};
		
		foreach (var set in sets )
		{
			var kinds = set.Split(",");
			var cubesByColor = new Dictionary<string, int>();
            foreach (var kind in kinds)
            {
				var number = int.Parse(kind.Trim().Split(" ")[0]);
				var color = kind.Trim().Split(" ")[1];
                cubesByColor.Add(color, number);
            }

			minRequiredColors = UpdateMinRequiredColors(minRequiredColors, cubesByColor);
        }
		
		return minRequiredColors.Aggregate(1, (previous,kvp) => previous * kvp.Value);
	}

	private static Dictionary<string, int> UpdateMinRequiredColors(Dictionary<string, int> currentMin, Dictionary<string, int> newMin)
	{
		var result = new Dictionary<string, int>();
		foreach (var (color, currentMinValue) in currentMin)
		{
			if (!newMin.TryGetValue(color, out int newMinValue))
				result.Add(color, currentMinValue);
			else
				result.Add(color, Math.Max(currentMinValue, newMinValue));
		}

		return result;
	}
}