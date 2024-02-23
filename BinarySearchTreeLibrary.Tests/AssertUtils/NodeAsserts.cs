using BinarySearchTreeLibrary.Interfaces;
using FluentAssertions;

namespace BinarySearchTreeLibrary.Tests.AssertUtils;

internal static class NodeAsserts
{
	public static void AssertNode<T>(INode<T>? node, object expData, int expHeight, bool expBalance, INode<T>? expParent)
	{
		node?.Data.Should().Be(expData);
		node?.Height.Should().Be(expHeight);
		node?.IsBalanced.Should().Be(expBalance);
		node?.Key.Should().Be(expData.GetHashCode());
		node?.Parent.Should().Be(expParent);
	}

	public static void AssertNode<T>(INode<T>? node, object expData, INode<T>? expParent)
	{
		node?.Data.Should().Be(expData);
		node?.Key.Should().Be(expData.GetHashCode());
		node?.Parent.Should().Be(expParent);
	}

	public static void AssertNode<T>(INode<object>? node, T expData)
	{
		node?.Data.Should().Be(expData);

		if (expData is not null)
			node?.Key.Should().Be(expData.GetHashCode());
	}
}