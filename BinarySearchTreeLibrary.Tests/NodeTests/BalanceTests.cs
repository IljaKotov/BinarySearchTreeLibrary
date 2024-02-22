using BinarySearchTreeLibrary.Interfaces;
using BinarySearchTreeLibrary.Models;
using FluentAssertions;

namespace BinarySearchTreeLibrary.Tests.NodeTests;

public static class BalanceTests
{
	[Theory(DisplayName = "Should balance the degenerate tree")]
	[InlineData(new[]
	{
		0, 1, 2, 3, 4, 5, 6
	}, 0)]
	[InlineData(new[]
	{
		0, 1, 2, 3, 4, 5, 6
	}, 6)]
	public static void Should_Balance_TheDegenerateTree1(int[] inputs, int startNode)
	{
		var root = new Node<int>(startNode);

		if (startNode == 0)
		{
			for (var i = 1; i < inputs.Length; i++)
				root.Insert(inputs[i]);
		}
		else
		{
			for (var i = inputs.Length - 2; i >= 0; i--)
				root.Insert(inputs[i]);
		}

		var result = root.Balance();

		result.Data.Should().Be(inputs[3]);
		result.Left?.Data.Should().Be(inputs[1]);
		result.Right?.Data.Should().Be(inputs[5]);
		result.Left?.Right?.Data.Should().Be(inputs[2]);
		result.Left?.Left?.Data.Should().Be(inputs[0]);
		result.Right?.Right?.Data.Should().Be(inputs[6]);
		result.Right?.Left?.Data.Should().Be(inputs[4]);
		result.Right?.Right?.Right.Should().BeNull();
		result.Right?.Right?.Left.Should().BeNull();
		result.Right?.Left?.Right.Should().BeNull();
		result.Right?.Left?.Left.Should().BeNull();
		result.Parent.Should().BeNull();

		result.Height.Should().Be(2);
		result.Right?.Height.Should().Be(1);
		result.Left?.Height.Should().Be(1);
		result.Left?.Left?.Height.Should().Be(0);
		result.Left?.Right?.Height.Should().Be(0);
		result.Right?.Left?.Height.Should().Be(0);
		result.Right?.Right?.Height.Should().Be(0);

		result.IsBalanced.Should().BeTrue();
		result.Right?.IsBalanced.Should().BeTrue();
		result.Left?.IsBalanced.Should().BeTrue();
		result.Left?.Left?.IsBalanced.Should().BeTrue();
		result.Left?.Right?.IsBalanced.Should().BeTrue();
		result.Right?.Left?.IsBalanced.Should().BeTrue();
		result.Right?.Right?.IsBalanced.Should().BeTrue();
	}

	[Fact(DisplayName = "Should balance the  tree")]
	public static void Should_Balance_TheTree()
	{
		var root = new Node<int>(100);

		var inputs = new[]
		{
			50, 150, 80, 70, 180, 190, 160
		};

		foreach (var data in inputs)
			root.Insert(data);

		root.IsBalanced.Should().BeFalse();
		root.Height.Should().Be(3);

		var result = root.Balance();

		result.Data.Should().Be(100);
		result.Left?.Data.Should().Be(70);
		result.Right?.Data.Should().Be(180);
		result.Left?.Left?.Data.Should().Be(50);
		result.Left?.Right?.Data.Should().Be(80);
		result.Right?.Left?.Data.Should().Be(150);
		result.Right?.Right?.Data.Should().Be(190);
		result.Right?.Left?.Right?.Data.Should().Be(160);
		result.Right?.Right?.Right.Should().BeNull();

		result.Height.Should().Be(3);
		result.IsBalanced.Should().BeTrue();
		result.Right?.Height.Should().Be(2);
		result.Left?.Height.Should().Be(1);
	}
	/* Visual representation of the tree before and after balancing
	*    UNBALANCED TREE --> BALANCED TREE
	*       100                  100
	*      /   \               /     \
	*     50   150           70     180
	*       \    \          /  \    /  \
	*       80   180       50  80  150 190
	*      /    /   \                \
	*     70  160  190 	    	  160
	*
	*   DEGENERATE-LEFT TREE --> BALANCED TREE  <--- DEGENERATE-RIGHT TREE
	*           0                     3                      6
	* 		      \                  /   \                   /
	* 			   1                1     5                 5
	* 			    \              / \   / \               /
	* 			     2           0    2 4   6             4
	* 			      \                                  /
	* 			       3                                3
	*                  \							   /
	* 		    		 4                            2
	*                    \ 						 /
	* 		       		   5                        1
	* 				     	\                      /
	* 			        	 6                    0
	*/
}