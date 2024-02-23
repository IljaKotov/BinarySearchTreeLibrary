using BinarySearchTreeLibrary.Interfaces;
using BinarySearchTreeLibrary.Models;

namespace BinarySearchTreeLibrary.Tests.NodeTests;

internal static class TestNodeFactory
{
	public static INode<T> CreateNode<T>(T value) => new Node<T>(value);
	
	public static INode<T> CreateNode<T>(IEnumerable<T> inputs,
		int startIndex = 0)
	{
		var inputList = inputs.ToList();
		var root = CreateNode(inputList[startIndex]);
		
		if (inputList.Count == 1)
			return root;

		var nodesToInsert = inputList
			.Where((_, index) => index != startIndex)
			.ToList();

		foreach (var data in nodesToInsert)
			root.Insert(data);
		
		return root;
	}
}

