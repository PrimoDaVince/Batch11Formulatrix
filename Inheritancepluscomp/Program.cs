﻿//Inheritance Composition
using carLib;
class Program {
	static void Main() {
		Engine engine = new Engine();
		Car car = new Car(engine);
		
		ElectricEngine electricEngine = new ElectricEngine();
		Car car2 = new Car(electricEngine);
	}
}