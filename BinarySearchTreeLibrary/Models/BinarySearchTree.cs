using BinarySearchTreeLibrary.Interfaces;

namespace BinarySearchTreeLibrary.Models;

public class BinarySearchTree<T> : IBinarySearchTree<T>
{
	private Node<T>? _root;
	public int Size { get; private set; }
	public int Height { get; private set; }
	public int RootBalanceFactor => _root is not null ? Math.Abs((_root.Left?.Height ?? -1) - (_root.Right?.Height ?? -1)) : 0;
	public object?  Root => _root is not null ? _root.Data : null;
	
	public BinarySearchTree()
	{
		Size = 0;
		Height = -1;
		//RootBalanceFactor = 0;
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
		/*var newHeight = Math.Max(_root.Left?.Height ?? -1, _root.Right?.Height ?? -1) + 1;
		if (newHeight > Height)
			Height = newHeight;*/
		Height=_root.Height;
		//RootBalanceFactor = Math.Abs(_root.Left?.Height ?? -1 - _root.Right?.Height ?? -1);
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
		{
			Size--;
			/*ar newHeight = Math.Max(_root.Left?.Height ?? -1, _root.Right?.Height ?? -1) + 1;

			if (newHeight > Height)
				Height = newHeight;*/
			Height=_root.Height;
		}

		if (Root is null)
		{
			Height = -1;
		}

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
		return IsBinarySearchTree(_root, default, default);
	}

	public bool Clear()
	{
		_root = null;
		Size = 0;
		Height = -1;
		//RootBalanceFactor = 0;
		return true;
	}
	
	private bool IsBinarySearchTree(INode<T>? node, T? minValue, T? maxValue)
	{
		if (node == null)
			return true;

		// Перевіряємо, чи вузол задовольняє умову BST
		if ((minValue != null && Comparer<T>.Default.Compare(node.Data, minValue) <= 0) ||
			(maxValue != null && Comparer<T>.Default.Compare(node.Data, maxValue) >= 0))
		{
			return false;
		}

		// Рекурсивно перевіряємо ліве та праве піддерева
		return IsBinarySearchTree(node.Left, minValue, node.Data) &&
			IsBinarySearchTree(node.Right, node.Data, maxValue);
	}
}