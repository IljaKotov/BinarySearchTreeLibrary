using BinarySearchTreeLibrary.Exceptions;
using BinarySearchTreeLibrary.Interfaces;

namespace BinarySearchTreeLibrary.Models;

public class BinarySearchTree<T> : IBinarySearchTree<T>
{
	private static readonly ITreeBalancer<T> _treeBalancer = new TreeBalancer<T>();
	private INode<T>? _root;
	public int Size { get; private set; }
	public int Height => _root?.Height ?? -1;
	public int RootBalanceFactor => _root?.BalanceFactor ?? 0;

	internal T? RootData => _root is not null ? _root.Data : default;

	public BinarySearchTree()
	{
	}

	internal BinarySearchTree(T rootData, T leftData, T rightData)
	{
		_root = new Node<T>(rootData);

		if (leftData is not null)
			_root.Left = new Node<T>(leftData);

		if (rightData is not null)
			_root.Right = new Node<T>(rightData);
	}

	public void Add(T data)
	{
		ArgumentNullException.ThrowIfNull(data);

		if (_root is null)
		{
			_root = new Node<T>(data);
			_root.RootDeleted += () => _root = null;
		}
		else
		{
			_root.Insert(data);
		}

		Size++;
	}

	public bool Contains(T data)
	{
		ArgumentNullException.ThrowIfNull(data);
		EmptyTreeException.ThrowIfEmptyTree(RootData);

		return _root?.GetNodeByKey(data.GetHashCode()) is not null;
	}

	public bool TryDelete(T data)
	{
		ArgumentNullException.ThrowIfNull(data);
		EmptyTreeException.ThrowIfEmptyTree(RootData);

		var removalNode = _root?.Remove(data.GetHashCode());

		var isRemoved = removalNode is not null;

		_ = isRemoved ? Size-- : Size;

		return isRemoved;
	}

	public void Balance()
	{
		EmptyTreeException.ThrowIfEmptyTree(RootData);

		if (_root is not null)
			_root = _treeBalancer.Balance(_root);
	}

	public bool IsBalanced()
	{
		return _root?.IsBalanced ?? true;
	}

	public bool IsBinarySearchTree()
	{
		EmptyTreeException.ThrowIfEmptyTree(RootData);

		return IsSubtreeValid(_root, int.MinValue, int.MaxValue);
	}

	private static bool IsSubtreeValid(INode<T>? node, int? min, int? max)
	{
		if (node is null)
			return true;

		if (node.Key <= min || node.Key > max)
			return false;

		return IsSubtreeValid(node.Left, min, node.Key) &&
			IsSubtreeValid(node.Right, node.Key, max);
	}
}