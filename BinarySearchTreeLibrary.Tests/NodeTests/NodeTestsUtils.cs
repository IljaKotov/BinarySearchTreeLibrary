using BinarySearchTreeLibrary.Models;
using FluentAssertions;

namespace BinarySearchTreeLibrary.Tests.NodeTests;

internal static class NodeTestsUtils
{
	public static void ValidateRoot<T>(Node<T> result, object data, int height, bool isBalanced)
	{
		result.Data.Should().Be(data);
		result.Key.Should().Be(data.GetHashCode());
		result.Height.Should().Be(height);
		result.Parent.Should().BeNull();
		result.IsBalanced.Should().Be(isBalanced);
	}
}