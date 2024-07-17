using System.ComponentModel;

namespace DelgetePractice;

public class _Subscriber
{
	private string _name;
	public _Subscriber(string name)
	{
		_name=name;
	}
	public void GetNotif(string pesan)
	{
		Console.WriteLine($"Subscriber{pesan}");
	}
}
