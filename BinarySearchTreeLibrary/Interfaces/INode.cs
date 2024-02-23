using BinarySearchTreeLibrary.Exceptions;

namespace BinarySearchTreeLibrary.Interfaces;

/// <summary>
///     Interface for a node in a binary search tree.
///     A node in a binary search tree holds data, and references to its left child, right child, and parent.
///     The node also contains properties to determine its height, balance factor, and whether it is a leaf or has both
///     children.
/// </summary>
/// <typeparam name="T">The type of data stored in the node.</typeparam>
internal interface INode<T>
{
 /// <summary>
 ///     Returns or sets the data stored in the node.
 /// </summary>
 T Data { get; set; }

 /// <summary>
 ///     Returns the key of the node.
 ///     The key is a unique identifier (hash-code) for the node and is used to organize the binary search tree.
 /// </summary>
 int Key { get; }

 /// <summary>
 ///     Returns or sets the left child of the node.
 ///     The left child is a node that has a key less than the current node.
 /// </summary>
 INode<T>? Left { get; set; }

 /// <summary>
 ///     Returns or sets the right child of the node.
 ///     The right child is a node that has a key greater than the current node.
 /// </summary>
 INode<T>? Right { get; set; }

 /// <summary>
 ///     Returns or sets the parent of the node.
 ///     The parent is the node from which the current node descends.
 /// </summary>
 INode<T>? Parent { get; set; }

 /// <summary>
 ///     Returns the height of the node.
 ///     The height of a node is the number of edges on the longest path from the node to a leaf.
 /// </summary>
 int Height { get; }

 /// <summary>
 ///     Returns a value indicating whether the node is balanced.
 ///     A node is balanced if the heights of its two child subtrees differ by no more than one and all its inheritors are
 ///     balanced.
 /// </summary>
 bool IsBalanced { get; }

 /// <summary>
 ///     Returns the balance factor of the node.
 ///     The balance factor of a node is the height of its left subtree minus the height of its right subtree.
 /// </summary>
 int BalanceFactor { get; }

 /// <summary>
 ///     Returns a value indicating whether the node is a leaf.
 ///     A leaf is a node that has no children.
 /// </summary>
 bool IsLeaf { get; }

 /// <summary>
 ///     Returns a value indicating whether the node has both children.
 ///     A node has both children if it has both a left child and a right child.
 /// </summary>
 bool HasBothChildren { get; }

 /// <summary>
 ///     Inserts data into the binary search tree.
 ///     The data is inserted as a new node at the appropriate location in the binary search tree.
 /// </summary>
 /// <param name="data">The data to insert.</param>
 void Insert(T data);

 /// <summary>
 ///     Gets the node by key.
 ///     This method returns the node with the specified key if it exists; otherwise, it returns null.
 /// </summary>
 /// <param name="key">The key of the node.</param>
 /// <returns>The node with the specified key, or null if the node does not exist.</returns>
 INode<T>? GetNodeByKey(int key);

 /// <summary>
 ///     Removes the node with the specified key.
 ///     If the node is successfully removed, this method returns the update root-node without removed node; otherwise, it
 ///     throws a
 ///     NodeNotFoundException.
 /// </summary>
 /// <param name="key">The key of the node to remove.</param>
 /// <returns>The removed node.</returns>
 /// <exception cref="NodeNotFoundException">Thrown when the node to be removed does not exist in the binary search tree.</exception>
 INode<T> Remove(int key);

 /// <summary>
 ///     Balances the node.
 ///     This method uses a specific balancing algorithm (like AVL) to ensure that the node and its
 ///     subtrees remain balanced.
 ///     The balancing operation is performed in-place, meaning that it modifies the existing tree structure rather than
 ///     creating a new one.
 /// </summary>
 /// <returns>The balanced node.</returns>
 INode<T> Balance();

 /// <summary>
 ///     Updates the height of the node.
 ///     The height of a node is the number of edges on the longest path from the node to a leaf.
 /// </summary>
 void UpdateHeight();

 /// <summary>
 ///     Updates the balancing data of the node.
 ///     The balancing data includes the height and balance factor of the node.
 /// </summary>
 void UpdateBalancingData();

 /// <summary>
 ///     Handles the event when the root is deleted from single node tree.
 ///     This method is called when the root of the binary search tree which consist of one node is deleted.
 /// </summary>
 void OnRootDeleted();

 /// <summary>
 ///     Occurs when the root is deleted.
 ///     This event is raised when the root of the binary search tree which consist of one node is deleted.
 /// </summary>
 event Action RootDeleted;
}