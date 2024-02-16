using BinarySearchTreeLibrary.Models;
using BinarySearchTreeLibrary.Tests.NodesCases;
using FluentAssertions;

namespace BinarySearchTreeLibrary.Tests.NodeTests;

public static class UpdateHeightAfterRemoveTests
{
	private static object[] _input = Array.Empty<object>();

	[Theory(DisplayName =
		"Update property Height after remove tests. Should correct update root's height after remove single child")]
	[MemberData(nameof(JustTwoNodesCaseGenerator.GetTwoNodesCases),
		MemberType = typeof(JustTwoNodesCaseGenerator))]
	public static void Should_CorrectlyUpdateHeight_RootsWithSingleChild(NodeCase testCase)
	{
		_input = testCase.InputData;
		var root = new Node<object>(_input[0]);

		root.Insert(_input[1]);
		root.Remove(_input[1].GetHashCode());

		root.Data.Should().Be(_input[0]);
		root.Height.Should().Be(0);
	}

	[Theory(DisplayName =
		"Update property Height after remove tests. Should correct update height after remove root with single child")]
	[MemberData(nameof(JustTwoNodesCaseGenerator.GetTwoNodesCases),
		MemberType = typeof(JustTwoNodesCaseGenerator))]
	public static void Should_CorrectlyUpdateHeight_RootWithSingleChild(NodeCase testCase)
	{
		_input = testCase.InputData;
		var root = new Node<object>(_input[0]);

		root.Insert(_input[1]);
		root.Remove(_input[0].GetHashCode());

		root.Data.Should().Be(_input[1]);
		root.Height.Should().Be(0);
	}

	[Theory(DisplayName =
		"Update property Height after remove tests. Should correct update height of sub-tree after remove root in deep tree.")]
	[MemberData(nameof(NodesFourLevelTreeCaseGenerator.GetNodesFourLevelTreeCases),
		MemberType = typeof(NodesFourLevelTreeCaseGenerator))]
	public static void Should_CorrectlyUpdateHeight_RootWithBothChildNodes(NodeCase testCase)
	{
		_input = testCase.InputData;
		var root = new Node<object>(_input[0]);

		for (var i = 1; i < _input.Length; i++)
			root.Insert(_input[i]);

		root.Remove(_input[0].GetHashCode());

		root.Data.Should().Be(_input[5]);
		root.Height.Should().Be(3);
		root.Left?.Height.Should().Be(2);
		root.Right?.Height.Should().Be(2);
		root.Right?.Left?.Height.Should().Be(0);
		root.Right?.Right?.Height.Should().Be(1);
		root.Right?.Right?.Left?.Height.Should().Be(0);
	}

	[Theory(DisplayName =
		"Update property Height after remove tests. Should correct update height of sub-tree after remove leaf in deep tree.")]
	[MemberData(nameof(NodesFourLevelTreeCaseGenerator.GetNodesFourLevelTreeCases),
		MemberType = typeof(NodesFourLevelTreeCaseGenerator))]
	public static void Should_CorrectlyUpdateHeight_Leaf(NodeCase testCase)
	{
		_input = testCase.InputData;
		var root = new Node<object>(_input[0]);

		for (var i = 1; i < _input.Length; i++)
			root.Insert(_input[i]);

		root.Remove(_input[9].GetHashCode());

		root.Data.Should().Be(_input[0]);
		root.Height.Should().Be(3);
		root.Left?.Height.Should().Be(2);
		root.Left?.Left?.Height.Should().Be(1);
		root.Left?.Right?.Height.Should().Be(1);
		root.Left?.Left?.Left?.Height.Should().Be(0);
		root.Right?.Height.Should().Be(2);
	}

	[Theory(DisplayName =
		"Update property Height after remove tests. Should correct update height of sub-tree after remove node with one child inside deep tree.")]
	[MemberData(nameof(NodesFourLevelTreeCaseGenerator.GetNodesFourLevelTreeCases),
		MemberType = typeof(NodesFourLevelTreeCaseGenerator))]
	public static void Should_CorrectlyUpdateHeight_NodeWithOneChild(NodeCase testCase)
	{
		_input = testCase.InputData;
		var root = new Node<object>(_input[0]);

		for (var i = 1; i < _input.Length; i++)
			root.Insert(_input[i]);

		root.Remove(_input[2].GetHashCode());
		root.Remove(_input[5].GetHashCode());

		root.Data.Should().Be(_input[0]);
		root.Height.Should().Be(3);
		root.Left?.Height.Should().Be(2);
		root.Left?.Right?.Height.Should().Be(0);
		root.Left?.Left?.Height.Should().Be(1);
		root.Left?.Left?.Left?.Height.Should().Be(0);
		root.Right?.Height.Should().Be(2);
		root.Right?.Left?.Height.Should().Be(0);
		root.Right?.Right?.Height.Should().Be(1);
		root.Right?.Right?.Left?.Height.Should().Be(0);
	}

	[Theory(DisplayName =
		"Update property Height after remove tests. Should correct update height of sub-tree after remove node with one sub-tree inside deep tree.")]
	[MemberData(nameof(NodesFourLevelTreeCaseGenerator.GetNodesFourLevelTreeCases),
		MemberType = typeof(NodesFourLevelTreeCaseGenerator))]
	public static void Should_CorrectlyUpdateHeight_NodeWithOneSubTree(NodeCase testCase)
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
		root.Height.Should().Be(2);
		root.Left?.Height.Should().Be(1);
		root.Left?.Left?.Height.Should().Be(0);
		root.Right?.Height.Should().Be(1);
		root.Right?.Right?.Height.Should().Be(0);
	}

	[Theory(DisplayName =
		"Update property Height after remove tests. Should correct update height of sub-tree after remove node with both sub-tree inside deep tree.")]
	[MemberData(nameof(NodesFourLevelTreeCaseGenerator.GetNodesFourLevelTreeCases),
		MemberType = typeof(NodesFourLevelTreeCaseGenerator))]
	public static void Should_CorrectlyUpdateHeight_NodeWithBothSubTrees(NodeCase testCase)
	{
		_input = testCase.InputData;
		var root = new Node<object>(_input[0]);

		for (var i = 1; i < _input.Length; i++)
			root.Insert(_input[i]);

		root.Remove(_input[3].GetHashCode());

		root.Data.Should().Be(_input[0]);
		root.Height.Should().Be(3);
		root.Left?.Height.Should().Be(2);
		root.Right?.Height.Should().Be(2);
		root.Right?.Left?.Height.Should().Be(1);
		root.Right?.Right?.Height.Should().Be(1);
		root.Right?.Right?.Right?.Height.Should().Be(0);
		root.Right?.Left?.Right?.Height.Should().Be(0);
	}
	
	[Theory(DisplayName = "Update property Height after remove tests. Should correct update height after remove all levels of tree")]
	[MemberData(nameof(NodesFourLevelTreeCaseGenerator.GetNodesFourLevelTreeCases),
		MemberType = typeof(NodesFourLevelTreeCaseGenerator))]
	
	public static void Should_CorrectlyUpdateHeight_AfterRemoveAllLevelsOfTree(NodeCase testCase)
	{
		
		_input = testCase.InputData;
		var root = new Node<object>(_input[0]);
		
		for (var i = 1; i < _input.Length; i++)
			root.Insert(_input[i]);

		root.Height.Should().Be(3);
		
		root.Remove(_input[8].GetHashCode());
		root.Remove(_input[9].GetHashCode());
		root.Remove(_input[10].GetHashCode());
		root.Remove(_input[12].GetHashCode());
		root.Remove(_input[6].GetHashCode());
		root.Remove(_input[11].GetHashCode());
		
		root.Height.Should().Be(2);
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