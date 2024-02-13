using BinarySearchTreeLibrary.Interfaces;
using BinarySearchTreeLibrary.Models;
using BinarySearchTreeLibrary.Tests.NodesCases;
using FluentAssertions;

namespace BinarySearchTreeLibrary.Tests.NodeTests;

public static class InsertTests
{
	private static object[] _input = Array.Empty<object>();
	
	[Fact(DisplayName = "Insert method should throw ArgumentNullException when inserting null data")]
	public static void Insert_NullData_ShouldThrowArgumentNullException()
	{
		var root = new Node<string?>("Some data");

		Assert.Throws<ArgumentNullException>(() => root.Insert(null));
	}

	[Theory(DisplayName = "Should correctly set properties' values for single node")]
	[MemberData(nameof(SingleNodeCaseGenerator.GetSingleNodeCases),
		MemberType = typeof(SingleNodeCaseGenerator))]
	public static void Should_CorrectlySetProperties_ForSingleNode(NodeCase testCase)
	{
		_input = testCase.InputData;
		var node = new Node<object>(_input[0]);

		node.Data.Should().Be(testCase.InputData[0]);
		node.Left.Should().BeNull();
		node.Right.Should().BeNull();
		node.Parent.Should().BeNull();
		node.Key.Should().Be(testCase.InputData[0].GetHashCode());
	}

	[Theory(DisplayName = "Should correctly set properties' values for Root and just one child-node")]
	[MemberData(nameof(JustTwoNodesCaseGenerator.GetTwoNodesCases),
		MemberType = typeof(JustTwoNodesCaseGenerator))]
	public static void Should_CorrectlySetProperties_ForRootAndJustOneChildNode(NodeCase testCase)
	{
		_input = testCase.InputData;
		var root = new Node<object>(_input[0]);
		root.Insert(_input[1]);

		root.Data.Should().Be(_input[0]);
		root.Parent.Should().BeNull();
		root.Key.Should().Be(_input[0].GetHashCode());

		INode<object>? child;

		if (_input[1].GetHashCode() > _input[0].GetHashCode())
		{
			child = root.Right;
			root.Left.Should().BeNull();
		}
		else
		{
			child = root.Left;
			root.Right.Should().BeNull();
		}

		child?.Data.Should().Be(_input[1]);
		child?.Parent.Should().Be(root);
		child?.Key.Should().Be(_input[1].GetHashCode());
		child?.Left.Should().BeNull();
		child?.Right.Should().BeNull();
	}

	[Theory(DisplayName = "Should correctly insert and set properties' values for four-level trees' nodes")]
	[MemberData(nameof(NodesFourLevelTreeCaseGenerator.GetNodesFourLevelTreeCases),
		MemberType = typeof(NodesFourLevelTreeCaseGenerator))]
	public static void Should_CorrectlyInsertAndSetProperties_FourLevelTreeNodes(NodeCase testCase)
	{
		_input = testCase.InputData;
		var root = new Node<object>(_input[0]);

		for (var i = 1; i < _input.Length; i++)
			root.Insert(_input[i]);

		root.Data.Should().Be(_input[0]);
		root.Parent.Should().BeNull();
		root.Key.Should().Be(_input[0].GetHashCode());

		Validate_CorrectlyInsert_LeftSubTree(root, _input);
		Validate_CorrectlyInsert_RightSubTree(root, _input);
	}

	private static void Validate_CorrectlyInsert_LeftSubTree(INode<object> root, IReadOnlyList<object> input)
	{
		root.Left?.Data.Should().Be(input[1]);
		root.Left?.Left?.Data.Should().Be(input[7]);
		root.Left?.Right?.Data.Should().Be(input[2]);
		root.Left?.Left?.Left?.Data.Should().Be(input[8]);
		root.Left?.Left?.Right?.Data.Should().Be(input[9]);
		root.Left?.Right?.Left?.Data.Should().Be(input[10]);
		root.Left?.Right?.Right?.Data.Should().BeNull();
	}

	private static void Validate_CorrectlyInsert_RightSubTree(INode<object> root, IReadOnlyList<object> input)
	{
		root.Right?.Data.Should().Be(input[3]);
		root.Right?.Left?.Data.Should().Be(input[5]);
		root.Right?.Right?.Data.Should().Be(input[4]);
		root.Right?.Left?.Left?.Data.Should().BeNull();
		root.Right?.Left?.Right?.Data.Should().Be(input[12]);
		root.Right?.Right?.Left?.Data.Should().Be(input[6]);
		root.Right?.Right?.Right?.Data.Should().Be(input[11]);
	}
}