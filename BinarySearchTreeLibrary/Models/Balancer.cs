using BinarySearchTreeLibrary.Interfaces;

namespace BinarySearchTreeLibrary.Models;

internal class Balancer<T> : IBalancer<T> 
{
	public INode<T> Balance(INode<T> node)
	{
		INode<T> newRoot = node;

		while (newRoot is not NullNode<T> && newRoot.IsBalanced==false)
			newRoot = BalanceNodeRecursive(newRoot) ?? node;

		return newRoot;
	}

	private static INode<T>? BalanceNodeRecursive(INode<T>? node)
	{
		if (node is null)
		{
			return null;
		}

		node = RotateNode(node);
		BalanceChildNodes(node);

		NodeUtils<T>.UpdateHeightProps(node); 

		return node;
	}

	private static INode<T>? RotateNode(INode<T>? node)
	{
		if (NodeUtils<T>.GetBalanceFactor(node)>1) //node.GetBalanceFactor() > 1)
		{
			if (node.Left is not NullNode<T> && NodeUtils<T>.GetBalanceFactor(node.Left)<0)//node.Left.GetBalanceFactor() < 0)
			{
				node.Left = node.Left.RotateLeft();
			}

			node = node.RotateRight();
		}
		else if (NodeUtils<T>.GetBalanceFactor(node)<-1) //node.GetBalanceFactor() < -1)
		{
			if (node.Right is not NullNode<T> && NodeUtils<T>.GetBalanceFactor(node.Right)>0)//node.Right.GetBalanceFactor() > 0)
			{
				node.Right = node.Right.RotateRight();
			}

			node = node.RotateLeft();
		}

		return node;
	}

	private static void BalanceChildNodes(INode<T> node)
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