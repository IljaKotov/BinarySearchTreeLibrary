using BinarySearchTreeLibrary.Interfaces;
using BinarySearchTreeLibrary.Models;
using BinarySearchTreeLibrary.Tests.NodesCases.FakeClass;
using Bogus;

namespace BinarySearchTreeLibrary.Tests.NodeTests;

public static class ConstructorTests
{
	[Theory(DisplayName = "Node Data and Key should be set to valid value")]
	[InlineData(new[] {1, 2, 3}, typeof(int[]))]
	[InlineData(new[] {'a', 'b', 'c', 'd', 'e'}, typeof(char[]))]
	[InlineData(new[] {1.1, 2.2, 3.3}, typeof(double[]))]
	public static void Should_CorrectlyCreateNode_WithArray(object data, Type typeData)
	{
		var testData = TestDataFactory.CreateNode(data);

		NodeAsserts.AssertNode(testData,
			expectedData: data,
			0,
			true,
			null);

		AssertNullChild(testData);
	}

	[Theory(DisplayName = "Should correctly set properties' values for single node with random string")]
	[InlineData("en")]
	[InlineData("uk")]
	[InlineData("fr")]
	public static void Should_CorrectlyCreateNode_WithRandomLocaleString(string locale)
	{
		var faker = new Faker(locale);
		var randomString = faker.Random.String();

		var testNode = TestDataFactory.CreateNode(randomString);

		NodeAsserts.AssertNode(testNode,
			expectedData: randomString,
			0,
			true,
			null);

		AssertNullChild(testNode);
	}

	[Fact(DisplayName = "Should correctly set properties' values for single node with custom class")]
	public static void Should_CorrectlyCreateNode_WithCustomClass()
	{
		var factory = new FakeClassFactory();
		var fakeClass = factory.Create(Randomizer.Seed.Next());

		var testNode = TestDataFactory.CreateNode(fakeClass);

		NodeAsserts.AssertNode(testNode,
			expectedData: fakeClass,
			0,
			true,
			null);

		AssertNullChild(testNode);
	}

	private static void AssertNullChild<T>(INode<T> node, NullNode<T>? expectedChild = null)
	{
		expectedChild ??= new NullNode<T>();

		if (node.Left is not null && node.Right is not null)
		{
			NodeAsserts.AssertNode(node.Left, expectedChild);
			NodeAsserts.AssertNode(node.Right, expectedChild);
		}

		
	}
}