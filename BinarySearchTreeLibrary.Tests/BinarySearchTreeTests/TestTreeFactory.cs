using BinarySearchTreeLibrary.Models;

namespace BinarySearchTreeLibrary.Tests.BinarySearchTreeTests;

internal static class TestTreeFactory
{
	public static BinarySearchTree<T?> CreateTree<T>(T? data)
	{
		var tree = new BinarySearchTree<T?>();
		tree.Add(data);

		return tree;
	}

	public static BinarySearchTree<T>? CreateTree<T>(IEnumerable<T> inputs, bool isList)
	{
		if (isList is false)
			return null;
		
		var tree = new BinarySearchTree<T>();
		var inputList = inputs.ToList();

		if (inputList.Count == 0)
			return tree;

		foreach (var t in inputList)
			tree.Add(t);

		return tree;
	}
}