public class Car
{
	private int price;
	public string brand;
	public string colour;
	public void Start()
	{
		Console.WriteLine("Start");
	}
	public void CheckPrice()
	{
		Console.WriteLine(price);
	}
	public void AddPrice(int add)
	{
		price +=add;
	}
	class program
	{
		static void Main()
		{
			Car car = new Car();
			car.AddPrice(100);
			car.CheckPrice();
		}
	}
}