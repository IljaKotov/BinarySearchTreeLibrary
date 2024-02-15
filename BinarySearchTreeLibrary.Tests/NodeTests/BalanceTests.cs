using FluentAssertions;
using BinarySearchTreeLibrary.Models;

namespace BinarySearchTreeLibrary.Tests.NodeTests;

public static class BalanceTests
{
	[Fact]
	
	public static void Should_Balance_The_Tree()
	{
		// Arrange
		var root = new Node<int>(8);
		root.Insert(10);
		root.Insert(5);
		root.Insert(15);
		root.Insert(3);
		root.Insert(4);
		root.Insert(18);
		// Act
		root.Balance();
		
		root.Height.Should().Be(2);
	}
}