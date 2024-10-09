namespace DotNet.DataStructures.PriorityQueues;

/// <summary>
/// Represents a binary max-heap implementation of a priority queue data structure.
/// </summary>
/// <remarks>
/// The <c>BinaryMaxHeap</c> class stores items in a heap structure, where each parent node has a higher priority than its children.
/// This ensures that the item with the highest priority is always at the top of the heap.
/// /// The highest priority means the item has the highest priority value.
/// </remarks>
/// <example>
/// The following example demonstrates how to use the <c>BinaryMaxHeap</c> class:
/// <code>
/// var heap = BinaryMaxHeap.Create();
/// heap.Insert(new PriorityQueueItem { Priority = 5, Value = "Item1" });
/// heap.Insert(new PriorityQueueItem { Priority = 10, Value = "Item2" });
/// var topItem = heap.Top(); // Retrieves and removes the item with the highest priority
/// var peekItem = heap.Peek(); // Retrieves but does not remove the top item
/// </code>
/// </example>
public sealed class BinaryMaxHeap
{
    private List<PriorityQueueItem> _items = [];
    private readonly Dictionary<object, int> _hashMap = [];
    private readonly bool _isHashMapEnabled = true;

    private BinaryMaxHeap()
    {
    }

    private BinaryMaxHeap(HeapOptions options)
    {
        _isHashMapEnabled = options.IsHashMapEnabled;
    }

    /// <summary>
    /// Creates a new instance of a <see cref="BinaryMaxHeap"/> using the provided configuration options.
    /// </summary>
    /// <param name="optionsBuilder">
    /// An optional delegate used to configure the heap options. If no options are provided, 
    /// a default <see cref="BinaryMaxHeap"/> is created with the standard heap settings.
    /// </param>
    /// <returns>
    /// A new instance of <see cref="BinaryMaxHeap"/> configured according to the specified options, 
    /// or with default options if none are provided.
    /// </returns>
    /// <example>
    /// Example of creating a <see cref="BinaryMaxHeap"/> with custom options:
    /// <code>
    /// var heap = BinaryMaxHeap.Create(options => options.DisableHashMap());
    /// </code>
    /// </example>
    /// <remarks>BinaryMaxHeap uses hashMap by default to make search faster, but this can be disabled.
    /// </remarks>
    public static BinaryMaxHeap Create(Action<HeapOptions>? optionsBuilder = null)
    {
        if (optionsBuilder is null)
        {
            return new BinaryMaxHeap();
        }

        var options = new HeapOptions();
        optionsBuilder.Invoke(options);
        return new BinaryMaxHeap(options);
    }

    private void UpdateHashMap(object value, int newIndex)
    {
        _hashMap[value] = newIndex;
    }

    private void DeleteFromHashMap(object value)
    {
        _hashMap.Remove(value);
    }

    private int GetIndexFromHashMap(object value)
    {
        var isFound = _hashMap.TryGetValue(value, out int index);
        return isFound ? index : -1;
    }

    private void BubbleUp(int index)
    {
        PriorityQueueItem current = _items[index];
        while (index > 0)
        {
            var parentIndex = GetParentIndex(index);
            if (_items[parentIndex].Priority > current.Priority) break;

            if (_isHashMapEnabled) UpdateHashMap(_items[parentIndex].Value, index);

            _items[index] = _items[parentIndex];
            index = parentIndex;
        }

        if (_isHashMapEnabled) UpdateHashMap(current.Value, index);

        _items[index] = current;
    }

    private void PushDown(int index)
    {
        PriorityQueueItem current = _items[index];
        while (HasChildren(index))
        {
            var highestPriorityChild = GetHighestPriorityChild(index);
            if (highestPriorityChild.Child.Priority < current.Priority) break;

            if (_isHashMapEnabled) UpdateHashMap(highestPriorityChild.Child.Value, index);

            _items[index] = highestPriorityChild.Child;
            index = highestPriorityChild.Index;
        }

        if (_isHashMapEnabled) UpdateHashMap(current.Value, index);

        _items[index] = current;
    }

    private static int GetParentIndex(int index)
    {
        return (index - 1) / 2;
    }

    private bool HasChildren(int index)
    {
        var firstChild = _items.ElementAtOrDefault(index * 2 + 1);
        return firstChild is not null;
    }

    private (int Index, PriorityQueueItem Child) GetHighestPriorityChild(int index)
    {
        var firstChildIndex = index * 2 + 1;
        var secondChildIndex = (index + 1) * 2;

        var firstChild = _items.ElementAt(firstChildIndex);
        var secondChild = _items.ElementAtOrDefault(secondChildIndex);

        if (secondChild is null) return (firstChildIndex, firstChild);

        return firstChild.Priority > secondChild.Priority
            ? (firstChildIndex, firstChild)
            : (secondChildIndex, secondChild);
    }

    /// <summary>
    /// Inserts a <see cref="PriorityQueueItem"/> item into the BinaryMaxHeap.
    /// </summary>
    /// <param name="item">The <see cref="PriorityQueueItem"/> item to be inserted into the BinaryMaxHeap.</param>
    public void Insert(PriorityQueueItem item)
    {
        _items.Add(item);

        if (_isHashMapEnabled) UpdateHashMap(item.Value, _items.Count - 1);

        BubbleUp(_items.IndexOf(item));
    }

    /// <summary>
    /// Retrieves and removes the <see cref="PriorityQueueItem"/> item at the top of the BinaryMaxHeap.
    /// </summary>
    /// <returns>
    /// The <see cref="PriorityQueueItem"/> item at the top of the BinaryMaxHeap, or <c>null</c> if the BinaryMaxHeap is empty.
    /// </returns>
    public PriorityQueueItem? Top()
    {
        if (_items.Count == 0) return null;

        var lastItem = RemoveLastItem();
        if (_items.Count == 0) return lastItem;

        var firstItem = _items.First();
        _items[0] = lastItem;
        PushDown(0);
        return firstItem;
    }

    private PriorityQueueItem RemoveLastItem()
    {
        var lastItem = _items.Last();
        _items.Remove(lastItem);

        if (_isHashMapEnabled) DeleteFromHashMap(lastItem.Value);

        return lastItem;
    }

    /// <summary>
    /// Updates the priority of a specified <see cref="PriorityQueueItem"/> item in the BinaryMaxHeap.
    /// </summary>
    /// <param name="item">The <see cref="PriorityQueueItem"/> item with the updated priority to be modified in the BinaryMaxHeap.</param>
    /// <remarks>
    /// If the <see cref="PriorityQueueItem"/> item does not exist in the BinaryMaxHeap, it is inserted as a new <see cref="PriorityQueueItem"/> item.
    /// </remarks>
    public void Update(PriorityQueueItem item)
    {
        var index = GetIndex(item);
        if (index < 0)
        {
            Insert(item);
        }
        else
        {
            var oldPriority = _items[index].Priority;
            _items[index] = item;
            if (item.Priority > oldPriority)
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
        if (_isHashMapEnabled)
        {
            return GetIndexFromHashMap(item.Value);
        }

        var foundItem = _items.FirstOrDefault(i => i.Value.Equals(item.Value));
        return foundItem is null ? -1 : _items.IndexOf(foundItem);
    }

    /// <summary>
    /// Retrieves the <see cref="PriorityQueueItem"/> item at the top of the BinaryMaxHeap.
    /// </summary>
    /// <returns>
    /// The <see cref="PriorityQueueItem"/> item at the top of the BinaryMaxHeap, or <c>null</c> if the BinaryMaxHeap is empty.
    /// </returns>
    /// <remarks>
    /// The <see cref="PriorityQueueItem"/> item still remains in the BinaryMaxHeap.
    /// </remarks>
    public PriorityQueueItem? Peek()
    {
        return _items.FirstOrDefault();
    }

    /// <summary>
    /// Retrieves the number of items in the BinaryMaxHeap.
    /// </summary>
    /// <returns>
    /// The number of items in the BinaryMaxHeap.
    /// </returns>
    public int Count()
    {
        return _items.Count;
    }

    /// <summary>
    /// Builds a max-heap from the specified list of <see cref="PriorityQueueItem"/> elements.
    /// </summary>
    /// <param name="items">
    /// A list of <see cref="PriorityQueueItem"/> elements to be arranged into a max-heap structure.
    /// The heap property will be enforced for all elements in the list.
    /// </param>
    /// <example>
    /// Example of using the <see cref="Heapify"/> method:
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
    public void Heapify(List<PriorityQueueItem> items)
    {
        _items = items.Select(p => p.DeepCopy()).ToList();

        var index = (items.Count - 1) / 2;
        while (index >= 0)
        {
            PushDown(index);
            if (index == 0) break;
            index = index / 2;
        }
    }
}