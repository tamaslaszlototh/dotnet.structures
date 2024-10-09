namespace DotNet.DataStructures.PriorityQueues;

public class PriorityQueueItem
{
    public required int Priority { get; set; }
    public required object Value { get; set; }

    /// <summary>
    /// Creates a new <see cref="PriorityQueueItem"/> with the specified priority and value.
    /// </summary>
    /// <typeparam name="T">The type of the value, which must implement <see cref="System.IEquatable{T}"/>.</typeparam>
    /// <param name="priority">The priority level assigned to the new item.</param>
    /// <param name="value">The value associated with the priority queue item.</param>
    /// <returns>A new <see cref="PriorityQueueItem"/> instance with the specified priority and value.</returns>
    /// <remarks>
    /// This method is generic and ensures that the value type implements the <see cref="System.IEquatable{T}"/> interface, enabling comparison within the priority queue.
    /// </remarks>
    public static PriorityQueueItem Create<T>(int priority, T value) where T : IEquatable<T>
    {
        return new PriorityQueueItem()
        {
            Priority = priority,
            Value = value
        };
    }
    
    public PriorityQueueItem DeepCopy()
    {
        return new PriorityQueueItem
        {
            Priority = Priority,
            Value = Value
        };
    }

    private PriorityQueueItem()
    {
    }
}