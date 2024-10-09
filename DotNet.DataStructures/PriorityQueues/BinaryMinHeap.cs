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
public sealed class BinaryMinHeap
{
    private readonly List<PriorityQueueItem> _items = [];

    private BinaryMinHeap()
    {
    }

    public static BinaryMinHeap Create()
    {
        return new BinaryMinHeap();
    }

    private void BubbleUp(int index)
    {
        PriorityQueueItem current = _items[index];
        while (index > 0)
        {
            var parentIndex = GetParentIndex(index);
            if (_items[parentIndex].Priority < current.Priority) break;
            _items[index] = _items[parentIndex];
            index = parentIndex;
        }

        _items[index] = current;
    }

    private void PushDown(int index)
    {
        PriorityQueueItem current = _items[index];
        while (HasChildren(index))
        {
            var highestPriorityChild = GetHighestPriorityChild(index);
            if (highestPriorityChild.Child.Priority > current.Priority) break;
            _items[index] = highestPriorityChild.Child;
            index = highestPriorityChild.Index;
        }

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

        return firstChild.Priority < secondChild.Priority
            ? (firstChildIndex, firstChild)
            : (secondChildIndex, secondChild);
    }

    /// <summary>
    /// Inserts a <see cref="PriorityQueueItem"/> item into the BinaryMinHeap.
    /// </summary>
    /// <param name="item">The <see cref="PriorityQueueItem"/> item to be inserted into the BinaryMinHeap.</param>
    public void Insert(PriorityQueueItem item)
    {
        _items.Add(item);
        BubbleUp(_items.IndexOf(item));
    }

    /// <summary>
    /// Retrieves and removes the <see cref="PriorityQueueItem"/> item at the top of the BinaryMinHeap.
    /// </summary>
    /// <returns>
    /// The <see cref="PriorityQueueItem"/> item at the top of the BinaryMinHeap, or <c>null</c> if the BinaryMinHeap is empty.
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
        return lastItem;
    }

    /// <summary>
    /// Updates the priority of a specified <see cref="PriorityQueueItem"/> item in the BinaryMinHeap.
    /// </summary>
    /// <param name="item">The <see cref="PriorityQueueItem"/> item with the updated priority to be modified in the BinaryMinHeap.</param>
    /// <remarks>
    /// If the <see cref="PriorityQueueItem"/> item does not exist in the BinaryMinHeap, it is inserted as a new <see cref="PriorityQueueItem"/> item.
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
        var foundItem = _items.FirstOrDefault(i => i.Value.Equals(item.Value));
        return foundItem is null ? -1 : _items.IndexOf(foundItem);
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
    public PriorityQueueItem? Peek()
    {
        return _items.FirstOrDefault();
    }

    /// <summary>
    /// Retrieves the number of items in the BinaryMinHeap.
    /// </summary>
    /// <returns>
    /// The number of items in the BinaryMinHeap.
    /// </returns>
    public int Count()
    {
        return _items.Count;
    }
}