﻿using BinarySearchTreeLibrary.Interfaces;
using BinarySearchTreeLibrary.Models;
using BinarySearchTreeLibrary.Tests.NodesCases;
using BinarySearchTreeLibrary.Tests.NodesCases.CaseGenerators;
using Bogus;
using FluentAssertions;
using NUnit.Framework;

namespace BinarySearchTreeLibrary.Tests.NodeTests;

public class FindByKeyTests
{
	private static object[] _input = Array.Empty<object>();
	private static readonly NullNode<object> _nullNode = new();
	private INode<object>? _testRoot;
	private int _mistakeKey;

	[Xunit.Theory(DisplayName = "Should find correct node in single node tree, " +
		"and return NullNode for non-existent key")]
	[MemberData(nameof(SingleNodeCase.GenerateCases),
		MemberType = typeof(SingleNodeCase))]
	public void SingleNode_FindCorrectNode(NodeCase testCase)
	{
		SetUp(testCase);

		if (_testRoot is not null)
			AssertFoundNode(_input[0].GetHashCode(), _testRoot);

		AssertFoundNode(_mistakeKey, _nullNode);
	}

	[Xunit.Theory(DisplayName = "Should correctly find child in two node tree, " +
		"and return NullNode for non-existent key")]
	[MemberData(nameof(TwoNodesCase.GetTwoNodesCases),
		MemberType = typeof(TwoNodesCase))]
	public void TwoNodeTree_FindCorrectNode(NodeCase testCase)
	{
		SetUp(testCase);

		var expectedChild = 
			_testRoot?.Right is not NullNode<object> ?
				_testRoot?.Right :
				_testRoot.Left;

		if (_testRoot is not null && expectedChild is not null)
		{
			AssertFoundNode(_input[0].GetHashCode(), _testRoot);
			AssertFoundNode(_input[1].GetHashCode(), expectedChild);
		}

		AssertFoundNode(_mistakeKey, _nullNode);
	}

	[Xunit.Theory(DisplayName = "Should correctly find 13 nodes in multi-level tree, " +
		"and return NullNode for non-existent key")]
	[MemberData(nameof(MultiLevelTreeCase.GetTreeCases),
		MemberType = typeof(MultiLevelTreeCase))]
	public void ForFourLevelTree_FindCorrectChild(NodeCase testCase)
	{
		SetUp(testCase);

		foreach (var data in testCase.InputData)
		{
			var child = _testRoot?.FindByKey(data.GetHashCode());
			child.Should().NotBeEquivalentTo(_nullNode);
			child?.Data.Should().Be(data);
		}

		AssertFoundNode(_mistakeKey, _nullNode);
	}

	[SetUp]
	internal void SetUp(NodeCase testCase)
	{
		_input = testCase.InputData;
		_testRoot = TestDataFactory.CreateNode(_input, 0);
		_mistakeKey = CreateMistakeKey();
	}

	private static int CreateMistakeKey()
	{
		HashSet<int> existingHashes = new();
		Faker faker = new();

		return faker.Random.Unique(() => faker.Random.Int(), x => x, existingHashes);
	}

	private void AssertFoundNode<T>(int key, INode<T> expectedNode)
	{
		var foundNode = _testRoot?.FindByKey(key);

		if (expectedNode is NullNode<T>)
			foundNode.Should().BeEquivalentTo(_nullNode);
		else
			foundNode?.Should().Be(expectedNode);
	}
}