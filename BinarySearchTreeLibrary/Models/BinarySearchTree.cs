using BinarySearchTreeLibrary.Exceptions;
using BinarySearchTreeLibrary.Interfaces;

namespace BinarySearchTreeLibrary.Models;

public class BinarySearchTree<T> : IBinarySearchTree<T>
{
	private Node<T>? _root;
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
		EmptyTreeException.ThrowIfEmptyTree((IBinarySearchTree<object>) this);
		
		return _root?.FindChild(data.GetHashCode()) is not null;
	}

	public bool Remove(T data)
	{
		ArgumentNullException.ThrowIfNull(data);
		EmptyTreeException.ThrowIfEmptyTree((IBinarySearchTree<object>) this);

		var success = _root!.Remove(data.GetHashCode());

		UpdateSize(success, -1);

		return success;
	}

	public bool Balance()
	{
		throw new NotImplementedException();
	}

	public bool IsBalanced()
	{
		return _root is null || _root.IsBalanced;
	}

	public bool IsBinarySearchTree()
	{
		if (_root is null)
			throw new EmptyTreeException("Tree is empty");
		
		return IsSubtreeLessThan(_root, false) && IsSubtreeGreaterThan(_root, false);
	}
	
	private bool IsSubtreeLessThan(INode<T>? node, bool inclusive)
	{
		if (node == null)
			return true;

		if (node.Left != null && (node.Left.Key >= node.Key || (inclusive && node.Left.Key == node.Key)))
			return false;

		if (node.Right != null && node.Right.Key <= node.Key)
			return false;

		return IsSubtreeLessThan(node.Left, inclusive) && IsSubtreeLessThan(node.Right, inclusive);
	}

	private bool IsSubtreeGreaterThan(INode<T>? node, bool inclusive)
	{
		if (node == null)
			return true;

		if (node.Left != null && node.Left.Key >= node.Key)
			return false;

		if (node.Right != null && (node.Right.Key <= node.Key || (inclusive && node.Right.Key == node.Key)))
			return false;

		return IsSubtreeGreaterThan(node.Left, inclusive) && IsSubtreeGreaterThan(node.Right, inclusive);
	}
	
	private void UpdateSize(bool success, int value)
	{
		Size = success ? Size + value : Size;
	}
}