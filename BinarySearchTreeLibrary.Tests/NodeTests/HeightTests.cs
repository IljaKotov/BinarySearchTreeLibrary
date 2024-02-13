using BinarySearchTreeLibrary.Models;
using BinarySearchTreeLibrary.Tests.NodesCases;
using FluentAssertions;

namespace BinarySearchTreeLibrary.Tests.NodeTests;

public static class HeightTests
{
	[Theory(DisplayName = "Should return height zero for single node")]
	[MemberData(nameof(SingleNodeCaseGenerator.GetSingleNodeCases),
		MemberType = typeof(SingleNodeCaseGenerator))]
	public static void CreateSingleNode_ExpectHeightZero(NodeCase testCase)
	{
		var node = new Node<object>(testCase.InputData[0]);
		node.Height.Should().Be(0);
	}

	[Theory(DisplayName = "Should return height one for root and zero for child-node")]
	[MemberData(nameof(JustTwoNodesCaseGenerator.GetTwoNodesCases),
		MemberType = typeof(JustTwoNodesCaseGenerator))]
	public static void CreateRootAndOneChildNode_ExpectHeightOneAndZero(NodeCase testCase)
	{
		var root = new Node<object>(testCase.InputData[0]);
		root.Insert(testCase.InputData[1]);

		root.Height.Should().Be(1);

		if (root.Left is not null)
			root.Left.Height.Should().Be(0);
		else
			root.Right?.Height.Should().Be(0);
	}

	[Theory(DisplayName = "Should return correct height for four-level tree nodes")]
	[MemberData(nameof(NodesFourLevelTreeCaseGenerator.GetNodesFourLevelTreeCases),
		MemberType = typeof(NodesFourLevelTreeCaseGenerator))]
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
}