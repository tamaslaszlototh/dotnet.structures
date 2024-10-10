namespace DotNet.DataStructures.PriorityQueues;

public class HeapOptions
{
    internal bool IsHashMapEnabled { get; set; } = true;
    
    public HeapOptions DisableHashMap()
    {
        IsHashMapEnabled = false;
        return this;
    }
}