The Task:


Create an in memory implementation of IRepository<T>



-----Comments:


I thought about using B+tree, but it's not implemented in c# and this task was not to implement a data structure, wasn't it?
So I decided to use SortedSet because it's implemented in .net as a Red Black Tree. 
Lookup time for b+tree and reb-black tree is similiar O(logn). 
The main advantage of b+tree is that getting all records is easier and faster.

This solution isn't concurrent safe.
SortedSet isn't concurrent safe collection. ConcurrentDictionary is implented in .net as an array. So adding/removing entities extends/shrinks the table, which is very expensive. 
I hope that this task wasn't about creating concurrent collection.

I decided to add update method. It's faster to update entity data instead of removing and adding node to self-balanced tree. SampleEntity contains only one field so there is no need to wrap it in a transaction.

