using BinarySearchTreeLibrary.Interfaces;
using BinarySearchTreeLibrary.Models;
using BinarySearchTreeLibrary.Tests.NodesCases;
using FluentAssertions;

namespace BinarySearchTreeLibrary.Tests.NodeTests;

public static class RemoveChildTests
{
	private static object[] _input = Array.Empty<object>();
	private static readonly NullNode<object> _nullNode = new();

	[Theory(DisplayName = "RemoveChild method tests. Should remove correct single node")]
	[MemberData(nameof(SingleNodeCaseGenerator.GetSingleNodeCases),
		MemberType = typeof(SingleNodeCaseGenerator))]
	public static void Should_CorrectlyRemove_SingleNode(NodeCase testCase)
	{
		var node = new Node<object>(testCase.InputData[0]);
		node.Remove(testCase.InputData[0].GetHashCode());

		node.Data.Should().BeNull();
		node.Left.Should().BeEquivalentTo(_nullNode);
		node.Right.Should().BeEquivalentTo(_nullNode);
		node.Parent.Should().BeNull();
		node.Key.Should().Be(0);
	}

	[Theory(DisplayName = "RemoveChild method tests. Should correct remove root's single child node")]
	[MemberData(nameof(JustTwoNodesCaseGenerator.GetTwoNodesCases),
		MemberType = typeof(JustTwoNodesCaseGenerator))]
	public static void Should_CorrectlyRemove_RootsSingleChild(NodeCase testCase)
	{
		_input = testCase.InputData;
		var root = new Node<object>(_input[0]);
		
		root.Insert(_input[1]);
		root.Remove(_input[1].GetHashCode());

		root.Data.Should().Be(_input[0]);
		root.Parent.Should().BeNull();
		root.Key.Should().Be(_input[0].GetHashCode());
		root.Right.Should().BeEquivalentTo(_nullNode);
		root.Left.Should().BeEquivalentTo(_nullNode);
		root.Left?.Left.Should().BeNull();
	}

	[Theory(DisplayName = "RemoveChild method tests. Should correct remove root with single child node")]
	[MemberData(nameof(JustTwoNodesCaseGenerator.GetTwoNodesCases),
		MemberType = typeof(JustTwoNodesCaseGenerator))]
	public static void Should_CorrectlyRemove_RootWithSingleChild(NodeCase testCase)
	{
		_input = testCase.InputData;
		var root = new Node<object>(_input[0]);
		
		root.Insert(_input[1]);
		root.Remove(_input[0].GetHashCode());

		root.Data.Should().Be(_input[1]);
		root.Parent.Should().BeNull();
		root.Key.Should().Be(_input[1].GetHashCode());
		root.Right.Should().BeEquivalentTo(_nullNode);
		root.Left.Should().BeEquivalentTo(_nullNode);
	}

	[Theory(DisplayName = "RemoveChild method tests. Should correct remove root with both child nodes")]
	[MemberData(nameof(NodesFourLevelTreeCaseGenerator.GetNodesFourLevelTreeCases),
		MemberType = typeof(NodesFourLevelTreeCaseGenerator))]
	public static void Should_CorrectlyRemove_RootWithBothChildNodes(NodeCase testCase)
	{
		_input = testCase.InputData;
		var root = new Node<object>(_input[0]);

		for (var i = 1; i < _input.Length; i++)
			root.Insert(_input[i]);

		root.Remove(_input[0].GetHashCode());

		root.Data.Should().Be(_input[5]);
		root.Parent.Should().BeNull();
		root.Left?.Data.Should().Be(_input[1]);
		root.Right?.Data.Should().Be(_input[3]);
		root.Right?.Left?.Data.Should().Be(_input[12]);
		root.Right?.Right?.Data.Should().Be(_input[4]);
		root.Right?.Right?.Right?.Data.Should().Be(_input[11]);
		root.Right?.Right?.Left?.Data.Should().Be(_input[6]);
		root.Right?.Left?.Right.Should().BeEquivalentTo(_nullNode);
		root.Right?.Left?.Left.Should().BeEquivalentTo(_nullNode);

		root.FindChild(_input[0].GetHashCode()).Should().BeEquivalentTo(_nullNode);
	}

	[Theory(DisplayName = "RemoveChild method tests. Should correct remove leaf node")]
	[MemberData(nameof(NodesFourLevelTreeCaseGenerator.GetNodesFourLevelTreeCases),
		MemberType = typeof(NodesFourLevelTreeCaseGenerator))]
	public static void Should_CorrectlyRemove_Leaf(NodeCase testCase)
	{
		_input = testCase.InputData;
		var root = new Node<object>(_input[0]);

		for (var i = 1; i < _input.Length; i++)
			root.Insert(_input[i]);

		root.Remove(_input[9].GetHashCode());

		root.Data.Should().Be(_input[0]);
		root.Parent.Should().BeNull();
		root.Right?.Data.Should().Be(_input[3]);
		root.Right?.Left?.Data.Should().Be(_input[5]);
		root.Right?.Right?.Data.Should().Be(_input[4]);
		root.Left?.Data.Should().Be(_input[1]);
		root.Left?.Left?.Data.Should().Be(_input[7]);
		root.Left?.Right?.Data.Should().Be(_input[2]);
		root.Left?.Left?.Left?.Data.Should().Be(_input[8]);
		root.Left?.Left?.Right.Should().BeEquivalentTo(_nullNode);
		root.Left?.Right?.Left?.Data.Should().Be(_input[10]);
		root.Left?.Right?.Right.Should().BeEquivalentTo(_nullNode);

		root.FindChild(_input[9].GetHashCode()).Should().BeEquivalentTo(_nullNode);
	}

	[Theory(DisplayName = "RemoveChild method tests. Should correct remove node with one left/right child")]
	[MemberData(nameof(NodesFourLevelTreeCaseGenerator.GetNodesFourLevelTreeCases),
		MemberType = typeof(NodesFourLevelTreeCaseGenerator))]
	public static void Should_CorrectlyRemove_NodeWithOneChild(NodeCase testCase)
	{
		_input = testCase.InputData;
		var root = new Node<object>(_input[0]);

		for (var i = 1; i < _input.Length; i++)
			root.Insert(_input[i]);

		root.Remove(_input[2].GetHashCode());
		root.Remove(_input[5].GetHashCode());

		root.Data.Should().Be(_input[0]);
		root.Parent.Should().BeNull();
		root.Right?.Data.Should().Be(_input[3]);
		root.Right?.Left?.Data.Should().Be(_input[12]);
		root.Right?.Right?.Data.Should().Be(_input[4]);
		root.Right?.Left?.Right?.Should().BeEquivalentTo(_nullNode);
		root.Right?.Left?.Left.Should().BeEquivalentTo(_nullNode);
		root.Left?.Data.Should().Be(_input[1]);
		root.Left?.Left?.Data.Should().Be(_input[7]);
		root.Left?.Right?.Data.Should().Be(_input[10]);
		root.Left?.Right?.Right.Should().BeEquivalentTo(_nullNode);

		root.FindChild(_input[2].GetHashCode()).Should().BeEquivalentTo(_nullNode);
		root.FindChild(_input[5].GetHashCode()).Should().BeEquivalentTo(_nullNode);
	}

	[Theory(DisplayName = "RemoveChild method tests. Should correct remove node with one left/right sub-tree")]
	[MemberData(nameof(NodesFourLevelTreeCaseGenerator.GetNodesFourLevelTreeCases),
		MemberType = typeof(NodesFourLevelTreeCaseGenerator))]
	public static void Should_CorrectlyRemove_NodeWithOneSubTree(NodeCase testCase)
	{
		_input = testCase.InputData;
		var root = new Node<object>(_input[0]);

		for (var i = 1; i < _input.Length; i++)
			root.Insert(_input[i]);

		root.Remove(_input[2].GetHashCode()); //removed 4 nodes for creating 2 node with single depth sub-tree (left and right)
		root.Remove(_input[10].GetHashCode());
		root.Remove(_input[5].GetHashCode());
		root.Remove(_input[12].GetHashCode());

		root.Remove(_input[1].GetHashCode());
		root.Remove(_input[3].GetHashCode());

		root.Data.Should().Be(_input[0]);
		root.Parent.Should().BeNull();
		root.Right?.Data.Should().Be(_input[4]);
		root.Right?.Right?.Data.Should().Be(_input[11]);
		root.Right?.Right?.Right?.Should().BeEquivalentTo(_nullNode);
		root.Right?.Left?.Data.Should().Be(_input[6]);
		root.Left?.Data.Should().Be(_input[7]);
		root.Left?.Left?.Data.Should().Be(_input[8]);
		root.Left?.Right?.Data.Should().Be(_input[9]);
		root.Left?.Left?.Left.Should().BeEquivalentTo(_nullNode);

		root.FindChild(_input[2].GetHashCode()).Should().BeEquivalentTo(_nullNode);
		root.FindChild(_input[10].GetHashCode()).Should().BeEquivalentTo(_nullNode);
		root.FindChild(_input[5].GetHashCode()).Should().BeEquivalentTo(_nullNode);
		root.FindChild(_input[12].GetHashCode()).Should().BeEquivalentTo(_nullNode);
		root.FindChild(_input[1].GetHashCode()).Should().BeEquivalentTo(_nullNode);
		root.FindChild(_input[3].GetHashCode()).Should().BeEquivalentTo(_nullNode);
	}

	[Theory(DisplayName = "RemoveChild method tests. Should correct remove node with both sub-trees")]
	[MemberData(nameof(NodesFourLevelTreeCaseGenerator.GetNodesFourLevelTreeCases),
		MemberType = typeof(NodesFourLevelTreeCaseGenerator))]
	public static void Should_CorrectlyRemove_NodeWithBothSubTrees(NodeCase testCase)
	{
		_input = testCase.InputData;
		var root = new Node<object>(_input[0]);

		for (var i = 1; i < _input.Length; i++)
			root.Insert(_input[i]);

		root.Remove(_input[3].GetHashCode());

		root.Data.Should().Be(_input[0]);
		root.Parent.Should().BeNull();
		root.Right?.Data.Should().Be(_input[6]);
		root.Right?.Right?.Data.Should().Be(_input[4]);
		root.Right?.Right?.Right?.Data.Should().Be(_input[11]);
		root.Right?.Right?.Left.Should().BeEquivalentTo(_nullNode);
		root.Right?.Left?.Data.Should().Be(_input[5]);
		root.Right?.Left?.Right?.Data.Should().Be(_input[12]);
		root.Left?.Data.Should().Be(_input[1]);

		root.FindChild(_input[3].GetHashCode()).Should().BeEquivalentTo(_nullNode);
	}
	/*    Visual representation of the test four-level tree (INDEXES of the test-case's input array)
	 *                     0
	 *                   /   \
	 *  		   	   /      \
	 * 				 1          3
	 * 			  /   \       /  \
	 * 			7     2      5    4
	 *		  / \	 /       \   / \
	 *		8	9	10		12	6  11
	 */
}