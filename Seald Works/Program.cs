﻿class GrandParent { }
sealed class Parent : GrandParent //if class is sealed then sealed class connot have childern but sealed class still can have a parent
{
	
}
class Child : Parent 
{
	
}