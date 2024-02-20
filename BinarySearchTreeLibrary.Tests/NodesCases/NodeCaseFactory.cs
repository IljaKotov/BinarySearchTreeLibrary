namespace BinarySearchTreeLibrary.Tests.NodesCases;

public static class NodeCaseFactory
{
	public static NodeCase Create(params object[] inputs)
	{
		return new NodeCase
		{
			InputData = inputs
		};
	}
}