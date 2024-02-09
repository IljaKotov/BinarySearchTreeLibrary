using BinarySearchTreeLibrary.Exceptions;
using BinarySearchTreeLibrary.Interfaces;

namespace BinarySearchTreeLibrary.Models;

internal class Node<T> : INode<T>
{
	public T Data { get; set; }
	public int Key { get; set; }
	public INode<T>? Left { get; set; }
	public INode<T>? Right { get; set; }
	public INode<T>? Parent { get; set; }
	public int Height { get; set; }
	public bool IsLeaf => Left is null && Right is null;
	//public bool IsLeftChild => Parent is not null && Parent.Left == this;
	//public bool IsRightChild => Parent is not null && Parent.Right == this;
	//public bool IsRoot => Parent is null;
	public bool HasBothChildren => Left is not null && Right is not null;
	//public bool HasLeftChild => Left is not null;
	//public bool HasRightChild => Right is not null;
	//public bool HasParent => Parent is not null;

	public bool IsBalanced => throw

		// Implement balance check logic here
		new NotImplementedException();

	public Node(T data)
	{
		ArgumentNullException.ThrowIfNull(data);

		Data = data;
		Key = data.GetHashCode();
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

	public void ReplaceData(T newData)
	{
		throw new NotImplementedException();
	}

	public void ReplaceNode(INode<T> newNode, bool isRoot)
	{
		Data = newNode.Data;
		Key = newNode.Data.GetHashCode();

		if (isRoot)
		{
			Left = newNode.Left;
			Right = newNode.Right;
		}
	}

	public void ReplaceChild(INode<T> existingChild, INode<T>? newChild)
	{
		if (Left == existingChild)
		{
			Left = newChild;
		}
		else if (Right == existingChild)
		{
			Right = newChild;
		}
	}

	public bool Insert(T data)
	{
		ArgumentNullException.ThrowIfNull(data);

		var compareKeyResult = data.GetHashCode().CompareTo(Key);

		DuplicateKeyException.ThrowIfEqual(Key, data.GetHashCode());

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

	public bool RemoveChild(int key)
	{
		var nodeToDelete = FindChild(key);

		if (nodeToDelete is null)
		{
			return false;
		}

		if (!nodeToDelete.IsLeaf)
		{
			if (nodeToDelete.HasBothChildren)
			{
				var replacementNode = nodeToDelete.Right?.Minimum();
				nodeToDelete.ReplaceNode(replacementNode, false);
				nodeToDelete = replacementNode;
			}
			else
			{
				var child = nodeToDelete.Left ?? nodeToDelete.Right;

				if (nodeToDelete.Parent is null)
				{
					nodeToDelete.ReplaceNode(child, isRoot:true);
				}
				else
				{
					nodeToDelete.Parent!.ReplaceChild(nodeToDelete, child);
				}
			}
		}

		if (nodeToDelete!.IsLeaf)
		{
			nodeToDelete.Parent?.ReplaceChild(nodeToDelete, null);
		}

		return true;
	}

	public INode<T> Minimum()
	{
		INode<T> current = this;

		while (current.Left != null)
		{
			current.Height--;
			current = current.Left;
		}

		return current;
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

	private void UpdateHeight()
	{
		Height = 1 + Math.Max(Left?.Height ?? -1, Right?.Height ?? -1);
	}
}