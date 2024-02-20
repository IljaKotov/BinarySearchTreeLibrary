using BinarySearchTreeLibrary.Exceptions;
using BinarySearchTreeLibrary.Interfaces;
using BinarySearchTreeLibrary.Models;
using BinarySearchTreeLibrary.Tests.NodesCases;
using BinarySearchTreeLibrary.Tests.NodesCases.CaseGenerators;
using FluentAssertions;

namespace BinarySearchTreeLibrary.Tests.BinarySearchTreeTests;

public static class InsertAndContainsTests
{
	private static object[] _input = Array.Empty<object>();

	[Fact(DisplayName = "Insert method should throw ArgumentNullException when inserting null data")]

	public static void Insert_NullData_ShouldThrowArgumentNullException()
	{
		var root = new Node<string?>("Some data");

		Assert.Throws<ArgumentNullException>(() => root.Insert(null));
	}

	[Fact(DisplayName = "Contains method should throw ArgumentNullException when inserting null data")]

	public static void Contains_NullData_ShouldThrowArgumentNullException()
	{
		var tree = new BinarySearchTree<object?>();

		Assert.Throws<ArgumentNullException>(() => tree.Contains(null));
	}

	[Fact(DisplayName = "Contains method should throw EmptyTreeException when searching in empty tree")]

	public static void Contains_InEmptyTree_ShouldThrowEmptyTreeException()
	{
		var tree = new BinarySearchTree<object>();

		Assert.Throws<EmptyTreeException>(() => tree.Contains(10));
	}

	[Theory(DisplayName = "Should correctly insert and contains data, and set properties' values")]
	[MemberData(nameof(DeepBalancedTreeCaseGenerator.GetTreeCases),
		MemberType = typeof(DeepBalancedTreeCaseGenerator))]
	public static void Should_CorrectlyInsertAndContainsAllData_AndSetPropertiesValues(NodeCase testCase)
	{
		_input = testCase.InputData;
		var tree = new BinarySearchTree<object>();

		foreach (var data in _input)
			tree.Insert(data);

		tree.Size.Should().Be(_input.Length);
		tree.Height.Should().Be(3);
		tree.RootBalanceFactor.Should().Be(0);
		tree.IsBalanced().Should().BeTrue();
		tree.Root.Should().Be(_input[0]);

		foreach (var data in _input)
			tree.Contains(data).Should().BeTrue();
	}

	[Fact(DisplayName = "Should correctly insert and contains data, and set properties' values ")]
	public static void Should_CorrectlyInsertAndContainsSomeData_AndSetPropertiesValues()
	{
		var nullNode = new NullNode<object>();
		var tree = new BinarySearchTree<object>();
		tree.Insert(10);
		tree.Insert(5);
		tree.Insert(2);

		tree.Size.Should().Be(3);
		tree.Height.Should().Be(2);
		tree.RootBalanceFactor.Should().Be(2);
		tree.IsBalanced().Should().BeFalse();

		tree.Contains(10).Should().BeTrue();
		tree.Contains(18).Should().BeFalse();

		tree.Insert(18);
		tree.Size.Should().Be(4);
		tree.Height.Should().Be(2);
		tree.RootBalanceFactor.Should().Be(1);
		tree.IsBalanced().Should().BeTrue();

		tree.Contains(18).Should().BeTrue();
		tree.Root.Should().Be(10);

		tree.Contains(35).Should().BeFalse();
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