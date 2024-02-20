using BinarySearchTreeLibrary.Exceptions;
using BinarySearchTreeLibrary.Interfaces;

namespace BinarySearchTreeLibrary.Models;

public class BinarySearchTree<T> : IBinarySearchTree<T>
{
	private readonly ITreeBalancer<T> _treeBalancer = new TreeBalancer<T>();
	private INode<T>? _root;
	public int Size { get; private set; }
	public int Height => Root is not null ? _root!.Height : -1;
	public int RootBalanceFactor => _root is not null ? NodeUtils<T>.GetBalanceFactor(_root) : 0;
	public object? Root => _root is not null ? _root.Data : null;

	public BinarySearchTree()
	{
	}

	internal BinarySearchTree(T rootData,
		T leftData = default,
		T rightData = default)
	{
		_root = new Node<T>(rootData)
		{
			Left = new Node<T>(leftData),
			Right = new Node<T>(rightData)
		};
	}

	public bool Add(T data)
	{
		ArgumentNullException.ThrowIfNull(data);
		bool isAdded;

		if (_root is null)
		{
			_root = new Node<T>(data);
			isAdded = true;
		}
		else
		{
			isAdded = _root.Insert(data);
		}

		UpdateSize(isAdded, 1);

		return isAdded;
	}

	public bool Contains(T? data)
	{
		ArgumentNullException.ThrowIfNull(data);
		EmptyTreeException.ThrowIfEmptyTree(this);

		return _root?.FindChild(data.GetHashCode()) is not NullNode<T>;
	}

	public bool Delete(T data)
	{
		ArgumentNullException.ThrowIfNull(data);
		EmptyTreeException.ThrowIfEmptyTree(this);

		var removedNode = _root?.Remove(data.GetHashCode());

		var isRemoved = removedNode is not NullNode<T>;

		UpdateSize(isRemoved, -1);

		return isRemoved;
	}

	public void Balance()
	{
		EmptyTreeException.ThrowIfEmptyTree(this);
		_root = _treeBalancer.Balance(_root);
	}

	public bool IsBalanced()
	{
		return _root is null || _root.IsBalanced;
	}

	public bool IsBinarySearchTree()
	{
		if (_root is null)
		{
			throw new EmptyTreeException("Tree is empty");
		}

		return IsSubtreeValid(_root, int.MinValue, int.MaxValue);
	}

	private static bool IsSubtreeValid(INode<T>? node,
		int? min,
		int? max)
	{
		if (node is NullNode<T>)
		{
			return true;
		}

		if (node?.Key <= min || node?.Key > max)
		{
			return false;
		}

		return IsSubtreeValid(node?.Left, min, node?.Key) && IsSubtreeValid(node?.Right, node?.Key, max);
	}

	private void UpdateSize(bool isSuccess, int value)
	{
		Size = isSuccess ? Size + value : Size;
	}
}