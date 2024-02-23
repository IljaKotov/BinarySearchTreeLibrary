using BinarySearchTreeLibrary.Interfaces;
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

	public static void AssertHeights<T>(INode<T>? root, int expLeftHeight, int expRightHeight)
	{
		root?.Left?.Height.Should().Be(expLeftHeight);
		root?.Right?.Height.Should().Be(expRightHeight);
	}

	public static void AssertIsBalanced<T>(INode<T>? node, bool expLeftBalance, bool expRightBalance)
	{
		node?.Left?.IsBalanced.Should().Be(expLeftBalance);
		node?.Right?.IsBalanced.Should().Be(expRightBalance);
	}

	public static void AssertData<T>(INode<object>? node, T? expLeftData, T? expRightData)
	{
		node?.Left?.Data.Should().Be(expLeftData);
		node?.Right?.Data.Should().Be(expRightData);
	}

	public static void AssertData(INode<int>? node, int? expLeftData, int? expRightData)
	{
		node?.Left?.Data.Should().Be(expLeftData);
		node?.Right?.Data.Should().Be(expRightData);
	}

	public static void AssertBalanceFactor<T>(INode<T>? node, int expLeftBalanceFactor, int expRightBalanceFactor)
	{
		node?.Left?.BalanceFactor.Should().Be(expLeftBalanceFactor);
		node?.Right?.BalanceFactor.Should().Be(expRightBalanceFactor);
	}
}