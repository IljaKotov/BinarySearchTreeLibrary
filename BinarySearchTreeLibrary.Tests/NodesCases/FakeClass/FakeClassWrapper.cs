namespace BinarySearchTreeLibrary.Tests.NodesCases.FakeClass;

public class FakeClassWrapper(int hashCode)
{
	private int HashCode { get; } = hashCode;

	public override int GetHashCode()
	{
		return HashCode;
	}
}