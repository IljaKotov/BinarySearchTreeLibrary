using BinarySearchTreeLibrary.Interfaces;

namespace BinarySearchTreeLibrary.Models;

internal class Balancer<T>
{
	public INode<T> Balance(INode<T> node)
	{
		INode<T> newRoot = node;

		while (newRoot is not NullNode<T> && newRoot.IsBalanced==false) //Math.Abs(newRoot.GetBalanceFactor()) > 1)
			newRoot = BalanceNodeRecursive(newRoot) ?? node;

		return newRoot;
	}
	
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

	public static INode<T>? BalanceNodeRecursive(INode<T>? node)
	{
		if (node is null)
		{
			return null;
		}

		node = RotateNode(node);
		BalanceChildNodes(node);

		UpdateHeightProps(node);

		return node;
	}

	public static INode<T>? RotateNode(INode<T>? node)
	{
		if (node.GetBalanceFactor() > 1)
		{
			if (node.Left is not NullNode<T> && node.Left.GetBalanceFactor() < 0)
			{
				node.Left = node.Left.RotateLeft();
			}

			node = node.RotateRight();
		}
		else if (node.GetBalanceFactor() < -1)
		{
			if (node.Right is not NullNode<T> && node.Right.GetBalanceFactor() > 0)
			{
				node.Right = node.Right.RotateRight();
			}

			node = node.RotateLeft();
		}

		return node;
	}

	public static void BalanceChildNodes(INode<T> node)
	{
		if (node.Left is not NullNode<T>)
		{
			node.Left = BalanceNodeRecursive(node.Left);
		}

		if (node.Right is not NullNode<T>)
		{
			node.Right = BalanceNodeRecursive(node.Right);
		}
	}
}