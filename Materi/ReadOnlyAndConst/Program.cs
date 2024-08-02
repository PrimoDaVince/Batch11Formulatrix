//ReadOnly And Const
class Car
{
	public int price;
	public readonly int price2=3;//Redonly Bisa diganti dengan menggunakan constructor
	/*public Car(int price2)
	{
		this.price2=price2;
	}*/
	public const int price3 =3;//Const harus di sign dari awal
	

}
class program
{
	static void Main()
	{
		Car car = new();
		Console.WriteLine(car.price2);
		car.price =1;
		Console.WriteLine(car.price);
		
		//car.price3=3;///gak bisa dikarenakan const bersifat static
		
		Console.WriteLine(Car.price3);//Akses const Langsung memanggil classnya tersebut
		
	}
}