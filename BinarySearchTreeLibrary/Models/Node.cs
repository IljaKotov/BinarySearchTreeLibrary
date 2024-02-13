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

		var direction = compareKeyResult < 0 ? Left : Right;

		if (direction is not null)
			direction.Insert(data);
		else
			CreateChild(data, compareKeyResult);

		UpdateHeight();

		return true;
	}

	public INode<T>? FindChild(int key)
	{
		var compareKeyResult = key.CompareTo(Key);

		if (compareKeyResult < 0)
			return Left?.FindChild(key);

		return compareKeyResult > 0 ? Right?.FindChild(key) : this;
	}

	public bool Remove(int key)
	{
		var nodeToRemove = FindChild(key);

		if (nodeToRemove is null)
			return false;

		RemoveNode(nodeToRemove);

		return true;
	}

	private void RemoveNode(INode<T> node)
	{
		if (node.IsLeaf)
			RemoveLeafNode(node);
		else if (!node.HasBothChildren)
			RemoveNodeWithSingleChild(node);
		else
			RemoveNodeWithTwoChildren(node);
		
		UpdateHeightUpwards(node);
	}

	private static void RemoveLeafNode(INode<T> node)
	{
		if (node.Parent is null)
			node.Data = default!;
		else
			ReplaceNode(node, null);
	}

	private static void RemoveNodeWithSingleChild(INode<T> node)
	{
		var child = node.Left ?? node.Right;
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
		if (nodeToReplace.Parent.Left == nodeToReplace)
			nodeToReplace.Parent.Left = newNode;
		else
			nodeToReplace.Parent.Right = newNode;
	}

	private void RemoveNodeWithTwoChildren(INode<T> node)
	{
		var successor = FindMin(node.Right);
		node.Data = successor.Data;
		RemoveNode(successor);
		
		UpdateHeightUpwards(node.Parent);
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
	private void UpdateHeight()
	{
		Height = 1 + Math.Max(Left?.Height ?? -1, Right?.Height ?? -1);
	}
	
	private void UpdateHeightUpwards(INode<T>? node)
	{
		while (node is not null)
		{
			UpdateHeight();
			node = node.Parent;
		}
	}
}