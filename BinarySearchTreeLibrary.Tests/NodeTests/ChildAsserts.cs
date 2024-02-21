using BinarySearchTreeLibrary.Interfaces;
using BinarySearchTreeLibrary.Models;
using FluentAssertions;

namespace BinarySearchTreeLibrary.Tests.NodeTests;

internal static class ChildAsserts
{
	public static void AssertHeights<T>(INode<T>? root, int expRootHeight, int expLeftHeight, int expRightHeight)
	{
		root?.Height.Should().Be(expRootHeight);
		root?.Left?.Height.Should().Be(expLeftHeight);
		root?.Right?.Height.Should().Be(expRightHeight);
	}
	
	public static void AssertHeights<T>(INode<T>? root,  int expLeftHeight, int expRightHeight)
	{
		root?.Left?.Height.Should().Be(expLeftHeight);
		root?.Right?.Height.Should().Be(expRightHeight);
	}
	
	public static void AssertData<T>(INode<T>? node,  T expLeftData, T expRightData)
	{
		if (expLeftData is NullNode<T>)
			node?.Left.Should().BeEquivalentTo(new NullNode<T>());
		else
			node?.Left?.Data.Should().Be(expLeftData);

		if (expRightData is NullNode<T>)
			node?.Right.Should().BeEquivalentTo(new NullNode<T>());
		else
			node?.Right?.Data.Should().Be(expRightData);
	}
}