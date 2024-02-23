using BinarySearchTreeLibrary.Exceptions;
using BinarySearchTreeLibrary.Models;
using BinarySearchTreeLibrary.Tests.NodesCases;
using BinarySearchTreeLibrary.Tests.NodesCases.CaseGenerators;
using FluentAssertions;

namespace BinarySearchTreeLibrary.Tests.BinarySearchTreeTests;

public static class IsBinarySearchTreeTests
{
	[Fact(DisplayName = "IsBinarySearchTree should throw EmptyTreeException when the tree is empty")]
	public static void IsBinarySearchTree_EmptyTree_ShouldThrowEmptyTreeException()
	{
		var tree = new BinarySearchTree<object>();

		Assert.Throws<EmptyTreeException>(() => tree.IsBinarySearchTree());
	}

	[Fact(DisplayName = "IsBinarySearchTree should return true when the tree has only one or two node")]
	public static void IsBinarySearchTree_TwoNode_ShouldReturnTrue()
	{
		var tree = TestTreeFactory.CreateTree(10);
		tree.IsBinarySearchTree().Should().BeTrue();

		tree.Add(20);
		tree.IsBinarySearchTree().Should().BeTrue();
	}

	[Theory(DisplayName = "IsBinarySearchTree should return true when the tree is a binary search tree")]
	[MemberData(nameof(MultiLevelTreeCase.GetTreeCases),
		MemberType = typeof(MultiLevelTreeCase))]
	public static void IsBinarySearchTree_MultiLevelTree_ShouldReturnTrue(NodeCase testCase)
	{
		var tree = TestTreeFactory.CreateTree(testCase.InputData, true);

		tree?.IsBinarySearchTree().Should().BeTrue();
	}

	[Theory(DisplayName = "IsBinarySearchTree should return false when the tree is not a binary search tree")]
	[InlineData(10, 20, 25)]
	[InlineData(10, 5, 6)]
	[InlineData(10, 15, 5)]
	public static void IsBinarySearchTree_NotBinarySearchTree_ShouldReturnFalse(object root,
		object left,
		object? right)
	{
		var tree = new BinarySearchTree<object?>(root, left, right);

		tree.IsBinarySearchTree().Should().BeFalse();
	}
}