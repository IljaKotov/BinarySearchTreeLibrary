using BinarySearchTreeLibrary.Exceptions;

namespace BinarySearchTreeLibrary.Interfaces;

/// <summary>
///     Interface for a binary search tree.
///     A binary search tree is a tree data structure in which each node has at most two children, referred to as the left
///     child and the right child.
///     For each node, all elements in the left subtree are less than the node, and all elements in the right subtree are
///     greater.
/// </summary>
/// <typeparam name="T">The type of data stored in the binary search tree.</typeparam>
public interface IBinarySearchTree<in T>
{
	/// <summary>
	///     Returns the number of elements in the binary search tree.
	///     Size is the total number of nodes (including the root) present in the tree.
	/// </summary>
	public int Size { get; }

	/// <summary>
	///     Returns the height of the binary search tree.
	///     The height of a tree is the maximum number of edges in a path from the root node to a leaf node.
	///     A leaf node is a node with no children.
	///     A tree with a single node (the root) has a height of 0..
	///     The height of an empty tree is -1.
	/// </summary>
	public int Height { get; }

	/// <summary>
	///     Gets the balance factor of the root of the binary search tree.
	///     The balance factor of a node in a binary tree is the height of its left subtree minus the height of its right
	///     subtree.
	/// </summary>
	public int RootBalanceFactor { get; }

	/// <summary>
	///     Adds an element to the binary search tree.
	///     The new element is always inserted as leaf.
	/// </summary>
	/// <param name="data">The data to add to the binary search tree.</param>
	/// <exception cref="ArgumentNullException">Thrown when the data is null.</exception>
	/// <exception cref="DuplicateKeyException">Thrown when the data already exists in the binary search tree.</exception>
	void Add(T data);

	/// <summary>
	///     Determines whether the binary search tree contains a specific value.
	///     This method returns true if the specified data is found in the tree; otherwise, false.
	/// </summary>
	/// <param name="data">The data to locate in the binary search tree.</param>
	/// <returns>true if the binary search tree contains an element with the specified value; otherwise, false.</returns>
	/// <exception cref="ArgumentNullException">Thrown when the data is null.</exception>
	/// <exception cref="EmptyTreeException">Thrown when the binary search tree is empty before.</exception>
	bool Contains(T data);

	/// <summary>
	///     Tries to remove the first occurrence of a specific value from the binary search tree.
	///     If the element is successfully removed, this method returns true; otherwise, it throws a NodeNotFoundException.
	///     This method no longer returns false if the element was not found in the binary search tree.
	///     The removal process depends on the number of children the node to be removed has:
	///     - If the node is a leaf (has no children), it is simply removed.
	///     - If the node has one child, the child replaces the node.
	///     - If the node has two children, the in-order successor of the node (the node with the smallest value that is larger
	///     than the node to be removed) replaces the node.
	/// </summary>
	/// <param name="data">The data to remove from the binary search tree.</param>
	/// <returns>true if the element is successfully removed; otherwise, it throws a NodeNotFoundException.</returns>
	/// <exception cref="NodeNotFoundException">Thrown when the node to be removed does not exist in the binary search tree.</exception>
	/// <exception cref="ArgumentNullException">Thrown when the data is null.</exception>
	/// <exception cref="EmptyTreeException">Thrown when the binary search tree is empty before.</exception>
	bool TryDelete(T data);

	/// <summary>
	///     Balances the binary search tree.
	///     This method uses a AVL balancing algorithm to ensure that the tree remains balanced after insertions or deletions.
	///     A balanced binary search tree has the minimum possible maximum height a tree with the same number of nodes could
	///     have.
	///     This is ideal because it minimizes the tree's height, and thus the time complexity of operations that depend on the
	///     tree's height.
	///     The balancing operation is performed in-place, meaning that it modifies the existing tree structure rather than
	///     creating a new one.
	/// </summary>
	/// <exception cref="EmptyTreeException">Thrown when the binary search tree is empty before.</exception>
	void Balance();

	/// <summary>
	///     Determines whether the binary search tree is balanced.
	///     A binary tree is balanced if the height of the two subtrees of any node never differ by more than one and all its
	///     inheritors are balanced, or when the
	///     tree is empty.
	/// </summary>
	/// <returns>true if the binary search tree is balanced; otherwise, false.</returns>
	bool IsBalanced();

	/// <summary>
	///     Determines whether the binary search tree is a valid binary search tree.
	///     A valid binary search tree is a binary tree in which for every node, X, every key in the left subtree is less than
	///     X, and every key in the right subtree is greater than X.
	/// </summary>
	/// <returns>true if the binary search tree is a valid binary search tree; otherwise, false.</returns>
	bool IsBinarySearchTree();
}