using BinarySearchTreeLibrary.Interfaces;
using BinarySearchTreeLibrary.Models;
using FluentAssertions;

namespace BinarySearchTreeLibrary.Tests.AssertUtils;

internal static class NodeAsserts
{
	public static void AssertNode<T>(INode<T>? node, object expData, int expHeight, bool expBalance, INode<T>? expParent)
	{
		/*if (node is NullNode<T>)
		{
			AssertNullNode(node);
			return;
		}*/
		node?.Data.Should().Be(expData);
		node?.Height.Should().Be(expHeight);
		node?.IsBalanced.Should().Be(expBalance);
		node?.Key.Should().Be(expData.GetHashCode());
		node?.Parent.Should().Be(expParent);
	}
	
	public static void AssertNode<T>(INode<T>? node, object expData, int expHeight)
	{
		/*if (node is NullNode<T>)
		{
			AssertNullNode(node);
			return;
		}*/
		node?.Data.Should().Be(expData);
		node?.Height.Should().Be(expHeight);
		node?.Key.Should().Be(expData.GetHashCode());
	}
	
	public static void AssertNode<T>(INode<T>? node, object expData, INode<T>? expParent)
	{
		/*if (node is NullNode<T>)
		{
			AssertNullNode(node);
			node.Parent.Should().Be(expParent);
			return;
		}*/
		node?.Data.Should().Be(expData);
		node?.Key.Should().Be(expData.GetHashCode());
		node?.Parent.Should().Be(expParent);
	}
	
	public static void AssertNode<T>(INode<T>? node, object expData)
	{
		/*if (node is NullNode<T>)
		{
			AssertNullNode(node);
			return;
		}*/
		node?.Data.Should().Be(expData);
		node?.Key.Should().Be(expData.GetHashCode());
	}

	/*public static void AssertNode<T>(INode<T>? node)
	{
		AssertNullNode(node);
	}*/
	
	/*private static void AssertNullNode<T>(INode<T>? node)
	{
		node.Should().BeEquivalentTo(new NullNode<T>());
		node?.Height.Should().Be(-1);
		node?.IsBalanced.Should().BeTrue();
		node?.Key.Should().Be(0);
	}*/
}