// See https://aka.ms/new-console-template for more information

using DotNet.DataStructures.PriorityQueues;

var binaryMaxHeap = BinaryMaxHeap.Create();

var item1 = PriorityQueueItem.Create(2, "Call boss");
binaryMaxHeap.Insert(item1);
Console.WriteLine(binaryMaxHeap.Peek().Value);