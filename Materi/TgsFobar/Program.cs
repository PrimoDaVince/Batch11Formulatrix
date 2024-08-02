class DictionaryClass
{
	Dictionary<int,string> myDictionary = new();
	public  int n = 15;
	 List<string> myList = new();
	 
	
	
	
}
class Program
{
	static void Main()
	{
		DictionaryClass dictionary= new DictionaryClass();
		// int nMain = DictionaryClass.n;
		// string foo = DictionaryClass.a;
		// string bar = DictionaryClass.b;
		
		for (int i = 1; i <=nMain ; i++)
		{
			if(i%5 == 0 && i%3==0)
			{
				Console.WriteLine(a+b);
			}
			else if(i%5 ==0)
			{
				Console.WriteLine(a);
			}
			else if(i%3 ==0)
			{
				Console.WriteLine(b);
			}
			else
			{
				Console.WriteLine(i);			
			}
		}
	}
}