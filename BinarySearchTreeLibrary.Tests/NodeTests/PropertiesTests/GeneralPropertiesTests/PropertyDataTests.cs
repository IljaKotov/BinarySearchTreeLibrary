using BinarySearchTreeLibrary.Models;
using Bogus;
using FluentAssertions;

namespace BinarySearchTreeLibrary.Tests.NodeTests.PropertiesTests.GeneralPropertiesTests;

public class PropertyDataTests
{
	[Theory (DisplayName = "Node Data should throw exception when null data provided")]
	[InlineData(null)]
	public void Node_Data_ShouldThrowException_WhenNullDataProvided(string? nullData)
	{
		Assert.Throws<ArgumentNullException>(() => new Node<string?>(null));
	}
	
	[Theory (DisplayName = "Node Data should be set to valid value")]
	[InlineData(10)]
	[InlineData(0)]
	[InlineData(-10)]
	public void Node_Data_ShouldSetValidValue(int data)
	{
		
		var node = new Node<int>(data);
		
		node.Data.Should().Be(data);
	}

	[Theory (DisplayName = "Node Data should be set to valid string value")]
	[InlineData("some string")]
	[InlineData("another string")]
	public void Node_Data_ShouldAllowStringData(string data)
	{
		// Arrange & Act
		var node = new Node<string>(data);

		// Assert
		node.Data.Should().Be(data);
	}

	[Theory (DisplayName = "Node Data should be set to valid value")]
	[InlineData(new int[]{1,2,3}, typeof(int[]))]
	[InlineData(new char[] {'a', 'b', 'c', 'd', 'e'}, typeof(char[]))]
	[InlineData(new double[]{1.1, 2.2, 3.3}, typeof(double[]))]
	public void Node_Data_ShouldAllowRandomIntegerData(object data, Type typeData)
	{
		// Act
		var node = new Node<object>(data);

		// Assert
		node.Data.Should().BeOfType(typeData);
		node.Key.Should().Be(data.GetHashCode());
	}

	[Theory (DisplayName = "Node Data should be set to valid string value")]
	[InlineData("en")]
	[InlineData("uk")]
	[InlineData("fr")]
	public void Node_Data_ShouldAllowRandomStringData(string locale)
	{
		// Arrange
		var faker = new Faker(locale);
		var randomString = faker.Random.String();

		// Act
		var node = new Node<string>(randomString);

		// Assert
		node.Data.Should().BeOfType<string>();
	}
}