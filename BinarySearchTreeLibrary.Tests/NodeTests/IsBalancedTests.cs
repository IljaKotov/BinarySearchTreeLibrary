using BinarySearchTreeLibrary.Interfaces;
using BinarySearchTreeLibrary.Models;
using BinarySearchTreeLibrary.Tests.NodesCases;
using BinarySearchTreeLibrary.Tests.NodesCases.CaseGenerators;
using FluentAssertions;
using NUnit.Framework;
using Asserts = BinarySearchTreeLibrary.Tests.AssertUtils.ChildAsserts;

namespace BinarySearchTreeLibrary.Tests.NodeTests;

public class IsBalancedTests
{
	private static object[] _input = Array.Empty<object>();
	private static INode<object> _testRoot = new NullNode<object>();

	[Xunit.Theory(DisplayName = "IsBalanced property tests. Should return true for single node")]
	[MemberData(nameof(SingleNodeCase.GenerateCases),
		MemberType = typeof(SingleNodeCase))]
	public static void IsBalanced_SingleNode_True(NodeCase testCase)
	{
		var node = TestNodeFactory.CreateNode(testCase.InputData[0]);

		node.IsBalanced.Should().BeTrue();
	}

	[Xunit.Theory(DisplayName = "IsBalanced should return false for triple degenerate tree. " +
		"Should return true after remove some node")]
	[InlineData(new[] {1, 2, 3})]
	[InlineData(new[] {1, 3, 2})]
	[InlineData(new[] {3, 2, 1})]
	[InlineData(new[] {3, 1, 2})]
	public static void IsBalanced_ShouldCorrectlyUpdate(int[] input)
	{
		var root = TestNodeFactory.CreateNode(input, 0);

		root.IsBalanced.Should().BeFalse();
		Asserts.AssertIsBalanced(root, true, true);

		root.Remove(input[2]);
		root.IsBalanced.Should().BeTrue();
		Asserts.AssertIsBalanced(root, true, true);
	}

	[Xunit.Theory(DisplayName = "IsBalanced expected return true result for all nodes in balanced tree")]
	[MemberData(nameof(MultiLevelTreeCase.GetTreeCases),
		MemberType = typeof(MultiLevelTreeCase))]
	public void IsBalanced_MultiLevelTree_SetCorrectProperty(NodeCase testCase)
	{
		SetUp(testCase);

		_testRoot.IsBalanced.Should().BeTrue();
		Asserts.AssertIsBalanced(_testRoot, true, true);
		Asserts.AssertIsBalanced(_testRoot.Left, true, true);
		Asserts.AssertIsBalanced(_testRoot.Right, true, true);
	}

	[Xunit.Theory(DisplayName = "IsBalanced expected return correct result after remove and insert nodes")]
	[MemberData(nameof(MultiLevelTreeCase.GetTreeCases),
		MemberType = typeof(MultiLevelTreeCase))]
	public void IsBalanced_RemoveAndInsertNodes_CorrectUpdateProperties(NodeCase testCase)
	{
		SetUp(testCase);

		_testRoot.IsBalanced.Should().BeTrue();

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
		_testRoot.Remove(_input[2].GetHashCode());

		_testRoot.IsBalanced.Should().BeTrue();
		Asserts.AssertIsBalanced(_testRoot, true, true);
	}

	private static void CheckBalanceAfterRemovingSubTree()
	{
		_testRoot.Remove(_input[10].GetHashCode());

		_testRoot.IsBalanced.Should().BeFalse();
		Asserts.AssertIsBalanced(_testRoot, false, true);
		Asserts.AssertIsBalanced(_testRoot.Left, true, true);
	}

	private static void CheckBalanceAfterInsertingIntoRemovableSubTree()
	{
		_testRoot.Insert(_input[2]);
		_testRoot.IsBalanced.Should().BeTrue();
		Asserts.AssertIsBalanced(_testRoot, true, true);
	}
}