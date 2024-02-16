using BinarySearchTreeLibrary.Models;
using BinarySearchTreeLibrary.Tests.NodesCases;
using Bogus;
using FluentAssertions;

namespace BinarySearchTreeLibrary.Tests.NodeTests;

public static class IsBalancedTests
{
	private static object[] _input = Array.Empty<object>();
	private static readonly NullNode<object> _nullNode = new();
	
	[Theory(DisplayName = "IsBalanced property tests. Should return true for single node")]
	[MemberData(nameof(SingleNodeCaseGenerator.GetSingleNodeCases),
		MemberType = typeof(SingleNodeCaseGenerator))]
	
	public static void Should_ReturnTrue_SingleNode(NodeCase testCase)
	{
		var node = new Node<object>(testCase.InputData[0]);
		node.IsBalanced.Should().BeTrue();
	}
	
	[Fact(DisplayName = "IsBalanced property tests. Should return true for root and one child node. Should return false after insert grand child and again true after remove some node")]
	
	public static void Should_ReturnTrue_RootAndOneChildNode()
	{
		var faker = new Faker();
		var root= new Node<int>(faker.Random.Int(int.MinValue, int.MaxValue-5000));
		root.Insert(faker.Random.Int(root.Data+1, int.MaxValue-3000));
		root.IsBalanced.Should().BeTrue();
		
		root.Insert(faker.Random.Int(root.Right!.Data+1, int.MaxValue));
		root.IsBalanced.Should().BeFalse();
		
		root.Remove(root.Right!.Data);
		root.IsBalanced.Should().BeTrue();
	}
	
	[Theory(DisplayName = "IsBalanced property tests. Expected return true result for all nodes")]
	[MemberData(nameof(NodesFourLevelTreeCaseGenerator.GetNodesFourLevelTreeCases),
		MemberType = typeof(NodesFourLevelTreeCaseGenerator))]
	public static void Should_CorrectlyUpdateBalanceFactor_DuringInsertNodes(NodeCase testCase)
	{
		_input = testCase.InputData;
		var root = new Node<object>(_input[0]);

		for (var i = 1; i < _input.Length; i++)
			root.Insert(_input[i]);

		root.IsBalanced.Should().BeTrue();
		root.Right?.IsBalanced.Should().BeTrue();
		root.Left?.IsBalanced.Should().BeTrue();
		root.Right?.Right?.IsBalanced.Should().BeTrue();
		root.Right?.Left?.IsBalanced.Should().BeTrue();
		root.Left?.Right?.IsBalanced.Should().BeTrue();
		root.Left?.Left?.IsBalanced.Should().BeTrue();
	}
	
	[Theory(DisplayName = "IsBalanced property tests. Expected return correct result after remove nodes")]
	[MemberData(nameof(NodesFourLevelTreeCaseGenerator.GetNodesFourLevelTreeCases),
		MemberType = typeof(NodesFourLevelTreeCaseGenerator))]
	public static void Should_CorrectlyUpdateBalanceFactor_DuringRemoveNodes(NodeCase testCase)
	{
		_input = testCase.InputData;
		var root = new Node<object>(_input[0]);

		for (var i = 1; i < _input.Length; i++)
			root.Insert(_input[i]);
		
		root.Remove(_input[2].GetHashCode());
		
		root.IsBalanced.Should().BeTrue();
		root.Left?.IsBalanced.Should().BeTrue();
		
		root.Remove(_input[10].GetHashCode());
		
		root.IsBalanced.Should().BeFalse();
		root.Right?.IsBalanced.Should().BeTrue();
		root.Left?.IsBalanced.Should().BeFalse();
		root.Left?.Left?.IsBalanced.Should().BeTrue();
		
		root.Insert(_input[2]);
		
		root.IsBalanced.Should().BeTrue();
	}
	
}