using BinarySearchTreeLibrary.Interfaces;
using BinarySearchTreeLibrary.Tests.AssertUtils;
using BinarySearchTreeLibrary.Tests.NodesCases;
using BinarySearchTreeLibrary.Tests.NodesCases.CaseGenerators;
using FluentAssertions;
using NUnit.Framework;
using Theory = Xunit.TheoryAttribute;

namespace BinarySearchTreeLibrary.Tests.NodeTests;

public class UpdatePropertiesAfterRemoveTests
{
	private static object[] _input = Array.Empty<object>();
	private static INode<object>? _root;

	[Theory(DisplayName =
		"Root`s properties should updates after remove its single child")]
	[MemberData(nameof(TwoNodesCase.GetTwoNodesCases),
		MemberType = typeof(TwoNodesCase))]
	public void Properties_RemoveRootsSingleChild_UpdateProperties(NodeCase testCase)
	{
		SetUp(testCase);
		_root?.Height.Should().Be(1);

		_root?.Remove(_input[1].GetHashCode());

		_root?.Height.Should().Be(0);
		_root?.BalanceFactor.Should().Be(0);
		_root?.IsBalanced.Should().BeTrue();
	}

	[Theory(DisplayName =
		"New root`s properties should updates after remove old root.")]
	[MemberData(nameof(TwoNodesCase.GetTwoNodesCases),
		MemberType = typeof(TwoNodesCase))]
	public void Properties_RemoveRootWithSingleChild_UpdateNewRootsProperties(NodeCase testCase)
	{
		SetUp(testCase);
		_root?.Remove(_input[0].GetHashCode());

		_root?.Height.Should().Be(0);
		_root?.BalanceFactor.Should().Be(0);
		_root?.IsBalanced.Should().BeTrue();
	}

	[Theory(DisplayName =
		"Properties of nodes in sub-tries should updates after remove root with both child nodes.")]
	[MemberData(nameof(MultiLevelTreeCase.GetTreeCases),
		MemberType = typeof(MultiLevelTreeCase))]
	public void Properties_RemoveRootWithBothChildNodes_UpdateNewRootProperties(NodeCase testCase)
	{
		SetUp(testCase);

		_root?.Remove(_input[0].GetHashCode());

		_root?.Height.Should().Be(3);
		_root?.IsBalanced.Should().BeTrue();

		ChildAsserts.AssertHeights(_root, 2, 2);
		ChildAsserts.AssertBalanceFactor(_root, 0, -1);
		ChildAsserts.AssertIsBalanced(_root, true, true);

		ChildAsserts.AssertHeights(_root?.Left, 1, 1);
		ChildAsserts.AssertBalanceFactor(_root?.Left, 0, 1);
		ChildAsserts.AssertIsBalanced(_root?.Left, true, true);

		ChildAsserts.AssertHeights(_root?.Right, 0, 1);
		ChildAsserts.AssertBalanceFactor(_root?.Right, 0, 0);
		ChildAsserts.AssertIsBalanced(_root?.Right, true, true);
	}

	[Theory(DisplayName =
		"Properties should correct update upwards after remove leaf node.")]
	[MemberData(nameof(MultiLevelTreeCase.GetTreeCases),
		MemberType = typeof(MultiLevelTreeCase))]
	public void Properties_RemoveLeaf_UpdatePropertiesInSubTree(NodeCase testCase)
	{
		SetUp(testCase);
		_root?.Remove(_input[9].GetHashCode());

		_root?.Height.Should().Be(3);
		_root?.IsBalanced.Should().BeTrue();
		_root?.BalanceFactor.Should().Be(0);

		ChildAsserts.AssertHeights(_root, 2, 2);
		ChildAsserts.AssertBalanceFactor(_root, 0, 0);
		ChildAsserts.AssertIsBalanced(_root, true, true);

		ChildAsserts.AssertHeights(_root?.Left, 1, 1);
		ChildAsserts.AssertBalanceFactor(_root?.Left, 1, 1);
		ChildAsserts.AssertIsBalanced(_root?.Left, true, true);
	}

	[Theory(DisplayName =
		"Properties should correct update upwards after remove node with one child inside deep tree.")]
	[MemberData(nameof(MultiLevelTreeCase.GetTreeCases),
		MemberType = typeof(MultiLevelTreeCase))]
	public void Properties_RemoveNodeWithOneChild_UpdatePropertiesInSubTree(NodeCase testCase)
	{
		SetUp(testCase);

		_root?.Remove(_input[2].GetHashCode());
		_root?.Remove(_input[5].GetHashCode());

		_root?.Height.Should().Be(3);
		_root?.IsBalanced.Should().BeTrue();
		_root?.BalanceFactor.Should().Be(0);

		ChildAsserts.AssertHeights(_root, 2, 2);
		ChildAsserts.AssertBalanceFactor(_root, 1, -1);
		ChildAsserts.AssertIsBalanced(_root, true, true);

		ChildAsserts.AssertHeights(_root?.Left, 1, 0);
		ChildAsserts.AssertBalanceFactor(_root?.Left, 0, 0);
		ChildAsserts.AssertIsBalanced(_root?.Left, true, true);

		ChildAsserts.AssertHeights(_root?.Right, 0, 1);
		ChildAsserts.AssertBalanceFactor(_root?.Right, 0, 0);
		ChildAsserts.AssertIsBalanced(_root?.Right, true, true);
	}

	[Theory(DisplayName =
		"Properties should correct update upwards and down after remove node with one sub-tree inside deep tree.")]
	[MemberData(nameof(MultiLevelTreeCase.GetTreeCases),
		MemberType = typeof(MultiLevelTreeCase))]
	public void Properties_RemoveNodeWithOneSubTree_UpdatePropertiesInSubTree(NodeCase testCase)
	{
		SetUp(testCase);

		var excessiveNodes = new[] {2, 10, 5, 12};

		var testTargetNodes = new[] {1, 3};

		if (_root is not null)
		{
			RemoveListNodes(_root, excessiveNodes);
			RemoveListNodes(_root, testTargetNodes);
		}

		_root?.Height.Should().Be(2);
		_root?.IsBalanced.Should().BeTrue();
		_root?.BalanceFactor.Should().Be(0);

		ChildAsserts.AssertHeights(_root, 1, 1);
		ChildAsserts.AssertBalanceFactor(_root, 0, 0);
		ChildAsserts.AssertIsBalanced(_root, true, true);
	}

	[Theory(DisplayName =
		"Properties should correct update upwards and down after remove node with both sub-tree inside deep tree.")]
	[MemberData(nameof(MultiLevelTreeCase.GetTreeCases),
		MemberType = typeof(MultiLevelTreeCase))]
	public void Properties_RemoveNodeWithBothSubTree_UpdatePropertiesInSubTree(NodeCase testCase)
	{
		SetUp(testCase);
		_root?.Remove(_input[3].GetHashCode());

		_root?.Height.Should().Be(3);
		_root?.IsBalanced.Should().BeTrue();
		_root?.BalanceFactor.Should().Be(0);

		ChildAsserts.AssertHeights(_root, 2, 2);
		ChildAsserts.AssertBalanceFactor(_root, 0, 0);
		ChildAsserts.AssertIsBalanced(_root, true, true);

		ChildAsserts.AssertHeights(_root?.Right, 1, 1);
		ChildAsserts.AssertBalanceFactor(_root?.Right, -1, -1);
		ChildAsserts.AssertIsBalanced(_root?.Right, true, true);
	}

	[Theory(DisplayName =
		"Update property Height after remove tests. Should correct update height after remove all levels of tree")]
	[MemberData(nameof(MultiLevelTreeCase.GetTreeCases),
		MemberType = typeof(MultiLevelTreeCase))]
	public void Properties_RemoveLowestLevelNodes_UpdatePropertiesInTree(NodeCase testCase)
	{
		SetUp(testCase);

		_root?.Height.Should().Be(3);

		var excessiveNodes = new[] {8, 9, 10, 12, 6, 11};

		if (_root is not null)
			_root = RemoveListNodes(_root, excessiveNodes); 

		_root?.Height.Should().Be(2);
		_root?.IsBalanced.Should().BeTrue();
		_root?.BalanceFactor.Should().Be(0);

		ChildAsserts.AssertHeights(_root, 1, 1);
		ChildAsserts.AssertBalanceFactor(_root, 0, 0);
		ChildAsserts.AssertIsBalanced(_root, true, true);
	}

	[SetUp]
	internal void SetUp(NodeCase testCase)
	{
		_input = testCase.InputData;
		_root = TestNodeFactory.CreateNode(_input, 0);
	}

	private static INode<T> RemoveListNodes<T>(INode<T> node, IEnumerable<int> keys)
	{
		return keys.Aggregate(node, (current, key) => current.Remove(_input[key].GetHashCode()));
	}
	/*    Visual representation of the test multi-level tree (INDEXES of the test-case's input array)
	*                        0
	* 		   		       /   \
	*                    /      \
	*  	     	       /         \
	* 				  1           3
	* 			   /   \        /   \
	* 			 7     2       5     4
	*		   /  \	  /        \    /  \
	*		  8	  9	 10	       12  6   11
	*/
}