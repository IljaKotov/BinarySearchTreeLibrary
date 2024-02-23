namespace BinarySearchTreeLibrary.Exceptions;

/// <summary>
///     Custom exception class for handling node not found during remove scenarios in a binary search tree.
/// </summary>
public class NodeNotFoundException : Exception
{
	/// <summary>
	///     Private constructor for the NodeNotFoundException class.
	/// </summary>
	/// <param name="message">The message that describes the error.</param>
	private NodeNotFoundException(string message) : base(message)
	{
	}

	/// <summary>
	///     Throws a NodeNotFoundException if the provided data is null.
	/// </summary>
	/// <typeparam name="T">The type of data stored in the node.</typeparam>
	/// <param name="data">The data to check.</param>
	public static void ThrowIfNodeNotFound<T>(T? data)
	{
		if (data is null)
		{
			throw new NodeNotFoundException("The node was not found in the tree.");
		}
	}
}