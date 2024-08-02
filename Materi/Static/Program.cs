using CarStatic;
class Program {
	static void Main() {
		Car car = new();
		Console.WriteLine(car.productionNumber);
		Console.WriteLine(Car.count);
		
		Car car2 = new();
		Console.WriteLine(car2.productionNumber);
		Console.WriteLine(Car.count);
		
		Car car3 = new();
		Console.WriteLine(car3.productionNumber);
		Console.WriteLine(Car.count);
		Car.Test();
	}
}