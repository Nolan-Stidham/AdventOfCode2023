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

	private static int ProccessInputLine(string inputLine) { return 0; }
}