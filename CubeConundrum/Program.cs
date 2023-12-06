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

			if(!IsValidSet(cubesByColor))
				return 0;
        }
		
		return gameNum;
	}

	private static bool IsValidSet(Dictionary<string, int> cubesByColor)
	{
		foreach (var cube in cubesByColor)
		{
			bool isCubeCountValid;
			switch (cube.Key)
			{
				case "red": 
					isCubeCountValid = cube.Value <= c_maxRed;
					break;
				case "green":
					isCubeCountValid = cube.Value <= c_maxGreen;
					break;
				case "blue":
					isCubeCountValid = cube.Value <= c_maxBlue;
					break;
				default:
					throw new Exception($"Color {cube.Key} is invalid");
			}

			if (!isCubeCountValid) return false;
		}

		return true;
	}


	private const int c_maxRed = 12;
	private const int c_maxGreen = 13;
	private const int c_maxBlue = 14;
}