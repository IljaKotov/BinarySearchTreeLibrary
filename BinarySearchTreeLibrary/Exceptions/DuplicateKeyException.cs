using BinarySearchTreeLibrary.Models;

namespace BinarySearchTreeLibrary.Exceptions;

/// <summary>
///     Represents an exception that is thrown when a duplicate key is detected in the binary search tree.
/// </summary>
public class DuplicateKeyException : Exception
{
	/// <summary>
	///     Initializes a new instance of the <see cref="DuplicateKeyException" /> class with a specified error message.
	/// </summary>
	/// <param name="message">The message that describes the error.</param>
	private DuplicateKeyException(string message) : base(message)
	{
	}

	/// <summary>
	///     Throws a <see cref="DuplicateKeyException" /> if the specified direction is <see cref="Direction.Same" />.
	/// </summary>
	/// <param name="direction">The direction to check.</param>
	public static void ThrowIfSameKeys(Direction direction)
	{
		if (direction is Direction.Same)
		{
			throw new DuplicateKeyException("The key already exists in the tree.");
		}
	}
}