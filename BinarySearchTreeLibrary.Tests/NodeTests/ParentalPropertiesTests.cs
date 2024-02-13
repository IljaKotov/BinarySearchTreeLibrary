using BinarySearchTreeLibrary.Models;
using BinarySearchTreeLibrary.Tests.NodesCases;
using FluentAssertions;

namespace BinarySearchTreeLibrary.Tests.NodeTests;

public static class ParentalPropertiesTests
{
	private static object[] _input = Array.Empty<object>();

	[Theory(DisplayName = "Should correctly set parental properties inside binary tree")]
	[MemberData(nameof(NodesFourLevelTreeCaseGenerator.GetNodesFourLevelTreeCases),
		MemberType = typeof(NodesFourLevelTreeCaseGenerator))]
	public static void Should_CorrectlySetParentalProperties_ForFourLevelTreeNodes(NodeCase testCase)
	{
		_input = testCase.InputData;
		var root = new Node<object>(_input[0]);

		for (var i = 1; i < _input.Length; i++)
			root.Insert(_input[i]);

		root.Parent.Should().BeNull();
		root.HasBothChildren.Should().BeTrue();
		root.IsLeaf.Should().BeFalse();

		var leaf = root.FindChild(_input[12].GetHashCode());
		leaf?.Parent.Should().NotBeNull();
		leaf?.Parent?.Data.Should().Be(_input[5]);
		leaf?.HasBothChildren.Should().BeFalse();
		leaf?.IsLeaf.Should().BeTrue();
		leaf?.Parent?.HasBothChildren.Should().BeFalse();
		leaf?.Parent?.IsLeaf.Should().BeFalse();
	}
}