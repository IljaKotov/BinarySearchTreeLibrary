using BinarySearchTreeLibrary.Models;
using FluentAssertions;

namespace BinarySearchTreeLibrary.Tests.NodeTests.PropertiesTests.GeneralPropertiesTests;

public class PropertyKeyTests
{
	[Theory]
	[InlineData(10)]
	[InlineData(0)]
	[InlineData(-10)]
	public void Node_Key_ShouldSetValidValueForInt(int data)
	{
		// Arrange
		var node = new Node<int>(data);

		// Assert
		node.Key.Should().Be(data.GetHashCode());
	}

	[Theory]
	[InlineData("some string")]
	[InlineData("another string")]
	public void Node_Key_ShouldSetValidValueForString(string data)
	{
		// Arrange
		var node = new Node<string>(data);

		// Assert
		node.Key.Should().Be(data.GetHashCode());
	}

	[Fact]
	public void Node_Key_ShouldSetValidValueForCustomClass()
	{
		// Arrange
		var customClass = new CustomClass { Value = 10 };
		var node = new Node<CustomClass>(customClass);

		// Assert
		node.Key.Should().Be(customClass.GetHashCode());
	}

	// Custom class for testing purposes
	private class CustomClass
	{
		public int Value { get; set; }
	}
}