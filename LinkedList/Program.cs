using DummyCheck;
using LinkedList;


DummyElement[] arr = { 
    new DummyElement(5),
    new DummyElement(10),
    new DummyElement(12),
    new DummyElement(7), 
    new DummyElement(9),
};

SinglyLinkedList<DummyElement> list = new SinglyLinkedList<DummyElement> (arr);

list.Add(new DummyElement (17));

Console.WriteLine(list.ToString());

list.RemoveAt(2,2);

Console.WriteLine(list.ToString());

list.MergeSort();

Console.WriteLine(list.ToString());