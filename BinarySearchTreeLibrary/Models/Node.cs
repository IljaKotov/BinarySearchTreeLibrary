using BinarySearchTreeLibrary.Exceptions;
using BinarySearchTreeLibrary.Interfaces;

namespace BinarySearchTreeLibrary.Models;

internal class Node<T> : INode<T>
{
	public T Data { get; set; }
	public int Key => Data != null ? Data.GetHashCode() : 0;
	public INode<T>? Left { get; set; }
	public INode<T>? Right { get; set; }
	public INode<T>? Parent { get; set; }
	public int Height { get; set; }
	public bool IsLeaf => Left is null && Right is null;
	public bool HasBothChildren => Left is not null && Right is not null;

	public bool IsBalanced { get; set; }

	public Node(T data)
	{
		ArgumentNullException.ThrowIfNull(data);
		Data = data;
	}

	public bool Insert(T data)
	{
		ArgumentNullException.ThrowIfNull(data);
		DuplicateKeyException.ThrowIfEqual(Key, data.GetHashCode());

		var compareKeyResult = data.GetHashCode().CompareTo(Key);

		if (compareKeyResult < 0)
		{
			if (Left is not null)
			{
				Left.Insert(data);
			}
			else
			{
				CreateLeftChild(data);
			}
		}
		else if (compareKeyResult > 0)
		{
			if (Right is not null)
			{
				Right.Insert(data);
			}
			else
			{
				CreateRightChild(data);
			}
		}

		UpdateHeight();

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
	
	public bool Remove(T data)
	{
		var nodeToRemove = FindChild(data.GetHashCode());

		if (nodeToRemove == null)
			return false;

		RemoveNode(nodeToRemove);

		return true;
	}

	private void UpdateHeight()
	{
		Height = 1 + Math.Max(Left?.Height ?? -1, Right?.Height ?? -1);
	}

	private void CreateLeftChild(T data)
	{
		Left = new Node<T>(data);
		Left.Parent = this;
	}

	private void CreateRightChild(T data)
	{
		Right = new Node<T>(data);
		Right.Parent = this;
	}

	private void RemoveNode(INode<T> node)
	{
		if (node.IsLeaf)
			RemoveLeafNode(node);
		else if (!node.HasBothChildren)
			RemoveNodeWithSingleChild(node);
		else
			RemoveNodeWithTwoChildren(node);
	}

	private void RemoveLeafNode(INode<T> node)
	{
		if (node.Parent == null)
			node.Data = default!;
		else if (node.Parent.Left == node)
			node.Parent.Left = null;
		else
			node.Parent.Right = null;
	}

	private void RemoveNodeWithSingleChild(INode<T> node)
	{
		var child = node.Left ?? node.Right;
		child.Parent = node.Parent;

		if (node.Parent == null)
		{
			node.Data = child.Data;
			node.Left = child.Left;
			node.Right = child.Right;
		}
		else if (node.Parent.Left == node)
		{
			node.Parent.Left = child;
		}
		else
		{
			node.Parent.Right = child;
		}
	}

	private void RemoveNodeWithTwoChildren(INode<T> node)
	{
		var successor = GetSuccessor(node);
		node.Data = successor.Data;
		RemoveNode(successor);
	}

	private INode<T> GetSuccessor(INode<T> node)
	{
		return FindMin(node.Right);
	}

	private INode<T> FindMin(INode<T> node)
	{
		while (node.Left != null)
			node = node.Left;

		return node;
	}
}