namespace BinarySearchTreeLibrary.Interfaces;

/// <summary>
///     Interface for a binary search tree navigator (find depend nodes in depends scenarios).
///     This interface defines a contract for navigating through a binary search tree.
///     It provides methods for finding nodes with specific characteristics, such as the node with the minimum key or a
///     node with a specific key.
/// </summary>
/// <typeparam name="T">The type of data stored in the node.</typeparam>
internal interface ITreeNavigator<T>
{
	/// <summary>
	///     Finds the node with the minimum key starting from the specified node.
	/// </summary>
	/// <param name="node">The node to start the search from.</param>
	/// <returns>The node with the minimum key.</returns>
	INode<T> FindMinAt(INode<T> node);

	/// <summary>
	///     Finds a node by its key starting from the specified node.
	/// </summary>
	/// <param name="node">The node to start the search from.</param>
	/// <param name="key">The key of the node to find.</param>
	/// <returns>The node with the specified key if it exists; otherwise, null.</returns>
	INode<T>? FindByKey(INode<T>? node, int key);
}