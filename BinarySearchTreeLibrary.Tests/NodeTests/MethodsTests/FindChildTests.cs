using BinarySearchTreeLibrary.Models;
using FluentAssertions;

namespace BinarySearchTreeLibrary.Tests.NodeTests.MethodsTests;

public class FindChildTests
{
	private readonly Node<int?> _rootNode;

	public FindChildTests()
	{
		_rootNode = new Node<int?>(10);
		_rootNode.Insert(5);
		_rootNode.Insert(15);
		_rootNode.Insert(7);
		_rootNode.Insert(12);
	}

	[Theory(DisplayName = "FindChild method should find existing data")]
	[InlineData(5)]
	[InlineData(15)]
	[InlineData(7)]
	[InlineData(12)]
	public void FindChild_ExistingData_ShouldFind(int data)
	{
		// Act
		var foundNode = _rootNode.FindChild(data.GetHashCode());

		// Assert
		foundNode.Should().NotBeNull();
		foundNode.Data.Should().Be(data);
		foundNode.Key.Should().Be(data.GetHashCode());
	}

	[Fact(DisplayName = "FindChild method should return itself if data not found")]
	public void FindChild_NonExistingData_ShouldReturnSelf()
	{
		// Arrange
		const int nonExistingData = 100;

		// Act
		var foundNode = _rootNode.FindChild(nonExistingData.GetHashCode());

		// Assert
		foundNode.Should().BeNull();

		//foundNode.Data.Should().Be(_rootNode.Data);
	}

	/*[Fact(DisplayName = "FindChild method should throw ArgumentNullException when searching for null data")]
	public void FindChild_NullData_ShouldThrowArgumentNullException()
	{
		// Act & Assert
		Assert.Throws<ArgumentNullException>(() => _rootNode.FindChild(null));
	}*/
}