using BinarySearchTreeLibrary.Interfaces;
using BinarySearchTreeLibrary.Tests.AssertUtils;
using BinarySearchTreeLibrary.Tests.NodesCases;
using BinarySearchTreeLibrary.Tests.NodesCases.CaseGenerators;
using FluentAssertions;
using NUnit.Framework;
using Theory = Xunit.TheoryAttribute;

namespace BinarySearchTreeLibrary.Tests.NodeTests;

public class IsBalancedTests
{
	private static object[] _input = Array.Empty<object>();
	private static INode<object>? _testRoot;

	[Theory(DisplayName = "IsBalanced should be true for single node")]
	[MemberData(nameof(SingleNodeCase.GenerateCases),
		MemberType = typeof(SingleNodeCase))]
	public static void IsBalanced_SingleNode_True(NodeCase testCase)
	{
		var node = TestNodeFactory.CreateNode(testCase.InputData[0]);

		node.IsBalanced.Should().BeTrue();
	}

	[Theory(DisplayName =
		"IsBalanced should be false for degenerate tree and should be true after remove some node")]
	[InlineData(new[] {1, 2, 3})]
	[InlineData(new[] {1, 3, 2})]
	[InlineData(new[] {3, 2, 1})]
	[InlineData(new[] {3, 1, 2})]
	public static void IsBalanced_RemoveNode_CorrectUpdate(int[] input)
	{
		var root = TestNodeFactory.CreateNode(input, 0);

		root.IsBalanced.Should().BeFalse();
		ChildAsserts.AssertIsBalanced(root, true, true);

		root.Remove(input[2]);
		root.IsBalanced.Should().BeTrue();
		ChildAsserts.AssertIsBalanced(root, true, true);
	}

	[Theory(DisplayName = "IsBalanced should be true for all nodes in balanced tree")]
	[MemberData(nameof(MultiLevelTreeCase.GetTreeCases),
		MemberType = typeof(MultiLevelTreeCase))]
	public void IsBalanced_MultiLevelTree_CorrectSetProperties(NodeCase testCase)
	{
		SetUp(testCase);

		_testRoot?.IsBalanced.Should().BeTrue();
		ChildAsserts.AssertIsBalanced(_testRoot, true, true);
		ChildAsserts.AssertIsBalanced(_testRoot?.Left, true, true);
		ChildAsserts.AssertIsBalanced(_testRoot?.Right, true, true);
	}

	[Theory(DisplayName = "IsBalanced should correct update after remove and insert nodes")]
	[MemberData(nameof(MultiLevelTreeCase.GetTreeCases),
		MemberType = typeof(MultiLevelTreeCase))]
	public void IsBalanced_RemoveAndInsertNodes_CorrectUpdateProperties(NodeCase testCase)
	{
		SetUp(testCase);

		_testRoot?.IsBalanced.Should().BeTrue();

		CheckBalanceAfterRemovingSingleNode();
		CheckBalanceAfterRemovingSubTree();
		CheckBalanceAfterInsertingIntoRemovableSubTree();
	}

	[SetUp]
	internal void SetUp(NodeCase testCase)
	{
		_input = testCase.InputData;
		_testRoot = TestNodeFactory.CreateNode(_input, 0);
	}

	private static void CheckBalanceAfterRemovingSingleNode()
	{
		_testRoot?.Remove(_input[2].GetHashCode());

		_testRoot?.IsBalanced.Should().BeTrue();
		ChildAsserts.AssertIsBalanced(_testRoot, true, true);
	}

	private static void CheckBalanceAfterRemovingSubTree()
	{
		_testRoot?.Remove(_input[10].GetHashCode());

		_testRoot?.IsBalanced.Should().BeFalse();
		ChildAsserts.AssertIsBalanced(_testRoot, false, true);
		ChildAsserts.AssertIsBalanced(_testRoot?.Left, true, true);
	}

	private static void CheckBalanceAfterInsertingIntoRemovableSubTree()
	{
		_testRoot?.Insert(_input[2]);
		_testRoot?.IsBalanced.Should().BeTrue();
		ChildAsserts.AssertIsBalanced(_testRoot, true, true);
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