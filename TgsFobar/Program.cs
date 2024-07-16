class Program
{
	
	static void Main()
	{
		int n = 15;
		string a="foo";
		string b="bar";
		for (int i = 1; i <= n; i++)
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