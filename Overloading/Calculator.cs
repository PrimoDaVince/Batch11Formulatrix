namespace ClcLib;


class Calculator<T>
{
	public T[] myCollection = new T[5];
	public int count = 0;
	
	public void Add(T input)
	{
		if (count == myCollection.Length)
		{
			return;
		}
		myCollection[count] = input;
		count++;
	}
	
	public T Get(int index)
	{
		return myCollection[index];
	}

	public int AddFunct(int a, int b)
	{
		
		return(a+b);
	}
	public int Multiply(int a, int b)
	{
		return(a*b);
	}
	
	public int Divide(int a, int b)
	{
		return(a/b);
	}
	public float Divide(float a, float b)
	{
		return (a/b);
	}
	public int Minus(int a, int b)
	{
		return(a-b);
	}
	
	
}

