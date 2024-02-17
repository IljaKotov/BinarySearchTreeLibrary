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
	
	public static CompareResult Compare(INode<T> node1, INode<T> node2)
	{
		if (node1 is NullNode<T>)
		{
			return node2 is NullNode<T> ? CompareResult.EqualTo : CompareResult.LessThan;
		}

		if (node2 is NullNode<T>)
		{
			return CompareResult.GreaterThan;
		}

		if (node1.Key < node2.Key)
		{
			return CompareResult.LessThan;
		}

		return node1.Key > node2.Key ? CompareResult.GreaterThan : CompareResult.EqualTo;
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

}