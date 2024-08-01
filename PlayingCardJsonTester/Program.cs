using System.Text.Json;
using System.Text.Json.Serialization;
using PlayingCardMakerJSon;

class Program
{
	public static void Main()
	{
		
		
		
		string result;
		using (StreamReader sr = new("./Cards.json"))
		{
		 
			 result = sr.ReadToEnd();
			
		}
	
		foreach (var i in result)
		{
			Console.WriteLine(
			Console.WriteLine(i.rank);
			Console.WriteLine(i.suit);
		}
	}
}