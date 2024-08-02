class Program
{
	static void Main()
	{
		string path = @"MyFile.txt";
		using FileStream fs= new(path, FileMode.OpenOrCreate)
		{
			Console.WriteLine("FileCreated");
		}
		
	}
}