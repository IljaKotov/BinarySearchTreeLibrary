using BinarySearchTreeLibrary.Models;
using BinarySearchTreeLibrary.Tests.NodesCases;
using BinarySearchTreeLibrary.Tests.NodesCases.CaseGenerators;
using FluentAssertions;

namespace BinarySearchTreeLibrary.Tests.NodeTests;

public static class HeightTests
{
	[Theory(DisplayName = "Should return height zero for single node")]
	[MemberData(nameof(SingleNodeCaseGenerator.GenerateCases),
		MemberType = typeof(SingleNodeCaseGenerator))]
	public static void CreateSingleNode_ExpectHeightZero(NodeCase testCase)
	{
		var node = new Node<object>(testCase.InputData[0]);
		node.Height.Should().Be(0);
	}

	[Theory(DisplayName = "Should return height one for root and zero for child-node")]
	[MemberData(nameof(TwoNodesCaseGenerator.GetTwoNodesCases),
		MemberType = typeof(TwoNodesCaseGenerator))]
	public static void CreateRootAndOneChildNode_ExpectHeightOneAndZero(NodeCase testCase)
	{
		var root = new Node<object>(testCase.InputData[0]);
		root.Insert(testCase.InputData[1]);

		root.Height.Should().Be(1);

		if (root.Left is not NullNode<object>)
		{
			root.Left.Height.Should().Be(0);
		}
		else
		{
			root.Right?.Height.Should().Be(0);
		}
	}

	[Theory(DisplayName = "Should return correct height for four-level tree nodes")]
	[MemberData(nameof(DeepBalancedTreeCaseGenerator.GetTreeCases),
		MemberType = typeof(DeepBalancedTreeCaseGenerator))]
	public static void CreateFourLevelTree_ExpectCorrectHeights(NodeCase testCase)
	{
		var root = new Node<object>(testCase.InputData[0]);

		for (var i = 1; i < testCase.InputData.Length; i++)
			root.Insert(testCase.InputData[i]);

		root.Height.Should().Be(3);
		root.Left?.Height.Should().Be(2);
		root.Right?.Height.Should().Be(2);
		root.Left?.Left?.Height.Should().Be(1);
		root.Left?.Right?.Height.Should().Be(1);
		root.Right?.Left?.Height.Should().Be(1);
		root.Right?.Right?.Height.Should().Be(1);
		root.Left?.Left?.Left?.Height.Should().Be(0);
		root.Left?.Left?.Right?.Height.Should().Be(0);
		root.Left?.Right?.Left?.Height.Should().Be(0);
	}
	/*    Visual representation of the test four-level tree (INDEXES of the test-case's input array)
	*                        0
	*                     /   \
	*  		   	        /      \
	* 				 1          3
	* 			  /   \       /  \
	* 			7     2      5    4
	*		  / \	 /       \   / \
	*		8	9	10		12	6  11
	*/
}