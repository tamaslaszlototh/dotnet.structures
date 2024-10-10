namespace DotNet.DataStructures.PriorityQueues;

/// <summary>
/// Represents a binary min-heap implementation of a priority queue data structure.
/// </summary>
/// <remarks>
/// The <c>BinaryMinHeap</c> class stores items in a heap structure, where each parent node has a higher priority than its children.
/// This ensures that the item with the highest priority is always at the top of the heap.
/// The highest priority means the item has the lowest priority value.
/// </remarks>
/// <example>
/// The following example demonstrates how to use the <c>BinaryMinHeap</c> class:
/// <code>
/// var heap = BinaryMinHeap.Create();
/// heap.Insert(new PriorityQueueItem { Priority = 5, Value = "Item1" });
/// heap.Insert(new PriorityQueueItem { Priority = 10, Value = "Item2" });
/// var topItem = heap.Top(); // Retrieves and removes the item with the highest priority
/// var peekItem = heap.Peek(); // Retrieves but does not remove the top item
/// </code>
/// </example>
public sealed class BinaryMinHeap : HeapBase
{
    private BinaryMinHeap()
    {
    }

    private BinaryMinHeap(HeapOptions options) : base(options)
    {
    }

    /// <summary>
    /// Creates a new instance of a <see cref="BinaryMinHeap"/> using the provided configuration options.
    /// </summary>
    /// <param name="optionsBuilder">
    /// An optional delegate used to configure the heap options. If no options are provided, 
    /// a default <see cref="BinaryMinHeap"/> is created with the standard heap settings.
    /// </param>
    /// <returns>
    /// A new instance of <see cref="BinaryMinHeap"/> configured according to the specified options, 
    /// or with default options if none are provided.
    /// </returns>
    /// <example>
    /// Example of creating a <see cref="BinaryMinHeap"/> with custom options:
    /// <code>
    /// var heap = BinaryMinHeap.Create(options => options.DisableHashMap());
    /// </code>
    /// </example>
    /// <remarks>BinaryMinHeap uses hashMap by default to make search faster, but this can be disabled.
    /// </remarks>
    internal static BinaryMinHeap Create(Action<HeapOptions>? optionsBuilder = null)
    {
        if (optionsBuilder is null)
        {
            return new BinaryMinHeap();
        }

        var options = new HeapOptions();
        optionsBuilder.Invoke(options);
        return new BinaryMinHeap(options);
    }

    private void UpdateHashMap(object value, int newIndex)
    {
        HashMap[value] = newIndex;
    }

    private void DeleteFromHashMap(object value)
    {
        HashMap.Remove(value);
    }

    private int GetIndexFromHashMap(object value)
    {
        var isFound = HashMap.TryGetValue(value, out int index);
        return isFound ? index : -1;
    }

    private void BubbleUp(int index)
    {
        PriorityQueueItem current = Items[index];
        while (index > 0)
        {
            var parentIndex = GetParentIndex(index);
            if (Items[parentIndex].Priority < current.Priority) break;

            if (IsHashMapEnabled) UpdateHashMap(Items[parentIndex].Value, index);

            Items[index] = Items[parentIndex];
            index = parentIndex;
        }

        if (IsHashMapEnabled) UpdateHashMap(current.Value, index);

        Items[index] = current;
    }

    private void PushDown(int index)
    {
        PriorityQueueItem current = Items[index];
        while (HasChildren(index))
        {
            var highestPriorityChild = GetHighestPriorityChild(index);
            if (highestPriorityChild.Child.Priority > current.Priority) break;

            if (IsHashMapEnabled) UpdateHashMap(highestPriorityChild.Child.Value, index);

            Items[index] = highestPriorityChild.Child;
            index = highestPriorityChild.Index;
        }

        if (IsHashMapEnabled) UpdateHashMap(current.Value, index);

        Items[index] = current;
    }

    private static int GetParentIndex(int index)
    {
        return (index - 1) / 2;
    }

    private bool HasChildren(int index)
    {
        var firstChild = Items.ElementAtOrDefault(index * 2 + 1);
        return firstChild is not null;
    }

    private (int Index, PriorityQueueItem Child) GetHighestPriorityChild(int index)
    {
        var firstChildIndex = index * 2 + 1;
        var secondChildIndex = (index + 1) * 2;

        var firstChild = Items.ElementAt(firstChildIndex);
        var secondChild = Items.ElementAtOrDefault(secondChildIndex);

        if (secondChild is null) return (firstChildIndex, firstChild);

        return firstChild.Priority < secondChild.Priority
            ? (firstChildIndex, firstChild)
            : (secondChildIndex, secondChild);
    }

    /// <summary>
    /// Inserts a <see cref="PriorityQueueItem"/> item into the BinaryMinHeap.
    /// </summary>
    /// <param name="item">The <see cref="PriorityQueueItem"/> item to be inserted into the BinaryMinHeap.</param>
    public override void Insert(PriorityQueueItem item)
    {
        Items.Add(item);
        if (IsHashMapEnabled) UpdateHashMap(item.Value, Items.Count - 1);
        BubbleUp(Items.IndexOf(item));
    }

    /// <summary>
    /// Retrieves and removes the <see cref="PriorityQueueItem"/> item at the top of the BinaryMinHeap.
    /// </summary>
    /// <returns>
    /// The <see cref="PriorityQueueItem"/> item at the top of the BinaryMinHeap, or <c>null</c> if the BinaryMinHeap is empty.
    /// </returns>
    public override PriorityQueueItem? Top()
    {
        if (Items.Count == 0) return null;

        var lastItem = RemoveLastItem();
        if (Items.Count == 0) return lastItem;

        var firstItem = Items.First();
        Items[0] = lastItem;
        PushDown(0);
        return firstItem;
    }

    private PriorityQueueItem RemoveLastItem()
    {
        var lastItem = Items.Last();
        Items.Remove(lastItem);
        if (IsHashMapEnabled) DeleteFromHashMap(lastItem.Value);
        return lastItem;
    }

    /// <summary>
    /// Updates the priority of a specified <see cref="PriorityQueueItem"/> item in the BinaryMinHeap.
    /// </summary>
    /// <param name="item">The <see cref="PriorityQueueItem"/> item with the updated priority to be modified in the BinaryMinHeap.</param>
    /// <remarks>
    /// If the <see cref="PriorityQueueItem"/> item does not exist in the BinaryMinHeap, it is inserted as a new <see cref="PriorityQueueItem"/> item.
    /// </remarks>
    public override void Update(PriorityQueueItem item)
    {
        var index = GetIndex(item);
        if (index < 0)
        {
            Insert(item);
        }
        else
        {
            var oldPriority = Items[index].Priority;
            Items[index] = item;
            if (item.Priority < oldPriority)
            {
                BubbleUp(index);
            }
            else
            {
                PushDown(index);
            }
        }
    }

    private int GetIndex(PriorityQueueItem item)
    {
        if (IsHashMapEnabled)
        {
            return GetIndexFromHashMap(item.Value);
        }

        var foundItem = Items.FirstOrDefault(i => i.Value.Equals(item.Value));
        return foundItem is null ? -1 : Items.IndexOf(foundItem);
    }

    /// <summary>
    /// Retrieves the <see cref="PriorityQueueItem"/> item at the top of the BinaryMinHeap.
    /// </summary>
    /// <returns>
    /// The <see cref="PriorityQueueItem"/> item at the top of the BinaryMinHeap, or <c>null</c> if the BinaryMinHeap is empty.
    /// </returns>
    /// <remarks>
    /// The <see cref="PriorityQueueItem"/> item still remains in the BinaryMinHeap.
    /// </remarks>
    public override PriorityQueueItem? Peek()
    {
        return Items.FirstOrDefault();
    }

    /// <summary>
    /// Retrieves the number of items in the BinaryMinHeap.
    /// </summary>
    /// <returns>
    /// The number of items in the BinaryMinHeap.
    /// </returns>
    public override int Count()
    {
        return Items.Count;
    }

    /// <summary>
    /// Builds a min-heap from the specified list of <see cref="PriorityQueueItem"/> elements with deep copy of the list.
    /// </summary>
    /// <param name="items">
    /// A list of <see cref="PriorityQueueItem"/> elements to be arranged into a min-heap structure.
    /// </param>
    /// <example>
    /// Example of using the <see cref="HeapifyDeep"/> method:
    /// <code>
    /// var items = new List&lt;PriorityQueueItem&gt; 
    /// {
    ///     new PriorityQueueItem(10, ...),
    ///     new PriorityQueueItem(5, ...),
    ///     new PriorityQueueItem(20, ...)
    /// };
    /// heap.Heapify(items);
    /// </code>
    /// </example>
    public override void HeapifyDeep(List<PriorityQueueItem> items)
    {
        Items = items.Select(p => p.DeepCopy()).ToList();

        var index = (items.Count - 1) / 2;
        while (index >= 0)
        {
            PushDown(index);
            if (index == 0) break;
            index = index / 2;
        }
    }

    /// <summary>
    /// Builds a min-heap from the specified list of <see cref="PriorityQueueItem"/> elements with shallow copy of the list.
    /// </summary>
    /// <param name="items">
    /// A list of <see cref="PriorityQueueItem"/> elements to be arranged into a min-heap structure.
    /// </param>
    /// <example>
    /// Example of using the <see cref="HeapifyDeep"/> method:
    /// <code>
    /// var items = new List&lt;PriorityQueueItem&gt; 
    /// {
    ///     new PriorityQueueItem(10, ...),
    ///     new PriorityQueueItem(5, ...),
    ///     new PriorityQueueItem(20, ...)
    /// };
    /// heap.Heapify(items);
    /// </code>
    /// </example>
    public override void HeapifyShallow(List<PriorityQueueItem> items)
    {
        Items = items;

        var index = (items.Count - 1) / 2;
        while (index >= 0)
        {
            PushDown(index);
            if (index == 0) break;
            index = index / 2;
        }
    }
}