using BinarySearchTreeLibrary.Models;
using FluentAssertions;

namespace BinarySearchTreeLibrary.Tests.NodeTests.PropertiesTests.AuxiliaryPropertiesTests;

public class IsLeafIsRootPropertiesTests
{
	[Fact]
	public void Node_IsLeaf_ShouldReturnTrueForLeafNode()
	{
		// Arrange
		var leafNode = new Node<int>(10);

		// Assert
		leafNode.IsLeaf.Should().BeTrue();
	}

	[Fact]
	public void Node_IsLeaf_ShouldReturnFalseForNonLeafNode()
	{
		// Arrange
		var parentNode = new Node<int>(10);
		parentNode.Insert(15);

		// Assert
		parentNode.IsLeaf.Should().BeFalse();
	}

	/*[Fact]
	public void Node_IsRoot_ShouldReturnTrueForRootNode()
	{
		// Arrange
		var rootNode = new Node<int>(10);

		// Assert
		rootNode.IsRoot.Should().BeTrue();
	}

	[Fact]
	public void Node_IsRoot_ShouldReturnFalseForNonRootNode()
	{
		// Arrange
		var rootNode = new Node<int>(10);
		rootNode.Insert(15);

		// Assert
		rootNode.Right.IsRoot.Should().BeFalse();
	}*/
}