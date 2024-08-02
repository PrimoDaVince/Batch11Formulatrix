class Program
{
//Dictionary
static void Main()
{
	Dictionary<int,string> myDictionary = new();
	myDictionary.Add(1,"hellow");
	myDictionary.Add(3,"belok");
	myDictionary.Add(4,"galow");
	myDictionary.Add(5,"sd");
	
	string result;
	bool status = myDictionary.TryGetValue(4,out result);
	Console.WriteLine(result);
}
}