using Bogus;

namespace BinarySearchTreeLibrary.Tests.NodesCases;

public static class JustTwoNodesCaseGenerator
{
	public static IEnumerable<object[]> GetTwoNodesCases()
	{
		yield return new object[] {GetTwoIntNodeTestCase()};

		yield return new object[] {GetTwoStringNodeTestCase()};

		yield return new object[] {GetTwoCustomClassNodeTestCase()};
	}

	private static NodeCase GetTwoIntNodeTestCase()
	{
		var faker = new Faker();
		var inputs = new object[2];

		for (var i = 0; i < 2; i++)
			inputs[i] = faker.Random.Int(-100, 100);

		return new NodeCase
		{
			InputData = inputs
		};
	}

	private static NodeCase GetTwoStringNodeTestCase()
	{
		var faker = new Faker();
		var inputs = new object[2];

		for (var i = 0; i < 2; i++)
			inputs[i] = faker.Random.String();

		return new NodeCase
		{
			InputData = inputs
		};
	}

	private static NodeCase GetTwoCustomClassNodeTestCase()
	{
		var inputs = new object[2];

		for (var i = 0; i < 2; i++)
			inputs[i] = new FakerClass();

		return new NodeCase
		{
			InputData = inputs
		};
	}
}