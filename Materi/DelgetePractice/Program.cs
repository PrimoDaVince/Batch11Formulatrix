
using DelgetePractice;

class Program
{
	static void Main()
	{
		Publisher pub = new();
		_Subscriber sub = new("Akmal");
		_Subscriber sub2 = new("Zaky");
		_Subscriber sub3 = new("Zaky");
		pub.AddSubscriber(sub.GetNotif);
		pub.AddSubscriber(sub.GetNotif);
		pub.AddSubscriber(sub.GetNotif);
		pub.AddSubscriber(sub.GetNotif);
		pub.SentNotification();
		
	}
}