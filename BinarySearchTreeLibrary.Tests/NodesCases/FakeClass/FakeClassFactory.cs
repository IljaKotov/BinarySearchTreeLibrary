using Bogus;
using NSubstitute;

namespace BinarySearchTreeLibrary.Tests.NodesCases.FakeClass;

public class FakeClassFactory
{
	private readonly Faker _faker = new();

	public FakeClassWrapper Create(int hashCode)
	{
		var fakeClass = Substitute.For<IFakeClass>();
		fakeClass.FakeInt.Returns(_faker.Random.Int());
		fakeClass.FakeString.Returns(_faker.Lorem.Sentence());
		fakeClass.FakeArray.Returns(_faker.Lorem.Word().ToArray());

		return new FakeClassWrapper(hashCode);
	}
}