using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

class MyCollection<T>
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
	public void Remove(int index)
	{
		myCollection[index] = default;
	}
	
}
class Program
{
	static void Main()
	{
		MyCollection<int> myCollec = new MyCollection<int>();
		MyCollection<string> myCollecint = new MyCollection<string>();
		string temp;
	
		for(int z = 0; z<5; z++)
		{
			temp = Console.ReadLine();
			int convertin1 = int.Parse(temp);
			myCollecint.Add(temp);
		}
		
		for (int i = 0; i < 5; i++)
		{
			Console.WriteLine(myCollecint.Get(i));
		}
		
	}
}