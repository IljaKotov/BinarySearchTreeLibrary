using BinarySearchTreeLibrary.Interfaces;
using BinarySearchTreeLibrary.Models;
using FluentAssertions;

namespace BinarySearchTreeLibrary.Tests.NodeTests;

internal static class NodeAsserts
{
	public static void AssertNode<T>(INode<T> node, object expectedData, int expectedHeight, bool expectedBalance, INode<T>? expectedParent)
	{
		if (node is NullNode<T>)
		{
			AssertNullNode(node);
			return;
		}
		node.Data.Should().Be(expectedData);
		node.Height.Should().Be(expectedHeight);
		node.IsBalanced.Should().Be(expectedBalance);
		node.Key.Should().Be(expectedData.GetHashCode());
		node.Parent.Should().Be(expectedParent);
	}
	
	public static void AssertNode<T>(INode<T> node, object expectedData, int expectedHeight)
	{
		if (node is NullNode<T>)
		{
			AssertNullNode(node);
			return;
		}
		node.Data.Should().Be(expectedData);
		node.Height.Should().Be(expectedHeight);
		node.Key.Should().Be(expectedData.GetHashCode());
	}
	
	public static void AssertNode<T>(INode<T> node, object expectedData)
	{
		if (node is NullNode<T>)
		{
			AssertNullNode(node);
			return;
		}
		node.Data.Should().Be(expectedData);
	}
	
	private static void AssertNullNode<T>(INode<T> node)
	{
		node.Should().BeEquivalentTo(new NullNode<T>());
		node.Height.Should().Be(-1);
		node.IsBalanced.Should().BeTrue();
		node.Key.Should().Be(0);
	}
}