using BinarySearchTreeLibrary.Exceptions;
using BinarySearchTreeLibrary.Interfaces;

namespace BinarySearchTreeLibrary.Models;

internal class Node<T>(T data) : INode<T>
{
	private Balancer<T> _balancer = new();
	public T Data { get; set; } = data ?? default!;
	public int Key => Data is not null ? Data.GetHashCode() : 0;
	public INode<T>? Left { get; set; }= new NullNode<T>();
	public INode<T>? Right { get; set; }= new NullNode<T>();
	public INode<T>? Parent { get; set; }
	public int Height { get; set; }
	public bool IsLeaf => Left is NullNode<T> && Right is NullNode<T>;
	public bool HasBothChildren => Left is not NullNode<T> && Right is not NullNode<T>;

	public bool IsBalanced { get; private set; } = true;

	public bool Insert(T data)
	{
		ArgumentNullException.ThrowIfNull(data);
		DuplicateKeyException.ThrowIfEqual(Key, data.GetHashCode());

		var compareKeyResult = data.GetHashCode().CompareTo(Key);
		var child = compareKeyResult < 0 ? Left : Right;

		if (child is not NullNode<T>)
		{
			child?.Insert(data);
		}
		else
		{
			CreateChild(data, compareKeyResult);
		}

		UpdateHeightProps(this);

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
		Height = 1 + Math.Max(Left.Height, Right.Height);
	}

	public int GetBalanceFactor()
	{
		return Left.Height - Right.Height;
	}

	public void UpdateBalanceFactor()
	{
		var balanceFactor = GetBalanceFactor();
		var leftBalance = Left.IsBalanced;
		var rightBalance = Right.IsBalanced;

		IsBalanced = Math.Abs(balanceFactor) <= 1 && leftBalance && rightBalance;
	}

	public INode<T>? Rotate(bool isRight)
	{
		var newRoot = isRight ? Left : Right;

		if (newRoot is NullNode<T>)
		{
			return null;
		}

		ReplaceNode(this, newRoot);

		if (isRight)
		{
			Left = newRoot.Right;

			if (Left is not NullNode<T>)
			{
				Left.Parent = this;
			}

			newRoot.Right = this;
		}
		else
		{
			Right = newRoot.Left;

			if (Right is not NullNode<T>)
			{
				Right.Parent = this;
			}

			newRoot.Left = this;
		}

		Parent = newRoot;
		newRoot.Parent = null;

		return newRoot;
	}

	public INode<T>? RotateLeft()
	{
		return Rotate(false);
	}

	public INode<T>? RotateRight()
	{
		return Rotate(true);
	}

	public INode<T> Balance()
	{
		/*INode<T> newRoot = this;

		while (newRoot is not NullNode<T> && newRoot.IsBalanced==false) //Math.Abs(newRoot.GetBalanceFactor()) > 1)
			newRoot = BalanceNodeRecursive(newRoot) ?? this;

		return newRoot;*/
		return _balancer.Balance(this);
	}

	public INode<T> Remove(int key)
	{
		var nodeToRemove = FindChild(key);

		if (nodeToRemove is  NullNode<T>)
		{
			return this;
		}

		RemoveNode(nodeToRemove);
		
		return this;
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

		UpdateHeightPropsUpwards(node);
	}

	private static void RemoveLeafNode(INode<T> node)
	{
		if (node.Parent is  null)
		{
			node.Data = default!;
		}
		else
		{
			ReplaceNode(node, new NullNode<T>());
		}
	}

	private static void RemoveNodeWithSingleChild(INode<T> node)
	{
		var child = node.Left is not NullNode<T> ? node.Left : node.Right;

		if (child is NullNode<T>)
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
		if (nodeToReplace.Parent is  null)
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

	private void RemoveNodeWithTwoChildren(INode<T> node)
	{
		var successor = FindMinInRight(node.Right!);
		node.Data = successor.Data;

		RemoveNode(successor);
		UpdateHeightPropsUpwards(node.Parent);
	}

	private static INode<T> FindMinInRight(INode<T> node)
	{
		while (node.Left is not NullNode<T>)
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

	private static void UpdateHeightPropsUpwards(INode<T>? node)
	{
		while (node is not NullNode<T> && node is not null)
		{
			UpdateHeightProps(node);
			node = node.Parent;
		}
	}

	private static void UpdateHeightProps(INode<T>? node)
	{
		if (node is NullNode<T> || node is null)
			return;

		node.UpdateHeight();
		node.UpdateBalanceFactor();
	}

	/*private static INode<T>? BalanceNodeRecursive(INode<T>? node)
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

	private static INode<T>? RotateNode(INode<T>? node)
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
	}*/
}