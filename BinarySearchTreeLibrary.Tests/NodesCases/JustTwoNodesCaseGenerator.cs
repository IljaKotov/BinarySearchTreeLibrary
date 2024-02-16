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
		
		inputs[0] = faker.Random.Int();

		while (true)
		{
			inputs[1] = faker.Random.Int();
			if (inputs[0] != inputs[1])
				break;
		}

		return new NodeCase
		{
			InputData = inputs
		};
	}

	private static NodeCase GetTwoStringNodeTestCase()
	{
		var faker = new Faker();
		var inputs = new object[2];
		
		inputs[0] = faker.Random.String();

		while (true)
		{
			inputs[1] = faker.Random.String();
			if (inputs[0].GetHashCode() != inputs[1].GetHashCode())
				break;
		}

		return new NodeCase
		{
			InputData = inputs
		};
	}

	private static NodeCase GetTwoCustomClassNodeTestCase()
	{
		var inputs = new object[2];

		inputs[0] = new FakeClass();

		while (true)
		{
			inputs[1] = new FakeClass();
			if (inputs[0].GetHashCode() != inputs[1].GetHashCode())
				break;
		}

		return new NodeCase
		{
			InputData = inputs
		};
	}
}