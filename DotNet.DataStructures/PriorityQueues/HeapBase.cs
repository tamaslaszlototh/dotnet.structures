namespace DotNet.DataStructures.PriorityQueues;

public abstract class HeapBase
{
    protected List<PriorityQueueItem> Items = [];
    protected readonly Dictionary<object, int> HashMap = [];
    protected readonly bool IsHashMapEnabled = true;

    protected HeapBase()
    {
    }

    protected HeapBase(HeapOptions options)
    {
        IsHashMapEnabled = options.IsHashMapEnabled;
    }

    public abstract void Insert(PriorityQueueItem item);
    public abstract PriorityQueueItem? Peek();
    public abstract int Count();
    public abstract PriorityQueueItem? Top();
    public abstract void Update(PriorityQueueItem item);
    public abstract void HeapifyDeep(List<PriorityQueueItem> items);
    public abstract void HeapifyShallow(List<PriorityQueueItem> items);
}