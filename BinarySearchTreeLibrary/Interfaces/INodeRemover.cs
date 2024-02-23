using BinarySearchTreeLibrary.Exceptions;

namespace BinarySearchTreeLibrary.Interfaces;

/// <summary>
///     Interface for a node remover in a binary search tree.
///     This interface defines a contract for removing nodes from a binary search tree.
/// </summary>
/// <typeparam name="T">The type of data stored in the node.</typeparam>
internal interface INodeRemover<T>
{
	/// <summary>
	///     Removes a node from the binary search tree.
	///     The removal process depends on the number of children the node to be removed has:
	///     - If the node is a leaf (has no children), it is simply removed.
	///     - If the node has one child, the child replaces the node.
	///     - If the node has two children, the in-order successor of the node (the node with the smallest value that is larger
	///     than the node to be removed) replaces the node.
	/// </summary>
	/// <param name="removalNode">The node to remove from the binary search tree.</param>
	/// <exception cref="ArgumentNullException">Thrown when the removalNode is null.</exception>
	/// <exception cref="NodeNotFoundException">Thrown when the removalNode does not exist in the binary search tree.</exception>
	void RemoveNode(INode<T> removalNode);
}