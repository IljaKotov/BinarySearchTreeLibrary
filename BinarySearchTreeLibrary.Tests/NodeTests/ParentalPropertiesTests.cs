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
	public static void Insert_MultiLevelTree_SetCorrectParentalProperties(NodeCase testCase)
	{
		_input = testCase.InputData;
		var root = TestNodeFactory.CreateNode(_input, 0);

		root.Parent.Should().BeNull();
		root.HasBothChildren.Should().BeTrue();
		root.IsLeaf.Should().BeFalse();

		var leaf = root.GetNodeByKey(_input[12].GetHashCode());

		leaf?.Parent.Should().NotBeNull();
		leaf?.Parent?.Data.Should().Be(_input[5]);
		leaf?.HasBothChildren.Should().BeFalse();
		leaf?.IsLeaf.Should().BeTrue();
		leaf?.Parent?.HasBothChildren.Should().BeFalse();
		leaf?.Parent?.IsLeaf.Should().BeFalse();
	}
	/*    Visual representation of the test multi-level tree (INDEXES of the test-case's input array)
	*                        0
	* 		   		       /   \
	*                    /      \
	*  	     	       /         \
	* 				  1           3
	* 			   /   \        /   \
	* 			 7     2       5     4
	*		   /  \	  /        \    /  \
	*		  8	  9	 10	       12  6   11
	*/
}