using BinarySearchTreeLibrary.Interfaces;
using BinarySearchTreeLibrary.Models;
using BinarySearchTreeLibrary.Tests.NodesCases;
using BinarySearchTreeLibrary.Tests.NodesCases.CaseGenerators;
using FluentAssertions;
using NUnit.Framework;
using Asserts = BinarySearchTreeLibrary.Tests.NodeTests.ChildAsserts;

namespace BinarySearchTreeLibrary.Tests.NodeTests;

public class HeightTests
{
	private INode<object>? _testRoot;

	[Xunit.Theory(DisplayName = "Should return height zero for single node")]
	[MemberData(nameof(SingleNodeCase.GenerateCases),
		MemberType = typeof(SingleNodeCase))]
	public static void SingleNode_HeightZero(NodeCase testCase)
	{
		var testNode = TestDataFactory.CreateNode(testCase.InputData[0]);

		testNode.Height.Should().Be(0);
		ChildAsserts.AssertHeights(testNode, -1, -1);
	}

	[Xunit.Theory(DisplayName = "Should return height one for root and zero for child-node")]
	[MemberData(nameof(TwoNodesCase.GetTwoNodesCases),
		MemberType = typeof(TwoNodesCase))]
	public void RootAndChildNode_HeightOneAndZero(NodeCase testCase)
	{
		SetUp(testCase);

		var leftHeight = _testRoot?.Left is not NullNode<object> ? 0 : -1;
		var rightHeight = leftHeight == 0 ? -1 : 0;

		_testRoot?.Height.Should().Be(1);
		ChildAsserts.AssertHeights(_testRoot, leftHeight, rightHeight);
	}

	[Xunit.Theory(DisplayName = "Should return correct heights for all nodes")]
	[MemberData(nameof(MultiLevelTreeCase.GetTreeCases),
		MemberType = typeof(MultiLevelTreeCase))]
	public void FourLevelTree_CorrectHeights(NodeCase testCase)
	{
		SetUp(testCase);

		Asserts.AssertHeights(_testRoot, 3, 2, 2);
		Asserts.AssertHeights(_testRoot?.Left, 1, 1);
		Asserts.AssertHeights(_testRoot?.Right, 1, 1);
		Asserts.AssertHeights(_testRoot?.Left?.Left, 0, 0);
		Asserts.AssertHeights(_testRoot?.Left?.Right, 0, -1);
		Asserts.AssertHeights(_testRoot?.Right?.Left, -1, 0);
		Asserts.AssertHeights(_testRoot?.Right?.Right, 0, 0);
	}

	[SetUp]
	internal void SetUp(NodeCase testCase)
	{
		var input = testCase.InputData;
		_testRoot = TestDataFactory.CreateNode(input, 0);
	}
	/*    Visual representation of the test four-level tree (INDEXES of the test-case's input array)
	*                        0
	* 				       /   \
	*                     /     \
	*  		   	        /         \
	* 				  1           3
	* 			   /   \        /    \
	* 			 7     2       5      4
	*		   /  \	  /        \     /  \
	*		  8	  9	 10	       12	6   11
	*/
}