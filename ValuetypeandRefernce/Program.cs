class Program
{
	class Car
	{
		public int price;
		public Car(int price)
		{
			this.price = price;
		}
	}

	static void Main()
	{
		//=============================================================
		Car car = new Car(3);
		Car car2 = car;
		car2.price += 2;
		//============================================================
		int a = 2;
		int b = a;
		b += 3;
		//============================================================
		string h = "hellow";
		string w = h;
		w += "world";

		//============================================================
		Console.WriteLine("a = " + a);// value a dan b berbeda karena di simpan di(stak) memori yang berbeda
		Console.WriteLine("b = " + b);
		//============================================================
		Console.WriteLine("Car Price = " + car2.price);
		Console.WriteLine("Car Price = " + car.price);//Value Car Akan di update dari car2 dikarenakan masi merefernsikikan memori class yg sama dari (Heap Memory)class car
		 //===========================================================
		Console.WriteLine(h);//hasilnya akan jadi hello world karena string immutable jadi akan di buat memori baru setiap penambahan string
		Console.WriteLine(w);
	}
}