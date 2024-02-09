using BinarySearchTreeLibrary.Exceptions;
using BinarySearchTreeLibrary.Models;
using Bogus;
using FluentAssertions;

namespace BinarySearchTreeLibrary.Tests.NodeTests.MethodsTests;

public class InsertTests
{
    private Node<object?>? _rootNode;
    
    private void CreateRootNode(object? data)
    {
        _rootNode = new Node<object>(data);
    }

    [Theory(DisplayName = "Insert method should insert data correctly")]
    [InlineData(5)]
    [InlineData(15)]
    [InlineData(7)]
    [InlineData(12)]
    public void Insert_ValidData_ShouldInsert(int data)
    {
        CreateRootNode(10);
        _rootNode?.Insert(data);

        _rootNode?.FindChild(data.GetHashCode()).Data.Should().Be(data);
    }

    [Fact(DisplayName = "Insert method should throw ArgumentNullException when inserting null data")]
    public void Insert_NullData_ShouldThrowArgumentNullException()
    {
        // Arrange
        object? nullData = null;
        CreateRootNode(10);

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => _rootNode?.Insert(nullData));
    }

    [Fact(DisplayName = "Insert method should throw DuplicateKeyException when inserting duplicate data")]
    public void Insert_DuplicateData_ShouldThrowDuplicateKeyException()
    {
        // Arrange
        CreateRootNode(10);
        const int data = 10;

        // Act & Assert
        Assert.Throws<DuplicateKeyException>(() => _rootNode.Insert(data));
    }

    [Theory(DisplayName = "Insert method should handle different data types")]
    [InlineData("test", "root")]
    [InlineData(3.14, 2.71)]
    [InlineData(true, false)]
    public void Insert_DifferentDataTypes_ShouldInsert(object data, object rootData)
    {
        CreateRootNode(rootData);
        _rootNode?.Insert(data);

        _rootNode?.FindChild(data.GetHashCode()).Data.Should().Be(data);
    }
  

    /*[Theory(DisplayName = "Insert method should handle array data")]
    [InlineData(new object[] {1, 2, 3})]
    [InlineData(new object[]{"a", "b", "c"})]
    public void Insert_ArrayData_ShouldInsert(object[] dataArray)
    {
        foreach (var data in dataArray)
        {
            _rootNode?.Insert(data);
        }

        foreach (var data in dataArray)
        {
            _rootNode?.FindChild(data).Data.Should().Be(data);
        }
    }*/

    [Fact(DisplayName = "Insert method should handle custom class data")]
    public void Insert_CustomClassData_ShouldInsert()
    {
        // Arrange
        var faker = new Faker<MyClass>()
            .RuleFor(m => m.Id, f => f.Random.Guid())
            .RuleFor(m => m.Name, f => f.Name.FirstName());

        var testData = faker.Generate();
        var rootData = faker.Generate();
        
        CreateRootNode(rootData);

        // Act
        _rootNode?.Insert(testData);

        // Assert
        _rootNode?.FindChild(testData.GetHashCode()).Data.Should().Be(testData);
    }
}
public class MyClass
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}