using ArrayList;
using DummyCheck;   

var arr = new ArrayList<DummyElement>();

arr.Add(new DummyElement(6));
arr.Add(new DummyElement(78));
arr.Add(new DummyElement(34));
arr.Add(new DummyElement(12));
arr.Add(new DummyElement(1));

Console.WriteLine(arr.ToString());

arr.QuickSort();

Console.WriteLine(arr.ToString());


