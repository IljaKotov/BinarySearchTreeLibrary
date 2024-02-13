using Bogus;

namespace BinarySearchTreeLibrary.Tests.NodesCases;

public static class NodesFourLevelTreeCaseGenerator
{
	public static IEnumerable<object[]> GetNodesFourLevelTreeCases()
	{
		yield return new[] {GetNodesFourLevelTreeIntTestCase()};

		yield return new[] {GetNodesFourLevelTreeStringTestCase()};
	}

	private static object GetNodesFourLevelTreeIntTestCase()
	{
		var inputs = new object[]
		{
			50, 25, 35, 75, 85, 65, 80, 15, 10, 20, 30, 90, 70
		};

		return new NodeCase
		{
			InputData = inputs
		};
	}

	private static object GetNodesFourLevelTreeStringTestCase()
	{
		var faker = new Faker();

		var inputs = new object[13];

		inputs[0] = faker.Random.String();
		inputs[1] = GetRandomString(int.MinValue + 3000, inputs[0].GetHashCode() - 1);
		inputs[2] = GetRandomString(inputs[1].GetHashCode() + 1, inputs[0].GetHashCode() - 1);
		inputs[3] = GetRandomString(inputs[0].GetHashCode() + 1, int.MaxValue - 3000);
		inputs[4] = GetRandomString(inputs[3].GetHashCode() + 1, int.MaxValue - 2000);
		inputs[5] = GetRandomString(inputs[0].GetHashCode() + 1, inputs[3].GetHashCode() - 1);
		inputs[6] = GetRandomString(inputs[3].GetHashCode() + 1, inputs[4].GetHashCode() - 1);
		inputs[7] = GetRandomString(int.MinValue + 2000, inputs[1].GetHashCode() - 1);
		inputs[8] = GetRandomString(int.MinValue + 1000, inputs[7].GetHashCode() - 1);
		inputs[9] = GetRandomString(inputs[7].GetHashCode() + 1, inputs[1].GetHashCode() - 1);
		inputs[10] = GetRandomString(inputs[1].GetHashCode() + 1, inputs[2].GetHashCode() - 1);
		inputs[11] = GetRandomString(inputs[4].GetHashCode() + 1, int.MaxValue - 1000);
		inputs[12] = GetRandomString(inputs[5].GetHashCode() + 1, inputs[3].GetHashCode() - 1);

		return new NodeCase
		{
			InputData = inputs
		};
	}

	private static string GetRandomString(int minHash = int.MinValue, int maxHash = int.MaxValue)
	{
		var faker = new Faker();
		var randomString = faker.Random.String();

		if (randomString.GetHashCode() < minHash || randomString.GetHashCode() > maxHash)
		{
			return GetRandomString(minHash, maxHash);
		}

		return randomString;
	}
}