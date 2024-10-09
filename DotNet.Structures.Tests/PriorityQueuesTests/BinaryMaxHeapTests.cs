using DotNet.DataStructures.PriorityQueues;

namespace DotNet.Structures.Tests.PriorityQueuesTests;

[TestFixture]
public class BinaryMaxHeapTests
{
    private BinaryMaxHeap _heap;

    [SetUp]
    public void SetUp()
    {
        _heap = BinaryMaxHeap.Create();
    }

    [Test]
    public void Insert_ShouldAddNewItemToHeap()
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
    public void Top_ShouldReturnAndRemoveHighestPriorityItem()
    {
        // Arrange
        _heap.Insert(PriorityQueueItem.Create(5, "LowPriority"));
        var highPriorityItem = PriorityQueueItem.Create(20, "HighPriority");
        _heap.Insert(highPriorityItem);

        // Act
        var topItem = _heap.Top();

        // Assert
        Assert.That(highPriorityItem, Is.EqualTo(topItem));
        Assert.That(_heap.Count(), Is.EqualTo(1));
    }

    [Test]
    public void Peek_ShouldReturnHighestPriorityItemWithoutRemovingIt()
    {
        // Arrange
        var item1 = PriorityQueueItem.Create(15, "Item1");
        var item2 = PriorityQueueItem.Create(30, "Item2");
        _heap.Insert(item1);
        _heap.Insert(item2);

        // Act
        var topItem = _heap.Peek();

        // Assert
        Assert.That(item2, Is.EqualTo(topItem));
        Assert.That(_heap.Count(), Is.EqualTo(2));
    }

    [Test]
    public void Update_ShouldModifyPriorityOfExistingItem()
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
    public void Update_ShouldInsertNewItemIfItDoesNotExist()
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
    public void Top_ShouldReturnNullWhenHeapIsEmpty()
    {
        // Act
        var result = _heap.Top();

        // Assert
        Assert.That(result, Is.Null);
    }

    [Test]
    public void Peek_ShouldReturnNullWhenHeapIsEmpty()
    {
        // Act
        var result = _heap.Peek();

        // Assert
        Assert.That(result, Is.Null);
    }

    [Test]
    public void Insert_ShouldMaintainHeapProperty()
    {
        // Arrange
        _heap.Insert(PriorityQueueItem.Create(5, "Item1"));
        _heap.Insert(PriorityQueueItem.Create(10, "Item2"));
        _heap.Insert(PriorityQueueItem.Create(15, "Item3"));

        // Act
        var topItem = _heap.Peek();

        // Assert
        Assert.That(topItem, Is.Not.Null);
        Assert.That(topItem.Priority, Is.EqualTo(15));
    }

    [Test]
    public void Insert_MultipleItems_ShouldArrangeItemsAccordingToPriority()
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
        Assert.That(topItem.Priority, Is.EqualTo(30));
    }
}