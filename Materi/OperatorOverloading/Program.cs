class Car
{
	public int price = 100;
	public static Car operator+(Car a, Car b)
	{
		int result = a.price + b.price;
		Car car = new Car();
		car.price = result;
		return car;
	}
	
}
class HumanName
{
	public string namaDepanBelakang;
	public static HumanName operator +(HumanName a,HumanName b)
	{
		string result = a.namaDepanBelakang + b.namaDepanBelakang;
		HumanName humanName = new HumanName();
		humanName.namaDepanBelakang = result;
		return humanName;
	}
}
class Program
{
	static void  Main()
	{
		HumanName human1 = new HumanName();
		human1.namaDepanBelakang="Akmal";
		HumanName human2 = new HumanName();
		human2.namaDepanBelakang="Zakyzain";
		HumanName result = human1+human2;
		Console.WriteLine(result.namaDepanBelakang);
	}
}