using Bogus;

namespace BinarySearchTreeLibrary.Tests.NodesCases;

public static class SingleNodeCaseGenerator
{
	public static IEnumerable<object[]> GetSingleNodeCases()
	{
		yield return new object[] {GetSingleIntNodeTestCase()};
		yield return new object[] {GetSingleStringNodeTestCase()};
		yield return new object[] {GetSingleCustomClassNodeTestCase()};
	}
	
	private static NodeCase GetSingleIntNodeTestCase()
	{
		return new NodeCase
		{
			InputData = new object[]
			{
				new Faker().Random.Int(-100, 100)
			}
		};
	}
	
	private static NodeCase GetSingleStringNodeTestCase()
	{
		return new NodeCase
		{
			InputData = new object[]
			{
				new Faker().Random.String()
			}
		};
	}

	private static NodeCase GetSingleCustomClassNodeTestCase()
	{
		return new NodeCase
		{
			InputData = new object[]
			{
				new FakerClass()
			}
		};
	}
}
