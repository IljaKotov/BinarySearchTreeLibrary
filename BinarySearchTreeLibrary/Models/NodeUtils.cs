using BinarySearchTreeLibrary.Interfaces;

namespace BinarySearchTreeLibrary.Models;

internal static class NodeUtils<T>
{
	public static void UpdateHeightPropsUpwards(INode<T>? node)
	{
		while (node is not NullNode<T> && node is not null)
		{
			UpdateHeightProps(node);
			node = node.Parent;
		}
	}

	public static void UpdateHeightProps(INode<T>? node)
	{
		if (node is NullNode<T> || node is null)
			return;

		node.UpdateHeight();
		node.UpdateBalanceFactor();
	}
	
	public static int GetBalanceFactor(INode<T>? node)
	{
		if (node is NullNode<T> || node is null)
			return 0;

		return node.Left.Height - node.Right.Height;
	}
	
	
	public static bool UpdateBalanceFactor(INode<T> node)
	{
		if (node is NullNode<T> or null)
			return true;

		var balanceFactor = GetBalanceFactor(node);
		var leftBalance = node.Left is {IsBalanced: true};
		var rightBalance = node.Right is {IsBalanced: true};

		return  Math.Abs(balanceFactor) <= 1 && leftBalance && rightBalance;
	}

	public static void ReplaceNode(INode<T> nodeToReplace, INode<T>? newNode)
	{
		if (nodeToReplace.Parent is null)
		{
			return;
		}

		if (nodeToReplace.Parent.Left == nodeToReplace)
		{
			nodeToReplace.Parent.Left = newNode;
		}
		else
		{
			nodeToReplace.Parent.Right = newNode;
		}
	}

}