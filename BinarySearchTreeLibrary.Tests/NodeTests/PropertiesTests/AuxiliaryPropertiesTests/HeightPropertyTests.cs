using BinarySearchTreeLibrary.Models;
using Bogus;
using FluentAssertions;

namespace BinarySearchTreeLibrary.Tests.NodeTests.PropertiesTests.AuxiliaryPropertiesTests;

public class HeightPropertyTests
{
	[Fact]
	public void Node_Height_ShouldReturnZeroForLeafNode()
	{
		// Arrange
		var leafNode = new Node<int>(10);

		// Assert
		leafNode.Height.Should().Be(0);
	}
	

	[Fact]
	public void Node_Height_ShouldReturnCorrectHeightForBalancedTree()
	{
		// Arrange
		var rootNode = new Node<int>(10);
		rootNode.Insert(5);
		rootNode.Insert(15);

		// Assert
		rootNode.Height.Should().Be(1);
		rootNode.Left.Height.Should().Be(0);
		rootNode.Right.Height.Should().Be(0);
	}

	[Fact]
	public void Node_Height_ShouldReturnCorrectHeightForUnbalancedTree()
	{
		// Arrange
		var rootNode = new Node<int>(10);
		rootNode.Insert(5);
		rootNode.Insert(2);
		rootNode.Insert(15);
		rootNode.Insert(20);
		rootNode.Insert(25);
		// Assert
		rootNode.Height.Should().Be(3);
		rootNode.Left.Height.Should().Be(1);
		rootNode.Left.Left.Height.Should().Be(0);
		rootNode.Right.Height.Should().Be(2);
		rootNode.Right.Right.Height.Should().Be(1);
		rootNode.Right.Right.Right.Height.Should().Be(0);
		
	}

	[Fact]
	public void Node_Height_ShouldReturnCorrectHeightForRandomData()
	{
		// Arrange
		var faker = new Faker();
		var count = faker.Random.Int(1, 100);
		var randomArray = new int[count];
		
		var start = faker.Random.Int(1, 100);
		var step = faker.Random.Int(1, 100);
		for (int i = 0; i < count; i++)
		{
			randomArray[i] = faker.Random.Int(min: start + step * i, max:start+step* (i+1));
		}
		var rootNode = new Node<int>(randomArray[0]);
		
		for (int i = 1; i < randomArray.Length; i++)
		{
			rootNode.Insert(randomArray[i]);
		}
		
		rootNode.Height.Should().Be(randomArray.Length-1);
	}
}