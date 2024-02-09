using BinarySearchTreeLibrary.Models;
using FluentAssertions;

namespace BinarySearchTreeLibrary.Tests.NodeTests.MethodsTests;

public class RemoveChild
{
	[Theory(DisplayName = "RemoveChild method tests")]
	[InlineData(5)]
	[InlineData(7)]
	[InlineData(3)]
	public void RemoveChild_Should_RemoveCorrectNode(int data)
	{
		// Arrange
		var rootNode = new Node<int>(5);
		rootNode.Insert(3);
		rootNode.Insert(7);

		// Act
		var result = rootNode.RemoveChild(data.GetHashCode());

		// Assert
		result.Should().BeTrue();
		rootNode.FindChild(data.GetHashCode()).Should().BeNull();
	}

	[Theory(DisplayName = "RemoveChild method with invalid data")]
	[InlineData(10)] // Key not present in the tree
	public void RemoveChild_Should_ReturnFalseForInvalidKey(int key)
	{
		// Arrange
		var rootNode = new Node<int>(5);
		rootNode.Insert(3);
		rootNode.Insert(7);

		// Act
		var result = rootNode.RemoveChild(key.GetHashCode());

		// Assert
		result.Should().BeFalse();
	}

	[Theory(DisplayName = "RemoveChild method with null data")]
	[InlineData(null)] // Null key
	public void RemoveChild_Should_ReturnFalseForNullKey(int? key)
	{
		// Arrange
		var rootNode = new Node<int>(5);
		rootNode.Insert(3);
		rootNode.Insert(7);
		key = key.GetHashCode();

		// Act
		var result = rootNode.RemoveChild(key ?? 0); // Providing a non-null default value

		// Assert
		result.Should().BeFalse();
	}

	[Fact]
	public void Remove_Child_Should_Return_True_And_Replacement_Nodes()
	{
		var rootNode = new Node<int>(10);
		rootNode.Insert(5);
		rootNode.Insert(2);
		rootNode.Insert(1);
		rootNode.Insert(4);
		rootNode.Insert(7);
		rootNode.Insert(6);
		rootNode.Insert(8);
		rootNode.Insert(15);
		rootNode.Insert(12);
		rootNode.Insert(17);
		rootNode.Insert(16);
		rootNode.Insert(13);

		//rootNode.Insert(11);
		rootNode.RemoveChild(12);
		rootNode.RemoveChild(17);
		rootNode.RemoveChild(5);

		rootNode.Right.Left.Data.Should().Be(13);
		rootNode.Right.Right.Data.Should().Be(16);
		rootNode.Left.Data.Should().Be(6);
		rootNode.Left.Right.Right.Data.Should().Be(8);
		rootNode.Left.Right.Left.Should().BeNull();
		rootNode.Left.Right.Data.Should().Be(7);
		rootNode.Left.Left.Data.Should().Be(2);
		rootNode.Left.Left.Left.Data.Should().Be(1);
		rootNode.Left.Left.Right.Data.Should().Be(4);

	}

	[Fact(DisplayName = "Remove Root should be true and replace root")]
	public void Remove_Root_Should_Be_True_And_Replace_Root()
	{
		var rootNode = new Node<int>(10);
		rootNode.Insert(5);
		rootNode.Insert(12);
		rootNode.Insert(2);
		rootNode.Insert(4);
		rootNode.Insert(1);

		rootNode.RemoveChild(10);
		rootNode.Data.Should().Be(12);
		rootNode.Left.Data.Should().Be(5);
		//rootNode.Right.Data.Should().Be(4);
		rootNode.Left.Left.Data.Should().Be(2);
		rootNode.Left.Left.Left.Data.Should().Be(1);
		rootNode.Left.Left.Right.Data.Should().Be(4);
		rootNode.Right.Should().BeNull();
	}
	
	[Fact(DisplayName = "Remove Root should be true and replace root")]
	public void Remove_Root_Should_Be_True_And_Replace_Root1()
	{
		var rootNode = new Node<int>(10);
		rootNode.Insert(5);
		rootNode.Insert(2);
		rootNode.Insert(4);
		rootNode.Insert(1);

		rootNode.RemoveChild(10);
		rootNode.Data.Should().Be(5);
		rootNode.Left.Data.Should().Be(2);
		rootNode.Left.Right.Data.Should().Be(4);
		rootNode.Left.Left.Data.Should().Be(1);
		rootNode.Left.Left.Right.Should().BeNull();
	}
	[Fact(DisplayName = "Remove Root should be true and replace root")]
	public void Remove_Root_Should_Be_True_And_Replace_Root2()
	{
		var rootNode = new Node<int>(10);
		rootNode.Insert(5);
		rootNode.Insert(12);
		rootNode.Insert(11);
		rootNode.Insert(14);
		rootNode.Insert(2);
		rootNode.Insert(4);
		rootNode.Insert(1);

		rootNode.RemoveChild(10);
		rootNode.Data.Should().Be(11);
		rootNode.Left.Data.Should().Be(5);
		rootNode.Right.Data.Should().Be(12);
		rootNode.Left.Left.Data.Should().Be(2);
		rootNode.Left.Left.Left.Data.Should().Be(1);
		rootNode.Left.Left.Right.Data.Should().Be(4);
		rootNode.Right.Right.Data.Should().Be(14);
		rootNode.Right.Left.Should().BeNull();
		rootNode.Right.Right.Right.Should().BeNull();
	}
}