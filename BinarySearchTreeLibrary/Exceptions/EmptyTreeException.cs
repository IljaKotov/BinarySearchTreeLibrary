namespace BinarySearchTreeLibrary.Exceptions;

/// <summary>
///     Represents an exception that is thrown when an operation is attempted on an empty binary search tree.
/// </summary>
public class EmptyTreeException : Exception
{
	/// <summary>
	///     Initializes a new instance of the <see cref="EmptyTreeException" /> class with a specified error message.
	/// </summary>
	/// <param name="message">The message that describes the error.</param>
	private EmptyTreeException(string message) : base(message)
	{
	}

	/// <summary>
	///     Throws an <see cref="EmptyTreeException" /> if the specified tree is empty.
	/// </summary>
	/// <param name="data"></param>
	public static void ThrowIfEmptyTree<T>(T? data)
	{
		if (data is null)
		{
			throw new EmptyTreeException("The tree is empty.");
		}
	}
}