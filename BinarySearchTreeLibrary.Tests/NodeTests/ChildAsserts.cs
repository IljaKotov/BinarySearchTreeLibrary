using BinarySearchTreeLibrary.Interfaces;
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
}