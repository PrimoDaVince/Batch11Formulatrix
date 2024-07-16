using System.Numerics;
using System.Xml.XPath;

class Calculator
{
	public T Add<T>(T a, T b)where T : IAdditionOperators<T,T,T> {
		return a + b;
	}
	public T Substract<T>(T a, T b)where T : ISubtractionOperators<T, T,T> {
		return a - b;
	}
	public T Multiply<T>(T a, T b)where T : IMultiplyOperators<T, T,T> {
		return a * b;
	}
	
	
	public T Divide<T>(T a, T b)where T : IDivisionOperators<T, T,T> {
		return a/b;
	}
	
}
class Program {
	static void Main() {
		Calculator calc = new();

		calc.Add<int>(3,4);
		calc.Divide<float>(3.0f, 2.0f);
		calc.Multiply<decimal>(3.0M, 2.0M);
		calc.Substract<double>(3.0, 2.0);
		
		
				
	}	
}