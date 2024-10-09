using DotNet.DataStructures.PriorityQueues;

namespace DotNet.Structures.Tests.PriorityQueuesTests;

[TestFixture]
public class BinaryMinHeapTests
{
    private BinaryMinHeap _heap;
    private BinaryMinHeap _heapWithHashMapDisabled;

    [SetUp]
    public void SetUp()
    {
        _heap = BinaryMinHeap.Create();
        _heapWithHashMapDisabled = BinaryMinHeap.Create(options => options.DisableHashMap());
    }

    [Test]
    public void Insert_ShouldAddNewItemToHeap_WithHashMap()
    {
        // Arrange
        var item = PriorityQueueItem.Create(10, "Item1");

        // Act
        _heap.Insert(item);

        // Assert
        Assert.That(_heap.Count, Is.EqualTo(1));
        Assert.That(_heap.Peek(), Is.EqualTo(item));
    }

    [Test]
    public void Insert_ShouldAddNewItemToHeap_WithoutHashMap()
    {
        // Arrange
        var item = PriorityQueueItem.Create(10, "Item1");

        // Act
        _heapWithHashMapDisabled.Insert(item);

        // Assert
        Assert.That(_heapWithHashMapDisabled.Count, Is.EqualTo(1));
        Assert.That(_heapWithHashMapDisabled.Peek(), Is.EqualTo(item));
    }

    [Test]
    public void Top_ShouldReturnAndRemoveHighestPriorityItem_WithHashMap()
    {
        // Arrange
        _heap.Insert(PriorityQueueItem.Create(20, "LowPriority"));
        var highPriorityItem = PriorityQueueItem.Create(5, "HighPriority");
        _heap.Insert(highPriorityItem);

        // Act
        var topItem = _heap.Top();

        // Assert
        Assert.That(highPriorityItem, Is.EqualTo(topItem));
        Assert.That(_heap.Count(), Is.EqualTo(1));
    }

    [Test]
    public void Top_ShouldReturnAndRemoveHighestPriorityItem_WithoutHashMap()
    {
        // Arrange
        _heapWithHashMapDisabled.Insert(PriorityQueueItem.Create(20, "LowPriority"));
        var highPriorityItem = PriorityQueueItem.Create(5, "HighPriority");
        _heapWithHashMapDisabled.Insert(highPriorityItem);

        // Act
        var topItem = _heapWithHashMapDisabled.Top();

        // Assert
        Assert.That(highPriorityItem, Is.EqualTo(topItem));
        Assert.That(_heapWithHashMapDisabled.Count(), Is.EqualTo(1));
    }

    [Test]
    public void Peek_ShouldReturnHighestPriorityItemWithoutRemovingIt_WithHashMap()
    {
        // Arrange
        var item1 = PriorityQueueItem.Create(15, "Item1");
        var item2 = PriorityQueueItem.Create(30, "Item2");
        _heap.Insert(item1);
        _heap.Insert(item2);

        // Act
        var topItem = _heap.Peek();

        // Assert
        Assert.That(item1, Is.EqualTo(topItem));
        Assert.That(_heap.Count(), Is.EqualTo(2));
    }

    [Test]
    public void Peek_ShouldReturnHighestPriorityItemWithoutRemovingIt_WithoutHashMap()
    {
        // Arrange
        var item1 = PriorityQueueItem.Create(15, "Item1");
        var item2 = PriorityQueueItem.Create(30, "Item2");
        _heapWithHashMapDisabled.Insert(item1);
        _heapWithHashMapDisabled.Insert(item2);

        // Act
        var topItem = _heapWithHashMapDisabled.Peek();

        // Assert
        Assert.That(item1, Is.EqualTo(topItem));
        Assert.That(_heapWithHashMapDisabled.Count(), Is.EqualTo(2));
    }

    [Test]
    public void Update_ShouldModifyPriorityOfExistingItem_WithHashMap()
    {
        // Arrange
        var item = PriorityQueueItem.Create(5, "Item1");
        _heap.Insert(item);
        var updatedItem = PriorityQueueItem.Create(25, "Item1");

        // Act
        _heap.Update(updatedItem);

        // Assert
        Assert.That(_heap.Peek(), Is.EqualTo(updatedItem));
        Assert.That(_heap.Peek().Priority, Is.EqualTo(25));
    }

    [Test]
    public void Update_ShouldModifyPriorityOfExistingItem_WithoutHashMap()
    {
        // Arrange
        var item = PriorityQueueItem.Create(5, "Item1");
        _heapWithHashMapDisabled.Insert(item);
        var updatedItem = PriorityQueueItem.Create(25, "Item1");

        // Act
        _heapWithHashMapDisabled.Update(updatedItem);

        // Assert
        Assert.That(_heapWithHashMapDisabled.Peek(), Is.EqualTo(updatedItem));
        Assert.That(_heapWithHashMapDisabled.Peek().Priority, Is.EqualTo(25));
    }

    [Test]
    public void Update_ShouldInsertNewItemIfItDoesNotExist_WithHashMap()
    {
        // Arrange
        var newItem = PriorityQueueItem.Create(50, "NewItem");

        // Act
        _heap.Update(newItem);

        // Assert
        Assert.That(_heap.Peek(), Is.EqualTo(newItem));
        Assert.That(_heap.Count(), Is.EqualTo(1));
    }

    [Test]
    public void Update_ShouldInsertNewItemIfItDoesNotExist_WithoutHashMap()
    {
        // Arrange
        var newItem = PriorityQueueItem.Create(50, "NewItem");

        // Act
        _heapWithHashMapDisabled.Update(newItem);

        // Assert
        Assert.That(_heapWithHashMapDisabled.Peek(), Is.EqualTo(newItem));
        Assert.That(_heapWithHashMapDisabled.Count(), Is.EqualTo(1));
    }

    [Test]
    public void Top_ShouldReturnNullWhenHeapIsEmpty_WithHashMap()
    {
        // Act
        var result = _heap.Top();

        // Assert
        Assert.That(result, Is.Null);
    }

    [Test]
    public void Top_ShouldReturnNullWhenHeapIsEmpty_WithoutHashMap()
    {
        // Act
        var result = _heapWithHashMapDisabled.Top();

        // Assert
        Assert.That(result, Is.Null);
    }

    [Test]
    public void Peek_ShouldReturnNullWhenHeapIsEmpty_WithHashMap()
    {
        // Act
        var result = _heap.Peek();

        // Assert
        Assert.That(result, Is.Null);
    }

    [Test]
    public void Peek_ShouldReturnNullWhenHeapIsEmpty_WithoutHashMap()
    {
        // Act
        var result = _heapWithHashMapDisabled.Peek();

        // Assert
        Assert.That(result, Is.Null);
    }

    [Test]
    public void Insert_ShouldMaintainHeapProperty_WithHashMap()
    {
        // Arrange
        _heap.Insert(PriorityQueueItem.Create(5, "Item1"));
        _heap.Insert(PriorityQueueItem.Create(10, "Item2"));
        _heap.Insert(PriorityQueueItem.Create(15, "Item3"));

        // Act
        var topItem = _heap.Peek();

        // Assert
        Assert.That(topItem, Is.Not.Null);
        Assert.That(topItem.Priority, Is.EqualTo(5));
    }

    [Test]
    public void Insert_ShouldMaintainHeapProperty_WithoutHashMap()
    {
        // Arrange
        _heapWithHashMapDisabled.Insert(PriorityQueueItem.Create(5, "Item1"));
        _heapWithHashMapDisabled.Insert(PriorityQueueItem.Create(10, "Item2"));
        _heapWithHashMapDisabled.Insert(PriorityQueueItem.Create(15, "Item3"));

        // Act
        var topItem = _heapWithHashMapDisabled.Peek();

        // Assert
        Assert.That(topItem, Is.Not.Null);
        Assert.That(topItem.Priority, Is.EqualTo(5));
    }

    [Test]
    public void Insert_MultipleItems_ShouldArrangeItemsAccordingToPriority_WithHashMap()
    {
        // Arrange
        var items = new[]
        {
            PriorityQueueItem.Create(10, "Item1"),
            PriorityQueueItem.Create(30, "Item2"),
            PriorityQueueItem.Create(20, "Item3"),
            PriorityQueueItem.Create(25, "Item4")
        };

        // Act
        foreach (var item in items)
        {
            _heap.Insert(item);
        }

        // Assert
        var topItem = _heap.Top();
        Assert.That(topItem, Is.Not.Null);
        Assert.That(topItem.Priority, Is.EqualTo(10));
    }

    [Test]
    public void Insert_MultipleItems_ShouldArrangeItemsAccordingToPriority_WithoutHashMap()
    {
        // Arrange
        var items = new[]
        {
            PriorityQueueItem.Create(10, "Item1"),
            PriorityQueueItem.Create(30, "Item2"),
            PriorityQueueItem.Create(20, "Item3"),
            PriorityQueueItem.Create(25, "Item4")
        };

        // Act
        foreach (var item in items)
        {
            _heapWithHashMapDisabled.Insert(item);
        }

        // Assert
        var topItem = _heapWithHashMapDisabled.Top();
        Assert.That(topItem, Is.Not.Null);
        Assert.That(topItem.Priority, Is.EqualTo(10));
    }
}