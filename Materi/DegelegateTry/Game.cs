namespace DegelegateTry;
public delegate void GameU(string text);
public class Game
{
	public GameU gameuser;
	public void GameUpdated()
	{
		Console.WriteLine("New Patch Uploaded...");
		SentNotofication("New Patch Updated Download And Play");

	}
	public void SentNotofication(string title)
	{
		gameuser(title);
	}

}
