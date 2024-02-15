using BinarySearchTreeLibrary.Interfaces;

namespace BinarySearchTreeLibrary.Exceptions;

public class EmptyTreeException: Exception
{
	internal EmptyTreeException(string message) : base(message)
	{
	}
	
	public static void ThrowIfEmptyTree(IBinarySearchTree<object> tree)
	{
		if (tree.Root is null)
		{
			throw new EmptyTreeException("The tree is empty.");
		}
	}
}