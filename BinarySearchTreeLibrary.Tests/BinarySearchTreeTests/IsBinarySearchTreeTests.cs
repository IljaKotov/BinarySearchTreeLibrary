using System.Runtime.InteropServices;
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
	
	[Fact(DisplayName = "IsBinarySearchTree should return true when the tree has only one node")]
	
	public static void IsBinarySearchTree_OneNode_ShouldReturnTrue()
	{
		var tree = new BinarySearchTree<int>();
		tree.Insert(10);
		tree.Insert(20);
		
		tree.IsBinarySearchTree().Should().BeTrue();
	}
	
	[Theory(DisplayName = "IsBinarySearchTree should return true when the tree is a binary search tree")]
	[MemberData(nameof(DeepBalancedTreeCaseGenerator.GetTreeCases),
		MemberType = typeof(DeepBalancedTreeCaseGenerator))]
	
	public static void IsBinarySearchTree_ShouldReturnTrue(NodeCase testCase)
	{
		var tree = new BinarySearchTree<object>();
		var input = testCase.InputData;
		
		foreach (var data in input)
			tree.Insert(data);
		
		tree.IsBinarySearchTree().Should().BeTrue();
	}
	
	[Theory(DisplayName = "IsBinarySearchTree should return false when the tree is not a binary search tree")]
	[InlineData(10, 20, 25)]
	[InlineData(10, 5, 6)]
	[InlineData(10, 15, 5)]
	
	public static void IsBinarySearchTree_NotBinarySearchTree_ShouldReturnFalse(object root, object left, object right)
	{
		var tree = new BinarySearchTree<object>(root, left, right);
		
		tree.IsBinarySearchTree().Should().BeFalse();
	}
}