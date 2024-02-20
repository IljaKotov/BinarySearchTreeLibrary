using BinarySearchTreeLibrary.Exceptions;
using BinarySearchTreeLibrary.Interfaces;

namespace BinarySearchTreeLibrary.Models;

internal class Node<T>(T data) : INode<T>
{
	private readonly ITreeBalancer<T> _treeBalancer = new TreeBalancer<T>();
	private readonly INodeRemover<T?> _nodeRemover = new NodeRemover<T?>();

	public T Data { get; set; } = data ?? default!;
	public int Key => Data is not null ? Data.GetHashCode() : 0;
	public INode<T>? Left { get; set; } = new NullNode<T>();
	public INode<T>? Right { get; set; } = new NullNode<T>();
	public INode<T>? Parent { get; set; }
	public int Height { get; set; }
	public bool IsLeaf => Left is NullNode<T> && Right is NullNode<T>;
	public bool HasBothChildren => Left is not NullNode<T> && Right is not NullNode<T>;

	public bool IsBalanced { get; set; } = true;

	public bool Insert(T data)
	{
		ArgumentNullException.ThrowIfNull(data);
		DuplicateKeyException.ThrowIfEqual(Key, data.GetHashCode());

		var compareKeyResult = data.GetHashCode().CompareTo(Key);
		var child = compareKeyResult < 0 ? Left : Right;

		if (child is not NullNode<T> and not null)
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

	public INode<T>? FindByKey(int key)
	{
		var compareKeyResult = key.CompareTo(Key);

		if (compareKeyResult < 0)
		{
			return Left?.FindByKey(key);
		}

		return compareKeyResult > 0 ? Right?.FindByKey(key) : this;
	}

	public INode<T> Remove(int key)
	{
		var nodeToRemove = FindByKey(key);

		if (nodeToRemove is NullNode<T> or null)
		{
			return this;
		}

		_nodeRemover.RemoveNode(nodeToRemove);

		return this;
	}

	public INode<T> Balance()
	{
		return _treeBalancer.Balance(this);
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