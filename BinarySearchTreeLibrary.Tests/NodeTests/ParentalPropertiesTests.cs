﻿using BinarySearchTreeLibrary.Models;
using BinarySearchTreeLibrary.Tests.NodesCases;
using BinarySearchTreeLibrary.Tests.NodesCases.CaseGenerators;
using FluentAssertions;

namespace BinarySearchTreeLibrary.Tests.NodeTests;

public static class ParentalPropertiesTests
{
	private static object[] _input = Array.Empty<object>();

	[Theory(DisplayName = "Should correctly set parental properties inside binary tree")]
	[MemberData(nameof(MultiLevelTreeCase.GetTreeCases),
		MemberType = typeof(MultiLevelTreeCase))]
	public static void Should_CorrectlySetParentalProperties_ForFourLevelTreeNodes(NodeCase testCase)
	{
		_input = testCase.InputData;
		var root = new Node<object>(_input[0]);

		for (var i = 1; i < _input.Length; i++)
			root.Insert(_input[i]);

		root.Parent.Should().BeNull();
		root.HasBothChildren.Should().BeTrue();
		root.IsLeaf.Should().BeFalse();

		var leaf = root.FindByKey(_input[12].GetHashCode());
		leaf?.Parent.Should().NotBeNull();
		leaf?.Parent?.Data.Should().Be(_input[5]);
		leaf?.HasBothChildren.Should().BeFalse();
		leaf?.IsLeaf.Should().BeTrue();
		leaf?.Parent?.HasBothChildren.Should().BeFalse();
		leaf?.Parent?.IsLeaf.Should().BeFalse();
	}
	/*    Visual representation of the test four-level tree (INDEXES of the test-case's input array)
	*                     0
	*                   /   \
	*  		   	   /      \
	* 				 1          3
	* 			  /   \       /  \
	* 			7     2      5    4
	*		  / \	 /       \   / \
	*		8	9	10		12	6  11
	*/
}