using BinarySearchTreeLibrary.Models;
using BinarySearchTreeLibrary.Tests.AssertUtils;
using FluentAssertions;

namespace BinarySearchTreeLibrary.Tests.NodeTests;

public static class BalanceTests
{
	[Theory(DisplayName = "Should balance the degenerate tree")]
	[InlineData(new[] {0, 1, 2, 3, 4, 5, 6}, 0)]
	[InlineData(new[] {0, 1, 2, 3, 4, 5, 6}, 6)]
	public static void Balance_DegenerateTree_BalancingTree(int[] inputs, int startNode)
	{
		var root = TestNodeFactory.CreateNode(inputs, startNode);

		var result = root.Balance();

		result.Data.Should().Be(inputs[3]);
		result.Parent.Should().BeNull();
		ChildAsserts.AssertData(result, inputs[1], inputs[5]);
		ChildAsserts.AssertData(result.Left, 0, 2);
		ChildAsserts.AssertData(result.Right, 4, 6);
		ChildAsserts.AssertData(result.Left?.Right, null, null);
		ChildAsserts.AssertData(result.Right?.Left, null, null);

		result.Height.Should().Be(2);
		ChildAsserts.AssertHeights(result, 1, 1);
		ChildAsserts.AssertHeights(result.Left, 0, 0);
		ChildAsserts.AssertHeights(result.Right, 0, 0);

		result.IsBalanced.Should().BeTrue();
		ChildAsserts.AssertIsBalanced(result, true, true);
		ChildAsserts.AssertIsBalanced(result.Left, true, true);
		ChildAsserts.AssertIsBalanced(result.Right, true, true);
	}

	[Fact(DisplayName = "Should balance the  tree")]
	public static void Balance_UnbalanceTree_BalancingTree()
	{
		var root = new Node<int>(100);

		var inputs = new[] {50, 150, 80, 70, 180, 190, 160};

		foreach (var data in inputs)
			root.Insert(data);

		root.IsBalanced.Should().BeFalse();
		root.Height.Should().Be(3);

		var result = root.Balance();

		result.Data.Should().Be(100);
		ChildAsserts.AssertData(result, 70, 180);
		ChildAsserts.AssertData(result.Left, 50, 80);
		ChildAsserts.AssertData(result.Right, 150, 190);
		ChildAsserts.AssertData(result.Right?.Left, null, 160);

		result.Height.Should().Be(3);
		ChildAsserts.AssertHeights(result, 1, 2);
		ChildAsserts.AssertIsBalanced(result, true, true);
	}
	/* Visual representation of the tree before and after balancing
	*    UNBALANCED TREE --> BALANCED TREE
	*       100                  100
	*      /   \               /     \
	*     50   150           70     180
	*       \    \          /  \    /  \
	*       80   180       50  80  150 190
	*      /    /   \                \
	*     70  160  190 	    	    160
	*
	*   DEGENERATE-LEFT TREE --> BALANCED TREE  <--- DEGENERATE-RIGHT TREE
	*           0                     3                      6
	* 		      \                  /   \                   /
	* 			   1                1     5                 5
	* 			    \              / \   / \               /
	* 			     2           0    2 4   6             4
	* 			      \                                  /
	* 			       3                                3
	*                   \							   /
	* 		    		 4                            2
	*                     \ 				    	 /
	* 		       		   5                        1
	* 				     	\                      /
	* 			        	 6                    0
	*/
}