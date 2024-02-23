using BinarySearchTreeLibrary.Interfaces;
using BinarySearchTreeLibrary.Tests.AssertUtils;
using BinarySearchTreeLibrary.Tests.NodesCases.FakeClass;
using Bogus;
using FluentAssertions;

namespace BinarySearchTreeLibrary.Tests.NodeTests;

public static class ConstructorTests
{
	[Theory(DisplayName = "Correctly create node with some array")]
	[InlineData(new[]
	{1, 2, 3})]
	[InlineData(new[]
	{'a', 'b', 'c', 'd', 'e'})]
	[InlineData(new[]
	{1.1, 2.2, 3.3})]
	public static void Constructor_Array_CorrectlyCreateNode(object data)
	{
		var testData = TestNodeFactory.CreateNode(data);

		NodeAsserts.AssertNode(testData,
			expData:data,
			0,
			true,
			null);

		AssertNullChild(testData);
	}

	[Theory(DisplayName = "Correctly create node with random string")]
	[InlineData("en")]
	[InlineData("uk")]
	[InlineData("fr")]
	public static void Constructor_RandomLocaleString_CorrectlyCreateNode(string locale)
	{
		var faker = new Faker(locale);
		var randomString = faker.Random.String();
		var testNode = TestNodeFactory.CreateNode(randomString);

		NodeAsserts.AssertNode(testNode,
			expData: randomString,
			0,
			true,
			null);

		AssertNullChild(testNode);
	}

	[Fact(DisplayName = "Correctly create node with custom class")]
	public static void Constructor_CustomClass_CorrectlyCreateNode()
	{
		var factory = new FakeClassFactory();
		var fakeClass = factory.Create(Randomizer.Seed.Next());
		var testNode = TestNodeFactory.CreateNode(fakeClass);

		NodeAsserts.AssertNode(testNode,
			expData: fakeClass,
			0,
			true,
			null);

		AssertNullChild(testNode);
	}

	private static void AssertNullChild<T>(INode<T> node)
	{
		node.Left.Should().BeNull();
		node.Right.Should().BeNull();
	}
}