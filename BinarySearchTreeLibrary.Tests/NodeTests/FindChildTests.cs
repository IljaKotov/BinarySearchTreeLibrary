using BinarySearchTreeLibrary.Interfaces;
using BinarySearchTreeLibrary.Models;
using BinarySearchTreeLibrary.Tests.NodesCases;
using BinarySearchTreeLibrary.Tests.NodesCases.CaseGenerators;
using Bogus;
using FluentAssertions;

namespace BinarySearchTreeLibrary.Tests.NodeTests;

public static class FindChildTests
{
	private static object[] _input = Array.Empty<object>();
	private static readonly NullNode<object> _nullNode = new();

	[Theory(DisplayName = "Should correctly find child for single node")]
	[MemberData(nameof(SingleNodeCaseGenerator.GenerateCases), MemberType = typeof(SingleNodeCaseGenerator))]
	public static void Should_CorrectlyFindChild_ForSingleNode(NodeCase testCase)
	{
		_input = testCase.InputData;
		var node = new Node<object>(_input[0]);

		var faker = new Faker();
		var mistakeKey = faker.Random.Int(int.MinValue, _input[0].GetHashCode() - 1);

		var child = node.FindChild(_input[0].GetHashCode());
		var mistakeChild = node.FindChild(mistakeKey);

		child.Should().Be(node);
		mistakeChild.Should().BeEquivalentTo(_nullNode);
	}

	[Theory(DisplayName = "Should correctly find child for Root and just one child-node")]
	[MemberData(nameof(TwoNodesCaseGenerator.GetTwoNodesCases), MemberType = typeof(TwoNodesCaseGenerator))]
	public static void Should_CorrectlyFindChild_ForRootAndJustOneChildNode(NodeCase testCase)
	{
		_input = testCase.InputData;
		var root = new Node<object>(_input[0]);
		root.Insert(_input[1]);

		var rootSearching = root.FindChild(_input[0].GetHashCode());
		var childSearching = root.FindChild(_input[1].GetHashCode());

		rootSearching?.Data.Should().Be(_input[0]);
		childSearching?.Data.Should().Be(_input[1]);
	}

	[Theory(DisplayName = "Should correctly find child for four-level tree nodes")]
	[MemberData(nameof(DeepBalancedTreeCaseGenerator.GetTreeCases),
		MemberType = typeof(DeepBalancedTreeCaseGenerator))]
	public static void Should_CorrectlyFindChild_ForFourLevelTreeNodes(NodeCase testCase)
	{
		var root = new Node<object>(testCase.InputData[0]);

		for (var i = 1; i < testCase.InputData.Length; i++)
			root.Insert(testCase.InputData[i]);

		foreach (var data in testCase.InputData)
		{
			var child = root.FindChild(data.GetHashCode());
			child.Should().NotBeEquivalentTo(_nullNode);
			child?.Data.Should().Be(data);
		}

		FindNonExistentChild_ExpectedReturnNull(testCase, root);
	}

	private static void FindNonExistentChild_ExpectedReturnNull(NodeCase testCase, INode<object> root)
	{
		var faker = new Faker();
		var nonExistentKey = faker.Random.Int();

		while (testCase.InputData.Contains(nonExistentKey.GetHashCode()))
			nonExistentKey = faker.Random.Int();

		var nonExistentChild = root.FindChild(nonExistentKey);
		nonExistentChild.Should().BeEquivalentTo(_nullNode);
	}
}