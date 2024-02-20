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
		fakeClass.FakeString.Returns(_faker.Random.String2(20));
		fakeClass.FakeArray.Returns(_faker.Random.Chars('a', 'z', 10).ToArray());

		return new FakeClassWrapper(fakeClass, hashCode);
	}
}