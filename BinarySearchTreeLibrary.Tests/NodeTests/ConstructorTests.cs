using BinarySearchTreeLibrary.Interfaces;
using BinarySearchTreeLibrary.Models;
using BinarySearchTreeLibrary.Tests.AssertUtils;
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
		var testData = TestNodeFactory.CreateNode(data);

		NodeAsserts.AssertNode(testData,
			expData: data,
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
		var testNode = TestNodeFactory.CreateNode(randomString);

		NodeAsserts.AssertNode(testNode,
			expData: randomString,
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
			NodeAsserts.AssertNode(node.Left, null);
			NodeAsserts.AssertNode(node.Right, null);
	}
}