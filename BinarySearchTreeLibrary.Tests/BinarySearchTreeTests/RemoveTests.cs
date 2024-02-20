using BinarySearchTreeLibrary.Exceptions;
using BinarySearchTreeLibrary.Models;
using BinarySearchTreeLibrary.Tests.NodesCases;
using BinarySearchTreeLibrary.Tests.NodesCases.CaseGenerators;
using FluentAssertions;

namespace BinarySearchTreeLibrary.Tests.BinarySearchTreeTests;

public static class RemoveTests
{
	private static object[] _input = Array.Empty<object>();

	[Fact(DisplayName = "Remove method should throw EmptyTreeException when removing from empty tree")]
	public static void Remove_EmptyTree_ShouldThrowEmptyTreeException()
	{
		var tree = new BinarySearchTree<object>();

		Assert.Throws<EmptyTreeException>(() => tree.Delete(10));
	}

	[Fact(DisplayName = "Remove method should throw ArgumentNullException when inserting null data")]
	public static void Remove_NullData_ShouldThrowArgumentNullException()
	{
		var tree = new BinarySearchTree<string?>();
		tree.Add("Some data");

		Assert.Throws<ArgumentNullException>(() => tree.Delete(null));
	}

	[Theory(DisplayName =
		"Should correctly Remove all nodes and Update Height.")]
	[MemberData(nameof(DeepBalancedTreeCaseGenerator.GetTreeCases),
		MemberType = typeof(DeepBalancedTreeCaseGenerator))]
	public static void Should_CorrectlyRemove_WorkWithAllNodes(NodeCase testCase)
	{
		var tree = new BinarySearchTree<object>();
		_input = testCase.InputData;

		foreach (var data in _input)
			tree.Add(data);

		tree.Height.Should().Be(3);
		tree.IsBalanced().Should().BeTrue();
		tree.Root.Should().Be(_input[0]);

		foreach (var data in _input)
			tree.Delete(data).Should().BeTrue();

		tree.Root.Should().BeNull();
		tree.Height.Should().Be(-1);
	}

	[Theory(DisplayName = "Should correctly Remove and replace Root.")]
	[MemberData(nameof(DeepBalancedTreeCaseGenerator.GetTreeCases),
		MemberType = typeof(DeepBalancedTreeCaseGenerator))]
	public static void Should_CorrectlyRemove_AndReplaceRoot(NodeCase testCase)
	{
		var tree = new BinarySearchTree<object>();
		_input = testCase.InputData;

		foreach (var data in _input)
			tree.Add(data);

		tree.Root.Should().Be(_input[0]);
		tree.Height.Should().Be(3);
		tree.Size.Should().Be(13);

		tree.Delete(_input[0]).Should().BeTrue();

		tree.Root.Should().Be(_input[5]);
		tree.Height.Should().Be(3);
		tree.IsBalanced().Should().BeTrue();
		tree.Contains(_input[0]).Should().BeFalse();
		tree.Size.Should().Be(12);
	}

	[Theory(DisplayName = "Should correctly remove nodes with update balancing value")]
	[MemberData(nameof(DeepBalancedTreeCaseGenerator.GetTreeCases),
		MemberType = typeof(DeepBalancedTreeCaseGenerator))]
	public static void Should_CorrectlyRemove_AndUpdateBalancingValue(NodeCase testCase)
	{
		var tree = new BinarySearchTree<object>();
		_input = testCase.InputData;

		foreach (var data in _input)
			tree.Add(data);

		tree.IsBalanced().Should().BeTrue();
		tree.RootBalanceFactor.Should().Be(0);

		tree.Delete(_input[2]).Should().BeTrue();
		tree.Height.Should().Be(3);
		tree.Delete(_input[10]).Should().BeTrue();
		tree.Height.Should().Be(3);

		tree.IsBalanced().Should().BeFalse();
		tree.RootBalanceFactor.Should().Be(0);
		tree.Contains(_input[10]).Should().BeFalse();
		tree.Contains(_input[2]).Should().BeFalse();
	}

	[Theory(DisplayName = "Should correctly remove last level of nodes and update Height, IsBalanced properties")]
	[MemberData(nameof(DeepBalancedTreeCaseGenerator.GetTreeCases),
		MemberType = typeof(DeepBalancedTreeCaseGenerator))]
	public static void Should_CorrectlyRemove_LastLevelOfNodes_AndUpdateHeightAndIsBalancedProperties(NodeCase testCase)
	{
		var tree = new BinarySearchTree<object>();
		_input = testCase.InputData;

		foreach (var data in _input)
			tree.Add(data);

		tree.Height.Should().Be(3);

		tree.Delete(_input[8]).Should().BeTrue();
		tree.Delete(_input[9]).Should().BeTrue();
		tree.Delete(_input[6]).Should().BeTrue();
		tree.Delete(_input[11]).Should().BeTrue();
		tree.Delete(_input[12]).Should().BeTrue();
		tree.Delete(_input[10]).Should().BeTrue();
		tree.Height.Should().Be(2);
		tree.IsBalanced().Should().BeTrue();
	}

	[Theory(DisplayName =
		"It's complexly test of Insert, Contains and Remove methods work. Should correctly functionality and Update Height, IsBalanced properties during work")]
	[MemberData(nameof(DeepBalancedTreeCaseGenerator.GetTreeCases),
		MemberType = typeof(DeepBalancedTreeCaseGenerator))]
	public static void Should_CorrectlyFunctionality_WorkWithSomeNodes(NodeCase testCase)
	{
		var tree = new BinarySearchTree<object>();
		_input = testCase.InputData;

		foreach (var data in _input)
			tree.Add(data);

		Validate_Remove_LeftSubTree(tree);
		Validate_RestorationBalanceProp_InsertNode(tree);
		Validate_Remove_LastLevelNodes(tree);
		Validate_UpdateHeight_InsertNodeToNextLevel(tree);
		Validate_RemoveInsert_UpdateProp(tree);
	}

	private static void Validate_Remove_LeftSubTree(BinarySearchTree<object> tree)
	{
		tree.Delete(_input[2]).Should().BeTrue();
		tree.Height.Should().Be(3);
		tree.IsBalanced().Should().BeTrue();

		tree.Delete(_input[10]).Should().BeTrue();
		tree.Height.Should().Be(3);
		tree.IsBalanced().Should().BeFalse();
		tree.RootBalanceFactor.Should().Be(0);
	}

	private static void Validate_RestorationBalanceProp_InsertNode(BinarySearchTree<object> tree)
	{
		tree.Add(_input[2]);
		tree.Height.Should().Be(3);
		tree.IsBalanced().Should().BeTrue();
	}

	private static void Validate_Remove_LastLevelNodes(BinarySearchTree<object> tree)
	{
		tree.Delete(_input[8]).Should().BeTrue();
		tree.Delete(_input[9]).Should().BeTrue();
		tree.Delete(_input[6]).Should().BeTrue();
		tree.Delete(_input[11]).Should().BeTrue();
		tree.Delete(_input[12]).Should().BeTrue();

		tree.Height.Should().Be(2);
		tree.IsBalanced().Should().BeTrue();
		tree.RootBalanceFactor.Should().Be(0);
	}

	private static void Validate_UpdateHeight_InsertNodeToNextLevel(BinarySearchTree<object> tree)
	{
		tree.Add(_input[12]);
		tree.Height.Should().Be(3);
		tree.IsBalanced().Should().BeTrue();
	}

	private static void Validate_RemoveInsert_UpdateProp(BinarySearchTree<object> tree)
	{
		tree.Delete(_input[5]).Should().BeTrue();
		tree.Height.Should().Be(2);
		tree.IsBalanced().Should().BeTrue();

		tree.Delete(_input[4]).Should().BeTrue();
		tree.Height.Should().Be(2);
		tree.IsBalanced().Should().BeTrue();

		tree.Add(_input[10]);
		tree.Delete(_input[3]).Should().BeTrue();
		tree.Height.Should().Be(3);
		tree.IsBalanced().Should().BeFalse();
		tree.RootBalanceFactor.Should().Be(2);
	}
	/*    Visual representation of the test four-level tree (INDEXES of the test-case's input array)
	*                     0
	*                   /   \
	*  		   	      /      \
	* 				 1        3
	* 			  /   \      /  \
	* 			7     2     5    4
	*		  / \	 /      \   / \
	*		8	9	10  	12	6  11
	*/
}