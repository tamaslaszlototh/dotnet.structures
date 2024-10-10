namespace DotNet.DataStructures.PriorityQueues;

public static class PriorityQueueFactory
{
    /// <summary>
    /// Creates a new instance of a Binary Max-Heap using the provided configuration options.
    /// </summary>
    /// <param name="optionsBuilder">
    /// An optional delegate used to configure the heap options. If no options are provided, 
    /// a default Binary Max-Heap is created with standard heap settings.
    /// </param>
    /// <returns>
    /// A new instance of a <see cref="HeapBase"/> configured as a Binary Max-Heap according to the specified options, 
    /// or with default options if none are provided.
    /// </returns>
    /// <example>
    /// Example of creating a Binary Max-Heap with custom options:
    /// <code>
    /// var heap = HeapFactory.CreateBinaryMaxHeap(options => options.DisableHashMap());
    /// </code>
    /// </example>
    /// <remarks>
    /// Binary Max-Heaps ensure that each parent node has a higher priority value than its children, 
    /// so the item with the highest priority is always at the root.
    /// </remarks>
    public static HeapBase CreateBinaryMaxHeap(Action<HeapOptions>? optionsBuilder = null)
    {
        return BinaryMaxHeap.Create(optionsBuilder);
    }

    /// <summary>
    /// Creates a new instance of a Binary Min-Heap using the provided configuration options.
    /// </summary>
    /// <param name="optionsBuilder">
    /// An optional delegate used to configure the heap options. If no options are provided, 
    /// a default Binary Min-Heap is created with standard heap settings.
    /// </param>
    /// <returns>
    /// A new instance of a <see cref="HeapBase"/> configured as a Binary Min-Heap according to the specified options, 
    /// or with default options if none are provided.
    /// </returns>
    /// <example>
    /// Example of creating a Binary Min-Heap with custom options:
    /// <code>
    /// var heap = HeapFactory.CreateBinaryMinHeap(options => options.DisableHashMap());
    /// </code>
    /// </example>
    /// <remarks>
    /// Binary Min-Heaps ensure that each parent node has a lower priority value than its children, 
    /// so the item with the lowest priority is always at the root.
    /// </remarks>
    public static HeapBase CreateBinaryMinHeap(Action<HeapOptions>? optionsBuilder = null)
    {
        return BinaryMinHeap.Create(optionsBuilder);
    }
}