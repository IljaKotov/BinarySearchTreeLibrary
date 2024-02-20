using BinarySearchTreeLibrary.Models;
using Bogus;
using FluentAssertions;

namespace BinarySearchTreeLibrary.Tests.NodeTests;

public static class ConstructorTests
{

	[Theory(DisplayName = "Node Data and Key should be set to valid value")]
	[InlineData(new[] {1, 2, 3}, typeof(int[]))]
	[InlineData(new[] {'a', 'b', 'c', 'd', 'e'}, typeof(char[]))]
	[InlineData(new[] {1.1, 2.2, 3.3}, typeof(double[]))]
	public static void Should_DataAndKey_CorrectlySetArraysValue(object data, Type typeData)
	{
		var node = new Node<object>(data);

		node.Data.Should().BeOfType(typeData);
		node.Data.Should().Be(data);
		node.Key.Should().Be(data.GetHashCode());
	}

	[Theory(DisplayName = "Node Data should be set to valid string value")]
	[InlineData("en")]
	[InlineData("uk")]
	[InlineData("fr")]
	public static void Should_Data_CorrectlySetRandomString(string locale)
	{
		var faker = new Faker(locale);
		var randomString = faker.Random.String();

		var node = new Node<string>(randomString);

		node.Data.Should().BeOfType<string>();
		node.Data.Should().Be(randomString);
		node.Key.Should().Be(randomString.GetHashCode());
	}

	/*[Fact(DisplayName = "Node Key should set valid value for custom class")]
	public static void Node_Key_ShouldSetValidValueForCustomClass()
	{
		var fakerClass = new FakeClass(2);
		var node = new Node<FakeClass>(fakerClass);

		node.Key.Should().Be(fakerClass.GetHashCode());
		node.Data.Should().Be(fakerClass);
	}*/
}