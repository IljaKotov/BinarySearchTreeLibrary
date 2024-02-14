using BinarySearchTreeLibrary.Models;
using FluentAssertions;

namespace BinarySearchTreeLibrary.Tests.BinarySearchTreeTests;

public static class ConstructorTests
{
	[Fact(DisplayName = "BinarySearchTree should correctly initialize properties")]
	
	public static void BinarySearchTree_ShouldCorrectlyInitializeProperties()
	{
		var tree = new BinarySearchTree<object>();

		tree.Size.Should().Be(0);
		tree.Height.Should().Be(-1);
		tree.RootBalanceFactor.Should().Be(0);
		tree.Root.Should().BeNull();
	}
}