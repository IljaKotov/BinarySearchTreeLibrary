namespace BinarySearchTreeLibrary.Interfaces;

/// <summary>
///     Interface for a tree balancer in a binary search tree.
///     This interface defines a contract for balancing nodes in a binary search tree.
///     The balancing process ensures that the tree remains balanced after insertions or deletions, minimizing the tree's
///     height and thus the time complexity of operations that depend on the tree's height.
/// </summary>
/// <typeparam name="T">The type of data stored in the node.</typeparam>
internal interface ITreeBalancer<T>
{
	/// <summary>
	///     Balances a node in the binary search tree.
	///     This method uses a specific balancing algorithm (like AVL) to ensure that the node and its
	///     subtrees remain balanced.
	///     The balancing operation is performed in-place, meaning that it modifies the existing tree structure rather than
	///     creating a new one.
	/// </summary>
	/// <param name="balancingNode">The node to balance in the binary search tree.</param>
	/// <returns>The balanced node.</returns>
	INode<T> Balance(INode<T> balancingNode);
}