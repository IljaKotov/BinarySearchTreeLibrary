using BinarySearchTreeLibrary.Interfaces;
using BinarySearchTreeLibrary.Tests.NodesCases;
using BinarySearchTreeLibrary.Tests.NodesCases.CaseGenerators;
using Bogus;
using FluentAssertions;
using NUnit.Framework;
using Theory = Xunit.TheoryAttribute;

namespace BinarySearchTreeLibrary.Tests.NodeTests;

public class GetNodeByKeyTests
{
	private static object[] _input = Array.Empty<object>();
	private INode<object>? _testRoot;
	private int _mistakeKey;

	[Theory(DisplayName = "Correct work GetNodeByKey in single node tree")]
	[MemberData(nameof(SingleNodeCase.GenerateCases),
		MemberType = typeof(SingleNodeCase))]
	public void GetNodeByKey_SingleNode_ReturnCorrectResult(NodeCase testCase)
	{
		SetUp(testCase);

		if (_testRoot is not null)
		{
			AssertFoundNode(_input[0].GetHashCode(), _testRoot);
		}

		AssertFoundNode<object>(_mistakeKey, null);
	}

	[Theory(DisplayName = "Correct work GetNodeByKey in two node tree")]
	[MemberData(nameof(TwoNodesCase.GetTwoNodesCases),
		MemberType = typeof(TwoNodesCase))]
	public void GetNodeByKey_TwoNodeTree_ReturnCorrectResult(NodeCase testCase)
	{
		SetUp(testCase);

		var expectedChild =
			_testRoot?.Right ?? _testRoot?.Left;

		if (_testRoot is not null && expectedChild is not null)
		{
			AssertFoundNode(_input[0].GetHashCode(), _testRoot);
			AssertFoundNode(_input[1].GetHashCode(), expectedChild);
		}

		AssertFoundNode<object>(_mistakeKey, null);
	}

	[Theory(DisplayName = "Correct work GetNodeByKey in multi-level tree")]
	[MemberData(nameof(MultiLevelTreeCase.GetTreeCases),
		MemberType = typeof(MultiLevelTreeCase))]
	public void GetNodeByKey_ForFourLevelTree_ReturnCorrectResult(NodeCase testCase)
	{
		SetUp(testCase);

		foreach (var data in testCase.InputData)
		{
			var child = _testRoot?.GetNodeByKey(data.GetHashCode());
			child.Should().NotBeNull();
			child?.Data.Should().Be(data);
		}

		AssertFoundNode<object>(_mistakeKey, null);
	}

	[SetUp]
	internal void SetUp(NodeCase testCase)
	{
		_input = testCase.InputData;
		_testRoot = TestNodeFactory.CreateNode(_input, 0);
		_mistakeKey = CreateMistakeKey();
	}

	private static int CreateMistakeKey()
	{
		HashSet<int> existingHashes = new();
		Faker faker = new();

		return faker.Random.Unique(() => faker.Random.Int(), x => x, existingHashes);
	}

	private void AssertFoundNode<T>(int key, INode<T>? expectedResult)
	{
		var foundNode = _testRoot?.GetNodeByKey(key);

		foundNode?.Should().Be(expectedResult);
	}
}