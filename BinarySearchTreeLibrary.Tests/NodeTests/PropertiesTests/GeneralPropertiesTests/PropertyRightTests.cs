using BinarySearchTreeLibrary.Models;
using FluentAssertions;

namespace BinarySearchTreeLibrary.Tests.NodeTests.PropertiesTests.GeneralPropertiesTests;

public class PropertyRightTests
{
	[Fact]
	public void Node_Right_ShouldBeNullInitially()
	{
		// Arrange
		var node = new Node<int>(10);

		// Assert
		node.Right.Should().BeNull();
	}

	[Fact]
	public void Node_Right_ShouldSetValidValueForRightChild()
	{
		// Arrange
		var node = new Node<int>(10);
		node.Insert(15);

		// Assert
		node.Right.Data.Should().Be(15);
	}
}