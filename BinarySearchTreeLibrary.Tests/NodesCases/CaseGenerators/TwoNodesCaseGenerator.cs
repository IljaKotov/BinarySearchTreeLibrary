using BinarySearchTreeLibrary.Tests.NodesCases.FakeClass;
using Bogus;

namespace BinarySearchTreeLibrary.Tests.NodesCases.CaseGenerators;

public static class TwoNodesCaseGenerator
{
	private static readonly Faker _faker = new();
	private static readonly HashSet<int> _existingHashes = new();

	public static IEnumerable<object[]> GetTwoNodesCases()
	{
		yield return new object[] {GetIntTestCase()};
		yield return new object[] {GetStringTestCase()};
		yield return new object[] {GetCustomClassTestCase()};
	}

	private static NodeCase GetIntTestCase()
	{
		var inputs = new int[2];

		for (var i = 0; i < 2; i++)
			inputs[i] = _faker.Random.Unique(() => _faker.Random.Int(), x => x, _existingHashes);

		return NodeCaseFactory.Create(inputs.Cast<object>().ToArray());
	}

	private static NodeCase GetStringTestCase()
	{
		_existingHashes.Clear();
		var inputs = new string[2];

		for (var i = 0; i < 2; i++)
			inputs[i] = _faker.Random.Unique(() => _faker.Random.String(), x => x.GetHashCode(), _existingHashes);

		return NodeCaseFactory.Create(inputs.Cast<object>().ToArray());
	}

	private static NodeCase GetCustomClassTestCase()
	{
		_existingHashes.Clear();
		var factory = new FakeClassFactory();
		var inputs = new object[2];

		for (var i = 0; i < 2; i++)
		{
			inputs[i] = _faker.Random.Unique(() => factory.Create(_faker.Random.Int()), x => x.GetHashCode(),
				_existingHashes);
		}

		return NodeCaseFactory.Create(inputs.ToArray());
	}
}