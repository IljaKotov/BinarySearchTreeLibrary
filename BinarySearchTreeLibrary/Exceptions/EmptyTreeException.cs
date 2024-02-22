using BinarySearchTreeLibrary.Interfaces;

namespace BinarySearchTreeLibrary.Exceptions;

public class EmptyTreeException : Exception
{
	private EmptyTreeException(string message) : base(message)
	{
	}

	public static void ThrowIfEmptyTree<T>(IBinarySearchTree<T> tree)
	{
		if (tree.RootData is null)
		{
			throw new EmptyTreeException("The tree is empty.");
		}
	}
}