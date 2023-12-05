using System.Text.RegularExpressions;

class Program
{
	public static void Main()
	{
		try
		{
			using var inputFile = new StreamReader("../../../input.csv");

			var calibrationValues = new List<int>();
			var nextLine = inputFile.ReadLine();
			while (nextLine is not null)
			{
				calibrationValues.Add(Int32.Parse(String.Concat(GetFirstNumber(nextLine), GetLastNumber(nextLine))));
				nextLine = inputFile.ReadLine();
			}
			
			Console.WriteLine($"RESULT: {calibrationValues.Sum()}");
		}
		catch (IOException e)
        {
            Console.WriteLine("The file could not be read:");
            Console.WriteLine(e.Message);
        }

	}

	private static int GetFirstNumber(string input)
	{
		var firstDigit = GetFirstDigit(input);
		var firstTextNumber = GetTextNumber(input);

		if (firstDigit is null && firstTextNumber is null)
			throw new Exception($"No numbers found in {input}");
		
		if (firstDigit is not null && firstTextNumber is not null)
			return firstDigit.Index < firstTextNumber.Index ? firstDigit.Number : firstTextNumber.Number;

		return (int)(firstDigit?.Number ?? firstTextNumber?.Number);
	}

	private static int GetLastNumber(string input)
	{
		var lastDigit = GetLastDigit(input);
		var lastTextNumber = GetTextNumber(input, rightToLeft: true);

		if (lastDigit is null && lastTextNumber is null)
			throw new Exception($"No numbers found in {input}");
		
		if (lastDigit is not null && lastTextNumber is not null)
			return lastDigit.Index > lastTextNumber.Index ? lastDigit.Number : lastTextNumber.Number;

		return (int)(lastDigit?.Number ?? lastTextNumber?.Number);
	}

	private static FoundDigit? GetFirstDigit(IEnumerable<char> input)
	{
		for (var i = 0; i < input.Count(); i++)
		{
			var c = input.ElementAt(i).ToString();
			var isDigit = int.TryParse(c, out int digit);
			if (isDigit)
			{
				return new FoundDigit {Number = digit, Index = i};
			}
		}

		return null;
	}

	private static FoundDigit? GetLastDigit(IEnumerable<char> input)
	{
		for (var i = input.Count() - 1; i >= 0; i--)
		{
			var c = input.ElementAt(i).ToString();
			var isDigit = int.TryParse(c, out int digit);
			if (isDigit)
			{
				return new FoundDigit { Number = digit, Index = i };
			}
		}

		return null;
	}

	private static FoundDigit? GetTextNumber(IEnumerable<char> input, bool rightToLeft = false)
	{
		var matches = Regex.Matches(input.ToString()!, c_patten, rightToLeft ? RegexOptions.RightToLeft : RegexOptions.None);
		if (matches.Count == 0)
			return null;

		return new FoundDigit(matches);
	}

	private sealed class FoundDigit 
	{
		public FoundDigit() {}
		public FoundDigit(MatchCollection matches)
		{
			if (!s_textToInt.TryGetValue(matches.First().Value, out var result))
			throw new Exception($"match {matches.First().Value} is invalid");
			
			this.Index = matches.First().Index;
			this.Number = Number = result;
		}

		public int Number { get; set; }
		public int Index { get; set; }
	}

	private const string c_patten = "(one|two|three|four|five|six|seven|eight|nine|ten)";

	private static readonly Dictionary<string, int> s_textToInt = new() { 
		{ "one", 1 },
		{ "two", 2 },
		{ "three", 3 },
		{ "four", 4 },
		{ "five", 5 },
		{ "six", 6 },
		{ "seven", 7 },
		{ "eight", 8 },
		{ "nine", 9 },
	};
}