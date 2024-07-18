//Enum
class Program
{
	static void Main()
		{
			StatusCheker stats = new();
			Status status = Status.xzc2123;
			stats.Check(status);
		}
}
public enum Status
	{
		Notfound,
		Redirected,
		WrongPassword,
		xzc2123
	}
class StatusCheker
{
	public void Check(Status status)
		{
		if(status==Status.Notfound)
			{
				Console.WriteLine("Eroor: NotFound");
			}
		else if(status == Status.Redirected)
			{
				Console.WriteLine("Warning : Redirected");
			}
		else if(status==Status.WrongPassword)
			{
				Console.WriteLine("Error : Worng Password");
			}
		else if(status==Status.xzc2123)
			{
				Console.WriteLine("Error : xzc2123");
			}
		else{
				Console.WriteLine("Status Not Found");
			}
		}
}
