using BinarySearchTreeLibrary.Exceptions;
using BinarySearchTreeLibrary.Interfaces;

namespace BinarySearchTreeLibrary.Models;

public class BinarySearchTree<T> : IBinarySearchTree<T>
{
	private INode<T>? _root;
	public int Size { get; private set; }
	public int Height => Root is not null ? _root!.Height : -1;
	public int RootBalanceFactor => _root is not null ? Math.Abs((_root.Left?.Height ?? -1) - (_root.Right?.Height ?? -1)) : 0;
	public object?  Root => _root is not null ? _root.Data : null;

	public BinarySearchTree() { }
	
	internal BinarySearchTree(T rootData, T leftData=default(T), T rightData=default(T))
	{
		_root = new Node<T>(rootData)
		{
			Left = new Node<T>(leftData),
			Right = new Node<T>(rightData)
		};
	}
	
	public bool Insert(T data)
	{
		ArgumentNullException.ThrowIfNull(data);
		bool success;
		
		if (_root is null)
		{
			_root = new Node<T>(data);
			success = true;
		}
		else
			success = _root.Insert(data);

		UpdateSize(success, 1);
		
		return success;
	}

	public bool Contains(T? data)
	{
		ArgumentNullException.ThrowIfNull(data);
		EmptyTreeException.ThrowIfEmptyTree(this);
		
		return _root?.FindChild(data.GetHashCode()) is not NullNode<T>;
	}

	public bool Remove(T data)
	{
		ArgumentNullException.ThrowIfNull(data);
		EmptyTreeException.ThrowIfEmptyTree(this);

		var removedNode = _root?.Remove(data.GetHashCode());
		
		var success = removedNode is not NullNode<T>;

		UpdateSize(success, -1);

		return success;
	}

	public void Balance()
	{
		EmptyTreeException.ThrowIfEmptyTree(this);
		_root = _root?.Balance();
	}

	public bool IsBalanced()
	{
		return _root is null || _root.IsBalanced;
	}

	public bool IsBinarySearchTree()
	{
		if (_root is null)
			throw new EmptyTreeException("Tree is empty");
		
		return IsSubtreeValid(_root, int.MinValue, int.MaxValue);
	}
	
	private bool IsSubtreeValid(INode<T>? node, int? min, int? max)
	{
		if (node is NullNode<T>)
			return true;

		if (node?.Key <= min || node?.Key > max)
			return false;

		return IsSubtreeValid(node?.Left, min, node?.Key) && IsSubtreeValid(node?.Right, node?.Key, max);
	}
	
	private void UpdateSize(bool success, int value)
	{
		Size = success ? Size + value : Size;
	}
}