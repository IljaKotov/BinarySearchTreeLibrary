namespace BinarySearchTreeLibrary.Models;

/// <summary>
///     Represents the direction of a node in a binary search tree.
/// </summary>
public enum Direction
{
	/// <summary>
	///     Represents the left direction, used when the new node's key is less than the current node's key.
	/// </summary>
	Left,

	/// <summary>
	///     Represents the same direction, used when the new node's key is equal to the current node's key.
	/// </summary>
	Same,

	/// <summary>
	///     Represents the right direction, used when the new node's key is greater than the current node's key.
	/// </summary>
	Right
}