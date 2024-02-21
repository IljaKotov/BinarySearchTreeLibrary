using BinarySearchTreeLibrary.Exceptions;
using BinarySearchTreeLibrary.Interfaces;
using BinarySearchTreeLibrary.Models;
using BinarySearchTreeLibrary.Tests.NodesCases;
using BinarySearchTreeLibrary.Tests.NodesCases.CaseGenerators;
using NUnit.Framework;
using Assert = Xunit.Assert;
using Asserts = BinarySearchTreeLibrary.Tests.NodeTests.ChildAsserts;

namespace BinarySearchTreeLibrary.Tests.NodeTests;

public class InsertTests
{
	private static object[] _input = Array.Empty<object>();
	private static readonly NullNode<object> _nullNode = new();
	private static INode<object>? _testRoot;

	[Fact(DisplayName = "Insert method should throw ArgumentNullException when inserting null data")]
	public void Insert_NullData_ShouldThrowArgumentNullException()
	{
		var testNode = new Node<string?>("Some data");

		Assert.Throws<ArgumentNullException>(() => testNode.Insert(null));
	}

	[Xunit.Theory(DisplayName = "Insert method should throw DuplicateKeyException when inserting duplicate key")]
	[MemberData(nameof(SingleNodeCase.GenerateCases),
		MemberType = typeof(SingleNodeCase))]
	public void Insert_DuplicateKey_ShouldThrowDuplicateKeyException(NodeCase testCase)
	{
		SetUp(testCase);
		
		if(_testRoot is not null)
			Assert.Throws<DuplicateKeyException>(() => _testRoot.Insert(_input[0]));
	}

	[Xunit.Theory(DisplayName = "Should correctly set properties' values for Root and just one child-node")]
	[MemberData(nameof(TwoNodesCase.GetTwoNodesCases),
		MemberType = typeof(TwoNodesCase))]
	public void Insert_TwoNodes_ShouldSetCorrectProperties(NodeCase testCase)
	{
		SetUp(testCase);

		NodeAsserts.AssertNode(_testRoot, _input[0], null);

		var child = 
			_input[1].GetHashCode() > _input[0].GetHashCode() ? 
				_testRoot?.Right : _testRoot?.Left;

		var secondChild = 
			child == _testRoot?.Left ? 
				_testRoot?.Right : _testRoot?.Left;

		NodeAsserts.AssertNode(child, _input[1]);
		NodeAsserts.AssertNode(secondChild, _nullNode);
		NodeAsserts.AssertNode(child?.Left, _nullNode);
		NodeAsserts.AssertNode(child?.Right, _nullNode);
	}

	[Xunit.Theory(DisplayName = "Should correctly insert and set properties' values for four-level trees' nodes")]
	[MemberData(nameof(MultiLevelTreeCase.GetTreeCases),
		MemberType = typeof(MultiLevelTreeCase))]
	public static void Should_CorrectlyInsertAndSetProperties_FourLevelTreeNodes(NodeCase testCase)
	{
		SetUp(testCase);

		NodeAsserts.AssertNode(_testRoot, _input[0], null);

		Asserts.AssertData(_testRoot, _input[1], _input[3]);
		Asserts.AssertData(_testRoot?.Left, _input[7], _input[2]);
		Asserts.AssertData(_testRoot?.Right, _input[5], _input[4]);
		Asserts.AssertData(_testRoot?.Left?.Left, _input[8], _input[9]);
		Asserts.AssertData(_testRoot?.Left?.Right, _input[10], _nullNode);
		Asserts.AssertData(_testRoot?.Right?.Left, _nullNode, _input[12]);
		Asserts.AssertData(_testRoot?.Right?.Right, _input[6], _input[11]);
	}

	[SetUp]
	internal static void SetUp(NodeCase testCase)
	{
		_input = testCase.InputData;
		_testRoot = TestDataFactory.CreateNode(_input, 0);
	}
	/*    Visual representation of the test four-level tree (INDEXES of the test-case's input array)
	*                        0
	* 				       /   \
	*                     /     \
	*  		   	        /         \
	* 				  1           3
	* 			   /   \        /    \
	* 			 7     2       5      4
	*		   /  \	  /        \     /  \
	*		  8	  9	 10	       12	6   11
	*/
}