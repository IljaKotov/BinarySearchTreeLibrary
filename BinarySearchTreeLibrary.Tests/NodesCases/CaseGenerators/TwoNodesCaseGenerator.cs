using BinarySearchTreeLibrary.Tests.NodesCases.FakeClass;
using Bogus;

namespace BinarySearchTreeLibrary.Tests.NodesCases.CaseGenerators;

public static class TwoNodesCaseGenerator
{
	private static readonly Faker _faker = new Faker();
	private static readonly HashSet<int> _existingHashes = new();
	public static IEnumerable<object[]> GetTwoNodesCases()
	{
		yield return new object[] { GetTwoIntNodeTestCase() };

		yield return new object[] { GetTwoStringNodeTestCase() };

		yield return new object[] { GetTwoCustomClassNodeTestCase() };
	}

	private static NodeCase GetTwoIntNodeTestCase()
	{
		var firstInt = _faker.Random.Unique(() => _faker.Random.Int(), x => x, _existingHashes);
		var secondInt = _faker.Random.Unique(() => _faker.Random.Int(), x => x, _existingHashes);
		
		return  NodeCaseFactory.Create(firstInt, secondInt) ;
	}

	private static NodeCase GetTwoStringNodeTestCase()
	{
		_existingHashes.Clear();
		
		var firstString = _faker.Random.Unique(() => _faker.Random.String(), x => x.GetHashCode(), _existingHashes);
		var secondString = _faker.Random.Unique(() => _faker.Random.String(), x => x.GetHashCode(), _existingHashes);
		
		return NodeCaseFactory.Create(firstString, secondString);
	}

	private static NodeCase GetTwoCustomClassNodeTestCase()
	{
		_existingHashes.Clear();
		var factory = new FakeClassFactory();

		var firstFakeClass = _faker.Random.Unique(() => factory.Create(_faker.Random.Int()), x => x.GetHashCode(), _existingHashes);
		var secondFakeClass = _faker.Random.Unique(() => factory.Create(_faker.Random.Int()), x => x.GetHashCode(), _existingHashes);

		return NodeCaseFactory.Create(firstFakeClass, secondFakeClass);
	}
}