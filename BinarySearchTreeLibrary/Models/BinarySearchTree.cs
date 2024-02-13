using BinarySearchTreeLibrary.Interfaces;

namespace BinarySearchTreeLibrary.Models;

public class BinarySearchTree<T> : IBinarySearchTree<T>
{
	private Node<T>? _root;

	public int Size { get; private set; }
	public int Height { get; private set; }
	public int RootBalanceFactor { get; private set; }

	public BinarySearchTree()
	{
		Size = 0;
		Height = -1;
		RootBalanceFactor = 0;
		_root = null;
	}

	public bool Insert(T data)
	{
		ArgumentNullException.ThrowIfNull(data);
		
		if (_root is null)
		{
			_root = new Node<T>(data);
			Size++;
			Height++;
			return true;
		}

		var success = _root.Insert(data);

		if (!success)
		{
			return success;
		}

		Size++;
		var newHeight = Math.Max(_root.Left?.Height ?? -1, _root.Right?.Height ?? -1) + 1;
		if (newHeight > Height)
			Height = newHeight;
		return success;
	}

	public bool Contains(T data)
	{
		ArgumentNullException.ThrowIfNull(data);
		return _root?.FindChild(data.GetHashCode()) is not null;
	}

	public bool Remove(T data)
	{
		ArgumentNullException.ThrowIfNull(data);
		
		if (_root is null)
			return false;

		var success = _root.Remove(data.GetHashCode());
		if (success)
			Size--;

		return success;
	}

	public bool Balance()
	{
		throw new NotImplementedException();
	}

	public bool IsBalanced()
	{
		if (_root == null)
			return true;

		return _root.IsBalanced;
	}

	public bool IsBinarySearchTree()
	{
		throw new NotImplementedException();
	}

	public bool Clear()
	{
		_root = null;
		Size = 0;
		Height = -1;
		RootBalanceFactor = 0;
		return true;
	}
}