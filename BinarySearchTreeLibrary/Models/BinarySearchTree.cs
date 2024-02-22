using BinarySearchTreeLibrary.Exceptions;
using BinarySearchTreeLibrary.Interfaces;

namespace BinarySearchTreeLibrary.Models;

/// <summary>
///     Represents a binary search tree.
/// </summary>
/// <typeparam name="T">The type of the data stored in the binary search tree.</typeparam>
public class BinarySearchTree<T> : IBinarySearchTree<T>
{
	private static readonly ITreeBalancer<T> _treeBalancer = new TreeBalancer<T>();
	private INode<T>? _root;

	/// <summary>
	///     Returns the data of the root node.
	/// </summary>
	public T? RootData => _root is not null ? _root.Data : default;

	/// <summary>
	///     Returns the size of the binary search tree.
	/// </summary>
	public int Size { get; private set; }

	/// <summary>
	///     Returns the height of the binary search tree.
	/// </summary>
	public int Height => _root?.Height ?? -1;

	/// <summary>
	///     Returns the balance factor of the root node.
	/// </summary>
	public int RootBalanceFactor => _root?.BalanceFactor ?? 0;

	/// <summary>
	///     Initializes a new instance of the <see cref="BinarySearchTree{T}" /> class.
	/// </summary>
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

	/// <summary>
	///     Adds a new node with the specified data to the binary search tree.
	///     If the tree is empty, it creates a new root node with the given data.
	///     Otherwise, it inserts the new node at the appropriate position in the tree.
	/// </summary>
	/// <param name="data">The data to add to the tree.</param>
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

	/// <summary>
	///     Checks if a node with the specified data exists in the binary search tree.
	///     Returns true if such a node is found, and false otherwise.
	/// </summary>
	/// <param name="data">The data to check for in the tree.</param>
	/// <returns>True if the data is found, false otherwise.</returns>
	public bool Contains(T data)
	{
		ArgumentNullException.ThrowIfNull(data);
		EmptyTreeException.ThrowIfEmptyTree(this);

		return _root?.GetNodeByKey(data.GetHashCode()) is not null;
	}

	/// <summary>
	///     Attempts to delete a node with the specified data from the binary search tree.
	///     If a node with the given data is found and successfully removed, it returns true.
	///     If no such node is found, it returns false.
	/// </summary>
	/// <param name="data">The data of the node to delete.</param>
	/// <returns>True if the node was found and deleted, false otherwise.</returns>
	public bool TryDelete(T data)
	{
		ArgumentNullException.ThrowIfNull(data);
		EmptyTreeException.ThrowIfEmptyTree(this);

		var removalNode = _root?.Remove(data.GetHashCode());

		var isRemoved = removalNode is not null;

		_ = isRemoved ? Size-- : Size;

		return isRemoved;
	}

	/// <summary>
	///     Balances the binary search tree.
	///     Ensures that the tree maintains its properties as a binary search tree after operations such as insertion or
	///     deletion.
	/// </summary>
	public void Balance()
	{
		EmptyTreeException.ThrowIfEmptyTree(this);

		if (_root is not null)
			_root = _treeBalancer.Balance(_root);
	}

	/// <summary>
	///     Checks if the binary search tree is balanced.
	///     A balanced binary search tree is one where the height of the left and right subtrees of any node differ by at most
	///     one.
	///     Returns true if the tree is balanced, and false otherwise.
	/// </summary>
	/// <returns>True if the tree is balanced, false otherwise.</returns>
	public bool IsBalanced()
	{
		return _root?.IsBalanced ?? true;
	}

	/// <summary>
	///     Checks if the binary search tree maintains its properties as a binary search tree.
	///     In a binary search tree, for any given node, all nodes in the left subtree have data less than the node's data,
	///     and all nodes in the right subtree have data greater than the node's data.
	///     Returns true if the tree maintains these properties, and false otherwise.
	/// </summary>
	/// <returns>True if the tree maintains the properties of a binary search tree, false otherwise.</returns>
	public bool IsBinarySearchTree()
	{
		EmptyTreeException.ThrowIfEmptyTree(this);

		return IsSubtreeValid(_root, int.MinValue, int.MaxValue);
	}

	private static bool IsSubtreeValid(INode<T>? node,
		int? min,
		int? max)
	{
		if (node is null)
			return true;

		if (node.Key <= min || node.Key > max)
			return false;

		return IsSubtreeValid(node.Left, min, node.Key) &&
			IsSubtreeValid(node.Right, node.Key, max);
	}
}