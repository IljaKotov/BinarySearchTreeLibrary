using BinarySearchTreeLibrary.Interfaces;
using BinarySearchTreeLibrary.Models;
using BinarySearchTreeLibrary.Tests.NodesCases;
using BinarySearchTreeLibrary.Tests.NodesCases.CaseGenerators;
using FluentAssertions;
using NSubstitute;

namespace BinarySearchTreeLibrary.Tests.NodeTests;

public static class InsertTests
{
	private static object[] _input = Array.Empty<object>();
	private static readonly NullNode<object> _nullNode = new();
	
	[Fact(DisplayName = "Insert method should throw ArgumentNullException when inserting null data")]
	public static void Insert_NullData_ShouldThrowArgumentNullException()
	{
		var root = new Node<string?>("Some data");

		Assert.Throws<ArgumentNullException>(() => root.Insert(null));
	}

	[Theory(DisplayName = "Should correctly set properties' values for single node")]
	[MemberData(nameof(SingleNodeCaseGenerator.GetSingleNodeCases),
		MemberType = typeof(SingleNodeCaseGenerator))]
	public static void Should_CorrectlySetProperties_ForSingleNode(NodeCase testCase)
	{
		_input = testCase.InputData;
		var node = new Node<object>(_input[0]);

		node.Data.Should().Be(testCase.InputData[0]);
		node.Left.Should().BeEquivalentTo(_nullNode);
		node.Right.Should().BeEquivalentTo(_nullNode);
		node.Parent.Should().BeNull();
		node.Key.Should().Be(testCase.InputData[0].GetHashCode());
	}

	[Theory(DisplayName = "Should correctly set properties' values for Root and just one child-node")]
	[MemberData(nameof(TwoNodesCaseGenerator.GetTwoNodesCases),
		MemberType = typeof(TwoNodesCaseGenerator))]
	public static void Should_CorrectlySetProperties_ForRootAndJustOneChildNode(NodeCase testCase)
	{
		_input = testCase.InputData;
		var root = new Node<object>(_input[0]);
		root.Insert(_input[1]);

		root.Data.Should().Be(_input[0]);
		root.Parent.Should().BeNull();
		root.Key.Should().Be(_input[0].GetHashCode());

		INode<object>? child;

		if (_input[1].GetHashCode() > _input[0].GetHashCode())
		{
			child = root.Right;
			root.Left.Should().BeEquivalentTo(_nullNode);
		}
		else
		{
			child = root.Left;
			root.Right.Should().BeEquivalentTo(_nullNode);
		}

		child?.Data.Should().Be(_input[1]);
		child?.Parent.Should().Be(root);
		child?.Key.Should().Be(_input[1].GetHashCode());
		child?.Left.Should().BeEquivalentTo(_nullNode);
		child?.Right.Should().BeEquivalentTo(_nullNode);
	}

	[Theory(DisplayName = "Should correctly insert and set properties' values for four-level trees' nodes")]
	[MemberData(nameof(DeepBalancedTreeCaseGenerator.GetTreeCases),
		MemberType = typeof(DeepBalancedTreeCaseGenerator))]
	public static void Should_CorrectlyInsertAndSetProperties_FourLevelTreeNodes(NodeCase testCase)
	{
		//var stringHasher = Substitute.For<IStringHasher>();
		//stringHasher.GetHash(Arg.Any<string>()).Returns(callInfo => ((string)callInfo[0]).Length);
		
		_input = testCase.InputData;
		//var root = new Node<object>(_input[0], stringHasher);
		var root = new Node<object>(_input[0]);
		for (var i = 1; i < _input.Length; i++)
			root.Insert(_input[i]);

		root.Data.Should().Be(_input[0]);
		root.Parent.Should().BeNull();
		root.Key.Should().Be(_input[0].GetHashCode());

		Validate_CorrectlyInsert_LeftSubTree(root, _input);
		Validate_CorrectlyInsert_RightSubTree(root, _input);
	}

	private static void Validate_CorrectlyInsert_LeftSubTree(INode<object> root, IReadOnlyList<object> input)
	{
		root.Left?.Data.Should().Be(input[1]);
		root.Left?.Left?.Data.Should().Be(input[7]);
		root.Left?.Right?.Data.Should().Be(input[2]);
		root.Left?.Left?.Left?.Data.Should().Be(input[8]);
		root.Left?.Left?.Right?.Data.Should().Be(input[9]);
		root.Left?.Right?.Left?.Data.Should().Be(input[10]);
		root.Left?.Right?.Right?.Should().BeEquivalentTo(_nullNode);
	}

	private static void Validate_CorrectlyInsert_RightSubTree(INode<object> root, IReadOnlyList<object> input)
	{
		root.Right?.Data.Should().Be(input[3]);
		root.Right?.Left?.Data.Should().Be(input[5]);
		root.Right?.Right?.Data.Should().Be(input[4]);
		root.Right?.Left?.Left?.Should().BeEquivalentTo(_nullNode);
		root.Right?.Left?.Right?.Data.Should().Be(input[12]);
		root.Right?.Right?.Left?.Data.Should().Be(input[6]);
		root.Right?.Right?.Right?.Data.Should().Be(input[11]);
	}
	/*    Visual representation of the test four-level tree (INDEXES of the test-case's input array)
	*                     0
	*                   /   \
	*  		      	   /      \
	* 				 1          3
	* 			  /   \       /  \
	* 			7     2      5    4
	*		  / \	 /       \   / \
	*		8	9	10		12	6  11
	*/
}