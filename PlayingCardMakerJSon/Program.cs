using System.Text.Json;
using PlayingCardMakerJSon;

class Program
{
	static void Main()
	{
		

		List<Card> playingCard = new List<Card>()
		{
		new Card (Rank.Two,Suit.Clubs),
		new Card (Rank.Three,Suit.Clubs),
		new Card (Rank.Four,Suit.Clubs),
		new Card (Rank.Five,Suit.Clubs),
		new Card (Rank.Six,Suit.Clubs),
		new Card (Rank.Seven,Suit.Clubs),
		new Card (Rank.Eight,Suit.Clubs),
		new Card (Rank.Nine,Suit.Clubs),
		new Card (Rank.Ten,Suit.Clubs),
		new Card (Rank.Jack,Suit.Clubs),
		new Card (Rank.Queen,Suit.Clubs),
		new Card (Rank.King,Suit.Clubs),
		
		new Card (Rank.Two,Suit.Diamonds),
		new Card (Rank.Three,Suit.Diamonds),
		new Card (Rank.Four,Suit.Diamonds),
		new Card (Rank.Five,Suit.Diamonds),
		new Card (Rank.Six,Suit.Diamonds),
		new Card (Rank.Seven,Suit.Diamonds),
		new Card (Rank.Eight,Suit.Diamonds),
		new Card (Rank.Nine,Suit.Diamonds),
		new Card (Rank.Ten,Suit.Diamonds),
		new Card (Rank.Jack,Suit.Diamonds),
		new Card (Rank.Queen,Suit.Diamonds),
		new Card (Rank.King,Suit.Diamonds),
		
		new Card (Rank.Two,Suit.Hearts),
		new Card (Rank.Three,Suit.Hearts),
		new Card (Rank.Four,Suit.Hearts),
		new Card (Rank.Five,Suit.Hearts),
		new Card (Rank.Six,Suit.Hearts),
		new Card (Rank.Seven,Suit.Hearts),
		new Card (Rank.Eight,Suit.Hearts),
		new Card (Rank.Nine,Suit.Hearts),
		new Card (Rank.Ten,Suit.Hearts),
		new Card (Rank.Jack,Suit.Hearts),
		new Card (Rank.Queen,Suit.Hearts),
		new Card (Rank.King,Suit.Hearts),
		
		new Card (Rank.Two,Suit.Spades),
		new Card (Rank.Three,Suit.Spades),
		new Card (Rank.Four,Suit.Spades),
		new Card (Rank.Five,Suit.Spades),
		new Card (Rank.Six,Suit.Spades),
		new Card (Rank.Seven,Suit.Spades),
		new Card (Rank.Eight,Suit.Spades),
		new Card (Rank.Nine,Suit.Spades),
		new Card (Rank.Ten,Suit.Spades),
		new Card (Rank.Jack,Suit.Spades),
		new Card (Rank.Queen,Suit.Spades),
		new Card (Rank.King,Suit.Spades),
		
		};
		
		string jsonString = JsonSerializer.Serialize(playingCard);
				
		using(StreamWriter sw = new("./Cards.json")) 
		{
			sw.WriteLine(jsonString);
		}
		// XmlSerializer serializer = new(typeof(List<Human>));
		
		// using (FileStream fs = new("./human.txt", FileMode.Create))
		// {
		// 	serializer.Serialize(fs, futurePresident);
		// }
	}
}