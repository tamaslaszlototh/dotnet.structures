# DotNet.Structures

**DotNet.Structures** is a specialized .NET library that provides robust implementations of advanced data structures, designed for high performance and versatility in complex computational scenarios. This library is ideal for developers and researchers looking to leverage efficient and scalable data structures for sophisticated applications.

## Key Features so far
- **Priority Queues**: Implementations of min-heaps and max-heaps for priority-based data handling.

## Key Features on waiting list
- **Balanced Trees**: AVL Trees, Red-Black Trees, and B-Trees for optimized search, insertion, and deletion operations.
- **Graphs**: Graph data structures supporting various traversal and search algorithms (DFS, BFS, etc.).
- **Trie and Suffix Trees**: Efficient string search and manipulation structures.
- **Skip Lists**: Probabilistic data structure for fast ordered operations.
- **Segment Trees**: Ideal for range queries in static and dynamic datasets.
- **Other Advanced Structures**: Custom data structures optimized for specific use cases (e.g., Disjoint Set, Fibonacci Heaps).
- **And more**

## 1. How to start with Priority Queues
### Using `HeapBaseFactory`

The `HeapBaseFactory` class provides an easy way to create and manage different types of heap-based data structures. It supports the creation of both **Binary Max-Heaps** and **Binary Min-Heaps** using the `HeapBase` interface. This allows you to define your heap type at runtime and configure its behavior through options.

#### Factory Methods

The `HeapBaseFactory` class includes the following static methods for creating heaps:

1. **`CreateBinaryMaxHeap(Action<HeapOptions>? optionsBuilder)`**
2. **`CreateBinaryMinHeap(Action<HeapOptions>? optionsBuilder)`**

#### Functionality

- **`CreateBinaryMaxHeap(Action<HeapOptions>? optionsBuilder)`**
  - **Description**: Creates a new instance of a **Binary Max-Heap**. A Binary Max-Heap ensures that each parent node has a higher priority value than its children, making the highest-priority item always available at the root.
  - **Parameters**: Accepts an optional `Action<HeapOptions>` delegate that allows you to customize the heap's behavior.
  - **Returns**: An instance of `HeapBase` configured as a Binary Max-Heap.
  
- **`CreateBinaryMinHeap(Action<HeapOptions>? optionsBuilder)`**
  - **Description**: Creates a new instance of a **Binary Min-Heap**. A Binary Min-Heap ensures that each parent node has a lower priority value than its children, making the lowest-priority item always available at the root.
  - **Parameters**: Accepts an optional `Action<HeapOptions>` delegate that allows you to customize the heap's behavior.
  - **Returns**: An instance of `HeapBase` configured as a Binary Min-Heap.

#### Example Usage

The following examples demonstrate how to use the `HeapBaseFactory` to create different types of heaps and perform basic operations:

##### Creating a Binary Max-Heap
```csharp
var maxHeap = HeapBaseFactory.CreateBinaryMaxHeap();
maxHeap.Insert(new PriorityQueueItem { Priority = 10, Value = "Item1" });
maxHeap.Insert(new PriorityQueueItem { Priority = 20, Value = "Item2" });
var topItem = maxHeap.Top(); // Retrieves and removes the item with the highest priority (Priority = 20)
Console.WriteLine($"Top item: {topItem?.Value}"); // Output: "Top item: Item2"
```

##### Creating a Binary Min-Heap
```csharp
var minHeap = HeapBaseFactory.CreateBinaryMinHeap();
minHeap.Insert(new PriorityQueueItem { Priority = 15, Value = "Item1" });
minHeap.Insert(new PriorityQueueItem { Priority = 5, Value = "Item2" });
var topItem = minHeap.Top(); // Retrieves and removes the item with the lowest priority number (Priority = 5)
Console.WriteLine($"Top item: {topItem?.Value}"); // Output: "Top item: Item5"
```


## Contributing
I welcome contributions! Feel free to submit issues and pull requests to add new advanced data structures or optimize existing ones.

## License
This library is distributed under the MIT License.
