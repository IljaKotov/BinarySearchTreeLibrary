using BinarySearchTreeLibrary.Models;

namespace BinarySearchTreeLibrary.Exceptions;

public class DuplicateKeyException : Exception
{
	private DuplicateKeyException(string message) : base(message)
	{
	}

	public static void ThrowIfSameKeys(Direction direction)
	{
		if (direction is Direction.Same)
		{
			throw new DuplicateKeyException("The key already exists in the tree.");
		}
	}
}