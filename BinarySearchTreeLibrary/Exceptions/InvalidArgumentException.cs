using BinarySearchTreeLibrary.Models;

namespace BinarySearchTreeLibrary.Exceptions;

/// <summary>
///     Custom exception class for handling invalid argument scenarios in a binary search tree.
/// </summary>
public class InvalidArgumentException : Exception
{
	/// <summary>
	///     Private constructor for the InvalidArgumentException class.
	/// </summary>
	/// <param name="message">The message that describes the error.</param>
	private InvalidArgumentException(string message) : base(message)
	{
	}

	/// <summary>
	///     Throws an InvalidArgumentException if the provided direction is same in Rotate method.
	/// </summary>
	/// <param name="direction">The direction to check.</param>
	public static void ThrowIfDirectionSame(Direction direction)
	{
		if (direction is Direction.Same)
		{
			throw new InvalidArgumentException("Cannot rotate in the same direction.");
		}
	}
}