using DegelegateTry;

class Program
{
	static void Main()
	{
		Game game = new Game();
		Notification notifClient = new Notification();
		Notification notifEmail = new Notification();
		Notification notifMobileApp = new Notification();
		game.gameuser += notifClient.ShowNotification;
		game.gameuser += notifEmail.ShowNotification;
		game.gameuser += notifMobileApp.ShowNotification;
		
		game.GameUpdated();

	}
}