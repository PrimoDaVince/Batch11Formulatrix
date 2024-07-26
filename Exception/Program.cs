class Program{
    static void Main()
    {
	Console.WriteLine("Program Running...");
	try{
			int[]myArray = null;
			myArray[4].Dump();
		}
	catch(FormatException e){
			Console.WriteLine("General Exception");
			Console.WriteLine(e.Message);
			}
	finally{
	"....".Dump();}
// You can define other methods, fields, classes and namespaces here

    }
}