using suffler;
using System.Text.Json;
class Program
{
	private static Card GetCardById(List<Card> cards, int id)
	{
		return cards.FirstOrDefault(card => card.idCard == id);
	}
	static void Main()
	{
		 // Create an array of numbers from 1 to 10
		 
		
		Random random= new Random();
		
		string filePath = "./Cards.json";
		using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
		using (StreamReader reader = new StreamReader(fs))
		{	
			//deserillized data dari card json
			string jsonFromFile = reader.ReadToEnd();
			List<Card> deserializedCards = JsonSerializer.Deserialize<List<Card>>(jsonFromFile);
			Card foundCard = GetCardById(deserializedCards,random.Next(1,52));
			
		
			
			
			
			
		}
		
		
	}
}