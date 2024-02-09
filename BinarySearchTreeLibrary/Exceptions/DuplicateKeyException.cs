namespace BinarySearchTreeLibrary.Exceptions;

public class DuplicateKeyException: Exception
{
	private DuplicateKeyException(string message) : base(message)
	{
	}
	
	public static void ThrowIfEqual(int currentKey, int newKey)
	{
		if (currentKey == newKey)
		{
			throw new DuplicateKeyException("The key already exists in the child nodes.");
		}
	}
}