//Normal Array
/*void Main(){
int[] myArray = new int [5];
myArray[0] = 1;
myArray[1] = 2;

int[] myArray2 = {1,2,3,4,5};
myArray[0]=3;

int[] myArray3 = [1,2,3,4,5]; // only Net 8.0++

myArray3.Length.Dump(); // Size of Array
myArray3.Contains(2); // Check if array contain x
}

///ArrayList cukup tau lebih baik tidak digunakan
	void Main()
	{
		ArrayList myArray = New();
		myArray.Add(True);
		myArray.Add(3);
		myArray.Add(3.9f);
		
	}
	
//List<T>
void Main()
{
	List<int> myList = new();
	myList.Add(3);
	myList.Add(4);
	myList.Add(5);
	myList.Add(6);
	
	int result = myList[0];
	result.Dump();
}
	//HashSet<T>

void Main()
{
	HashSet<int> mySet =new();
	mySet.Add(3);
	mySet.Add(2);
	mySet.Add(5);
	mySet.Add(4);
	mySet.Add(3);
	
	mySet.Dump();
}

//Hash set Union
void Main()
{
	HashSet<int>A=new(){1,2,3,4,5};
	HashSet<int>B=new(){6,7,8,9,10};
	A.UnionWith(B);
	A.Dump();
}
//HashSet SupersetSubset
void Main()
{
	HashSet<int>A=new(){1,2,3,4,5};
	HashSet<int>B=new(){1,2,3};
	
	bool status = B.IsSupersetOf(A);
	status.Dump();
	bool status1 = B.IsSubsetOf(A);
	status1.Dump();
}