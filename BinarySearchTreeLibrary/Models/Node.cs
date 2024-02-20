using BinarySearchTreeLibrary.Exceptions;
using BinarySearchTreeLibrary.Interfaces;

namespace BinarySearchTreeLibrary.Models;

internal class Node<T>(T data) : INode<T>
{
	private readonly ITreeBalancer<T> _treeBalancer = new TreeBalancer<T>();
	private readonly INodeRemover<T> _nodeRemover = new NodeRemover<T>();
	
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

		NodeUtils<T>.UpdateHeightProps(this);

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

	public void UpdateBalanceFactor()
	{
		IsBalanced = NodeUtils<T>.UpdateBalanceFactor(this);
	}

	public INode<T>? Rotate(bool isRight)
	{
		var newRoot = isRight ? Left : Right;

		if (newRoot is NullNode<T>)
		{
			return null;
		}

		NodeUtils<T>.ReplaceNode(this, newRoot);

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
		return _treeBalancer.Balance(this);
	}

	public INode<T> Remove(int key)
	{
		var nodeToRemove = FindChild(key);

		if (nodeToRemove is  NullNode<T> or null)
		{
			return this;
		}
		
		_nodeRemover.RemoveNode(nodeToRemove);
		
		return this;
	}
	

	private void CreateChild(T data, int compareDirection)
	{
		if (compareDirection < 0)
		{
			Left = new Node<T>(data);
			Left.Parent = this;

			return;
		}

		Right = new Node<T>(data);
		Right.Parent = this;
	}
}