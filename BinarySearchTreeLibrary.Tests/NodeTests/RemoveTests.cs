using BinarySearchTreeLibrary.Exceptions;
using BinarySearchTreeLibrary.Interfaces;
using BinarySearchTreeLibrary.Models;
using BinarySearchTreeLibrary.Tests.AssertUtils;
using BinarySearchTreeLibrary.Tests.NodesCases;
using BinarySearchTreeLibrary.Tests.NodesCases.CaseGenerators;
using FluentAssertions;
using NUnit.Framework;
using Assert = Xunit.Assert;
using Theory = Xunit.TheoryAttribute;

namespace BinarySearchTreeLibrary.Tests.NodeTests;

public class RemoveTests
{
	private static object[] _input = Array.Empty<object>();
	private static INode<object>? _root;

	[Theory(DisplayName = "Should throw NodeNotFoundException when removing non-existent node")]
	[MemberData(nameof(MultiLevelTreeCase.GetTreeCases),
		MemberType = typeof(MultiLevelTreeCase))]
	public void Remove_NonExistentNode_ShouldThrowNodeNotFoundException(NodeCase testCase)
	{
		SetUp(testCase);

		var twiceRemovedIndex = _input[1].GetHashCode();

		_root?.Remove(twiceRemovedIndex);

		Assert.Throws<NodeNotFoundException>(() => _root?.Remove(twiceRemovedIndex));
	}

	[Theory(DisplayName = "Should correct remove single node without children")]
	[MemberData(nameof(SingleNodeCase.GenerateCases),
		MemberType = typeof(SingleNodeCase))]
	public static void Remove_SingleNode_ShouldBeRemoved(NodeCase testCase)
	{
		_root = new Node<object>(testCase.InputData[0]);
		var isRootDeleted = false;

		_root.RootDeleted += () => isRootDeleted = true;

		_root.Remove(testCase.InputData[0].GetHashCode());

		isRootDeleted.Should().BeTrue();
	}

	[Theory(DisplayName = "Should correct remove root's single child node")]
	[MemberData(nameof(TwoNodesCase.GetTwoNodesCases),
		MemberType = typeof(TwoNodesCase))]
	public void Remove_RootsSingleChild_CorrectlyRemove(NodeCase testCase)
	{
		SetUp(testCase);

		_root?.Remove(_input[1].GetHashCode());
		_root?.GetNodeByKey(_input[1].GetHashCode()).Should().BeNull();

		NodeAsserts.AssertNode(_root, _input[0], null);
		ChildAsserts.AssertData<object?>(_root, null, null);
	}

	[Theory(DisplayName = "Should correct remove root and set new root.")]
	[MemberData(nameof(TwoNodesCase.GetTwoNodesCases),
		MemberType = typeof(TwoNodesCase))]
	public void Remove_RootWithSingleChild_CorrectlyRemove_(NodeCase testCase)
	{
		SetUp(testCase);

		_root?.Remove(_input[0].GetHashCode());
		_root?.GetNodeByKey(_input[0].GetHashCode()).Should().BeNull();

		NodeAsserts.AssertNode(_root, _input[1], null);
		ChildAsserts.AssertData<object?>(_root, null, null);
	}

	[Theory(DisplayName = "Should correct remove root and set new root.")]
	[MemberData(nameof(MultiLevelTreeCase.GetTreeCases),
		MemberType = typeof(MultiLevelTreeCase))]
	public void Remove_RootWithBothChild_CorrectRemove(NodeCase testCase)
	{
		SetUp(testCase);

		_root?.Remove(_input[0].GetHashCode());
		_root?.GetNodeByKey(_input[0].GetHashCode()).Should().BeNull();

		NodeAsserts.AssertNode(_root, _input[5], null);
		ChildAsserts.AssertData(_root, _input[1], _input[3]);
		ChildAsserts.AssertData(_root?.Left, _input[7], _input[2]);
		ChildAsserts.AssertData(_root?.Right, _input[12], _input[4]);
		ChildAsserts.AssertData<object?>(_root?.Right?.Left, null, null);
		ChildAsserts.AssertData(_root?.Right?.Right, _input[6], _input[11]);
	}

	[Theory(DisplayName = "Should correct remove leaf node")]
	[MemberData(nameof(MultiLevelTreeCase.GetTreeCases),
		MemberType = typeof(MultiLevelTreeCase))]
	public void Remove_Leaf_ShouldBeRemoved(NodeCase testCase)
	{
		SetUp(testCase);

		_root?.Remove(_input[9].GetHashCode());
		_root?.GetNodeByKey(_input[9].GetHashCode()).Should().BeNull();

		NodeAsserts.AssertNode(_root, _input[0], null);
		ChildAsserts.AssertData(_root, _input[1], _input[3]);
		ChildAsserts.AssertData(_root?.Left, _input[7], _input[2]);
		ChildAsserts.AssertData(_root?.Left?.Left, _input[8], null);
		ChildAsserts.AssertData(_root?.Left?.Right, _input[10], null);
	}

	[Theory(DisplayName = "Should correct remove (with replace) node with one left/right child ")]
	[MemberData(nameof(MultiLevelTreeCase.GetTreeCases),
		MemberType = typeof(MultiLevelTreeCase))]
	public void Remove_NodeWithOneChild_CorrectRemove(NodeCase testCase)
	{
		SetUp(testCase);

		_root?.Remove(_input[2].GetHashCode());
		_root?.Remove(_input[5].GetHashCode());

		_root?.GetNodeByKey(_input[2].GetHashCode()).Should().BeNull();
		_root?.GetNodeByKey(_input[5].GetHashCode()).Should().BeNull();

		NodeAsserts.AssertNode(_root, _input[0], null);
		ChildAsserts.AssertData(_root, _input[1], _input[3]);
		ChildAsserts.AssertData(_root?.Left, _input[7], _input[10]);
		ChildAsserts.AssertData(_root?.Left?.Left, _input[8], _input[9]);
		ChildAsserts.AssertData(_root?.Right, _input[12], _input[4]);
		ChildAsserts.AssertData<object?>(_root?.Right?.Left, null, null);
		ChildAsserts.AssertData<object?>(_root?.Left?.Right, null, null);
	}

	[Theory(DisplayName = "Should correct remove node with one deep left/right sub-tree")]
	[MemberData(nameof(MultiLevelTreeCase.GetTreeCases),
		MemberType = typeof(MultiLevelTreeCase))]
	public void Remove_NodeWithOneSubTree_ShouldCorrectRemove(NodeCase testCase)
	{
		SetUp(testCase);

		var excessiveNodes = new[] {2, 10, 5, 12};
		var testTargetNodes = new[] {1, 3};

		if (_root is not null)
		{
			_root = RemoveListNodes(_root, excessiveNodes);
			_root = RemoveListNodes(_root, testTargetNodes);
			ValidateRemovedNodes(_root, excessiveNodes);
			ValidateRemovedNodes(_root, testTargetNodes);
		}

		NodeAsserts.AssertNode(_root, _input[0], null);
		ChildAsserts.AssertData(_root, _input[7], _input[4]);
		ChildAsserts.AssertData(_root?.Left, _input[8], _input[9]);
		ChildAsserts.AssertData(_root?.Right, _input[6], _input[11]);
		ChildAsserts.AssertData<object?>(_root?.Right?.Right, null, null);
		ChildAsserts.AssertData<object?>(_root?.Left?.Right, null, null);
	}

	[Theory(DisplayName = "Should correct remove node with both sub-trees")]
	[MemberData(nameof(MultiLevelTreeCase.GetTreeCases),
		MemberType = typeof(MultiLevelTreeCase))]
	public void Remove_NodeWithBothSubTrees_ShouldCorrectRemove(NodeCase testCase)
	{
		SetUp(testCase);

		_root?.Remove(_input[3].GetHashCode());
		_root?.GetNodeByKey(_input[3].GetHashCode()).Should().BeNull();

		NodeAsserts.AssertNode(_root, _input[0], null);
		ChildAsserts.AssertData(_root, _input[1], _input[6]);
		ChildAsserts.AssertData(_root?.Left, _input[7], _input[2]);
		ChildAsserts.AssertData(_root?.Right, _input[5], _input[4]);
		ChildAsserts.AssertData(_root?.Right?.Left, null, _input[12]);
		ChildAsserts.AssertData(_root?.Right?.Right, null, _input[11]);
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

	private static void ValidateRemovedNodes<T>(INode<T> node, IEnumerable<int> keys)
	{
		foreach (var key in keys)
			node.GetNodeByKey(_input[key].GetHashCode()).Should().BeNull();
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