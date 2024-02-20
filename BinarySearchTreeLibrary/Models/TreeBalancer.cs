using BinarySearchTreeLibrary.Interfaces;

namespace BinarySearchTreeLibrary.Models;

internal class TreeBalancer<T> : ITreeBalancer<T> 
{
	public INode<T>? Balance(INode<T> node)
	{
		var newRoot = node;

		while (newRoot is not NullNode<T> and not null && newRoot.IsBalanced==false)
			newRoot = BalanceNodeRecursive(newRoot);

		return newRoot;
	}

	private static INode<T>? BalanceNodeRecursive(INode<T> node)
	{
		if (node is NullNode<T> or null)
			return null;

		node = AdjustNodeBalance(node);
		BalanceChildNodes(node);

		NodeUtils<T>.UpdateHeightProps(node); 

		return node;
	}

	private static INode<T> AdjustNodeBalance(INode<T> node)
	{
		if (NodeUtils<T>.GetBalanceFactor(node)>1) 
		{
			if (node.Left is not NullNode<T> and not null && NodeUtils<T>.GetBalanceFactor(node.Left)<0)
				node.Left = ChooseRotation(node.Left, isRight: false);
			
			node = ChooseRotation(node, isRight: true);
		}
		else if (NodeUtils<T>.GetBalanceFactor(node)<-1) 
		{
			if (node.Right is not NullNode<T> and not null && NodeUtils<T>.GetBalanceFactor(node.Right)>0)
				node.Right = ChooseRotation(node.Right,isRight: true);
			
			node = ChooseRotation(node, isRight: false);
		}

		return node;
	}
	
	private static INode<T> ChooseRotation(INode<T> node, bool isRight)
	{
		var newRoot = isRight ? node.Left : node.Right;

		if (newRoot is NullNode<T> or null)
			return node;

		if (isRight)
			RotateRight(node, newRoot);
		else
			RotateLeft(node, newRoot);

		return newRoot;
	}
	
	private static void RotateRight(INode<T> node, INode<T> newRoot)
	{
		NodeUtils<T>.ReplaceNode(node, newRoot);

		node.Left = newRoot.Right;

		if (node.Left is not NullNode<T> and not null)
			node.Left.Parent = node;

		newRoot.Right = node;
		node.Parent = newRoot;
		newRoot.Parent = null;
	}

	private static void RotateLeft(INode<T> node, INode<T> newRoot)
	{
		NodeUtils<T>.ReplaceNode(node, newRoot);

		node.Right = newRoot.Left;

		if (node.Right is not NullNode<T> and not null)
			node.Right.Parent = node;

		newRoot.Left = node;
		node.Parent = newRoot;
		newRoot.Parent = null;
	}

	private static void BalanceChildNodes(INode<T> node)
	{
		if (node.Left is not NullNode<T> and not null)
			node.Left = BalanceNodeRecursive(node.Left);

		if (node.Right is not NullNode<T> and not null)
			node.Right = BalanceNodeRecursive(node.Right);
	}
}