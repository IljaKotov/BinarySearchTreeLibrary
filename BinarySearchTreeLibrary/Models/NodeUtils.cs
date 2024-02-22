using BinarySearchTreeLibrary.Interfaces;

namespace BinarySearchTreeLibrary.Models;

internal static class NodeUtils<T>
{
	public static int GetBalanceFactor(INode<T> node)
	{
		var (leftHeight, rightHeight) = GetNodeHeights(node);

		return leftHeight - rightHeight;
	}

	public static void UpdateHeightProps(INode<T> node)
	{
		node.Height = GetHeight(node);
		node.IsBalanced = UpdateBalanceFactor(node);
	}

	public static void UpdateHeightPropsUpwards(INode<T>? node)
	{
		while (node is not null)
		{
			UpdateHeightProps(node);
			node = node.Parent;
		}
	}

	public static void ReplaceNode(INode<T> nodeToReplace, INode<T>? newNode)
	{
		if (nodeToReplace.Parent is null)
			return;

		if (nodeToReplace.Parent.Left == nodeToReplace)
			nodeToReplace.Parent.Left = newNode;
		else
			nodeToReplace.Parent.Right = newNode;
	}

	private static int GetHeight(INode<T> node)
	{
		var (leftHeight, rightHeight) = GetNodeHeights(node);

		return 1 + Math.Max(leftHeight, rightHeight);
	}

	private static bool UpdateBalanceFactor(INode<T> node)
	{
		var balanceFactor = GetBalanceFactor(node);
		var leftBalance =  node.Left?.IsBalanced ?? true;
		var rightBalance =  node.Right?.IsBalanced ?? true;

		return Math.Abs(balanceFactor) <= 1 && leftBalance && rightBalance;
	}
	
	private static (int leftHeight, int rightHeight) GetNodeHeights(INode<T> node)
	{
		var leftHeight = node.Left?.Height ?? -1;
		var rightHeight = node.Right?.Height ?? -1;

		return (leftHeight, rightHeight);
	}
}