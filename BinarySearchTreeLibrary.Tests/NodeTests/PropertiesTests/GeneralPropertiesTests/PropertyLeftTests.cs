using BinarySearchTreeLibrary.Models;
using FluentAssertions;

namespace BinarySearchTreeLibrary.Tests.NodeTests.PropertiesTests.GeneralPropertiesTests;

public class PropertyLeftTests
{
	[Fact]
	public void Node_Left_ShouldBeNullInitially()
	{
		// Arrange
		var node = new Node<int>(10);

		// Assert
		node.Left.Should().BeNull();
	}

	[Fact]
	public void Node_Left_ShouldSetValidValueForLeftChild()
	{
		var node = new Node<int>(10);
		node.Insert(5);

		node.Left.Data.Should().Be(5);
	}
	
}