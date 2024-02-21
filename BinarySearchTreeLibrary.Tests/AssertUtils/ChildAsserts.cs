using BinarySearchTreeLibrary.Interfaces;
using BinarySearchTreeLibrary.Models;
using FluentAssertions;

namespace BinarySearchTreeLibrary.Tests.AssertUtils;

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
	
	public static void AssertIsBalanced<T>(INode<T>? node, bool expLeftBalance, bool expRightBalance)
	{
		node?.Left?.IsBalanced.Should().Be(expLeftBalance);
		node?.Right?.IsBalanced.Should().Be(expRightBalance);
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