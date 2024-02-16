using System.Runtime.InteropServices;
using BinarySearchTreeLibrary.Exceptions;
using BinarySearchTreeLibrary.Models;
using FluentAssertions;

namespace BinarySearchTreeLibrary.Tests.BinarySearchTreeTests;

public static class BalanceTests
{
	[Fact(DisplayName = "Balance method should throw EmptyTreeException when balancing empty tree")]
	
	public static void Balance_EmptyTree_ShouldThrowEmptyTreeException()
	{
		var tree = new BinarySearchTree<object>();
		
		Assert.Throws<EmptyTreeException>(() => tree.Balance());
	}
	
	[Fact(DisplayName = "Should correctly balance the left degenerate tree")]

	public static void Should_CorrectlyBalanceLeftDegenerateTree()
	{
		var inputs = new[]
		{
			60, 50, 40, 30, 20, 10,0
		};
		
		var tree = new BinarySearchTree<int>();

		foreach (var data in  inputs)
			tree.Insert(data);

		tree.Balance();

		tree.IsBalanced().Should().BeTrue();
		
		tree.Root.Should().Be(30);
	}
}