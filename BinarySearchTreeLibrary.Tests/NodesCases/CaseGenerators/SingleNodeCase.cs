using BinarySearchTreeLibrary.Tests.NodesCases.FakeClass;
using Bogus;

namespace BinarySearchTreeLibrary.Tests.NodesCases.CaseGenerators;

public static class SingleNodeCase
{
	private static readonly Faker _faker = new();

	public static IEnumerable<object[]> GenerateCases()
	{
		yield return new[] {GetIntTestCase()};
		yield return new[] {GetStringTestCase()};
		yield return new[] {GetFakeClassTestCase()};
	}

	private static object GetIntTestCase()
	{
		return NodeCaseFactory.Create(_faker.Random.Int());
	}

	private static object GetStringTestCase()
	{
		return NodeCaseFactory.Create(_faker.Random.String());
	}

	private static object GetFakeClassTestCase()
	{
		var fakeClass = new FakeClassFactory().Create(_faker.Random.Int());

		return NodeCaseFactory.Create(fakeClass);
	}
}