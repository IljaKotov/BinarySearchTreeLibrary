using BinarySearchTreeLibrary.Models;
using Bogus;
using FluentAssertions;

namespace BinarySearchTreeLibrary.Tests.NodeTests.PropertiesTests.AuxiliaryPropertiesTests;

public class ParentalProperties
{
	[Fact]
	public void Node_ShouldReturnCorrectChildAndParentProperties()
	{
		// Arrange
		var root= new Node<int>(10);
		
		//root.HasParent.Should().BeFalse();
		root.HasBothChildren.Should().BeFalse();
		//root.HasLeftChild.Should().BeFalse();
		//root.HasRightChild.Should().BeFalse();
	}
	
	[Fact]
	public void Nodes_ShouldReturnCorrectChildAndParentProperties()
	{
		// Arrange
		var root= new Node<int>(10);
		root.Insert(15);
		root.Insert(5);
		root.Insert(20);
		root.Insert(3);
		
		//root.HasParent.Should().BeFalse();
		root.HasBothChildren.Should().BeTrue();
		//root.HasLeftChild.Should().BeTrue();
		//root.HasRightChild.Should().BeTrue();
		
		//root.Left.HasParent.Should().BeTrue();
		root.Left.HasBothChildren.Should().BeFalse();
		//root.Left.HasLeftChild.Should().BeTrue();
		//root.Left.HasRightChild.Should().BeFalse();
		
		//root.Right.HasParent.Should().BeTrue();
		root.Right.HasBothChildren.Should().BeFalse();
		//root.Right.HasLeftChild.Should().BeFalse();
		//root.Right.HasRightChild.Should().BeTrue();
		
		//root.Left.Left.HasParent.Should().BeTrue();
		root.Left.Left.HasBothChildren.Should().BeFalse();
		//root.Left.Left.HasLeftChild.Should().BeFalse();
		//root.Left.Left.HasRightChild.Should().BeFalse();
	}
}