using BinarySearchTreeLibrary.Interfaces;
using BinarySearchTreeLibrary.Models;

namespace BinarySearchTreeLibrary.Exceptions;

public class EmptyTreeException : Exception
{
	internal EmptyTreeException(string message) : base(message)
	{
	}

	public static void ThrowIfEmptyTree<T>(IBinarySearchTree<T> tree)
	{
		if (tree.RootData is null || tree.RootData is NullNode<T>)
		{
			throw new EmptyTreeException("The tree is empty.");
		}
	}
}