using BinarySearchTreeLibrary.Exceptions;
using BinarySearchTreeLibrary.Interfaces;

namespace BinarySearchTreeLibrary.Models;

internal class Node<T> : INode<T>
{
	public T Data { get; set; }
	public int Key => Data is not null ? Data.GetHashCode() : 0;
	public INode<T>? Left { get; set; }
	public INode<T>? Right { get; set; }
	public INode<T>? Parent { get; set; }
	public int Height { get; set; }
	public bool IsLeaf => Left is null && Right is null;
	public bool HasBothChildren => Left is not null && Right is not null;

	public bool IsBalanced { get; private set; }

	public Node(T data)
	{
		ArgumentNullException.ThrowIfNull(data);
		Data = data;
		IsBalanced = true;
	}

	public bool Insert(T data)
	{
		ArgumentNullException.ThrowIfNull(data);
		DuplicateKeyException.ThrowIfEqual(Key, data.GetHashCode());

		var compareKeyResult = data.GetHashCode().CompareTo(Key);

		var direction = compareKeyResult < 0 ? Left : Right;

		if (direction is not null)
		{
			direction.Insert(data);
		}
		else
		{
			CreateChild(data, compareKeyResult);
		}

		UpdateHeight();
		UpdateBalanceFactor();

		return true;
	}

	public INode<T>? FindChild(int key)
	{
		var compareKeyResult = key.CompareTo(Key);

		if (compareKeyResult < 0)
		{
			return Left?.FindChild(key);
		}

		return compareKeyResult > 0 ? Right?.FindChild(key) : this;
	}

	public void UpdateHeight()
	{
		Height = 1 + Math.Max(Left?.Height ?? -1, Right?.Height ?? -1);
	}

	public void UpdateBalanceFactor()
	{
		var leftHeight = Left?.Height ?? -1;
		var rightHeight = Right?.Height ?? -1;

		var balanceFactor = Math.Abs(leftHeight - rightHeight);

		var leftBalance = Left?.IsBalanced ?? true;
		var rightBalance = Right?.IsBalanced ?? true;

		IsBalanced = balanceFactor <= 1 && leftBalance && rightBalance;
	}

	public bool Remove(int key)
	{
		var nodeToRemove = FindChild(key);

		if (nodeToRemove is null)
		{
			return false;
		}

		RemoveNode(nodeToRemove);

		return true;
	}

	private void RemoveNode(INode<T> node)
	{
		if (node.IsLeaf)
		{
			RemoveLeafNode(node);
		}
		else if (!node.HasBothChildren)
		{
			RemoveNodeWithSingleChild(node);
		}
		else
		{
			RemoveNodeWithTwoChildren(node);
		}

		UpdateHeightBalanceFactorUpwards(node);
	}

	private static void RemoveLeafNode(INode<T> node)
	{
		if (node.Parent is null)
		{
			node.Data = default!;
		}
		else
		{
			ReplaceNode(node, null);
		}
	}

	private static void RemoveNodeWithSingleChild(INode<T> node)
	{
		var child = node.Left ?? node.Right;

		if (child is null)
		{
			return;
		}

		child.Parent = node.Parent;

		if (node.Parent is not null)
		{
			ReplaceNode(node, child);

			return;
		}

		node.Data = child.Data;
		node.Left = child.Left;
		node.Right = child.Right;
	}

	private static void ReplaceNode(INode<T> nodeToReplace, INode<T>? newNode)
	{
		if (nodeToReplace.Parent!.Left == nodeToReplace)
		{
			nodeToReplace.Parent.Left = newNode;
		}
		else
		{
			nodeToReplace.Parent.Right = newNode;
		}
	}

	private void RemoveNodeWithTwoChildren(INode<T> node)
	{
		var successor = FindMin(node.Right!);
		node.Data = successor.Data;
		RemoveNode(successor);
		UpdateHeightBalanceFactorUpwards(node.Parent);
	}

	private static INode<T> FindMin(INode<T> node)
	{
		while (node.Left is not null)
			node = node.Left;

		return node;
	}

	private void CreateChild(T data, int direction)
	{
		if (direction < 0)
		{
			Left = new Node<T>(data);
			Left.Parent = this;

			return;
		}

		Right = new Node<T>(data);
		Right.Parent = this;
	}

	private static void UpdateHeightBalanceFactorUpwards(INode<T>? node)
	{
		while (node is not null)
		{
			node.UpdateHeight();
			node.UpdateBalanceFactor();
			node = node.Parent;
		}
	}
	
	public void Balance()
	{
		// Код для балансування дерева

		BalanceRecursive(this); // Рекурсивний виклик балансування
	}

	private INode<T>? BalanceRecursive(INode<T>? node)
	{
		if (node is null)
		{
			return null;
		}
		
		node.Left = BalanceRecursive(node.Left);
		node.Right = BalanceRecursive(node.Right);

		int balanceFactor = node.GetBalanceFactor();

		if (balanceFactor>1)
		{
			if (node.Left is not null && node.Left.GetBalanceFactor() < 0)
			{
				node.Left=node.Left.RotateLeft();
			}
			return node.RotateRight();
		}
		if (balanceFactor<-1)
		{
			if (node.Right is not null && node.Right.GetBalanceFactor() > 0)
			{
				node.Right=node.Right.RotateRight();
			}
			return node.RotateLeft();
		}
		node.UpdateHeight();
		node.UpdateBalanceFactor();
		return node;
		
	}

	public INode<T> RotateRight()
	{
		if (Left is null)
		{
			return this;
		}
		var newRoot = Left;
		
		var temp = newRoot.Right;
		newRoot.Right = this;
		Left = temp;
		
		UpdateHeight();
		UpdateBalanceFactor();
		return newRoot;
	}

	public INode<T> RotateLeft()
	{
		if (Right is null)
		{
			return this;
		}
		var newRoot = Right;
		
		var temp = newRoot.Left;
		newRoot.Left = this;
		Right = temp;
		UpdateHeight();
		UpdateBalanceFactor();
		return newRoot;
	}

	public int GetBalanceFactor()
	{
		return (Left?.Height ?? -1) - (Right?.Height ?? -1);
	}
}