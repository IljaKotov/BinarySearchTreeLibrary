using BinarySearchTreeLibrary.Interfaces;
using BinarySearchTreeLibrary.Tests.AssertUtils;
using BinarySearchTreeLibrary.Tests.NodesCases;
using BinarySearchTreeLibrary.Tests.NodesCases.CaseGenerators;
using FluentAssertions;
using NUnit.Framework;
using Theory = Xunit.TheoryAttribute;

namespace BinarySearchTreeLibrary.Tests.NodeTests;

public class HeightTests
{
	private INode<object>? _testRoot;

	[Theory(DisplayName = "Height is zero for some leaf and -1 for its children")]
	[MemberData(nameof(SingleNodeCase.GenerateCases),
		MemberType = typeof(SingleNodeCase))]
	public static void Height_SingleNode_Zero(NodeCase testCase)
	{
		var testNode = TestNodeFactory.CreateNode(testCase.InputData[0]);

		testNode.Height.Should().Be(0);
		ChildAsserts.AssertHeights(testNode, -1, -1);
	}

	[Theory(DisplayName = "Height is 0 for leaf, 1 for its parent and -1 for its child")]
	[MemberData(nameof(TwoNodesCase.GetTwoNodesCases),
		MemberType = typeof(TwoNodesCase))]
	public void Height_TwoNode_CorrectHeights(NodeCase testCase)
	{
		SetUp(testCase);

		var leftHeight = _testRoot?.Left is not null ? 0 : -1;
		var rightHeight = leftHeight == 0 ? -1 : 0;

		_testRoot?.Height.Should().Be(1);
		ChildAsserts.AssertHeights(_testRoot, leftHeight, rightHeight);
	}

	[Theory(DisplayName = "Should return correct heights for all nodes")]
	[MemberData(nameof(MultiLevelTreeCase.GetTreeCases),
		MemberType = typeof(MultiLevelTreeCase))]
	public void Height_MultiLevelTree_CorrectHeights(NodeCase testCase)
	{
		SetUp(testCase);

		ChildAsserts.AssertHeights(_testRoot, 3, 2, 2);
		ChildAsserts.AssertHeights(_testRoot?.Left, 1, 1);
		ChildAsserts.AssertHeights(_testRoot?.Right, 1, 1);
		ChildAsserts.AssertHeights(_testRoot?.Left?.Left, 0, 0);
		ChildAsserts.AssertHeights(_testRoot?.Left?.Right, 0, -1);
		ChildAsserts.AssertHeights(_testRoot?.Right?.Left, -1, 0);
		ChildAsserts.AssertHeights(_testRoot?.Right?.Right, 0, 0);
	}

	[SetUp]
	internal void SetUp(NodeCase testCase)
	{
		var input = testCase.InputData;
		_testRoot = TestNodeFactory.CreateNode(input, 0);
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