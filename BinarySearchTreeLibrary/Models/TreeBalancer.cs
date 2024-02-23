using BinarySearchTreeLibrary.Exceptions;
using BinarySearchTreeLibrary.Interfaces;

namespace BinarySearchTreeLibrary.Models;

internal class TreeBalancer<T> : ITreeBalancer<T>
{
	public INode<T> Balance(INode<T> balancingNode)
	{
		var node = balancingNode;

		while (node.IsBalanced is false)
			node = BalanceRecursive(node);

		return node;
	}

	private static INode<T> BalanceRecursive(INode<T> node)
	{
		Utils.UpdateProperties(node);

		if (node.BalanceFactor > 1)
			node = CorrectRightHeavy(node);

		else if (node.BalanceFactor < -1)
			node = CorrectLeftHeavy(node);

		BalanceNodeChildren(node);

		node.UpdateHeight();

		return node;
	}

	private static INode<T> CorrectRightHeavy(INode<T> node)
	{
		if (node.Left is not null && node.Left.BalanceFactor < 0)
		{
			node.Left = Rotate(node.Left, Direction.Left);
		}

		return Rotate(node, Direction.Right);
	}

	private static INode<T> CorrectLeftHeavy(INode<T> node)
	{
		if (node.Right is not null && node.Right.BalanceFactor > 0)
		{
			node.Right = Rotate(node.Right, Direction.Right);
		}

		return Rotate(node, Direction.Left);
	}

	private static INode<T> Rotate(INode<T> node, Direction direction)
	{
		InvalidArgumentException.ThrowIfDirectionSame(direction);

		var newNode = direction is Direction.Right ? node.Left : node.Right;

		ArgumentNullException.ThrowIfNull(newNode);

		if (direction is Direction.Right)
			RotateRight(node, newNode);
		else
			RotateLeft(node, newNode);

		return newNode;
	}

	private static void RotateRight(INode<T> node, INode<T>? newNode)
	{
		Utils.ReplaceNodes(node, newNode);

		node.Left = newNode?.Right;

		if (node.Left is not null)
			node.Left.Parent = node;

		if (newNode is null)
			return;

		newNode.Right = node;
		node.Parent = newNode;
		newNode.Parent = null;
	}

	private static void RotateLeft(INode<T> node, INode<T>? newNode)
	{
		Utils.ReplaceNodes(node, newNode);

		node.Right = newNode?.Left;

		if (node.Right is not null)
			node.Right.Parent = node;

		if (newNode is null)
			return;

		newNode.Left = node;
		node.Parent = newNode;
		newNode.Parent = null;
	}

	private static void BalanceNodeChildren(INode<T> node)
	{
		if (node.Left is not null)
			node.Left = BalanceRecursive(node.Left);

		if (node.Right is not null)
			node.Right = BalanceRecursive(node.Right);
	}
}