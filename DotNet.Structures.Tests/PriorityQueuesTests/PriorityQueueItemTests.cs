using DotNet.DataStructures.PriorityQueues;

namespace DotNet.Structures.Tests.PriorityQueuesTests;

[TestFixture]
public class PriorityQueueItemTests
    {
        [Test]
        public void Create_WithValidPriorityAndValue_ShouldReturnCorrectItem()
        {
            // Arrange
            int priority = 10;
            string value = "TestItem";

            // Act
            var result = PriorityQueueItem.Create(priority, value);

            // Assert
            Assert.NotNull(result);
            Assert.That(result.Priority, Is.EqualTo(priority));
            Assert.That(result.Value, Is.EqualTo(value));
        }
        
        [Test]
        public void Create_WithDifferentEquatableTypes_ShouldReturnCorrectItems()
        {
            // Arrange
            int priorityInt = 5;
            int intValue = 100;

            int priorityString = 3;
            string stringValue = "StringItem";

            // Act
            var intItem = PriorityQueueItem.Create(priorityInt, intValue);
            var stringItem = PriorityQueueItem.Create(priorityString, stringValue);

            // Assert
            Assert.That(intItem.Priority, Is.EqualTo(priorityInt));
            Assert.That(intItem.Value, Is.EqualTo(intValue));

            Assert.That(stringItem.Priority, Is.EqualTo(priorityString));
            Assert.That(stringItem.Value, Is.EqualTo(stringValue));
        }
        
        [Test]
        public void Create_WithCustomType_ShouldReturnCorrectItem()
        {
            // Arrange
            int priority = 7;
            var customObject = new CustomEquatableType { Id = 1, Name = "TestObject" };

            // Act
            var result = PriorityQueueItem.Create(priority, customObject);

            // Assert
            Assert.That(result.Priority, Is.EqualTo(priority));
            Assert.That(result.Value, Is.EqualTo(customObject));
            Assert.IsInstanceOf<CustomEquatableType>(result.Value);
            Assert.That(((CustomEquatableType)result.Value).Name, Is.EqualTo("TestObject"));
        }
        
        private class CustomEquatableType : IEquatable<CustomEquatableType>
        {
            public int Id { get; set; }
            public string Name { get; set; } = string.Empty;

            public bool Equals(CustomEquatableType? other)
            {
                return other != null && Id == other.Id && Name == other.Name;
            }

            public override bool Equals(object? obj)
            {
                return Equals(obj as CustomEquatableType);
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(Id, Name);
            }
        }
    }