using BinarySearchTreeLibrary.Exceptions;
using BinarySearchTreeLibrary.Models;
using BinarySearchTreeLibrary.Tests.NodesCases;
using BinarySearchTreeLibrary.Tests.NodesCases.CaseGenerators;
using FluentAssertions;
using NUnit.Framework;
using Assert = Xunit.Assert;

namespace BinarySearchTreeLibrary.Tests.BinarySearchTreeTests;

public class DeleteTests
{
	private static object[] _input = Array.Empty<object>();
	private static BinarySearchTree<object>? _tree;

	[Fact(DisplayName = "Delete_method should throw EmptyTreeException when deleting from empty tree")]
	public static void Delete_EmptyTree_ShouldThrowEmptyTreeException()
	{
		var tree = new BinarySearchTree<object>();

		Assert.Throws<EmptyTreeException>(() => tree.TryDelete(10));
	}

	[Fact(DisplayName = "Delete_method should throw ArgumentNullException when inserting null data")]
	public static void Delete_NullData_ShouldThrowArgumentNullException()
	{
		var tree = TestTreeFactory.CreateTree("Some data");

		Assert.Throws<ArgumentNullException>(() => tree.TryDelete(null));
	}

	[Fact(DisplayName = "Delete_method should throw NodeNotFoundException when deleting not existing data")]
	public static void Delete_NotExistingData_ShouldThrowNodeNotFoundException()
	{
		var tree = TestTreeFactory.CreateTree(10);

		Assert.Throws<NodeNotFoundException>(() => tree.TryDelete(20));
	}

	[Xunit.Theory(DisplayName =
		"Should correctly delete all nodes and Update Height.")]
	[MemberData(nameof(MultiLevelTreeCase.GetTreeCases),
		MemberType = typeof(MultiLevelTreeCase))]
	public void Delete_MultiLevelTree_DeleteAllNodes(NodeCase testCase)
	{
		SetUp(testCase);

		_tree?.Height.Should().Be(3);
		_tree?.IsBalanced().Should().BeTrue();
		_tree?.RootData.Should().Be(_input[0]);

		foreach (var data in _input)
			_tree?.TryDelete(data).Should().BeTrue();

		_tree?.RootData.Should().BeNull();
		_tree?.Height.Should().Be(-1);
		_tree?.Size.Should().Be(0);
	}

	[Xunit.Theory(DisplayName = "Should correctly delete and replace Root.")]
	[MemberData(nameof(MultiLevelTreeCase.GetTreeCases),
		MemberType = typeof(MultiLevelTreeCase))]
	public void Delete_Root_RemoveAndReplaceRoot(NodeCase testCase)
	{
		SetUp(testCase);

		_tree?.RootData.Should().Be(_input[0]);
		_tree?.Height.Should().Be(3);
		_tree?.Size.Should().Be(13);

		_tree?.TryDelete(_input[0]).Should().BeTrue();

		_tree?.RootData.Should().Be(_input[5]);
		_tree?.Height.Should().Be(3);
		_tree?.IsBalanced().Should().BeTrue();
		_tree?.Contains(_input[0]).Should().BeFalse();
		_tree?.Size.Should().Be(12);
	}

	[Xunit.Theory(DisplayName = "Should correctly delete nodes with update balancing value")]
	[MemberData(nameof(MultiLevelTreeCase.GetTreeCases),
		MemberType = typeof(MultiLevelTreeCase))]
	public void Delete_SubTree_UpdateBalancingValue(NodeCase testCase)
	{
		SetUp(testCase);

		_tree?.IsBalanced().Should().BeTrue();
		_tree?.RootBalanceFactor.Should().Be(0);

		_tree?.TryDelete(_input[2]).Should().BeTrue();
		_tree?.Height.Should().Be(3);

		_tree?.TryDelete(_input[10]).Should().BeTrue();
		_tree?.Height.Should().Be(3);

		_tree?.IsBalanced().Should().BeFalse();
		_tree?.RootBalanceFactor.Should().Be(0);
		_tree?.Contains(_input[10]).Should().BeFalse();
		_tree?.Contains(_input[2]).Should().BeFalse();
	}

	[Xunit.Theory(DisplayName =
		"Should correctly delete the lowest level of nodes and update Height, IsBalanced properties")]
	[MemberData(nameof(MultiLevelTreeCase.GetTreeCases),
		MemberType = typeof(MultiLevelTreeCase))]
	public void Delete_LowestLevelOfNodes_UpdateHeightAndIsBalancedProperties(NodeCase testCase)
	{
		SetUp(testCase);

		_tree?.Height.Should().Be(3);

		var lowestNodes = new[]
		{
			_input[8], _input[9], _input[6], _input[11], _input[12], _input[10]
		};

		foreach (var node in lowestNodes)
			_tree?.TryDelete(node).Should().BeTrue();

		_tree?.Height.Should().Be(2);
		_tree?.IsBalanced().Should().BeTrue();
	}

	[Xunit.Theory(DisplayName =
		"Complexly test of Add, Contains and Delete methods work.")]
	[MemberData(nameof(MultiLevelTreeCase.GetTreeCases),
		MemberType = typeof(MultiLevelTreeCase))]
	public void Delete_ComplexTest_CorrectWork(NodeCase testCase)
	{
		SetUp(testCase);

		Validate_Remove_LeftSubTree(_tree);
		Validate_RestorationBalanceProp_InsertNode(_tree);
		Validate_Remove_LastLevelNodes(_tree);
		Validate_UpdateHeight_InsertNodeToNextLevel(_tree);
		Validate_RemoveInsert_UpdateProp(_tree);
	}

	[SetUp]
	internal void SetUp(NodeCase testCase)
	{
		_input = testCase.InputData;
		_tree = TestTreeFactory.CreateTree(_input, true);
	}

	private static void Validate_Remove_LeftSubTree(BinarySearchTree<object>? tree)
	{
		tree?.TryDelete(_input[2]).Should().BeTrue();
		tree?.Height.Should().Be(3);
		tree?.IsBalanced().Should().BeTrue();

		tree?.TryDelete(_input[10]).Should().BeTrue();
		tree?.Height.Should().Be(3);
		tree?.IsBalanced().Should().BeFalse();
		tree?.RootBalanceFactor.Should().Be(0);
	}

	private static void Validate_RestorationBalanceProp_InsertNode(BinarySearchTree<object>? tree)
	{
		tree?.Add(_input[2]);
		tree?.Height.Should().Be(3);
		tree?.IsBalanced().Should().BeTrue();
	}

	private static void Validate_Remove_LastLevelNodes(BinarySearchTree<object>? tree)
	{
		var lowestNodes = new[]
		{
			_input[8], _input[9], _input[6], _input[11], _input[12]
		};

		foreach (var node in lowestNodes)
			_tree?.TryDelete(node).Should().BeTrue();

		tree?.Height.Should().Be(2);
		tree?.IsBalanced().Should().BeTrue();
		tree?.RootBalanceFactor.Should().Be(0);
	}

	private static void Validate_UpdateHeight_InsertNodeToNextLevel(BinarySearchTree<object>? tree)
	{
		tree?.Add(_input[12]);
		tree?.Height.Should().Be(3);
		tree?.IsBalanced().Should().BeTrue();
	}

	private static void Validate_RemoveInsert_UpdateProp(BinarySearchTree<object>? tree)
	{
		tree?.TryDelete(_input[5]).Should().BeTrue();
		tree?.Height.Should().Be(2);
		tree?.IsBalanced().Should().BeTrue();

		tree?.TryDelete(_input[4]).Should().BeTrue();
		tree?.Height.Should().Be(2);
		tree?.IsBalanced().Should().BeTrue();

		tree?.Add(_input[10]);
		tree?.TryDelete(_input[3]).Should().BeTrue();
		tree?.Height.Should().Be(3);
		tree?.IsBalanced().Should().BeFalse();
		tree?.RootBalanceFactor.Should().Be(2);
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